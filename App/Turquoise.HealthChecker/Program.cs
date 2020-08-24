using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Serilog;
using Serilog.Events;
using Turquoise.Common.Scheduler;
using Microsoft.Extensions.Logging;
using Turquoise.K8s.Services;
using AutoMapper;
using k8s.Models;
using Turquoise.K8s;
using Turquoise.Common.Scheduler.QuartzScheduler;
using Turquoise.HealthChecker.Services;
using System.Net.Http.Headers;
using Turquoise.Common;
using System.Net.Http;

namespace Turquoise.HealthChecker
{
    public class Program
    {


        public static void Main(string[] args)
        {
            var host = new HostBuilder()
             .ConfigureAppConfiguration((hostContext, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: true);
                config.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true);
                config.AddCommandLine(args);
                config.AddEnvironmentVariables();
            })
            .ConfigureLogging((hostContext, logging) =>
            {
                logging.AddConsole();
                logging.AddSerilog();
                logging.AddDebug();
            })
            .ConfigureServices((hostContext, services) =>
            {
                var env = hostContext.HostingEnvironment;
                var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(hostContext.Configuration)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Enviroment", env.EnvironmentName)
                .Enrich.WithProperty("ApplicationName", "Turquoise.Scheduler")
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .WriteTo.Console()
                .WriteTo.File("Logs/logs.txt");

                logger.WriteTo.Console();
                Log.Logger = logger.CreateLogger();
                services.AddLogging();
                services.AddSingleton<IConfiguration>(hostContext.Configuration);

                //Add Dependencies

                // services.AddAutoMapper(typeof(Program).Assembly, typeof(K8sService).Assembly, typeof(Turquoise.Models.Deployment).Assembly);

                if (hostContext.Configuration["RunOnCluster"] == "true") { services.AddSingleton<IKubernetesClient, KubernetesClientInClusterConfig>(); }
                else { services.AddSingleton<IKubernetesClient, KubernetesClientFromConfigFile>(); }
                services.AddSingleton<K8sService>();

                services.Configure<AZAuthServiceSettings>(hostContext.Configuration.GetSection("AzureAd"));
                services.AddSingleton<AZAuthService>();


                services.AddSingleton<EasyNetQ.IBus>((ctx) =>
                {
                    return RabbitHutch.CreateBus(hostContext.Configuration["RabbitMQConnection"]);
                });

                services.AddMangoRepo<Turquoise.Models.Mongo.ServiceV1>(
                    hostContext.Configuration["Mongodb:ConnectionString"],
                    hostContext.Configuration["Mongodb:DatabaseName"],
                    "ServiceSet",
                    p => p.Name
                    );


                services.AddMangoRepo<Turquoise.Models.Mongo.AliveAndWellResult>(
                    hostContext.Configuration["Mongodb:ConnectionString"],
                    hostContext.Configuration["Mongodb:DatabaseName"],
                    "AliveAndWellResult",
                    p => p.Id
                    );




                services.AddHttpClient<IsAliveAndWellHealthChecker>("HealthCheckReportDownloader", options =>
                {
                    // options.BaseAddress = new Uri(Configuration["CrmConnection:ServiceUrl"] + "api/data/v8.2/");
                    options.Timeout = new TimeSpan(0, 2, 0);
                    options.DefaultRequestHeaders.Add("OData-MaxVersion", "4.0");
                    options.DefaultRequestHeaders.Add("OData-Version", "4.0");
                    options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                })
                .ConfigurePrimaryHttpMessageHandler((ch) =>
                {
                    var handler = new HttpClientHandler();
                    //handler.
                    return handler;
                })

                // .ConfigurePrimaryHttpMessageHandler((ch) =>
                // {
                //     var handler = new HttpClientHandler();
                //     handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                //     handler.ClientCertificates.Add(HttpClientHelpers.GetCert());
                //     return handler;
                // })
                //.AddHttpMessageHandler()
                //  .AddHttpMessageHandler<OAuthTokenHandler>()
                .AddPolicyHandler(HttpClientHelpers.GetRetryPolicy())
                .AddPolicyHandler(HttpClientHelpers.GetCircuitBreakerPolicy());





                services.AddHostedService<HealthcheckQueueSubscriber>();
            })
            .UseConsoleLifetime()
            .Build();
            using (host)
            {
                var hoststart = host.StartAsync();
                hoststart.Wait();
                // Monitor for new background queue work items
                // var monitorLoop = host.Services.GetRequiredService<MonitorLoop>();
                // monitorLoop.StartMonitorLoop();
                var waitforshutdown = host.WaitForShutdownAsync();
                waitforshutdown.Wait();
            }


        }



        // public static void Main(string[] args)
        // {
        //     CreateHostBuilder(args).Build().Run();
        // }

        // public static IHostBuilder CreateHostBuilder(string[] args) =>
        //     Host.CreateDefaultBuilder(args)
        //         .ConfigureServices((hostContext, services) =>
        //         {
        //             services.AddHostedService<Worker>();
        //         });
    }
}
