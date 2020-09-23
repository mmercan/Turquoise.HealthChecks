using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Quartz;
using Turquoise.Common.Mongo;
using Turquoise.K8s.Services;

namespace Turquoise.Scheduler.JobSchedules
{

    [DisallowConcurrentExecution]
    public class SyncK8sDeploymentV1 : IJob
    {
        private readonly ILogger<SyncK8sDeploymentV1> logger;
        private readonly K8sService k8sService;
        private readonly MangoBaseRepo<Turquoise.Models.Mongo.DeploymentV1> deploymentRepo;
        private readonly IMapper mapper;

        public SyncK8sDeploymentV1(ILogger<SyncK8sDeploymentV1> logger, K8sService k8sService, MangoBaseRepo<Turquoise.Models.Mongo.DeploymentV1> deploymentRepo, IMapper mapper)
        {
            this.logger = logger;
            this.k8sService = k8sService;
            this.deploymentRepo = deploymentRepo;
            this.mapper = mapper;
        }


        public async Task Execute(IJobExecutionContext context)
        {
            var deployments = await k8sService.GetAllDeploymentsAsync();
            var syncTime = DateTime.UtcNow;

            var dtoitems = mapper.Map<List<Turquoise.Models.Mongo.DeploymentV1>>(deployments);
            foreach (var item in dtoitems)
            {
                item.Name = item.Metadata.Name;
                item.Namespace = item.Metadata.Namespace;
                item.SyncDate = syncTime;


                await deploymentRepo.Upsert(item, p => p.Name == item.Name && p.Namespace == item.Namespace);
            }


            var mongodbservices = await deploymentRepo.GetAllAsync();
            foreach (var item in mongodbservices)
            {
                if (!deployments.Any(p => p.Metadata.Name == item.Name && p.Metadata.NamespaceProperty == item.Namespace))
                {
                    item.Deleted = true;
                    await deploymentRepo.UpdateAsync(item);
                }
            }
            logger.LogCritical("Deployment Sync Completed ...!");
            //  logger.LogCritical(dtoitems.ToJSON());
        }


    }
}