using System.Collections.Generic;
using k8s;
using k8s.Models;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Turquoise.Models;
using System.Linq;
using Microsoft.Rest;
using Microsoft.Extensions.Logging;
using Turquoise.K8s.K8sClients;

namespace Turquoise.K8s.Services
{
    public class K8sService
    {

        private Kubernetes client;
        private IMapper mapper;
        private K8sNamespaceClient namespacesClient;
        private K8sDeploymentClient deploymentsClient;
        private K8sNodeClient nodesClient;
        private K8sPodClient podsClient;
        private K8sServiceClient serviceClient;
        private K8sIngressClient ingressClient;
        private ILogger<K8sService> logger;

        public K8sService(IKubernetesClient kubernetesClient, IMapper mapper, ILogger<K8sService> logger)
        {
            this.client = kubernetesClient.Client;
            this.mapper = mapper;
            namespacesClient = new K8sNamespaceClient(this.client);
            deploymentsClient = new K8sDeploymentClient(this.client);
            nodesClient = new K8sNodeClient(this.client);
            podsClient = new K8sPodClient(this.client);
            serviceClient = new K8sServiceClient(this.client);
            ingressClient = new K8sIngressClient(this.client);
            this.logger = logger;
        }

        public async Task<List<V1Namespace>> GetNamespaces()
        {
            var items = await namespacesClient.GetAsync();
            return items.ToList();//items.Select(ns => ns.Name()).ToList();
        }

        public async Task<IList<V1Deployment>> GetDeploymentsAsync(string namespaceParam)
        {

            var items = await deploymentsClient.GetAsync(namespaceParam);
            // var dtoitems = mapper.Map<IList<Deployment>>(items);
            return items;//items;
        }

        public async Task<IList<V1Service>> GetServicesforCron()
        {
            var services = await this.serviceClient.GetAllAsync();
            logger.LogWarning(services.Count + "service count");
            var filtered = services.Where(p => p.Metadata != null && p.Metadata.Annotations != null && p.Metadata.Annotations.Keys.Contains("healthcheck/isalive"));
            return filtered.ToList();


        }



        public async Task<IList<V1Service>> GetServices(string namespaceParam)
        {
            var services = await this.serviceClient.GetAsync(namespaceParam);
            logger.LogWarning(services.Count + "service count");
            // var filtered = services.Where(p => p.Metadata != null && p.Metadata.Annotations != null && p.Metadata.Annotations.Keys.Contains("healthcheck/isalive"));
            return services.ToList();
        }


        public async Task<IList<V1Service>> GetAllServicesAsync()
        {
            var services = await this.serviceClient.GetAllAsync();
            logger.LogWarning(services.Count + "GetAllServicesAsync count");
            return services.ToList();
        }


        public async Task<IList<Extensionsv1beta1Ingress>> GetAllIngressAsync()
        {
            var ingresses = await this.ingressClient.GetAllAsync();
            logger.LogWarning(ingresses.Count + "GetAllServicesAsync count");
            return ingresses.ToList();
        }

        public async Task<List<MapServiceIngressPod>> MapServiceIngressAndPods()
        {
            var ingressTask = this.ingressClient.GetAllAsync();
            var serviceTask = this.serviceClient.GetAllAsync();
            // var podTask = this.podsClient.GetAllAsync();
            Task.WaitAll(ingressTask, serviceTask);

            var ingresses = ingressTask.Result;
            var services = serviceTask.Result;
            // var pods = podTask.Result;

            List<MapServiceIngressPod> maps = new List<MapServiceIngressPod>();

            foreach (var service in services)
            {

                var serviceName = service.Name();
                var serviceNamespace = service.Namespace();

                var map = new MapServiceIngressPod();
                map.Service = service;
                maps.Add(map);
                logger.LogInformation("Pods for service: " + service.Metadata.Name);
                logger.LogInformation("=-=-=-=-=-=-=-=-=-=-=");
                if (service.Spec == null || service.Spec.Selector == null)
                {
                    continue;
                }

                var labels = new List<string>();
                foreach (var key in service.Spec.Selector)
                {
                    labels.Add(key.Key + "=" + key.Value);
                }

                var labelStr = string.Join(",", labels.ToArray());
                logger.LogInformation(labelStr);
                var podList = client.ListNamespacedPod(serviceNamespace, labelSelector: labelStr);

                foreach (var pod in podList.Items)
                {
                    map.Pods.Add(pod);
                }


                var ings = ingresses.Where(p => p.Metadata.Namespace() == serviceNamespace && p.Spec.Rules.FirstOrDefault()?.Http.Paths.FirstOrDefault()?.Backend.ServiceName == serviceName);
                map.ingress = ings.FirstOrDefault();

            }
            return maps;

        }

        public void GetDeploymentDescribe()
        {

        }

    }

    public class MapServiceIngressPod
    {
        public MapServiceIngressPod()
        {
            Pods = new List<V1Pod>();
        }
        public Extensionsv1beta1Ingress ingress { get; set; }
        public V1Service Service { get; set; }

        public List<V1Pod> Pods { get; set; }
    }
}