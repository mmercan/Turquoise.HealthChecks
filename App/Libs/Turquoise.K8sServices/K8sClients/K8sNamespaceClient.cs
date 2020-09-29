using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Logging;
using Turquoise.Models.Mongo;

namespace Turquoise.K8sServices.K8sClients
{
    public class K8sNamespaceClient
    {
        private Kubernetes client;
        private ILogger logger;
        private IMapper mapper;

        public K8sNamespaceClient(Kubernetes kubernetesClient, ILogger logger, IMapper mapper)
        {
            this.client = kubernetesClient;
            this.logger = logger;
            this.mapper = mapper;
        }

        public IList<V1Namespace> Get()
        {
            var namespaces = client.ListNamespace().Items;
            //var mappedns = mapper.Map<List<NamespaceV1>>(namespaces);
            return namespaces;
        }

        public IList<NamespaceV1> GetMongo()
        {
            var namespaces = client.ListNamespace().Items;
            var mappedns = mapper.Map<List<NamespaceV1>>(namespaces);
            return mappedns;
        }

        public async Task<IList<V1Namespace>> GetAsync()
        {
            var namespaces = await client.ListNamespaceAsync();
            //   var mappedns = mapper.Map<List<NamespaceV1>>(namespaces.Items);
            return namespaces.Items;
        }

        public async Task<IList<NamespaceV1>> GetMongoAsync()
        {
            var namespaces = await client.ListNamespaceAsync();
            var mappedns = mapper.Map<List<NamespaceV1>>(namespaces.Items);
            return mappedns;
        }

        public IList<NamespaceV1> Get(string labelSelector)
        {
            var namespaces = client.ListNamespace(labelSelector: labelSelector).Items;
            var mappedns = mapper.Map<List<NamespaceV1>>(namespaces);
            return mappedns;
        }

        public async Task<IList<NamespaceV1>> GetAsync(string labelSelector)
        {
            var namespaces = await client.ListNamespaceAsync(labelSelector: labelSelector);
            var mappedns = mapper.Map<List<NamespaceV1>>(namespaces.Items);
            return mappedns;
        }


    }
}