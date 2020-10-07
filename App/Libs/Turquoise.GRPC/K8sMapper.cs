using AutoMapper;
using Turquoise.Models;
using k8s.Models;
using System.Collections.Generic;
using System.Linq;
using Turquoise.Models.Mongo;
using Turquoise.GRPC.GRPCServices;

namespace Turquoise.K8s
{
    public class K8sMapper : Profile
    {
        public K8sMapper()
        {


            // CreateMap<DeploymentV1, DeploymentReply>()
            //     .ForMember(dto => dto.Name, map => map.MapFrom(source =>
            //          source.Metadata.Name
            //       ))
            //      .ForMember(dto => dto.Labels, map => map.MapFrom(source =>
            //          source.Metadata.Labels.Select(p => new Turquoise.GRPC.GRPCServices.Pair { Key = p.Key, Value = p.Value }).ToList()
            //      ))
            //     .ForMember(dto => dto.Annotations, map => map.MapFrom(source =>
            //         source.Metadata.Annotations.Select(p => new Turquoise.Models.Mongo.Label(p.Key, p.Value)).ToList()
            //     ))
            // //     .ForMember(dto => dto.Image, map => map.MapFrom(source =>
            // //          source.Metadata.Name
            // //       ))
            // //     .ForMember(dto => dto.Status, map => map.MapFrom(source =>
            // //          source.Metadata.Name
            // //       ));



        }
    }
}