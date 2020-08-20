using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Logging;

namespace Turquoise.K8s.Services
{
    public class K8sMetricsService
    {
        private Kubernetes client;
        private IMapper mapper;

        public K8sMetricsService(IKubernetesClient kubernetesClient, IMapper mapper, ILogger<K8sMetricsService> logger)
        {
            this.client = kubernetesClient.Client;
            this.mapper = mapper;
        }


        public async Task<List<NodeMetrics>> GetNodeMetrics()
        {
            var result = await this.client.GetKubernetesNodesMetricsAsync();
            return result.Items.ToList();
        }


        public async Task<IEnumerable<PodMetrics>> GetPodsMetrics(string namespaceParam)
        {
            var result = await this.client.GetKubernetesPodsMetricsByNamespaceAsync(namespaceParam);
            return result.Items;
        }
    }
}