using AutoMapper;
using Turquoise.Models;
using k8s.Models;

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

            // .ForMember(dto => dto.Metadata, map => map.)
            //.ForMember(                dto => dto.Currency, map => map.MapFrom(source => new Currency
            //{
            //    Code = source.CurrencyCode,
            //    Value = source.CurrencyValue.ToString("0.00")
            //}));
            // All other mappings goes here
        }
    }
}