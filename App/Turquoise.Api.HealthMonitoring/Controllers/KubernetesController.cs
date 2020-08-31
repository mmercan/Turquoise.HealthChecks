using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using k8s;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Turquoise.K8s.Services;

namespace Turquoise.Api.HealthMonitoring.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KubernetesController
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly K8sService _k8sService;
        private readonly AZAuthService _azservice;

        public KubernetesController(ILogger<WeatherForecastController> logger, K8sService k8sService, AZAuthService azservice)
        {
            _logger = logger;
            _k8sService = k8sService;
            _azservice = azservice;
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
            return await _k8sService.GetDeploymentsAsync("sentinel-dev");
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
        public async Task<object> GetAllevents()
        {
            _logger.LogCritical("GetDeployment called the service");
            return await _k8sService.GetEventsAsync("sentinel-dev");
        }
    }
}