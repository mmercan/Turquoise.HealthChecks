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
using Turquoise.Worker.Scheduler.QuartzJobSchedules;
using Turquoise.Worker.Scheduler.Schedules;

namespace Turquoise.Worker.Scheduler
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
            services.AddHealthChecks()
            .AddSystemInfoCheck();

            // services.AddAutoMapper(typeof(Program).Assembly, typeof(Turquoise.Models.Mongo.DeploymentV1).Assembly);

            // if (Configuration["RunOnCluster"] == "true") { services.AddSingleton<IKubernetesClient, KubernetesClientInClusterConfig>(); }
            // else { services.AddSingleton<IKubernetesClient, KubernetesClientFromConfigFile>(); }
            // services.AddSingleton<K8sGeneralService>();

            services.AddSingleton<EasyNetQ.IBus>((ctx) =>
            {
                return RabbitHutch.CreateBus(Configuration["RabbitMQConnection"]);
            });

            services.AddMangoRepo<Turquoise.Models.Mongo.NamespaceV1>(
                Configuration["Mongodb:ConnectionString"], Configuration["Mongodb:DatabaseName"],
                "NamespaceSet", p => p.Name
                );

            services.AddMangoRepo<Turquoise.Models.Mongo.ServiceV1>(
                Configuration["Mongodb:ConnectionString"], Configuration["Mongodb:DatabaseName"],
                "ServiceSet", p => p.Name
                );

            services.AddMangoRepo<Turquoise.Models.Mongo.DeploymentV1>(
                Configuration["Mongodb:ConnectionString"], Configuration["Mongodb:DatabaseName"],
                "DeploymentSet", p => p.NameandNamespace
                );

            services.AddHostedService<QuartzHostedService>();
            // // Add Quartz services
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            // Add our job

            services.AddSingleton<HealthCheckSchedulerRepositoryFeeder>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(HealthCheckSchedulerRepositoryFeeder), cronExpression: "0 */2 * * * ?"));

            services.AddSingleton<DeploymentSchedulerRepositoryFeeder>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(DeploymentSchedulerRepositoryFeeder), cronExpression: "0 */2 * * * ?"));

            services.AddHealthCheckSchedulerRepository<Turquoise.Models.Mongo.ServiceV1>();
            services.AddDeploymentSchedulerRepository<Turquoise.Models.Mongo.DeploymentV1>();

            services.AddHostedService<HealthCheckScheduler>();
            services.AddHostedService<DeploymentScaleScheduler>();

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