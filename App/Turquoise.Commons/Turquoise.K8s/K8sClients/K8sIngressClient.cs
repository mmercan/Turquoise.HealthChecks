using System.Collections.Generic;
using System.Threading.Tasks;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Logging;

namespace Turquoise.K8s.K8sClients
{
    public class K8sIngressClient
    {
        private Kubernetes client;

        public K8sIngressClient(Kubernetes kubernetesClient)
        {
            this.client = kubernetesClient;
        }

        public IList<Extensionsv1beta1Ingress> GetAll()
        {
            return client.ListIngressForAllNamespaces().Items;
        }

        public async Task<IList<Extensionsv1beta1Ingress>> GetAllAsync()
        {
            var result = await client.ListIngressForAllNamespacesAsync();
            return result.Items;
        }
    }
}