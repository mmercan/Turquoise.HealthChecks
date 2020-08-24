using AutoMapper;
using Turquoise.Models;
using k8s.Models;
using System.Collections.Generic;
using System.Linq;
using Turquoise.Models.Mongo;
using Turquoise.Common.Scheduler;

namespace Turquoise.Scheduler
{
    public class K8sMapper : Profile
    {
        public K8sMapper()
        {

            //   CreateMap<V1Namespace, IHealthCheckScheduledTask<Turquoise.Models.Mongo.ServiceV1>>();

            //     CreateMap<V1Namespace, Turquoise.Models.Mongo.NamespaceV1>()
            //     .ForMember(dto => dto.Labels, map => map.MapFrom(source =>
            //         source.Metadata.Labels.Select(p => new Turquoise.Models.Mongo.Label(p.Key, p.Value)).ToList()
            //     ))
            //     .ForMember(dto => dto.Name, map => map.MapFrom(source =>
            //         source.Metadata.Name
            //     ))
            //     .ForMember(dto => dto.Uid, map => map.MapFrom(source =>
            //         source.Metadata.Uid
            //     ))
            //     .ForMember(dto => dto.CreationTime, map => map.MapFrom(source =>
            //         source.Metadata.CreationTimestamp.Value
            //     ))
            //     .ForMember(dto => dto.Status, map => map.MapFrom(source =>
            //         source.Status.Phase
            //     ));



            //     CreateMap<V1Service, Turquoise.Models.Mongo.ServiceV1>()
            //     .ForMember(dto => dto.Labels, map => map.MapFrom(source =>
            //         source.Metadata.Labels.Select(p => new Turquoise.Models.Mongo.Label(p.Key, p.Value)).ToList()
            //     ))
            //     .ForMember(dto => dto.LabelSelector, map => map.MapFrom(source =>
            //         source.Spec.Selector.Select(p => new Turquoise.Models.Mongo.Label(p.Key, p.Value)).ToList()
            //     ))
            //     .ForMember(dto => dto.Annotations, map => map.MapFrom(source =>
            //        source.Metadata.Annotations.Select(p => new Turquoise.Models.Mongo.Label(p.Key, p.Value)).ToList()
            //    ))
            //     .ForMember(dto => dto.Name, map => map.MapFrom(source =>
            //         source.Metadata.Name
            //     ))
            //     .ForMember(dto => dto.Namespace, map => map.MapFrom(source =>
            //         source.Metadata.Namespace()
            //     ))
            //     .ForMember(dto => dto.Uid, map => map.MapFrom(source =>
            //         source.Metadata.Uid
            //     ))
            //     .ForMember(dto => dto.CreationTime, map => map.MapFrom(source =>
            //         source.Metadata.CreationTimestamp.Value
            //     ))
            //     .ForMember(dto => dto.Type, map => map.MapFrom(source =>
            //         source.Spec.Type
            //     ))
            //     .ForMember(dto => dto.ClusterIP, map => map.MapFrom(source =>
            //         source.Spec.ClusterIP
            //     ))
            //     .ForMember(dto => dto.InternalEndpoints, map => map.MapFrom(source =>
            //        source.Spec.Ports.Select(p => source.Metadata.Name + "." + source.Metadata.Namespace() + ":" + p.Port.ToString()).ToList()
            //    ))
            //     .ForMember(dto => dto.ExternalEndpoints, map => map.MapFrom(source =>
            //        source.Spec.Ports.Select(p => source.Status.LoadBalancer.Ingress.FirstOrDefault() == null ? "" : source.Status.LoadBalancer.Ingress.FirstOrDefault().Ip.ToString() + ":" + p.Port.ToString()).ToList()
            //    ))
            //     .ForMember(dto => dto.SessionAffinity, map => map.MapFrom(source =>
            //        source.Spec.SessionAffinity
            //    ));




        }
    }
}