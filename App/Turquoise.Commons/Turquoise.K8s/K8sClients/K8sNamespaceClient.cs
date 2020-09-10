using System.Collections.Generic;
using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace Turquoise.K8s.K8sClients
{
    public class K8sNamespaceClient
    {
        private Kubernetes client;

        public K8sNamespaceClient(Kubernetes kubernetesClient)
        {
            this.client = kubernetesClient;
        }

        public IList<V1Namespace> Get()
        {
            var namespaces = client.ListNamespace().Items;
            return namespaces;
        }


        public async Task<IList<V1Namespace>> GetAsync()
        {
            var namespaces = await client.ListNamespaceAsync();
            return namespaces.Items;
        }

        public IList<V1Namespace> Get(string labelSelector)
        {
            var namespaces = client.ListNamespace(labelSelector: labelSelector).Items;
            return namespaces;
        }


        public async Task<IList<V1Namespace>> GetAsync(string labelSelector)
        {
            var namespaces = await client.ListNamespaceAsync(labelSelector: labelSelector);
            return namespaces.Items;
        }

    }
}