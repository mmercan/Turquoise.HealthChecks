using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
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
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Swashbuckle.AspNetCore.SwaggerGen;
using Turquoise.Api.HealthMonitoring.CustomFeatureFilter;
using Turquoise.Api.HealthMonitoring.GRPCServices;
using Turquoise.Api.HealthMonitoring.Helpers;
using Turquoise.Api.HealthMonitoring.Hubs;
using Turquoise.Common;
using Turquoise.Common.Services;
using Turquoise.HealthChecks.Common;
using Turquoise.HealthChecks.Common.CheckCaller;
using Turquoise.HealthChecks.Common.Checks;
using Turquoise.K8s;
using Turquoise.K8sServices;
using Turquoise.K8sServices.K8sClients;

namespace Turquoise.Api.HealthMonitoring
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
            services.AddControllers();

            services.AddSingleton<IServiceCollection>(services);
            services.AddSingleton<IConfiguration>(Configuration);
            // services.AddSingleton<PushNotificationService>();

            //Add Health Check
            services.AddHealthChecks()
            // .AddProcessList()
            // .AddPerformanceCounter("Win32_PerfRawData_PerfOS_Memory")
            // .AddPerformanceCounter("Win32_PerfRawData_PerfOS_Memory", "AvailableMBytes")
            // .AddPerformanceCounter("Win32_PerfRawData_PerfOS_Memory", "PercentCommittedBytesInUse", "PercentCommittedBytesInUse_Base")
            .AddSystemInfoCheck()
            .AddPrivateMemorySizeCheckKB(300000)
            .AddWorkingSetCheckKB(3000000)
             // //.AddCheck<SlowDependencyHealthCheck>("Slow", failureStatus: null, tags: new[] { "ready", })
             //  .SqlConnectionHealthCheck(Configuration["SentinelConnection"])
             // .AddApiIsAlive(Configuration.GetSection("sentinel-ui-sts:ClientOptions"), "api/healthcheck/isalive")
             // .AddApiIsAlive(Configuration.GetSection("sentinel-api-member:ClientOptions"), "api/healthcheck/isalive")
             // .AddApiIsAlive(Configuration.GetSection("sentinel-api-product:ClientOptions"), "api/healthcheck/isalive")
             // .AddApiIsAlive(Configuration.GetSection("sentinel-api-comms:ClientOptions"), "api/healthcheck/isalive")
             // .AddMongoHealthCheck(Configuration["Mongodb:ConnectionString"])
             // .AddRabbitMQHealthCheck(Configuration["RabbitMQConnection"])
             // .AddRedisHealthCheck(Configuration["RedisConnection"])
             .AddDIHealthCheck(services);


            // Add App insight 
            services.AddApplicationInsightsTelemetry("15ce6ddc-8d32-418e-9d5c-eed1cd7d6096");
            services.AddApplicationInsightsKubernetesEnricher();

            services.ConfigureJwtAuthService(Configuration);
            services.AddCors(
                o =>
                {
                    o.AddPolicy("MyPolicy", builder =>
                    {
                        // builder.AllowAnyOrigin()
                        // .AllowAnyMethod()
                        // .AllowAnyHeader()
                        // .SetIsOriginAllowedToAllowWildcardSubdomains();
                        // //.AllowCredentials();
                        builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowCredentials()
                        .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding")
                        .WithOrigins("http://localhost:4201", "http://localhost:4200",
                        "https://health.dev.turk.mercan.io",
                        "https://app-health-ui.dev.myrcan.com",
                        "https://health.dev.ui.sentinel.mercan.io",
                        "https://turquoise-ui-healthmonitoring.dev.turk.mercan.io");
                    });

                    o.AddPolicy("GRPCPolicy", builder =>
                    {
                        builder.WithOrigins("localhost:4200", "YourCustomDomain");
                        builder.WithMethods("POST, OPTIONS");
                        builder.AllowAnyHeader();
                        builder.WithExposedHeaders("Grpc-Status", "Grpc-Message");
                    });
                }
            );


            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<SwaggerDefaultValues>();
                options.IncludeXmlComments(XmlCommentsFilePath);

                options.AddSecurityDefinition("BearerAuth", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "Bearer "
                });

            });
            services.AddSingleton<EasyNetQ.IBus>((ctx) =>
            {
                return RabbitHutch.CreateBus(Configuration["RabbitMQConnection"]);
            });
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration["RedisConnection"];
                options.InstanceName = "ApiComms";
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


            services.AddMangoRepo<Turquoise.Models.Mongo.ServiceHealthCheckResultSummary>(
                            Configuration["Mongodb:ConnectionString"], Configuration["Mongodb:DatabaseName"],
                            "HealthCheckResultSummary", p => p.NameandNamespace
                            );

            // services.AddMangoRepo<PushNotificationModel>(Configuration.GetSection("Mongodb"));

            // services.AddHttpClient("run_with_try", options =>
            // {
            //     options.Timeout = new TimeSpan(0, 2, 0);
            //     options.DefaultRequestHeaders.Add("OData-MaxVersion", "4.0");
            //     options.DefaultRequestHeaders.Add("OData-Version", "4.0");
            //     options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // }).ConfigurePrimaryHttpMessageHandler<CertMessageHandler>()
            // // ConfigurePrimaryHttpMessageHandler((ch) =>
            // // {
            // //     var handler = new HttpClientHandler();
            // //     handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            // //     handler.ClientCertificates.Add(HttpClientHelpers.GetCert());
            // //     return handler;

            // // })
            // // .AddHttpMessageHandler()
            // // .AddHttpMessageHandler<OAuthTokenHandler>()
            // //.AddHttpMessageHandler(*)
            // .AddPolicyHandler(HttpClientHelpers.GetRetryPolicy())
            // .AddPolicyHandler(HttpClientHelpers.GetCircuitBreakerPolicy());

            services.AddHttpClient<HealthCheckReportDownloaderService>("HealthCheckReportDownloader", options =>
            {
                // options.BaseAddress = new Uri(Configuration["CrmConnection:ServiceUrl"] + "api/data/v8.2/");
                options.Timeout = new TimeSpan(0, 2, 0);
                options.DefaultRequestHeaders.Add("OData-MaxVersion", "4.0");
                options.DefaultRequestHeaders.Add("OData-Version", "4.0");
                options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            })
            // .ConfigurePrimaryHttpMessageHandler((ch) =>
            // {
            //     var handler = new HttpClientHandler();
            //     handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            //     handler.ClientCertificates.Add(HttpClientHelpers.GetCert());
            //     return handler;
            // })
            //.AddHttpMessageHandler()
            // .AddHttpMessageHandler<OAuthTokenHandler>()
            .AddPolicyHandler(HttpClientHelpers.GetRetryPolicy())
            .AddPolicyHandler(HttpClientHelpers.GetCircuitBreakerPolicy());

            services.AddHttpContextAccessor();

            services.AddFeatureManagement()
            .AddFeatureFilter<PercentageFilter>()
            .AddFeatureFilter<HeadersFeatureFilter>();
            // services.AddHostedService<HealthCheckSubscribeService>();

            services.AddSignalR();
            services.AddAutoMapper(typeof(Startup).Assembly, typeof(K8sGeneralService).Assembly, typeof(Turquoise.Models.Mongo.DeploymentV1).Assembly);
            services.AddGrpc();

            services.AddMemoryCache();
            services.Configure<AZAuthServiceSettings>(Configuration.GetSection("AzureAd"));
            services.AddSingleton<AZAuthService>();

            if (Configuration["RunOnCluster"] == "true") { services.AddSingleton<IKubernetesClient, KubernetesClientInClusterConfig>(); }
            else { services.AddSingleton<IKubernetesClient, KubernetesClientFromConfigFile>(); }

            services.AddSignalR();

            services.AddSingleton<K8sGeneralService>();
            //  services.AddSingleton<K8sMetricsService>();
            services.AddSingleton<MongoAliveAndWellResultStats>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IApiVersionDescriptionProvider provider, IHostApplicationLifetime lifetime, IDistributedCache cache)
        {
            //     lifetime.ApplicationStarted.Register(() =>
            //    {
            //        var currentTimeUTC = DateTime.UtcNow.ToString();
            //        byte[] encodedCurrentTimeUTC = Encoding.UTF8.GetBytes(currentTimeUTC);
            //        var options = new DistributedCacheEntryOptions()
            //            .SetSlidingExpiration(TimeSpan.FromSeconds(20));
            //        cache.Set("cachedTimeUTC", encodedCurrentTimeUTC, options);
            //    });


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Enviroment", env.EnvironmentName)
                .Enrich.WithProperty("ApplicationName", "Api App")
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .WriteTo.Console()
                .WriteTo.File("Logs/logs.txt");
            //.WriteTo.Elasticsearch()
            logger.WriteTo.Console();
            loggerFactory.AddSerilog();
            Log.Logger = logger.CreateLogger();
            app.UseExceptionLogger();


            app.UseSwagger(e => { e.AddHealthCheckSwaggerOptions(); });
            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
                options.RoutePrefix = string.Empty;
            });

            app.UseSignalRJwtAuthentication();

            app.UseRouting();
            app.UseCors("MyPolicy");
            app.UseAllAuthentication();
            app.UseAuthorization();

            app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGrpcService<GreeterGRPCService>();
                endpoints.MapGrpcService<CountryGRPCService>();
                endpoints.MapGrpcService<NamespaceGRPCService>();
                endpoints.MapGrpcService<MetricGRPCService>();
                // endpoints.MapGrpcService<MeterReaderService>();

                endpoints.MapHub<K8sHub>("/K8sHub");
            });


            app.UseHealthChecks("/Health/IsAliveAndWell", new HealthCheckOptions()
            {
                ResponseWriter = WriteResponses.WriteListResponse,
            });

            app.Map("/Health/IsAlive", (ap) =>
            {
                ap.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync("{\"IsAlive\":true}");
                });
            });

        }

        static string XmlCommentsFilePath
        {
            get
            {
                //var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var basePath = AppContext.BaseDirectory;
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return System.IO.Path.Combine(basePath, fileName);
            }
        }
    }


}
