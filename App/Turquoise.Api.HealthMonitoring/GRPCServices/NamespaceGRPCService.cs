using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using MongoDB.Driver;
using Turquoise.Api.HealthMonitoring.Helpers;
using Turquoise.Api.HealthMonitoring.Models;
using Turquoise.Common.Mongo;
using Turquoise.GRPC;
using Turquoise.GRPC.Converters;
using Turquoise.GRPC.GRPCServices;
using Turquoise.K8sServices;
using Turquoise.Models.Mongo;

namespace Turquoise.Api.HealthMonitoring.GRPCServices
{
    [Authorize]
    public class NamespaceGRPCService : NamespaceService.NamespaceServiceBase
    {
        private readonly ILogger<NamespaceGRPCService> logger;
        private readonly K8sGeneralService k8sService;
        private readonly MangoBaseRepo<ServiceV1> serviceMongoRepo;
        private readonly MangoBaseRepo<ServiceHealthCheckResultSummary> serviceCheckSummaryRepo;
        private readonly IFeatureManager featureManager;
        private readonly MongoAliveAndWellResultStats aliveAndWellResultStats;
        private readonly MangoBaseRepo<AliveAndWellResult> healthCheckMongoRepo;
        private readonly MangoBaseRepo<DeploymentScaleHistory> deploymentScaleHistoryRepo;
        private readonly IMapper mapper;
        public NamespaceGRPCService(
            ILogger<NamespaceGRPCService> logger,
            K8sGeneralService k8sService,
            MangoBaseRepo<ServiceV1> serviceMongoRepo,
            MangoBaseRepo<AliveAndWellResult> healthCheckMongoRepo,
            IFeatureManager featureManager,
            MongoAliveAndWellResultStats aliveAndWellResultStats,
            MangoBaseRepo<ServiceHealthCheckResultSummary> serviceCheckSummaryRepo,
            MangoBaseRepo<DeploymentScaleHistory> deploymentScaleHistoryRepo,
            IMapper mapper
            )
        {
            this.logger = logger;
            this.k8sService = k8sService;
            this.serviceMongoRepo = serviceMongoRepo;
            this.featureManager = featureManager;
            this.aliveAndWellResultStats = aliveAndWellResultStats;
            this.healthCheckMongoRepo = healthCheckMongoRepo;
            this.serviceCheckSummaryRepo = serviceCheckSummaryRepo;
            this.mapper = mapper;
            this.deploymentScaleHistoryRepo = deploymentScaleHistoryRepo;
        }
        public override async Task<NamespaceListReply> GetNamespaces(Google.Protobuf.WellKnownTypes.Empty request, Grpc.Core.ServerCallContext context)
        {
            logger.LogCritical("Got in to Get Namespace GRPC");
            var namespaces = new NamespaceListReply();
            var ns = await k8sService.NamespaceClient.GetAsync();
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
            var deployments = await k8sService.DeploymentClient.GetAsync(ns);
            logger.LogCritical("deployments'Count " + deployments.Count.ToString());
            DeploymentListReply deploy = DeploymentListReplyConverter.ConvertToDeploymentListReply(deployments);
            return deploy;
        }


        public override async Task<DeploymentReply> GetDeployment(GetDeploymentRequest request, Grpc.Core.ServerCallContext context)
        {
            var ns = request.Namespace;
            var deploymentName = request.DeploymentName;
            if (String.IsNullOrWhiteSpace(ns))
            {
                throw new ArgumentException("Namespace is missing");
            }
            if (String.IsNullOrWhiteSpace(deploymentName))
            {
                throw new ArgumentException("Deployment is missing");
            }

            var deployment = await k8sService.DeploymentClient.GetSingleAsync(deploymentName, ns);
            logger.LogCritical("deployment Name " + deployment.Metadata.Name);
            var deploy = DeploymentListReplyConverter.ConvertToDeploymentReply(deployment);
            return deploy;
        }

        public override Task<DeploymentScaleHistoryListReply> GetDeploymentHistories(GetDeploymentRequest request, ServerCallContext context)
        {

            var ns = request.Namespace;
            var deploymentName = request.DeploymentName;
            if (String.IsNullOrWhiteSpace(ns))
            {
                throw new ArgumentException("Namespace is missing");
            }
            if (String.IsNullOrWhiteSpace(deploymentName))
            {
                throw new ArgumentException("Deployment is missing");
            }

            var builder = Builders<DeploymentScaleHistory>.Filter;
            var filter = builder.Eq(x => x.Namespace, ns) & builder.Eq(x => x.Name, deploymentName) & builder.Gt(x => x.ScaledUtc, DateTime.UtcNow.AddDays(-14));
            var mongoScalehistories = deploymentScaleHistoryRepo.Items.Find(filter).SortByDescending(p => p.ScaledUtc).Limit(50).ToList();

            var reply = DeploymentListReplyConverter.ConvertListDeploymentScaleHistory(mongoScalehistories);
            return Task.FromResult(reply);
        }

