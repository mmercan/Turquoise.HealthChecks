using System.Collections.Generic;
using k8s;
using k8s.Models;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace Turquoise.K8s.K8sClients
{
    public class K8sPodClient
    {
        private Kubernetes client;
        public K8sPodClient(Kubernetes kubernetesClient)
        {
            this.client = kubernetesClient;
        }

        public IList<V1Pod> Get(string nameSpace)
        {
            return this.client.ListNamespacedPod(nameSpace).Items;
        }

        public async Task<IList<V1Pod>> GetAsync(string nameSpace)
        {
            var pods = await this.client.ListNamespacedPodAsync(nameSpace);
            return pods.Items;
        }


        public IList<V1Pod> Get(string nameSpace, string labelSelector)
        {
            return this.client.ListNamespacedPod(nameSpace, labelSelector: labelSelector).Items;
        }

        public async Task<IList<V1Pod>> GetAsync(string nameSpace, string labelSelector)
        {
            var pods = await this.client.ListNamespacedPodAsync(nameSpace, labelSelector: labelSelector);
            return pods.Items;
        }

        public IList<V1Pod> GetAll()
        {
            var result = new List<V1Pod>();
            var namespaces = client.ListNamespace();
            foreach (var ns in namespaces.Items)
            {
                var list = client.ListNamespacedPod(ns.Metadata.Name).Items;
                result.AddRange(list);
            }
            return result;
        }


        public async Task<IList<V1Pod>> GetAllAsync()
        {
            var result = new List<V1Pod>();
            var namespaces = await client.ListNamespaceAsync();
            foreach (var ns in namespaces.Items)
            {
                var list = await client.ListNamespacedPodAsync(ns.Metadata.Name);
                result.AddRange(list.Items);
            }
            return result;
        }

        public IList<V1Pod> GetAll(string labelSelector)
        {
            var result = new List<V1Pod>();
            var namespaces = client.ListNamespace();
            foreach (var ns in namespaces.Items)
            {
                var list = client.ListNamespacedPod(ns.Metadata.Name, labelSelector: labelSelector).Items;
                result.AddRange(list);
            }
            return result;
        }

        public async Task<IList<V1Pod>> GetAllAsync(string labelSelector)
        {
            var result = new List<V1Pod>();
            var namespaces = await client.ListNamespaceAsync();
            foreach (var ns in namespaces.Items)
            {
                var list = await client.ListNamespacedPodAsync(ns.Metadata.Name, labelSelector: labelSelector);
                result.AddRange(list.Items);
            }
            return result;
        }

        public string GetLogs(string PodName, string nameSpace)
        {
            var logStream = client.ReadNamespacedPodLog(PodName, nameSpace);
            StreamReader reader = new StreamReader(logStream);
            string log = reader.ReadToEnd();
            return log;
        }

        public async Task<string> GetLogsAsync(string PodName, string nameSpace)
        {
            var logStream = await client.ReadNamespacedPodLogAsync(PodName, nameSpace);
            StreamReader reader = new StreamReader(logStream);
            string log = reader.ReadToEnd();
            return log;
        }

    }
}
