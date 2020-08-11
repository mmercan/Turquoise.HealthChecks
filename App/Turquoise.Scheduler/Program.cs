using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Turquoise.Scheduler.Services;
using Microsoft.Extensions.Logging;

namespace Turquoise.Scheduler
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



                services.AddSingleton<EasyNetQ.IBus>((ctx) =>
                {
                    return RabbitHutch.CreateBus(hostContext.Configuration["RabbitMQConnection"]);
                });

                services.AddSingleton<HealthCheckRepo>();

                services.AddHostedService<AppHealthCheckScheduler>();
                //                services.AddHostedService<ProductAsyncSubscribeService>();

                // services.AddHostedService<QuartzHostedService>();
                // // Add Quartz services
                // services.AddSingleton<IJobFactory, SingletonJobFactory>();
                // services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

                // // Add our job
                // services.AddSingleton<HelloWorldJob>();
                // services.AddSingleton(new JobSchedule(
                //     jobType: typeof(HelloWorldJob),
                //     cronExpression: "0/5 * * * * ?"));



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