        public async override Task<ServiceReply> GetService(GetServiceRequest request, ServerCallContext context)
        {
            var ns = request.NamespaceParam;
            var servicename = request.ServiceName;
            if (String.IsNullOrWhiteSpace(ns))
            {
                throw new ArgumentException("Namespace is missing");
            }

            if (String.IsNullOrWhiteSpace(servicename))
            {
                throw new ArgumentException("ServiceName is missing");
            }

            return await getMongoDbService(ns, servicename);
        }

        public async override Task<ServiceListReply> GetServices(GetServicesRequest request, ServerCallContext context)
        {
            var ns = request.NamespaceParam;
            if (String.IsNullOrWhiteSpace(ns))
            {
                throw new ArgumentException("Namespace is missing");
            }

            if (await featureManager.IsEnabledAsync(nameof(HealthMonitoringFeatureFlags.UseMongoData)))
            {
                return await getMongoDbServices(ns);
            }
            else
            {
                return await getLiveServices(ns);
            }

        }

        private async Task<ServiceListReply> getMongoDbServices(string namespaceParam)
        {
            ServiceListReply servicelist = new ServiceListReply();
            var services = await serviceMongoRepo.FindAsync(p => p.Deleted == false && p.Namespace == namespaceParam);
            var serviceresultsummaries = await serviceCheckSummaryRepo.FindAsync(p => p.Namespace == namespaceParam);
            foreach (var item in services)
            {
                var summary = serviceresultsummaries.FirstOrDefault(p => p.NameandNamespace == item.NameandNamespace);
                var srv = Turquoise.GRPC.Converters.ServiceReplyConverter.ConvertToServiceReply(item, summary, logger);
                servicelist.Services.Add(srv);
            }
            return servicelist;
        }

        private async Task<ServiceReply> getMongoDbService(string namespaceParam, string serviceName)
        {

            var services = await serviceMongoRepo.FindAsync(p => p.Deleted == false && p.Namespace == namespaceParam && p.Name == serviceName);
            var item = services.FirstOrDefault();

            var serviceresultsummaries = await serviceCheckSummaryRepo.FindAsync(p => p.Namespace == namespaceParam && p.Name == serviceName);
            var summary = serviceresultsummaries.FirstOrDefault();
            if (summary == null) { logger.LogCritical("healthcheck summary not found " + namespaceParam + "" + serviceName); }
            var srv = ServiceReplyConverter.ConvertToServiceReply(item, summary, logger);
            return srv;
        }

        private async Task<ServiceListReply> getLiveServices(string namespaceParam)
        {
            var services = await k8sService.ServiceClient.GetAsync(namespaceParam);
            ServiceListReply servicelist = new ServiceListReply();
            var serviceresultsummaries = await serviceCheckSummaryRepo.FindAsync(p => p.Namespace == namespaceParam);
            foreach (var item in services)
            {
                var result = serviceresultsummaries.FirstOrDefault(p => p.Name == item.Metadata.Name && p.Namespace == item.Metadata.NamespaceProperty);
                var srv = ServiceReplyConverter.ConvertToServiceReply(item, result, logger);
                servicelist.Services.Add(srv);
            }
            return servicelist;
        }
        public async override Task<EventListReply> GetEvents(GetEventListRequest request, ServerCallContext context)
        {

            var ns = request.NamespaceParam;
            if (String.IsNullOrWhiteSpace(ns))
            {
                throw new ArgumentException("Namespace is missing");
            }
            var eventlist = await k8sService.EventClient.GetAsync(ns);
            var events = Turquoise.GRPC.Converters.EventReplyConverter.ConvertToEventListReply(eventlist);
            return events;
        }

