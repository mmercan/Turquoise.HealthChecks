using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;
using Turquoise.Common.Mongo;
using Turquoise.K8sServices;

namespace Turquoise.Worker.Sync.JobSchedules
{
    [DisallowConcurrentExecution]
    public class SyncServices : IJob
    {
        private readonly ILogger<SyncServices> logger;
        private readonly K8sGeneralService k8sService;
        private readonly MangoBaseRepo<Turquoise.Models.Mongo.ServiceV1> serviceMongoRepo;

        public SyncServices(ILogger<SyncServices> logger, K8sGeneralService k8sService, MangoBaseRepo<Turquoise.Models.Mongo.ServiceV1> serviceMongoRepo)
        {
            this.logger = logger;
            this.k8sService = k8sService;
            this.serviceMongoRepo = serviceMongoRepo;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var services = await k8sService.ServiceClient.GetAllMongoServiceAsync();
            var syncTime = DateTime.UtcNow;

            foreach (var item in services)
            {
                logger.LogInformation(item.NameandNamespace + " upsert");
                item.LatestSyncDateUTC = syncTime;
                await serviceMongoRepo.Upsert(item, p => p.Name == item.Name && p.Namespace == item.Namespace);
            }

            var mongodbservices = await serviceMongoRepo.GetAllAsync();
            foreach (var item in mongodbservices)
            {
                if (!services.Any(p => p.Uid == item.Uid))
                {
                    item.Deleted = true;
                    logger.LogInformation(item.NameandNamespace + " tag as deleted");
                    await serviceMongoRepo.UpdateAsync(item);
                }
            }

            var textarr = services.Select(n => n.Name);
            var text = string.Join(".", textarr);
            logger.LogCritical(text);

            logger.LogInformation("SyncK8sServiceV1 Completed");
            // return Task.CompletedTask;
        }

    }
}