using AutoMapper;
using EasyNetQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Serilog;
using Serilog.Events;
using Turquoise.HealthChecks.Common;
using Turquoise.K8sServices;
using Turquoise.K8sServices.K8sClients;
using Turquoise.Worker.Comms.BackgroundServices;

namespace Turquoise.Worker.Comms
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IServiceCollection>(services);
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddFeatureManagement();


            //Add Health Check
            services.AddHealthChecks();

            services.AddAutoMapper(typeof(Program).Assembly, typeof(K8sGeneralService).Assembly, typeof(Turquoise.Models.Mongo.DeploymentV1).Assembly);

            if (Configuration["RunOnCluster"] == "true") { services.AddSingleton<IKubernetesClient, KubernetesClientInClusterConfig>(); }
            else { services.AddSingleton<IKubernetesClient, KubernetesClientFromConfigFile>(); }
            services.AddSingleton<K8sGeneralService>();

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

            services.AddMailService(Configuration.GetSection("SMTP"));

            services.AddMemoryCache();

            services.AddHostedService<NotifyServiceHealthCheckQueueSubscriber>();

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
                .Enrich.WithProperty("ApplicationName", "Turquoise.Worker.Comms")
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