        public override Task<HealthCheckStatsReply> GetHealthCheckStats(HealthCheckStatsRequest request, ServerCallContext context)
        {
            var ns = request.NamespaceParam;
            if (String.IsNullOrWhiteSpace(ns))
            {
                throw new ArgumentException("Namespace is missing");
            }

            HealthCheckStatsReply stats = new HealthCheckStatsReply();
            var res = aliveAndWellResultStats.GetResultStatWithCache(ns);


            stats.AllRunsOnToday = res.AllRunsOnToday;
            stats.AllServices = res.AllServices;
            stats.HealthyRunsOnToday = res.HealthyRunsOnToday;
            stats.SyncDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(res.syncDate);
            stats.UnhealthyRunsOnToday = res.UnhealthyRunsOnToday;

            if (res.UnhealthyServicesToday != null && res.UnhealthyServicesToday.Count > 0)
            {
                stats.UnhealthyServicesToday.AddRange(res.UnhealthyServicesToday.Select(p => new StringMessage { Value = p }));
            }

            return Task.FromResult(stats);
            //return base.GetIsAliveAndWellStatsReply(request, context);
        }

        public override Task<HealthCheckResultReply> GetLastHealthCheckResult(HealthCheckResultRequest request, ServerCallContext context)
        {
            var ns = request.NamespaceParam;
            if (String.IsNullOrWhiteSpace(ns))
            {
                throw new ArgumentException("Namespace is missing");
            }

            var serviceName = request.ServiceName;
            if (String.IsNullOrWhiteSpace(serviceName))
            {
                throw new ArgumentException("ServiceName is missing");
            }


            var builder = Builders<AliveAndWellResult>.Filter;
            var filter = builder.Eq(x => x.ServiceNamespace, ns) & builder.Eq(x => x.ServiceName, serviceName) & builder.Gt(x => x.CreationTime, DateTime.UtcNow.AddDays(-1));

            AliveAndWellResult mongores = healthCheckMongoRepo.Items.Find(filter).SortByDescending(p => p.CreationTime).FirstOrDefault(); //.OrderByDescending(p => p.CreationTime).FirstOrDefault();

            var result = new HealthCheckResultReply();
            result.Id = mongores.Id.ToString();
            result.CreationTime = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(mongores.CreationTime);
            result.ServiceUid = mongores.ServiceUid;
            result.ServiceName = mongores.ServiceName;
            result.ServiceNamespace = mongores.ServiceNamespace;
            result.Status = mongores.Status;
            result.StringResult = mongores.StringResult;
            result.CheckedUrl = mongores.CheckedUrl;
            return Task.FromResult(result);
        }


        public override Task<HealthCheckResultListReply> GetHealthCheckResultList(HealthCheckResultRequest request, ServerCallContext context)
        {
            var ns = request.NamespaceParam;
            if (String.IsNullOrWhiteSpace(ns))
            {
                throw new ArgumentException("Namespace is missing");
            }

            var serviceName = request.ServiceName;
            if (String.IsNullOrWhiteSpace(serviceName))
            {
                throw new ArgumentException("ServiceName is missing");
            }
            var reply = new HealthCheckResultListReply();

            var builder = Builders<AliveAndWellResult>.Filter;
            var filter = builder.Eq(x => x.ServiceNamespace, ns) & builder.Eq(x => x.ServiceName, serviceName) & builder.Gt(x => x.CreationTime, DateTime.UtcNow.AddDays(-1));

            var mongores = healthCheckMongoRepo.Items.Find(filter).SortByDescending(p => p.CreationTime).Limit(50).ToList(); //.OrderByDescending(p => p.CreationTime).FirstOrDefault();

            foreach (var item in mongores)
            {
                var result = new HealthCheckResultReply();
                result.Id = item.Id.ToString();
                result.CreationTime = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(item.CreationTime);
                result.ServiceUid = item.ServiceUid;
                result.ServiceName = item.ServiceName;
                result.ServiceNamespace = item.ServiceNamespace;
                result.Status = item.Status;
                result.StringResult = item.StringResult;
                result.CheckedUrl = item.CheckedUrl;
                reply.Results.Add(result);
            }
            return Task.FromResult(reply);
        }

        public override Task<NodeListReply> GetNodes(Google.Protobuf.WellKnownTypes.Empty request, ServerCallContext context)
        {
            var nodesTask = k8sService.NodeClient.GetAsync();
            var metricTask = k8sService.MetricsClient.GetNodeMetrics();
            Task.WaitAll(nodesTask, metricTask);
            logger.LogCritical(nodesTask.Result.Count.ToString());
            var replies = NodeListReplyConverter.ConvertToNodeListReply(nodesTask.Result, metricTask.Result);

            return Task.FromResult(replies);
        }

    }
}