using System;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Turquoise.Api.HealthMonitoring.Helpers;
using Turquoise.Api.HealthMonitoring.Models;
using Turquoise.Common.Mongo;
using Turquoise.K8s.Services;
using Turquoise.Models.Mongo;

namespace Turquoise.Api.HealthMonitoring.GRPCServices
{
    //  [Authorize]
    public class NamespaceGRPCService : NamespaceService.NamespaceServiceBase
    {
        private ILogger<NamespaceGRPCService> _logger;
        private K8sService k8sService;
        private MangoBaseRepo<ServiceV1> serviceMongoRepo;
        private IFeatureManager featureManager;
        private MongoAliveAndWellResultStats aliveAndWellResultStats;

        public NamespaceGRPCService(
            ILogger<NamespaceGRPCService> logger,
            K8sService k8sService,
            MangoBaseRepo<ServiceV1> serviceMongoRepo,
            IFeatureManager featureManager,
            MongoAliveAndWellResultStats aliveAndWellResultStats
            )
        {
            _logger = logger;
            this.k8sService = k8sService;
            this.serviceMongoRepo = serviceMongoRepo;
            this.featureManager = featureManager;
            this.aliveAndWellResultStats = aliveAndWellResultStats;
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

            // DeploymentListReply deploy = new DeploymentListReply();
            // foreach (var item in deployments)
            // {
            //     var dep = new DeploymentReply();
            //     dep.Name = item.Metadata.Name;
            //     dep.Image = item.Spec.Template.Spec.Containers.FirstOrDefault().Image;
            //     dep.Status = item.Status.Conditions.FirstOrDefault().Status.ToString();
            //     dep.Labels.AddRange(item.Metadata.Labels.Select(lab => { return new Pair { Key = lab.Key, Value = lab.Value }; }));
            //     deploy.Deployments.Add(dep);
            // }
            // return deploy;

            // return base.GetServices(request, context);
        }


        private async Task<ServiceListReply> getMongoDbServices(string namespaceParam)
        {
            ServiceListReply servicelist = new ServiceListReply();
            var services = await serviceMongoRepo.FindAsync(p => p.Deleted == false && p.Namespace == namespaceParam);
            foreach (var item in services)
            {
                var srv = new ServiceReply();
                servicelist.Services.Add(srv);



                srv.NameandNamespace = item.NameandNamespace;
                srv.Uid = item.Uid;
                srv.Name = item.Name;
                srv.Namespace = item.Namespace;

                if (item.Labels != null && item.Labels.Count > 0)
                {
                    srv.Labels.AddRange(item.Labels.Select(p => new Pair { Key = p.Key, Value = p.Value }));
                }
                srv.CreationTime = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(item.CreationTime);


                if (item.LabelSelector != null && item.LabelSelector.Count > 0)
                {
                    srv.LabelSelector.AddRange(item.LabelSelector.Select(p => new Pair { Key = p.Key, Value = p.Value }));
                }


                if (item.Annotations != null && item.Annotations.Count > 0)
                {
                    srv.Annotations.AddRange(item.Annotations.Select(p => new Pair { Key = p.Key, Value = p.Value }));
                }

                srv.ServiceType = item.Type;
                srv.SessionAffinity = item.SessionAffinity;
                srv.ClusterIP = item.ClusterIP;
                if (item.InternalEndpoints != null && item.InternalEndpoints.Count > 0)
                {
                    srv.InternalEndpoints.AddRange(item.InternalEndpoints.Select(p => new StringMessage { Value = p }));
                }
                if (item.ExternalEndpoints != null && item.ExternalEndpoints.Count > 0)
                {
                    srv.ExternalEndpoints.AddRange(item.ExternalEndpoints.Select(p => new StringMessage { Value = p }));
                }
                if (item.IngressUrl != null)
                {
                    srv.IngressUrl = item.IngressUrl;
                }
                if (item.VirtualServiceUrl != null)
                {
                    srv.VirtualServiceUrl = item.VirtualServiceUrl;
                }
                if (item.LatestSyncDateUTC != DateTime.MinValue)
                {
                    srv.LatestSyncDateUTC = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(item.LatestSyncDateUTC);
                }
                srv.Deleted = item.Deleted;
                if (item.HealthIsalive != null)
                {
                    srv.HealthIsalive = item.HealthIsalive;
                }
                if (item.HealthIsaliveSyncDateUTC != DateTime.MinValue)
                {
                    srv.HealthIsaliveSyncDateUTC = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(item.HealthIsaliveSyncDateUTC);
                }
                if (item.HealthIsaliveAndWell != null)
                {
                    _logger.LogCritical("HealthIsaliveAndWell :" + item.HealthIsaliveAndWell);
                    srv.HealthIsaliveAndWell = item.HealthIsaliveAndWell;
                }

                if (item.HealthIsaliveAndWellSyncDateUTC != DateTime.MinValue)
                {
                    srv.HealthIsaliveAndWellSyncDateUTC = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(item.HealthIsaliveAndWellSyncDateUTC);
                }

            }
            return servicelist;
        }

