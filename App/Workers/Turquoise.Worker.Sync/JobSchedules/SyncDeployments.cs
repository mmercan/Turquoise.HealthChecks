using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Quartz;
using Turquoise.Common.Mongo;
using Turquoise.K8sServices;
using Turquoise.Models.Mongo;

namespace Turquoise.Worker.Sync.JobSchedules
{
    public class SyncDeployments : IJob
    {
        private readonly ILogger<SyncDeployments> logger;
        private readonly K8sGeneralService k8sService;
        private readonly MangoBaseRepo<DeploymentV1> deploymentMongoRepo;

        public SyncDeployments(ILogger<SyncDeployments> logger, K8sGeneralService k8sService, MangoBaseRepo<DeploymentV1> deploymentMongoRepo)
        {
            this.logger = logger;
            this.k8sService = k8sService;
            this.deploymentMongoRepo = deploymentMongoRepo;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var dtoitems = await k8sService.DeploymentClient.GetAllMongoDeploymentsAsync();
            var syncTime = DateTime.UtcNow;

            foreach (var item in dtoitems)
            {
                item.Name = item.Metadata.Name;
                item.Namespace = item.Metadata.Namespace;
                item.SyncDate = syncTime;

                logger.LogInformation(item.NameandNamespace + " upsert");
                await deploymentMongoRepo.Upsert(item, p => p.Name == item.Name && p.Namespace == item.Namespace);
            }


            var mongodbservices = await deploymentMongoRepo.GetAllAsync();
            foreach (var item in mongodbservices)
            {
                if (!dtoitems.Any(p => p.Metadata.Name == item.Name && p.Metadata.Namespace == item.Namespace))
                {
                    item.Deleted = true;
                    logger.LogInformation(item.NameandNamespace + " tag as deleted");
                    await deploymentMongoRepo.UpdateAsync(item);
                }
            }
            logger.LogCritical("Deployment Sync Completed ...!");
            //  logger.LogCritical(dtoitems.ToJSON());
        }

    }
}