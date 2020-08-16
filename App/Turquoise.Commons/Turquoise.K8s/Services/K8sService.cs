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

        public void GetDeploymentDescribe()
        {

        }

    }
}