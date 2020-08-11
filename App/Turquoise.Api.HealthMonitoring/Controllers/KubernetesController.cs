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

        public KubernetesController(ILogger<WeatherForecastController> logger, K8sService k8sService)
        {
            _logger = logger;
            _k8sService = k8sService;
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

        [HttpGet("deployment")]
        public async Task<object> GetDeployment()
        {
            _logger.LogCritical("GetDeployment called the service");
            return await _k8sService.GetDeploymentsAsync("ingress-basic");
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

            // var list = client.ListNamespacedDeployment("cookbook-dev");
            // foreach (var item in list.Items)
            // {
            //     Console.WriteLine(item.Metadata.Name);
            // }
            // if (list.Items.Count == 0)
            // {
            //     Console.WriteLine("Empty!");
            // }
            // return list.Items;
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

        [HttpGet("services")]
        public async Task<object> GetServices()
        {
            var items = await _k8sService.GetServicesforCron();
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
    }
}