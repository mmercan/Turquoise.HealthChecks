using AutoMapper;
using Turquoise.Models;
using k8s.Models;
using System.Collections.Generic;
using System.Linq;
using Turquoise.Models.Mongo;

namespace Turquoise.K8s
{
    public class K8sMapper : Profile
    {
        public K8sMapper()
        {

            CreateMap<V1Namespace, Turquoise.Models.Mongo.NamespaceV1>()
                .ForMember(dto => dto.Labels, map => map.MapFrom(source =>
                    source.Metadata.Labels.Select(p => new Turquoise.Models.Mongo.Label(p.Key, p.Value)).ToList()
                ))
                .ForMember(dto => dto.Name, map => map.MapFrom(source =>
                    source.Metadata.Name
                ))
                .ForMember(dto => dto.Uid, map => map.MapFrom(source =>
                    source.Metadata.Uid
                ))
                .ForMember(dto => dto.CreationTime, map => map.MapFrom(source =>
                    source.Metadata.CreationTimestamp.Value
                ))
                .ForMember(dto => dto.Status, map => map.MapFrom(source =>
                    source.Status.Phase
                ));



            CreateMap<V1Service, Turquoise.Models.Mongo.ServiceV1>()
                .ForMember(dto => dto.Labels, map => map.MapFrom(source =>
                    source.Metadata.Labels.Select(p => new Turquoise.Models.Mongo.Label(p.Key, p.Value)).ToList()
                ))
                .ForMember(dto => dto.LabelSelector, map => map.MapFrom(source =>
                    source.Spec.Selector.Select(p => new Turquoise.Models.Mongo.Label(p.Key, p.Value)).ToList()
                ))
                .ForMember(dto => dto.Annotations, map => map.MapFrom(source =>
                   source.Metadata.Annotations.Select(p => new Turquoise.Models.Mongo.Label(p.Key, p.Value)).ToList()
               ))
                .ForMember(dto => dto.Name, map => map.MapFrom(source =>
                    source.Metadata.Name
                ))
                .ForMember(dto => dto.Namespace, map => map.MapFrom(source =>
                    source.Metadata.Namespace()
                ))
                .ForMember(dto => dto.Uid, map => map.MapFrom(source =>
                    source.Metadata.Uid
                ))
                .ForMember(dto => dto.CreationTime, map => map.MapFrom(source =>
                    source.Metadata.CreationTimestamp.Value
                ))
                .ForMember(dto => dto.Type, map => map.MapFrom(source =>
                    source.Spec.Type
                ))
                .ForMember(dto => dto.ClusterIP, map => map.MapFrom(source =>
                    source.Spec.ClusterIP
                ))
                .ForMember(dto => dto.InternalEndpoints, map => map.MapFrom(source =>
                   source.Spec.Ports.Select(p => source.Metadata.Name + "." + source.Metadata.Namespace() + ":" + p.Port.ToString()).ToList()
               ))
                .ForMember(dto => dto.ExternalEndpoints, map => map.MapFrom(source =>
                   source.Spec.Ports.Select(p => source.Status.LoadBalancer.Ingress.FirstOrDefault() == null ? "" : source.Status.LoadBalancer.Ingress.FirstOrDefault().Ip.ToString() + ":" + p.Port.ToString()).ToList()
               ))
                .ForMember(dto => dto.SessionAffinity, map => map.MapFrom(source =>
                   source.Spec.SessionAffinity
               ))
                .ForMember(dto => dto.ServiceApiVersion, map => map.MapFrom(source =>
                   source.ApiVersion
               ))
                .ForMember(dto => dto.ServiceResourceVersion, map => map.MapFrom(source =>
                   source.ResourceVersion()
               ))
                .ForMember(dto => dto.LabelSelectorString, map => map.MapFrom(source =>
                       string.Join(",", source.Spec.Selector.OrderBy(p => p.Key).Select(p => p.Key + "=" + p.Value).ToArray())
               ));



            CreateMap<V1Deployment, DeploymentV1>()
                .ForMember(dto => dto.Name, map => map.MapFrom(source =>
                        source.Metadata.Name
                    ))
                    .ForMember(dto => dto.Namespace, map => map.MapFrom(source =>
                        source.Metadata.Namespace()
                   ))
                    .ForMember(dto => dto.Kind, map => map.MapFrom(source =>
                        source.Kind
                   ));

            CreateMap<V1DeploymentStatus, DeploymentStatusV1>();
            CreateMap<V1DeploymentCondition, DeploymentConditionV1>();

            CreateMap<V1ObjectMeta, MetadataV1>()
                .ForMember(dto => dto.Annotations, map => map.MapFrom(source =>
                    source.Annotations.Select(p => new Turquoise.Models.Mongo.Label(p.Key, p.Value)).ToList()
                ))
                .ForMember(dto => dto.CreationTime, map => map.MapFrom(source =>
                    source.CreationTimestamp
               ))
                .ForMember(dto => dto.Generation, map => map.MapFrom(source =>
                    source.Generation

               ))
                .ForMember(dto => dto.Labels, map => map.MapFrom(source =>
                    source.Labels.Select(p => new Turquoise.Models.Mongo.Label(p.Key, p.Value)).ToList()
               ))
                .ForMember(dto => dto.Name, map => map.MapFrom(source =>
                    source.Name
               ))
                .ForMember(dto => dto.Namespace, map => map.MapFrom(source =>
                    source.Namespace()
               ))
                .ForMember(dto => dto.ResourceVersion, map => map.MapFrom(source =>
                    source.ResourceVersion
               ))
                .ForMember(dto => dto.Uid, map => map.MapFrom(source =>
                    source.Uid
               ));

            CreateMap<V1DeploymentSpec, DeploymentSpecV1>()
                .ForMember(dto => dto.ProgressDeadlineSeconds, map => map.MapFrom(source =>
                    source.ProgressDeadlineSeconds
               ))
                 .ForMember(dto => dto.Replicas, map => map.MapFrom(source =>
                    source.Replicas
               ))
                 .ForMember(dto => dto.RevisionHistoryLimit, map => map.MapFrom(source =>
                    source.RevisionHistoryLimit
               ))
                 .ForMember(dto => dto.Selector, map => map.MapFrom(source =>
                    source.Selector.MatchLabels.Select(p => new Turquoise.Models.Mongo.Label(p.Key, p.Value)).ToList()
               ))
                 .ForMember(dto => dto.Template, map => map.MapFrom(source =>
                    source.Template
               ))
                .ForMember(dto => dto.SelectorString, map => map.MapFrom(source =>
                       string.Join(",", source.Selector.MatchLabels.OrderBy(p => p.Key).Select(p => p.Key + "=" + p.Value).ToArray())
               ));

            CreateMap<V1OwnerReference, OwnerReferenceV1>();

            CreateMap<V1PodTemplateSpec, PodTemplateSpecV1>();

            CreateMap<V1PodSpec, PodSpecV1>()
                .ForMember(dto => dto.PreemptionPolicy, map => map.MapFrom(source =>
                    source.PreemptionPolicy
               ))
                .ForMember(dto => dto.Priority, map => map.MapFrom(source =>
                    source.Priority
               ))
                .ForMember(dto => dto.PriorityClassName, map => map.MapFrom(source =>
                    source.PriorityClassName
               ))
                .ForMember(dto => dto.RestartPolicy, map => map.MapFrom(source =>
                    source.RestartPolicy
               ))
                .ForMember(dto => dto.RuntimeClassName, map => map.MapFrom(source =>
                    source.RuntimeClassName
               ))
                .ForMember(dto => dto.SchedulerName, map => map.MapFrom(source =>
                    source.SchedulerName
               ))
                .ForMember(dto => dto.ServiceAccount, map => map.MapFrom(source =>
                    source.ServiceAccount
               ))
                .ForMember(dto => dto.ServiceAccountName, map => map.MapFrom(source =>
                    source.ServiceAccountName
               ))
                .ForMember(dto => dto.ShareProcessNamespace, map => map.MapFrom(source =>
                    source.ShareProcessNamespace
               ))
                .ForMember(dto => dto.Subdomain, map => map.MapFrom(source =>
                    source.ShareProcessNamespace
               ))
                .ForMember(dto => dto.TerminationGracePeriodSeconds, map => map.MapFrom(source =>
                    source.TerminationGracePeriodSeconds
               ))
                .ForMember(dto => dto.NodeSelector, map => map.MapFrom(source =>
                    source.NodeSelector.Select(p => new Turquoise.Models.Mongo.Label(p.Key, p.Value)).ToList()
               ))
                .ForMember(dto => dto.NodeName, map => map.MapFrom(source =>
                    source.NodeName
               ))
                .ForMember(dto => dto.ActiveDeadlineSeconds, map => map.MapFrom(source =>
                    source.ActiveDeadlineSeconds
               ))
                .ForMember(dto => dto.AutomountServiceAccountToken, map => map.MapFrom(source =>
                    source.AutomountServiceAccountToken
               ))
               .ForMember(dto => dto.Containers, map => map.MapFrom(source =>
                    source.Containers
               ));



            CreateMap<V1Container, ContainerV1>();

            CreateMap<V1Pod, PodV1>();

            CreateMap<V1ContainerPort, ContainerPortV1>();


            CreateMap<V1Probe, ProbeV1>()
                 .ForMember(dto => dto.Exec, map => map.MapFrom(source =>
                   source.Exec.Command
               ))
                .ForMember(dto => dto.FailureThreshold, map => map.MapFrom(source =>
                   source.FailureThreshold
               ))
                .ForMember(dto => dto.InitialDelaySeconds, map => map.MapFrom(source =>
                   source.InitialDelaySeconds
               ))
                .ForMember(dto => dto.HttpGetHost, map => map.MapFrom(source =>
                source.HttpGet == null ? default : source.HttpGet.Host
               ))
                .ForMember(dto => dto.HttpGetHttpHeaders, map => map.MapFrom(source =>
                      source.HttpGet == null ? default : source.HttpGet.HttpHeaders.Select(p => new HttpHeaderV1 { Name = p.Name, Value = p.Value })
               ))
                .ForMember(dto => dto.HttpGetPath, map => map.MapFrom(source =>
                    source.HttpGet == null ? default : source.HttpGet.Path
               ))
                .ForMember(dto => dto.HttpGetPort, map => map.MapFrom(source =>
                   source.HttpGet == null ? default : source.HttpGet.Port.Value
              ))
                .ForMember(dto => dto.HttpGetScheme, map => map.MapFrom(source =>
                  source.HttpGet == null ? default : source.HttpGet.Scheme
               ));

        }
    }
}