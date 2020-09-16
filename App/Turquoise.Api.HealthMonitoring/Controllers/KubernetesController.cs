using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using k8s;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Turquoise.Api.HealthMonitoring.Helpers;
using Turquoise.Api.HealthMonitoring.Models;
using Turquoise.Common.Mongo;
using Turquoise.K8s.Services;
using Turquoise.Models.Mongo;

namespace Turquoise.Api.HealthMonitoring.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KubernetesController
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly K8sService _k8sService;
        private readonly AZAuthService _azservice;
        private readonly IFeatureManager featureManager;
        private readonly MangoBaseRepo<ServiceV1> serviceMongoRepo;
        private readonly MangoBaseRepo<AliveAndWellResult> aliveAndWellResultRepo;
        private readonly MongoAliveAndWellResultStats aliveAndWellstats;

        public KubernetesController(
            ILogger<WeatherForecastController> logger,
            K8sService k8sService,
            AZAuthService azservice,
            IFeatureManager featureManager,
            MangoBaseRepo<Turquoise.Models.Mongo.ServiceV1> serviceMongoRepo,
            MangoBaseRepo<Turquoise.Models.Mongo.AliveAndWellResult> aliveAndWellResultRepo,
            MongoAliveAndWellResultStats aliveAndWellstats
            )
        {
            _logger = logger;
            _k8sService = k8sService;
            _azservice = azservice;
            this.featureManager = featureManager;
            this.serviceMongoRepo = serviceMongoRepo;
            this.aliveAndWellResultRepo = aliveAndWellResultRepo;
            this.aliveAndWellstats = aliveAndWellstats;
        }


        [HttpGet("pods")]
        public object GetPods()
        {
            KubernetesClientConfiguration config = null;
            try
            {
                config = KubernetesClientConfiguration.BuildDefaultConfig();
            }
            catch
            {
                config = KubernetesClientConfiguration.InClusterConfig();
            }
            IKubernetes client = new Kubernetes(config);
            Console.WriteLine("Starting Request!");

            var list = client.ListNamespacedPod("cookbook-dev");
            foreach (var item in list.Items)
            {
                Console.WriteLine(item.Metadata.Name);
            }
            if (list.Items.Count == 0)
            {
                Console.WriteLine("Empty!");
            }
            return list.Items;
        }


        [HttpGet("podsmapped")]
        public async Task<object> GetPodsMapped()
        {

            await _azservice.Authenticate();

            _logger.LogCritical("GetDeployment called the service");
            return await _k8sService.GetPodsMapped("sentinel-dev");
        }

        [HttpGet("deployment")]
        public async Task<object> GetDeployment()
        {
            _logger.LogCritical("GetDeployment called the service");
            return await _k8sService.GetAllDeploymentsAsync();
        }



        [HttpGet("replicaset")]
        public object GetReplicaset()
        {
            KubernetesClientConfiguration config = null;
            try
            {
                config = KubernetesClientConfiguration.BuildDefaultConfig();
            }
            catch
            {
                config = KubernetesClientConfiguration.InClusterConfig();
            }
            IKubernetes client = new Kubernetes(config);
            Console.WriteLine("Starting Request!");

            var list = client.ListNamespacedReplicaSet("cookbook-dev");
            foreach (var item in list.Items)
            {
                Console.WriteLine(item.Metadata.Name);
            }
            if (list.Items.Count == 0)
            {
                Console.WriteLine("Empty!");
            }
            return list.Items;
        }


        [HttpGet("k8services")]
        public async Task<object> GetK8Services()
        {
            var items = await _k8sService.GetAllServicesAsync();
            return items;
        }

        [HttpGet("services")]
        public async Task<object> GetServices()
        {
            // var items = await _k8sService.GetServicesforCron();
            //   var items = await _k8sService.GetServices("sentinel-dev");


            var items = await _k8sService.GetAllServicesWithIngressAsync();
            return items;
            // KubernetesClientConfiguration config = null;
            // try
            // {
            //     config = KubernetesClientConfiguration.BuildDefaultConfig();
            // }
            // catch
            // {
            //     config = KubernetesClientConfiguration.InClusterConfig();
            // }
            // IKubernetes client = new Kubernetes(config);
            // Console.WriteLine("Starting Request!");

            // var list = client.ListNamespacedService("cookbook-dev");
            // return list.Items;
        }


        [HttpGet("namespaces")]
        public object GetNamespaces()
        {
            KubernetesClientConfiguration config = null;
            try
            {
                config = KubernetesClientConfiguration.BuildDefaultConfig();
            }
            catch
            {
                config = KubernetesClientConfiguration.InClusterConfig();
            }
            IKubernetes client = new Kubernetes(config);
            Console.WriteLine("Starting Request!");


            var namespaces = client.ListNamespace();
            return namespaces.Items;
        }



        [HttpGet("ingress")]
        public async Task<object> GetIngress()
        {
            KubernetesClientConfiguration config = null;
            try
            {
                config = KubernetesClientConfiguration.BuildDefaultConfig();
            }
            catch
            {
                config = KubernetesClientConfiguration.InClusterConfig();
            }
            IKubernetes client = new Kubernetes(config);
            Console.WriteLine("Starting Request!");


            var ingresses = await _k8sService.GetAllIngressAsync();
            return ingresses;
        }

        [HttpGet("MapServiceIngressAndPods")]
        public async Task<object> GetMapServiceIngressAndPods()
        {
            KubernetesClientConfiguration config = null;
            try
            {
                config = KubernetesClientConfiguration.BuildDefaultConfig();
            }
            catch
            {
                config = KubernetesClientConfiguration.InClusterConfig();
            }
            IKubernetes client = new Kubernetes(config);
            Console.WriteLine("Starting Request!");


            var ingresses = _k8sService.MapServiceIngressAndPodsAsync();
            return ingresses;
        }


        [HttpGet("NodeMetrics")]
        public async Task<object> GetNodeMetrics()
        {
            KubernetesClientConfiguration config = null;
            try
            {
                config = KubernetesClientConfiguration.BuildDefaultConfig();
            }
            catch
            {
                config = KubernetesClientConfiguration.InClusterConfig();
            }
            IKubernetes client = new Kubernetes(config);
            Console.WriteLine("Starting Request!");


            var nodesMetrics = await client.GetKubernetesNodesMetricsAsync().ConfigureAwait(false);


            foreach (var item in nodesMetrics.Items)
            {
                Console.WriteLine(item.Metadata.Name);

                foreach (var metric in item.Usage)
                {
                    Console.WriteLine($"{metric.Key}: {metric.Value}");
                }
            }
            return nodesMetrics;
        }


        [HttpGet("PodMetrics")]
        public async Task<object> GetPodMetrics()
        {
            KubernetesClientConfiguration config = null;
            try
            {
                config = KubernetesClientConfiguration.BuildDefaultConfig();
            }
            catch
            {
                config = KubernetesClientConfiguration.InClusterConfig();
            }
            IKubernetes client = new Kubernetes(config);
            Console.WriteLine("Starting Request!");

            var podsMetrics = await client.GetKubernetesPodsMetricsAsync().ConfigureAwait(false);

            if (!podsMetrics.Items.Any())
            {
                Console.WriteLine("Empty");
            }

            foreach (var item in podsMetrics.Items)
            {
                foreach (var container in item.Containers)
                {
                    Console.WriteLine(container.Name);

                    foreach (var metric in container.Usage)
                    {
                        Console.WriteLine($"{metric.Key}: {metric.Value}");
                    }
                }
                Console.Write(Environment.NewLine);
            }
            return podsMetrics;
        }


        [HttpGet("virtualservices")]
        public async Task<object> GetVirtualservices()
        {
            _logger.LogCritical("GetDeployment called the service");
            return await _k8sService.GetVirtualServicesAsync("sentinel-dev");
        }


        [HttpGet("allevents")]
        public async Task<object> GetAllevents(string namespaceparam)
        {
            _logger.LogCritical("GetDeployment called the service");
            return await _k8sService.GetEventsAsync(namespaceparam);
        }


        [HttpGet("getmongoevent")]
        public async Task<object> GetCollectedServices(string namespaceParam)
        {
            if (await featureManager.IsEnabledAsync(nameof(HealthMonitoringFeatureFlags.UseMongoData)))
            {
                return serviceMongoRepo.Find(p => p.Deleted == false && p.Namespace == namespaceParam).ToList();
            }
            else
            {
                return "Just Blah No Queue involved.";
            }
        }

        [HttpGet("getresultstats")]
        public async Task<object> GetScheduledTeststats(string namespaceParam)
        {
            if (await featureManager.IsEnabledAsync(nameof(HealthMonitoringFeatureFlags.UseMongoData)))
            {
                return aliveAndWellstats.GetResultStatWithCache(namespaceParam);
                // var stats = new AliveAndWellResultStat();


                // var totalservices = serviceMongoRepo.Find(p => p.Deleted == false && p.Namespace == namespaceParam).Count();
                // var unhealthystats = aliveAndWellResultRepo.Find(p => p.ServiceNamespace == namespaceParam && p.Status != "OK" && p.CreationTime > DateTime.UtcNow.AddDays(-1)).Count();
                // var healthystats = aliveAndWellResultRepo.Find(p => p.ServiceNamespace == namespaceParam && p.Status == "OK" && p.CreationTime > DateTime.UtcNow.AddDays(-1)).Count();
                // var unhealthyserv = aliveAndWellResultRepo.Find(p => p.ServiceNamespace == namespaceParam && p.Status != "OK" && p.CreationTime > DateTime.UtcNow.AddDays(-1)).Select(p => p.ServiceName).Distinct().ToList();
                // stats.AllServices = totalservices;
                // stats.AllRunsOnToday = unhealthystats + healthystats;
                // stats.HealthyRunsOnToday = healthystats;
                // stats.UnhealthyRunsOnToday = unhealthystats;
                // stats.UnhealthyServicesToday = unhealthyserv;
                // //var stats2 = aliveAndWellResultRepo.Find(p => p.ServiceNamespace == namespaceParam && p.CreationTime > DateTime.UtcNow.AddDays(-1)).Distinct();
                // return stats;
            }
            else
            {
                return "Just Blah No Queue involved.";
            }
        }


        [HttpGet("newevent")]
        public async Task<object> CreateNewEvent()
        {

            var services = await _k8sService.GetServices("sentinel-dev");
            var service = services.FirstOrDefault(p => p.Metadata.Name == "sentinel-dev-admin-ui");


            if (service != null)
            {

                var message = service.Metadata.Name + " HealthCheck isAlive and Well Failed";
                var returnedevent = await _k8sService.EventClient.CountUpOrCreateEvent("sentinel-dev", service, message);
                return returnedevent;
            }
            else
            {
                return "service Not Found";
            }

        }


        [HttpGet("getmongodeployment")]
        public async Task<object> GetMongoDeployment()
        {
            var deployments = await _k8sService.GetAllMongoDeploymentsAsync();
            return deployments;
        }



    }
}