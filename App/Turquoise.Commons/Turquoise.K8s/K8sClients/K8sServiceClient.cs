using System.Collections.Generic;
using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace Turquoise.K8s.K8sClients
{
    public class K8sServiceClient
    {
        private Kubernetes client;
        public K8sServiceClient(Kubernetes kubernetesClient)
        {
            this.client = kubernetesClient;
        }

        public IList<V1Service> GetAll()
        {
            return client.ListServiceForAllNamespaces().Items;

        }

        public async Task<IList<V1Service>> GetAllAsync()
        {
            var services = await client.ListServiceForAllNamespacesAsync();
            return services.Items;
        }

        public IList<V1Service> Get(string namespaceparam)
        {
            return client.ListNamespacedService(namespaceparam).Items;

        }

        public async Task<IList<V1Service>> GetAsync(string namespaceparam)
        {
            var services = await client.ListNamespacedServiceAsync(namespaceparam);
            return services.Items;
        }

    }
}