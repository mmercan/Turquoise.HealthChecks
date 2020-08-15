using AutoMapper;
using Turquoise.Models;
using k8s.Models;
using System.Collections.Generic;
using System.Linq;

namespace Turquoise.K8s
{
    public class K8sMapper : Profile
    {
        public K8sMapper()
        {
            CreateMap<V1Deployment, Deployment>();
            CreateMap<V1DeploymentCondition, DeploymentCondition>();
            CreateMap<V1DeploymentStatus, DeploymentStatus>();
            CreateMap<V1ManagedFieldsEntry, ManagedFieldsEntry>();

            CreateMap<V1ObjectMeta, ObjectMeta>();
            CreateMap<V1OwnerReference, OwnerReference>();

            CreateMap<V1DeploymentSpec, DeploymentSpec>();
            // .ForMember(dto => dto.Metadata, map => map.)
            //.ForMember(                dto => dto.Currency, map => map.MapFrom(source => new Currency
            //{
            //    Code = source.CurrencyCode,
            //    Value = source.CurrencyValue.ToString("0.00")
            //}));
            // All other mappings goes here

            CreateMap<V1Namespace, Turquoise.Models.Mongo.NamespaceV1>()
            .ForMember(dto => dto.Labels, map => map.MapFrom(source =>
                source.Metadata.Labels.Select(p => new Turquoise.Models.Mongo.Label(p.Key, p.Value)).ToList()
            ))
            .ForMember(dto => dto.Name, map => map.MapFrom(source =>
                source.Metadata.Name
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

        }
    }
}