using System.Collections.Generic;
using System.Linq;
using Google.Protobuf.WellKnownTypes;
using k8s.Models;
using Turquoise.GRPC.GRPCServices;

namespace Turquoise.GRPC.Converters
{
    public static class NodeListReplyConverter
    {
        public static NodeListReply ConvertToEventListReply(IList<V1Node> nodelist)
        {
            NodeListReply reply = new NodeListReply();
            //  reply.Nodes.AddRange

            // reply.Nodes.
            foreach (var item in nodelist)
            {
                NodeReply node = new NodeReply();
                reply.Nodes.Add(node);

                node.Name = item.Name();
                node.Uid = item.Uid();
                node.ProviderID = item.Spec.ProviderID;
                node.Labels.AddRange(item.Metadata.Labels.Select(p => new Pair { Key = p.Key, Value = p.Value }));
                node.Annotations.AddRange(item.Metadata.Annotations.Select(p => new Pair { Key = p.Key, Value = p.Value }));
                //string status = 6;

                node.NodeInfo = new NodeInfo
                {
                    Architecture = item.Status.NodeInfo.Architecture,
                    BootID = item.Status.NodeInfo.BootID,
                    ContainerRuntimeVersion = item.Status.NodeInfo.ContainerRuntimeVersion,
                    KernelVersion = item.Status.NodeInfo.KernelVersion,
                    KubeProxyVersion = item.Status.NodeInfo.KubeProxyVersion,
                    KubeletVersion = item.Status.NodeInfo.KubeletVersion,
                    MachineID = item.Status.NodeInfo.MachineID,
                    OperatingSystem = item.Status.NodeInfo.OperatingSystem,
                    OsImage = item.Status.NodeInfo.OsImage,
                    SystemUUID = item.Status.NodeInfo.SystemUUID,
                };

                node.Allocatables.AddRange(item.Status.Allocatable.Select(alloc => new Mertic { Format = alloc.Key, Key = alloc.Key, Value = alloc.Value.CanonicalizeString() }));
                node.Capacities.AddRange(item.Status.Capacity.Select(cap => new Mertic { Format = cap.Key, Key = cap.Key, Value = cap.Value.CanonicalizeString() }));
                node.Conditions.AddRange(item.Status.Conditions.Select(p => new NodeReplyCondition
                {
                    LastHeartbeatTime = Timestamp.FromDateTime(p.LastHeartbeatTime.Value),
                    LastTransitionTime = Timestamp.FromDateTime(p.LastTransitionTime.Value),
                    Message = p.Message,
                    Reason = p.Reason,
                    Status = p.Status,
                    Type = p.Type
                }));

                node.Images.AddRange(item.Status.Images.Select(p => new NodeReplyImage { Name = p.Names.FirstOrDefault(), Size = p.SizeBytes.ToString() }));
            }

            return reply;
        }

    }
}