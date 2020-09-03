using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EasyNetQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
using Turquoise.HealthChecker.Services;
using System.Net.Http;
using System.Net.Http.Headers;
using Turquoise.Common;
using Turquoise.HealthChecks.Mongo;
using Turquoise.HealthChecker.InternalHealthCheck;

namespace Turquoise.HealthChecker
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
            // .AddPrivateMemorySizeCheckKB(800000)
            // .AddWorkingSetCheckKB(8000000)
            .AddMongoHealthCheck(Configuration["Mongodb:ConnectionString"])
            .AddCheck<QueueSubscribeHealthCheck>("queue_health_check");

            services.AddAutoMapper(typeof(Program).Assembly, typeof(K8sService).Assembly, typeof(Turquoise.Models.Deployment).Assembly);

            // if (Configuration["RunOnCluster"] == "true") { services.AddSingleton<IKubernetesClient, KubernetesClientInClusterConfig>(); }
            // else { services.AddSingleton<IKubernetesClient, KubernetesClientFromConfigFile>(); }
            // services.AddSingleton<K8sService>();

            services.Configure<AZAuthServiceSettings>(Configuration.GetSection("AzureAd"));
            services.AddSingleton<AZAuthService>();

            services.AddSingleton<EasyNetQ.IBus>((ctx) =>
            {
                return RabbitHutch.CreateBus(Configuration["RabbitMQConnection"]);
            });


            services.AddMangoRepo<Turquoise.Models.Mongo.ServiceV1>(
                Configuration["Mongodb:ConnectionString"],
                Configuration["Mongodb:DatabaseName"],
                "ServiceSet",
                p => p.Name
            );


            services.AddMangoRepo<Turquoise.Models.Mongo.AliveAndWellResult>(
                Configuration["Mongodb:ConnectionString"],
                Configuration["Mongodb:DatabaseName"],
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

            });
            // .ConfigurePrimaryHttpMessageHandler((ch) =>
            // {
            //     var handler = new HttpClientHandler();
            //     handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            //     handler.ClientCertificates.Add(HttpClientHelpers.GetCert());
            //     return handler;
            // })
            //.AddHttpMessageHandler()
            //  .AddHttpMessageHandler<OAuthTokenHandler>()
            // .AddPolicyHandler(HttpClientHelpers.GetRetryPolicy())
            // .AddPolicyHandler(HttpClientHelpers.GetCircuitBreakerPolicy());

            services.AddMemoryCache();

            services.AddHostedService<HealthcheckQueueSubscriber>();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
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
