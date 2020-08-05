using System.Collections.Generic;
using k8s;
using k8s.Models;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace Turquoise.K8s
{
    public class K8sNodeClient
    {
        private Kubernetes client;
        public K8sNodeClient(Kubernetes kubernetesClient)
        {
            this.client = kubernetesClient;
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