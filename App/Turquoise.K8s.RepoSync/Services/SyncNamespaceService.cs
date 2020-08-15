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
    public class SyncNamespaceService : IJob
    {
        private readonly ILogger<SyncNamespaceService> _logger;
        private readonly K8sService k8sService;
        private readonly MangoBaseRepo<Turquoise.Models.Mongo.NamespaceV1> namespaceRepo;
        private readonly IMapper mapper;

        public SyncNamespaceService(ILogger<SyncNamespaceService> logger, K8sService k8sService, MangoBaseRepo<Turquoise.Models.Mongo.NamespaceV1> namespaceRepo, IMapper mapper)
        {
            _logger = logger;
            this.k8sService = k8sService;
            this.namespaceRepo = namespaceRepo;
            this.mapper = mapper;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var ns = await k8sService.GetNamespaces();

            var dtoitems = mapper.Map<IList<Turquoise.Models.Mongo.NamespaceV1>>(ns);
            _logger.LogCritical(dtoitems.ToJson());

            foreach (var item in dtoitems)
            {
                await namespaceRepo.Upsert(item, p => p.Name == item.Name);
            }


            // var text = ns.ToJson();
            var textarr = ns.Select(n => n.Metadata.Name);
            var text = string.Join(".", textarr);
            _logger.LogCritical(text);

            _logger.LogInformation("Hello world!");
            // return Task.CompletedTask;
        }
    }
}