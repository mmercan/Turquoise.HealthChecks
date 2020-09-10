using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Quartz;
using Turquoise.K8s.Services;
using System.Linq;
using Turquoise.Common.Mongo;
using k8s.Models;
using AutoMapper;
using System.Collections.Generic;
using System;

namespace Turquoise.Scheduler.JobSchedules
{
    [DisallowConcurrentExecution]
    public class SyncK8sServiceV1 : IJob
    {
        private readonly ILogger<SyncK8sServiceV1> _logger;
        private readonly K8sService k8sService;
        private readonly MangoBaseRepo<Turquoise.Models.Mongo.ServiceV1> serviceRepo;
        private readonly IMapper mapper;

        public SyncK8sServiceV1(ILogger<SyncK8sServiceV1> logger, K8sService k8sService, MangoBaseRepo<Turquoise.Models.Mongo.ServiceV1> serviceRepo, IMapper mapper)
        {
            _logger = logger;
            this.k8sService = k8sService;
            this.serviceRepo = serviceRepo;
            this.mapper = mapper;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var services = await k8sService.GetAllServicesWithIngressAsync();
            var syncTime = DateTime.UtcNow;

            foreach (var item in services)
            {
                item.LatestSyncDateUTC = syncTime;
                await serviceRepo.Upsert(item, p => p.Name == item.Name && p.Namespace == item.Namespace);
            }

            var mongodbservices = await serviceRepo.GetAllAsync();
            foreach (var item in mongodbservices)
            {
                if (!services.Any(p => p.Uid == item.Uid))
                {
                    item.Deleted = true;
                    await serviceRepo.UpdateAsync(item);
                }
            }

            var textarr = services.Select(n => n.Name);
            var text = string.Join(".", textarr);
            _logger.LogCritical(text);

            _logger.LogInformation("SyncK8sServiceV1 Completed");
            // return Task.CompletedTask;
        }
    }
}