using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Turquoise.K8s.Services;
namespace Turquoise.Api.HealthMonitoring.GRPCServices
{
    public class NamespaceGRPCService : NamespaceService.NamespaceServiceBase
    {
        private ILogger<NamespaceGRPCService> _logger;
        private K8sService k8sService;

        public NamespaceGRPCService(ILogger<NamespaceGRPCService> logger, K8sService k8sService)
        {
            _logger = logger;
            this.k8sService = k8sService;
        }
        public override async Task<NamespaceListReply> GetNamespaces(Google.Protobuf.WellKnownTypes.Empty request, Grpc.Core.ServerCallContext context)
        {
            _logger.LogCritical("Got in to Get Namespace GRPC");

            var namespaces = new NamespaceListReply();

            var ns = await k8sService.GetNamespaces();

            namespaces.Namespaces.AddRange(ns);
            return namespaces;
            //   countriesReply.Countries.Add(new CountryReply { Description = "Blah", Id = 1, Name = "Australia" });
            //  countriesReply.Countries.Add(new CountryReply { Description = "Blah Blahhh", Id = 2, Name = "Turkey" });
            //  return Task.FromResult(countriesReply);

        }
    }
}