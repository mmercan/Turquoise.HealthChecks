using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using Turquoise.Models.Mongo;

namespace Turquoise.K8sServices.K8sClients
{
    public class K8sDeploymentClient : IK8sClient<V1Deployment>
    {
        private readonly Kubernetes client;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public K8sDeploymentClient(Kubernetes kubernetesClient, ILogger logger, IMapper mapper)
        {
            this.client = kubernetesClient;
            this.logger = logger;
            this.mapper = mapper;
        }

        public IList<V1Deployment> Get(string nameSpace)
        {
            var deploys = client.ListNamespacedDeployment(nameSpace).Items;
            //  var mappedDeploys = mapper.Map<List<DeploymentV1>>(deploys);
            return deploys;
        }

        public async Task<IList<V1Deployment>> GetAsync(string nameSpace)
        {
            var deploys = await client.ListNamespacedDeploymentAsync(nameSpace);
            //  var mappedDeploys = mapper.Map<List<DeploymentV1>>(deploys.Items);
            return deploys.Items;
        }

        public async Task<IList<V1Deployment>> GetAllAsync()
        {
            var deploys = await client.ListDeploymentForAllNamespacesAsync();
            //  var mappedDeploys = mapper.Map<List<DeploymentV1>>(deploys.Items);
            return deploys.Items;
        }

        public async Task<IList<Turquoise.Models.Mongo.DeploymentV1>> GetAllMongoDeploymentsAsync()
        {
            var items = await GetAllAsync();
            var dtoitems = mapper.Map<IList<Turquoise.Models.Mongo.DeploymentV1>>(items);
            return dtoitems;
        }


        public async Task<V1Deployment> GetSingleAsync(string name, string nameSpace)
        {
            var deployment = await client.ReadNamespacedDeploymentAsync(name, nameSpace);

            return deployment;
        }

        public async Task<V1Deployment> ScaleDeployment(string name, string nameSpace, int ScaleNumber)
        {

            var patch = new JsonPatchDocument<V1Deployment>();
            patch.Replace(e => e.Spec.Replicas, ScaleNumber);
            //  var orjdeployment  =await GetSingleAsync(name,nameSpace);
            //   orjdeployment.Spec.Replicas =
            // var deployment = await client.ReplaceNamespacedDeployment()



            var deployment = await client.PatchNamespacedDeploymentAsync(new V1Patch(patch), name, nameSpace);
            return deployment;
        }

    }
}