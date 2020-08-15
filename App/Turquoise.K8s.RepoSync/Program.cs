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
using Turquoise.K8s.RepoSync.Services;
using Turquoise.K8s.Services;
using AutoMapper;
using k8s.Models;

namespace Turquoise.K8s.RepoSync
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
                .Enrich.WithProperty("ApplicationName", "Sentinel.Handler.Comms")
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .WriteTo.Console()
                .WriteTo.File("Logs/logs.txt");

                logger.WriteTo.Console();
                Log.Logger = logger.CreateLogger();
                services.AddLogging();
                services.AddSingleton<IConfiguration>(hostContext.Configuration);

                //Add Dependencies

                services.AddAutoMapper(typeof(Program).Assembly, typeof(K8sService).Assembly, typeof(Turquoise.Models.Deployment).Assembly);

                if (hostContext.Configuration["RunOnCluster"] == "true")
                {
                    services.AddSingleton<IKubernetesClient, KubernetesClientInClusterConfig>();
                }
                else
                {
                    services.AddSingleton<IKubernetesClient, KubernetesClientFromConfigFile>();
                }
                services.AddSingleton<K8sService>();

                services.AddSingleton<EasyNetQ.IBus>((ctx) =>
                {
                    return RabbitHutch.CreateBus(hostContext.Configuration["RabbitMQConnection"]);
                });


                services.AddMangoRepo<Turquoise.Models.Mongo.NamespaceV1>(
                    hostContext.Configuration["Mongodb:ConnectionString"],
                    hostContext.Configuration["Mongodb:DatabaseName"],
                    hostContext.Configuration["Mongodb:CollectionName"],
                    p => p.Name
                    );

                services.AddHostedService<QuartzHostedService>();
                // // Add Quartz services
                services.AddSingleton<IJobFactory, SingletonJobFactory>();
                services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

                // Add our job
                services.AddSingleton<SyncNamespaceService>();
                services.AddSingleton(new JobSchedule(
                    jobType: typeof(SyncNamespaceService),
                    cronExpression: "0 */2 * * * ?"));
                // cronExpression: "0 */15 * * * ?"));
                // cronExpression: "0/5 * * * * ?"));



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
    }
}
