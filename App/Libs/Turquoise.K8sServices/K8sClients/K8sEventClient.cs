using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using Turquoise.Models.Mongo;

namespace Turquoise.K8sServices.K8sClients
{
    public class K8sEventClient : IK8sClient<V1Event>
    {
        private readonly Kubernetes client;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public K8sEventClient(Kubernetes kubernetesClient, ILogger logger, IMapper mapper)
        {
            this.client = kubernetesClient;
            this.logger = logger;
            this.mapper = mapper;
        }

        public IList<V1Event> Get(string nameSpace)
        {
            var events = client.ListNamespacedEvent(nameSpace);
            return events.Items;

        }

        public async Task<IList<V1Event>> GetAsync(string nameSpace)
        {
            var events = await client.ListNamespacedEventAsync(nameSpace);
            return events.Items;
        }

        public async Task<IList<V1Event>> GetAllAsync()
        {
            var events = await client.ListEventForAllNamespacesAsync();
            return events.Items;
        }

        public async Task<V1Event> FindEventAsync(string namespaceParam, V1Service service, string message)
        {
            var events = await client.ListNamespacedEventAsync(namespaceParam);

            var res = events.Items.FirstOrDefault(p => p.InvolvedObject.Uid == service.Uid() && p.Message == message);
            return res;
        }


        public async Task<V1Event> FindEventAsync(string namespaceParam, string serviceUid, string message)
        {
            var events = await client.ListNamespacedEventAsync(namespaceParam);

            var res = events.Items.FirstOrDefault(p => p.InvolvedObject.Uid == serviceUid && p.Message == message);
            return res;
        }


        public async Task<V1Event> CountUpOrCreateEvent(string namespaceParam, V1Service service, string message,
            string reason = "Unhealthy",
            string type = "Warning")
        {
            V1Event v1event = null;
            v1event = await FindEventAsync(namespaceParam, service, message);
            if (v1event != null)
            {
                var newcount = v1event.Count + 1;
                logger.LogCritical("Patch existed Event : Counted : " + newcount.ToString());
                var name = v1event.Metadata.Name;

                var patch = new JsonPatchDocument<V1Event>();
                patch.Replace(e => e.Count, newcount);
                patch.Replace(e => e.LastTimestamp, DateTime.UtcNow);
                v1event = await client.PatchNamespacedEventAsync(new V1Patch(patch), name, namespaceParam);
            }
            else
            {
                logger.LogCritical("Create a new Event");
                v1event = await CreateNewEventAsync(
                    namespaceParam,
                    service.Name(),
                    service.Uid(),
                    service.Namespace(),
                    service.ApiVersion,
                    service.ResourceVersion(),
                    message,
                    reason,
                    type
                    );
            }
            return v1event;
        }



        public async Task<V1Event> CountUpOrCreateEvent(
            string namespaceParam,
            string serviceName,
            string serviceUid,
            string serviceNamespace,
            string serviceApiVersion,
            string serviceResourceVersion,
            string message,
            string reason = "Unhealthy",
            string type = "Warning"
            )
        {
            V1Event v1event = null;
            v1event = await FindEventAsync(namespaceParam, serviceUid, message);
            if (v1event != null)
            {
                var newcount = v1event.Count + 1;
                logger.LogCritical("Patch existed Event : Counted : " + newcount.ToString());
                var name = v1event.Metadata.Name;

                var patch = new JsonPatchDocument<V1Event>();
                patch.Replace(e => e.Count, newcount);
                patch.Replace(e => e.LastTimestamp, DateTime.UtcNow);
                v1event = await client.PatchNamespacedEventAsync(new V1Patch(patch), name, namespaceParam);
            }
            else
            {
                logger.LogCritical("Create a new Event");
                v1event = await CreateNewEventAsync(
                    namespaceParam,
                    serviceName,
                    serviceUid,
                    serviceNamespace,
                    serviceApiVersion,
                    serviceResourceVersion,
                    message,
                    reason,
                    type
                    );
            }
            return v1event;
        }


        public async Task<V1Event> CreateNewEventAsync(
            string namespaceParam,
            string serviceName,
            string serviceUid,
            string serviceNamespace,
            string serviceApiVersion,
            string serviceResourceVersion,
            string message,
            string reason,
            string type
            )
        {

            V1ObjectReference refer = new V1ObjectReference();
            refer.ApiVersion = serviceApiVersion;
            refer.Kind = "Service";
            refer.Name = serviceName;
            refer.NamespaceProperty = serviceNamespace;
            refer.Uid = serviceUid;
            refer.ResourceVersion = serviceResourceVersion;
            refer.FieldPath = "metadata.annotations";

            V1Event newEvent = new V1Event { };
            newEvent.FirstTimestamp = DateTime.UtcNow;
            newEvent.LastTimestamp = DateTime.UtcNow;
            newEvent.InvolvedObject = refer;
            newEvent.Count = 1;
            newEvent.LastTimestamp = DateTime.UtcNow;
            newEvent.Message = message;
            newEvent.Metadata = new V1ObjectMeta();
            newEvent.Metadata.CreationTimestamp = DateTime.UtcNow;
            newEvent.Metadata.NamespaceProperty = namespaceParam;

            newEvent.Metadata.Name = serviceName + Guid.NewGuid().ToString();

            newEvent.Reason = reason;
            newEvent.Type = type;

            var savedevent = await this.client.CreateNamespacedEventAsync(newEvent, namespaceParam);

            logger.LogCritical("Saved UID :" + savedevent.Uid());
            return savedevent;

        }
    }
}