        private async Task<ServiceListReply> getLiveServices(string namespaceParam)
        {
            var services = await k8sService.GetServices(namespaceParam);
            ServiceListReply servicelist = new ServiceListReply();

            foreach (var item in services)
            {
                var srv = new ServiceReply();
                servicelist.Services.Add(srv);
                srv.Name = item.Metadata.Name;
                srv.Uid = item.Metadata.Uid;
                srv.Namespace = item.Metadata.NamespaceProperty;

                if (item.Metadata.CreationTimestamp.HasValue)
                {
                    srv.CreationTime = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(item.Metadata.CreationTimestamp.Value);
                }


                if (item.Metadata.Annotations != null && item.Metadata.Annotations.Count > 0)
                {
                    srv.Annotations.AddRange(
                        item.Metadata.Annotations.Select(p => new Pair { Key = p.Key, Value = p.Value })
                    );
                }

                if (item.Metadata.Labels != null && item.Metadata.Labels.Count > 0)
                {
                    srv.Labels.AddRange(
                        item.Metadata.Labels.Select(p => new Pair { Key = p.Key, Value = p.Value })
                    );
                }

                if (item.Spec.Selector != null && item.Spec.Selector.Count > 0)
                {
                    srv.LabelSelector.AddRange(
                        item.Spec.Selector.Select(p => new Pair { Key = p.Key, Value = p.Value })
                    );
                }

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
            EventListReply events = new EventListReply();
            var eventlist = await k8sService.GetEventsAsync(ns);
            foreach (var item in eventlist)
            {
                var ev = new EventReply();
                events.Events.Add(ev);

                ev.Message = item.Message;

                ev.Name = item.Metadata.Name;
                ev.Message = item.Message;

                if (item.Count.HasValue)
                {
                    ev.Count = item.Count.Value;
                }
                ev.Reason = item.Reason;
                ev.Type = item.Type;
                if (item.FirstTimestamp.HasValue && item.FirstTimestamp.Value != DateTime.MinValue)
                {
                    ev.FirstTimestamp = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(item.FirstTimestamp.Value);
                }


                if (item.LastTimestamp.HasValue && item.LastTimestamp.Value != DateTime.MinValue)
                {
                    ev.LastTimestamp = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(item.LastTimestamp.Value);
                }

                if (item.InvolvedObject != null)
                {
                    if (item.InvolvedObject.Name != null)
                    {
                        ev.InvolvedObjectName = item.InvolvedObject.Name;
                    }

                    if (item.InvolvedObject.Kind != null)
                    {
                        ev.InvolvedObjectKind = item.InvolvedObject.Kind;
                    }
                    if (item.InvolvedObject.NamespaceProperty != null)
                    {
                        ev.InvolvedObjectNamespace = item.InvolvedObject.NamespaceProperty;
                    }
                    if (item.InvolvedObject.Uid != null)
                    {
                        ev.InvolvedObjectUid = item.InvolvedObject.Uid;
                    }
                }

                //                 ev.Metadata
            }
            return events;


        }


        public override Task<IsAliveAndWellStatsReply> GetIsAliveAndWellStatsReply(IsAliveAndWellStatsRequest request, ServerCallContext context)
        {
            var ns = request.NamespaceParam;
            if (String.IsNullOrWhiteSpace(ns))
            {
                throw new ArgumentException("Namespace is missing");
            }

            IsAliveAndWellStatsReply stats = new IsAliveAndWellStatsReply();
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

    }
}