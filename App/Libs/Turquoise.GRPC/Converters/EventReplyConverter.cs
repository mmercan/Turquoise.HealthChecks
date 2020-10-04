using System;
using System.Collections.Generic;
using System.Linq;
using k8s.Models;
using Microsoft.Extensions.Logging;
using Turquoise.Models.Mongo;
using Turquoise.GRPC.GRPCServices;

namespace Turquoise.GRPC.Converters
{
    public static class EventReplyConverter
    {


        public static EventListReply ConvertToEventListReply(IList<V1Event> eventlist)
        {
            EventListReply events = new EventListReply();
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
            }
            return events;
        }

    }
}