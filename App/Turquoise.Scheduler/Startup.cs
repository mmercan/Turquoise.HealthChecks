using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EasyNetQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Serilog;
using Serilog.Events;
using Turquoise.Common.Scheduler.QuartzScheduler;
using Turquoise.HealthChecks.Common;
using Turquoise.HealthChecks.Common.Checks;
using Turquoise.K8s;
using Turquoise.K8s.Services;
using Turquoise.Scheduler.HostedServices;
using Turquoise.Scheduler.JobSchedules;
using Turquoise.Scheduler.Services;

namespace Turquoise.Scheduler
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IServiceCollection>(services);
            services.AddSingleton<IConfiguration>(Configuration);

            //Add Health Check
            services.AddHealthChecks()
            .AddSystemInfoCheck()
            .AddPrivateMemorySizeCheckKB(300000)
            .AddWorkingSetCheckKB(3000000);

            services.AddFeatureManagement()
            .AddFeatureFilter<PercentageFilter>();
            // .AddFeatureFilter<HeadersFeatureFilter>();

            services.AddAutoMapper(typeof(Program).Assembly, typeof(K8sService).Assembly, typeof(Turquoise.Models.Deployment).Assembly);

            if (Configuration["RunOnCluster"] == "true") { services.AddSingleton<IKubernetesClient, KubernetesClientInClusterConfig>(); }
            else { services.AddSingleton<IKubernetesClient, KubernetesClientFromConfigFile>(); }
            services.AddSingleton<K8sService>();

            services.AddSingleton<EasyNetQ.IBus>((ctx) =>
            {
                return RabbitHutch.CreateBus(Configuration["RabbitMQConnection"]);
            });

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration["RedisConnection"];
                options.InstanceName = "k8s";
            });

            services.AddMangoRepo<Turquoise.Models.Mongo.NamespaceV1>(
                Configuration["Mongodb:ConnectionString"],
                Configuration["Mongodb:DatabaseName"],
                "NamespaceSet",
                p => p.Name
                );

            services.AddMangoRepo<Turquoise.Models.Mongo.ServiceV1>(
                Configuration["Mongodb:ConnectionString"],
                Configuration["Mongodb:DatabaseName"],
                "ServiceSet",
                p => p.Name
                );


            services.AddMangoRepo<Turquoise.Models.Mongo.DeploymentV1>(
                Configuration["Mongodb:ConnectionString"],
                Configuration["Mongodb:DatabaseName"],
                "DeploymentSet",
                p => p.NameandNamespace
                );

            services.AddHostedService<QuartzHostedService>();
            // // Add Quartz services
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            // Add our job

            services.AddSingleton<SyncK8sServiceV1>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(SyncK8sServiceV1), cronExpression: "0 */15 * * * ?"));


            services.AddSingleton<HealthCheckSchedulerRepositoryFeeder>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(HealthCheckSchedulerRepositoryFeeder), cronExpression: "0 */2 * * * ?"));


            services.AddSingleton<DeploymentSchedulerRepositoryFeeder>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(DeploymentSchedulerRepositoryFeeder), cronExpression: "0 */2 * * * ?"));


            services.AddSingleton<SyncNamespaceService>();
            services.AddSingleton(new JobSchedule(
               jobType: typeof(SyncNamespaceService), cronExpression: "0 */15 * * * ?"));


            services.AddSingleton<SyncK8sDeploymentV1>();
            services.AddSingleton(new JobSchedule(
               jobType: typeof(SyncK8sDeploymentV1), cronExpression: "0 */15 * * * ?"));


            services.AddHealthCheckSchedulerRepository<Turquoise.Models.Mongo.ServiceV1>();
            services.AddDeploymentSchedulerRepository<Turquoise.Models.Mongo.DeploymentV1>();

            services.AddHostedService<AppHealthCheckScheduler>();

            services.AddHostedService<DeploymentSyncService>();

            services.AddHostedService<DeploymentScaleDownScheduler>();
            services.AddHostedService<DeploymentScaleUpScheduler>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IHostApplicationLifetime lifetime, IDistributedCache cache)
        {
            lifetime.ApplicationStarted.Register(() =>
          {
              var currentTimeUTC = DateTime.UtcNow.ToString();
              byte[] encodedCurrentTimeUTC = Encoding.UTF8.GetBytes(currentTimeUTC);
              var options = new DistributedCacheEntryOptions()
                  .SetSlidingExpiration(TimeSpan.FromSeconds(20));
              cache.Set("cachedTimeUTC", encodedCurrentTimeUTC, options);
          });


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



            // app.UseRouting();
            // app.UseAuthorization();
            // app.UseEndpoints(endpoints =>
            // {
            // });


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
