using AutoMapper;
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
using Turquoise.K8sServices;
using Turquoise.K8sServices.K8sClients;
using Turquoise.Common.Scheduler.QuartzScheduler;
using Quartz.Impl;
using Turquoise.Worker.Sync.JobSchedules;

namespace Turquoise.Worker.Sync
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
            // .AddFeatureFilter<PercentageFilter>();
            // .AddFeatureFilter<HeadersFeatureFilter>();

            //Add Health Check
            services.AddHealthChecks();
            // .AddCheck<QueueSubscribeHealthCheck>("queue_health_check");

            services.AddAutoMapper(typeof(Program).Assembly, typeof(K8sGeneralService).Assembly, typeof(Turquoise.Models.Mongo.DeploymentV1).Assembly);

            if (Configuration["RunOnCluster"] == "true") { services.AddSingleton<IKubernetesClient, KubernetesClientInClusterConfig>(); }
            else { services.AddSingleton<IKubernetesClient, KubernetesClientFromConfigFile>(); }
            services.AddSingleton<K8sServices.K8sGeneralService>();

            // services.Configure<AZAuthServiceSettings>(Configuration.GetSection("AzureAd"));
            // services.AddSingleton<AZAuthService>();
            // services.AddSingleton<EasyNetQ.IBus>((ctx) =>
            // {
            //     return RabbitHutch.CreateBus(Configuration["RabbitMQConnection"]);
            // });


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

            services.AddSingleton<SyncNamespaces>();
            services.AddSingleton(new JobSchedule(
               jobType: typeof(SyncNamespaces), cronExpression: Configuration["Schedules:SyncNamespaces"]));

            services.AddSingleton<SyncServices>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(SyncServices), cronExpression: Configuration["Schedules:SyncServices"]));

            services.AddSingleton<SyncDeployments>();
            services.AddSingleton(new JobSchedule(
               jobType: typeof(SyncDeployments), cronExpression: Configuration["Schedules:SyncDeployments"]));


            // services.AddSingleton<HealthCheckSchedulerRepositoryFeeder>();
            // services.AddSingleton(new JobSchedule(
            //     jobType: typeof(HealthCheckSchedulerRepositoryFeeder), cronExpression: "0 */2 * * * ?"));


            // services.AddSingleton<DeploymentSchedulerRepositoryFeeder>();
            // services.AddSingleton(new JobSchedule(
            //     jobType: typeof(DeploymentSchedulerRepositoryFeeder), cronExpression: "0 */2 * * * ?"));





            // services.AddHttpClient<IsAliveAndWellHealthChecker>("HealthCheckReportDownloader", options =>
            // {
            //     // options.BaseAddress = new Uri(Configuration["CrmConnection:ServiceUrl"] + "api/data/v8.2/");
            //     options.Timeout = new TimeSpan(0, 2, 0);
            //     options.DefaultRequestHeaders.Add("OData-MaxVersion", "4.0");
            //     options.DefaultRequestHeaders.Add("OData-Version", "4.0");
            //     options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // });
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

            // services.AddMemoryCache();
            // services.AddHostedService<HealthcheckQueueSubscriber>();

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
                .Enrich.WithProperty("ApplicationName", "Turquoise.Worker.Sync")
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