using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Turquoise.K8s.Services;

namespace Turquoise.Api.HealthMonitoring.GRPCServices
{
    public class MetricGRPCService : K8MetricService.K8MetricServiceBase
    {

        private ILogger<MetricGRPCService> _logger;
        private K8sMetricsService k8SMetricsService;

        public MetricGRPCService(ILogger<MetricGRPCService> logger, K8sMetricsService k8SMetricsService)
        {
            _logger = logger;
            this.k8SMetricsService = k8SMetricsService;
        }


        public override async Task<NodeMetricListReply> GetNodeMetric(Google.Protobuf.WellKnownTypes.Empty request, Grpc.Core.ServerCallContext context)
        {
            NodeMetricListReply reply = new NodeMetricListReply();
            var metrics = await k8SMetricsService.GetNodeMetrics();

            foreach (var item in metrics)
            {

                NodeMetricReply nodemetric = new NodeMetricReply();

                reply.Metrics.Add(nodemetric);
                nodemetric.Name = item.Metadata.Name;
                nodemetric.Window = item.Window;
                nodemetric.Timestamp = item.Timestamp.ToString();
                foreach (var metric in item.Usage)
                {
                    nodemetric.Usages.Add(new UsagePair { Key = metric.Key, Value = metric.Value.CanonicalizeString() });
                }
            }
            return reply;

        }
    }
}