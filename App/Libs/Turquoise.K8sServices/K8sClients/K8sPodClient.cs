using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Logging;
using Turquoise.Models.Mongo;

namespace Turquoise.K8sServices.K8sClients
{
    public class K8sPodClient : IK8sClient<PodV1>
    {
        private readonly Kubernetes client;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public K8sPodClient(Kubernetes kubernetesClient, ILogger logger, IMapper mapper)
        {
            this.client = kubernetesClient;
            this.logger = logger;
            this.mapper = mapper;
        }

        public IList<PodV1> Get(string nameSpace)
        {
            var pods = this.client.ListNamespacedPod(nameSpace).Items;
            var mappedPods = mapper.Map<List<PodV1>>(pods);
            return mappedPods;
        }

        public async Task<IList<PodV1>> GetAsync(string nameSpace)
        {
            var pods = await this.client.ListNamespacedPodAsync(nameSpace);
            var mappedPods = mapper.Map<List<PodV1>>(pods.Items);
            return mappedPods;
        }


        public async Task<IList<PodV1>> GetAllAsync()
        {
            var pods = await client.ListPodForAllNamespacesAsync();
            var mappedpods = mapper.Map<List<PodV1>>(pods);
            return mappedpods;

            // var result = new List<V1Pod>();
            // var namespaces = await client.ListNamespaceAsync();
            // foreach (var ns in namespaces.Items)
            // {
            //     var list = await client.ListNamespacedPodAsync(ns.Metadata.Name);
            //     result.AddRange(list.Items);
            // }
            // return result;
        }

        public IList<PodV1> Get(string nameSpace, string labelSelector)
        {
            var pods = this.client.ListNamespacedPod(nameSpace, labelSelector: labelSelector).Items;
            var mappedpods = mapper.Map<List<PodV1>>(pods);
            return mappedpods;
        }

        public async Task<IList<PodV1>> GetAsync(string nameSpace, string labelSelector)
        {
            var pods = await this.client.ListNamespacedPodAsync(nameSpace, labelSelector: labelSelector);
            var mappedPods = mapper.Map<List<PodV1>>(pods);
            return mappedPods;
        }

        public IList<V1Pod> GetAll(string labelSelector)
        {


            var pods = client.ListNamespacedPod("", labelSelector: labelSelector).Items;
            var mappedpods = mapper.Map<List<V1Pod>>(pods);
            return mappedpods;

            // var result = new List<V1Pod>();
            // var namespaces = client.ListNamespace();
            // foreach (var ns in namespaces.Items)
            // {
            //     var list = client.ListNamespacedPod(ns.Metadata.Name, labelSelector: labelSelector).Items;
            //     result.AddRange(list);
            // }
            // return result;
        }

        public async Task<IList<PodV1>> GetAllAsync(string labelSelector)
        {

            var pods = await client.ListNamespacedPodAsync("", labelSelector: labelSelector);
            var mappedpods = mapper.Map<List<PodV1>>(pods);
            return mappedpods;
            // var result = new List<V1Pod>();
            // var namespaces = await client.ListNamespaceAsync();
            // foreach (var ns in namespaces.Items)
            // {
            //     var list = await client.ListNamespacedPodAsync(ns.Metadata.Name, labelSelector: labelSelector);
            //     result.AddRange(list.Items);
            // }
            // return result;
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