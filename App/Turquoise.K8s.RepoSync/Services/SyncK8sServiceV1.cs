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

namespace Turquoise.K8s.RepoSync.Services
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
            var serives = await k8sService.GetAllServicesAsync();

            var dtoitems = mapper.Map<IList<Turquoise.Models.Mongo.ServiceV1>>(serives);
            _logger.LogCritical(dtoitems.ToJson());

            foreach (var item in dtoitems)
            {
                await serviceRepo.Upsert(item, p => p.Name == item.Name);
            }

            var textarr = serives.Select(n => n.Metadata.Name);
            var text = string.Join(".", textarr);
            _logger.LogCritical(text);

            _logger.LogInformation("Hello world!");
            // return Task.CompletedTask;
        }
    }
}