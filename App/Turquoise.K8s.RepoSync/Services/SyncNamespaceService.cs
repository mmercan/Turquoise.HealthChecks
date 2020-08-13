using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Quartz;
using Turquoise.K8s.Services;
using System.Linq;

namespace Turquoise.K8s.RepoSync.Services
{

    [DisallowConcurrentExecution]
    public class SyncNamespaceService : IJob
    {
        private readonly ILogger<SyncNamespaceService> _logger;
        private readonly K8sService k8sService;

        public SyncNamespaceService(ILogger<SyncNamespaceService> logger, K8sService k8sService)
        {
            _logger = logger;
            this.k8sService = k8sService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var ns = await k8sService.GetNamespaces();


            // var text = ns.ToJson();
            var textarr = ns.Select(n => n.Metadata.Name);
            var text = string.Join(".", textarr);
            _logger.LogCritical(text);

            _logger.LogInformation("Hello world!");
            // return Task.CompletedTask;
        }
    }
}