using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Turquoise.K8s.Services;
namespace Turquoise.Api.HealthMonitoring.GRPCServices
{
    [Authorize]
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
            var listns = ns.Select(ss => { return new NamespaceReply { CreationDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(ss.Metadata.CreationTimestamp.Value), Namespace = ss.Metadata.Name }; });
            namespaces.Namespaces.AddRange(listns);
            namespaces.UpdatedTime = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow);
            return namespaces;
        }

        public override async Task<DeploymentListReply> GetDeployments(GetDeploymentsRequest request, Grpc.Core.ServerCallContext context)
        {
            var ns = request.Namespace;
            if (String.IsNullOrWhiteSpace(ns))
            {
                throw new ArgumentException("Namespace is missing");
            }
            var deployments = await k8sService.GetDeploymentsAsync(ns);
            DeploymentListReply deploy = new DeploymentListReply();

            foreach (var item in deployments)
            {
                var dep = new DeploymentReply();
                dep.Name = item.Metadata.Name;
                dep.Image = item.Spec.Template.Spec.Containers.FirstOrDefault().Image;
                dep.Status = item.Status.Conditions.FirstOrDefault().Status.ToString();
                dep.Labels.AddRange(item.Metadata.Labels.Select(lab => { return new Pair { Key = lab.Key, Value = lab.Value }; }));

                deploy.Deployments.Add(dep);
            }

            return deploy;
        }
    }
}