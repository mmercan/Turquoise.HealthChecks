using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Quartz;
using Turquoise.Common.Mongo;
using Turquoise.K8sServices;

namespace Turquoise.Worker.Sync.JobSchedules
{
    [DisallowConcurrentExecution]
    public class SyncNamespaces : IJob
    {
        private readonly ILogger<SyncNamespaces> logger;
        private readonly K8sServices.K8sGeneralService k8sService;
        private readonly MangoBaseRepo<Turquoise.Models.Mongo.NamespaceV1> namespaceMongoRepo;

        public SyncNamespaces(ILogger<SyncNamespaces> logger, K8sGeneralService k8sService, MangoBaseRepo<Turquoise.Models.Mongo.NamespaceV1> namespaceMongoRepo)
        {
            this.logger = logger;
            this.k8sService = k8sService;
            this.namespaceMongoRepo = namespaceMongoRepo;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var ns = await k8sService.NamespaceClient.GetMongoAsync();
            var syncTime = DateTime.UtcNow;
            logger.LogCritical(ns.ToJson());

            foreach (var item in ns)
            {
                item.LatestSyncDateUTC = syncTime;
                await namespaceMongoRepo.Upsert(item, p => p.Name == item.Name);
            }

            var textarr = ns.Select(n => n.Name);
            var text = string.Join(".", textarr);
            logger.LogCritical(text);

            logger.LogInformation("SyncNamespaceService Completed!");
        }

    }
}