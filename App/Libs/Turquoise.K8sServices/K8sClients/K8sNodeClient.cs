using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Logging;
using Turquoise.Models.Mongo;


namespace Turquoise.K8sServices.K8sClients
{
    public class K8sNodeClient
    {
        private readonly Kubernetes client;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public K8sNodeClient(Kubernetes kubernetesClient, ILogger logger, IMapper mapper)
        {
            this.client = kubernetesClient;
            this.logger = logger;
            this.mapper = mapper;
        }

        public IList<V1Node> Get()
        {
            return client.ListNode().Items;
        }

        public async Task<IList<V1Node>> GetAsync()
        {
            var nodes = await client.ListNodeAsync();
            return nodes.Items;

        }
    }
}