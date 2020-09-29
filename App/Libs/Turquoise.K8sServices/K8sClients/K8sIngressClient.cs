using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Logging;
using Turquoise.Models.Mongo;

namespace Turquoise.K8sServices.K8sClients
{
    public class K8sIngressClient : IK8sClient<Extensionsv1beta1Ingress>
    {
        private readonly Kubernetes client;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public K8sIngressClient(Kubernetes kubernetesClient, ILogger logger, IMapper mapper)
        {
            this.client = kubernetesClient;
            this.logger = logger;
            this.mapper = mapper;
        }

        public IList<Extensionsv1beta1Ingress> Get(string nameSpace)
        {
            return client.ListIngressForAllNamespaces().Items;
        }

        public async Task<IList<Extensionsv1beta1Ingress>> GetAsync(string nameSpace)
        {
            var ingress = await client.ListNamespacedIngressAsync(nameSpace);
            return ingress.Items;
        }

        public async Task<IList<Extensionsv1beta1Ingress>> GetAllAsync()
        {
            var result = await client.ListIngressForAllNamespacesAsync();
            return result.Items;
        }


    }
}