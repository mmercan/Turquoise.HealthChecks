using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using Turquoise.Models.Mongo;
using Turquoise.GRPC.GRPCServices;
using k8s.Models;

namespace Turquoise.GRPC.Converters
{
    public static class ServiceReplyConverter
    {


        public static ServiceReply ConvertToServiceReply(ServiceV1 service, ServiceHealthCheckResultSummary summary, ILogger logger)
        {
            var srv = new ServiceReply();


            srv.NameandNamespace = service.NameandNamespace;
            srv.Uid = service.Uid;
            srv.Name = service.Name;
            srv.Namespace = service.Namespace;

            if (service.Labels != null && service.Labels.Count > 0)
            {
                srv.Labels.AddRange(service.Labels.Select(p => new Pair { Key = p.Key, Value = p.Value }));
            }
            srv.CreationTime = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(service.CreationTime);


            if (service.LabelSelector != null && service.LabelSelector.Count > 0)
            {
                srv.LabelSelector.AddRange(service.LabelSelector.Select(p => new Pair { Key = p.Key, Value = p.Value }));
            }


            if (service.Annotations != null && service.Annotations.Count > 0)
            {
                srv.Annotations.AddRange(service.Annotations.Select(p => new Pair { Key = p.Key, Value = p.Value }));
            }

            srv.ServiceType = service.Type;
            srv.SessionAffinity = service.SessionAffinity;
            srv.ClusterIP = service.ClusterIP;
            if (service.InternalEndpoints != null && service.InternalEndpoints.Count > 0)
            {
                srv.InternalEndpoints.AddRange(service.InternalEndpoints.Select(p => new StringMessage { Value = p }));
            }
            if (service.ExternalEndpoints != null && service.ExternalEndpoints.Count > 0)
            {
                srv.ExternalEndpoints.AddRange(service.ExternalEndpoints.Select(p => new StringMessage { Value = p }));
            }
            if (service.IngressUrl != null)
            {
                srv.IngressUrl = service.IngressUrl;
            }
            if (service.VirtualServiceUrl != null)
            {
                srv.VirtualServiceUrl = service.VirtualServiceUrl;
            }
            if (service.LatestSyncDateUTC != DateTime.MinValue)
            {
                srv.LatestSyncDateUTC = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(service.LatestSyncDateUTC);
            }
            srv.Deleted = service.Deleted;

            if (summary != null)
            {
                if (summary.HealthIsalive != null)
                {
                    srv.HealthIsalive = summary.HealthIsalive;
                }
                if (summary.HealthIsaliveSyncDateUTC != DateTime.MinValue)
                {
                    srv.HealthIsaliveSyncDateUTC = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(summary.HealthIsaliveSyncDateUTC);
                }
                if (summary.HealthIsaliveAndWell != null)
                {
                    logger.LogCritical("HealthIsaliveAndWell :" + summary.HealthIsaliveAndWell);
                    srv.HealthIsaliveAndWell = summary.HealthIsaliveAndWell;
                }

                if (summary.HealthIsaliveAndWellSyncDateUTC != DateTime.MinValue)
                {
                    srv.HealthIsaliveAndWellSyncDateUTC = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(summary.HealthIsaliveAndWellSyncDateUTC);
                }
            }

            // if (item.LivenessProbe != null)
            // {
            //     srv.LivenessProbe = item.LivenessProbe;
            // }
            // if (item.ReadinessProbe != null)
            // {
            //     srv.ReadinessProbe = item.ReadinessProbe;
            // }
            // if (item.StartupProbe != null )
            // {
            //     srv.StartupProbe = item.StartupProbe;
            // }
            if (service.CronDescription != null)
            {
                srv.CronDescription = service.CronDescription;
            }
            if (service.CronTab != null)
            {
                srv.CronTab = service.CronTab;
            }
            if (service.CronTabException != null)
            {
                srv.CronTabException = service.CronTabException;
            }

            return srv;
        }


        public static ServiceReply ConvertToServiceReply(V1Service service, ServiceHealthCheckResultSummary summary, ILogger logger)
        {
            var srv = new ServiceReply();

            srv.Name = service.Metadata.Name;
            srv.Uid = service.Metadata.Uid;
            srv.Namespace = service.Metadata.NamespaceProperty;

            if (service.Metadata.CreationTimestamp.HasValue)
            {
                srv.CreationTime = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(service.Metadata.CreationTimestamp.Value);
            }


            if (service.Metadata.Annotations != null && service.Metadata.Annotations.Count > 0)
            {
                srv.Annotations.AddRange(
                    service.Metadata.Annotations.Select(p => new Pair { Key = p.Key, Value = p.Value })
                );
            }

            if (service.Metadata.Labels != null && service.Metadata.Labels.Count > 0)
            {
                srv.Labels.AddRange(
                    service.Metadata.Labels.Select(p => new Pair { Key = p.Key, Value = p.Value })
                );
            }

            if (service.Spec.Selector != null && service.Spec.Selector.Count > 0)
            {
                srv.LabelSelector.AddRange(
                    service.Spec.Selector.Select(p => new Pair { Key = p.Key, Value = p.Value })
                );
            }


            return srv;
        }
    }
}