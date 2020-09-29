using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Logging;
using Turquoise.Models.Mongo;

namespace Turquoise.K8sServices.K8sClients
{
    public class K8sReplicaSetClient : IK8sClient<V1ReplicaSet>
    {
        private readonly Kubernetes client;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public K8sReplicaSetClient(Kubernetes kubernetesClient, ILogger logger, IMapper mapper)
        {
            this.client = kubernetesClient;
            this.logger = logger;
            this.mapper = mapper;
        }

        public IList<V1ReplicaSet> Get(string nameSpace)
        {
            var replicasets = client.ListNamespacedReplicaSet(nameSpace).Items;
            //  var mappedDeploys = mapper.Map<List<DeploymentV1>>(deploys);
            return replicasets;
        }

        public async Task<IList<V1ReplicaSet>> GetAsync(string nameSpace)
        {
            var replicasets = await client.ListNamespacedReplicaSetAsync(nameSpace);
            //  var mappedDeploys = mapper.Map<List<DeploymentV1>>(deploys.Items);
            return replicasets.Items;
        }

        public async Task<IList<V1ReplicaSet>> GetAllAsync()
        {
            var replicasets = await client.ListReplicaSetForAllNamespacesAsync();
            //var mappedDeploys = mapper.Map<List<DeploymentV1>>(deploys.Items);
            return replicasets.Items;
        }


    }
}