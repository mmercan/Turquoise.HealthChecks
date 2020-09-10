using System.Collections.Generic;
using k8s;
using k8s.Models;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace Turquoise.K8s.K8sClients
{
    public class K8sDeploymentClient
    {

        private Kubernetes client;
        public K8sDeploymentClient(Kubernetes kubernetesClient)
        {
            this.client = kubernetesClient;
        }

        public IList<V1Deployment> Get(string nameSpace)
        {
            return client.ListNamespacedDeployment(nameSpace).Items;

        }

        public async Task<IList<V1Deployment>> GetAsync(string nameSpace)
        {
            var deployments = await client.ListNamespacedDeploymentAsync(nameSpace);
            return deployments.Items;
        }


        public async Task<IList<V1Deployment>> GetAllAsync()
        {
            var deployments = await client.ListDeploymentForAllNamespacesAsync();
            return deployments.Items;
        }

    }
}