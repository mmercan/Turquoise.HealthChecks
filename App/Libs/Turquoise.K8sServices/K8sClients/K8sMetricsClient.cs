using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Logging;

namespace Turquoise.K8sServices.K8sClients
{
    public class K8sMetricsClient
    {
        private readonly Kubernetes client;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public K8sMetricsClient(Kubernetes kubernetesClient, ILogger logger, IMapper mapper)
        {
            this.client = kubernetesClient;
            this.logger = logger;
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