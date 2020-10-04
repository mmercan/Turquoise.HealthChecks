using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Quartz.Spi;
using Quartz;
using Serilog;
using Serilog.Events;
using Turquoise.HealthChecks.Common;
using Turquoise.Common.Scheduler.QuartzScheduler;
using Quartz.Impl;
using Turquoise.HealthChecks.Common.Checks;
using EasyNetQ;
using System;
using Microsoft.Extensions.Caching.Distributed;
using System.Net.Http.Headers;
using Turquoise.Common.Services;
using Turquoise.Worker.Scaler.JobSchedules;
using Turquoise.K8sServices.K8sClients;
using Turquoise.K8sServices;
using AutoMapper;

namespace Turquoise.Worker.Scaler
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IServiceCollection>(services);
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddFeatureManagement();
            //Add Health Check
            services.AddHealthChecks();
            // .AddSystemInfoCheck();

            services.AddAutoMapper(typeof(Program).Assembly, typeof(Turquoise.Models.Mongo.DeploymentV1).Assembly);

            if (Configuration["RunOnCluster"] == "true") { services.AddSingleton<IKubernetesClient, KubernetesClientInClusterConfig>(); }
            else { services.AddSingleton<IKubernetesClient, KubernetesClientFromConfigFile>(); }
            services.AddSingleton<K8sGeneralService>();

            services.AddSingleton<EasyNetQ.IBus>((ctx) =>
            {
                return RabbitHutch.CreateBus(Configuration["RabbitMQConnection"]);
            });

            // services.AddMangoRepo<Turquoise.Models.Mongo.AliveAndWellResult>(
            //     Configuration["Mongodb:ConnectionString"], Configuration["Mongodb:DatabaseName"],
            //     "AliveAndWellResult", p => p.Id
            //     );

            // services.AddMangoRepo<Turquoise.Models.Mongo.ServiceV1>(
            //     Configuration["Mongodb:ConnectionString"], Configuration["Mongodb:DatabaseName"],
            //     "ServiceSet", p => p.Name
            //     );

            // services.AddMangoRepo<Turquoise.Models.Mongo.DeploymentV1>(
            //     Configuration["Mongodb:ConnectionString"], Configuration["Mongodb:DatabaseName"],
            //     "DeploymentSet", p => p.NameandNamespace
            //     );

            // services.AddMangoRepo<Models.Mongo.ServiceHealthCheckResultSummary>(
            //     Configuration["Mongodb:ConnectionString"], Configuration["Mongodb:DatabaseName"],
            //     "HealthCheckResultSummary", p => p.NameandNamespace
            //     );

            services.AddMangoRepo<Models.Mongo.DeploymentScaleHistory>(
                Configuration["Mongodb:ConnectionString"], Configuration["Mongodb:DatabaseName"],
                "DeploymentScaleHistory", p => p.Id
                );


            services.AddHostedService<QuartzHostedService>();
            // // Add Quartz services
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            // Add our job

            services.Configure<AZAuthServiceSettings>(Configuration.GetSection("AzureAd"));
            services.AddSingleton<AZAuthService>();

            // services.AddHttpClient<IsAliveAndWellHealthChecker>("HealthCheckReportDownloader", options =>
            // {
            //     // options.BaseAddress = new Uri(Configuration["CrmConnection:ServiceUrl"] + "api/data/v8.2/");
            //     options.Timeout = new TimeSpan(0, 2, 0);
            //     options.DefaultRequestHeaders.Add("OData-MaxVersion", "4.0");
            //     options.DefaultRequestHeaders.Add("OData-Version", "4.0");
            //     options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // });

            services.AddMemoryCache();
            services.AddHostedService<DeploymentScalerQueueSubscriber>();
            // services.AddHostedService<DeploymentSyncService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IHostApplicationLifetime lifetime)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Enviroment", env.EnvironmentName)
                .Enrich.WithProperty("ApplicationName", "Turquoise.Scheduler")
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .WriteTo.Console()
                .WriteTo.File("Logs/logs.txt");
            //.WriteTo.Elasticsearch()
            logger.WriteTo.Console();
            loggerFactory.AddSerilog();
            Log.Logger = logger.CreateLogger();
            app.UseExceptionLogger();


            app.UseHealthChecks("/Health/IsAliveAndWell", new HealthCheckOptions()
            {
                ResponseWriter = WriteResponses.WriteListResponse,
            });

            app.Map("", (ap) =>
            {
                ap.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync("{\"IsAlive\":true}");
                });
            });

        }


    }
}