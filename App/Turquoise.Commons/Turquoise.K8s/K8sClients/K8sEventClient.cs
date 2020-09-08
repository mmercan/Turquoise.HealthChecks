using System;
using System.Linq;
using System.Threading.Tasks;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;

namespace Turquoise.K8s.K8sClients
{
    public class K8sEventClient
    {
        private Kubernetes client;
        private ILogger logger;

        public K8sEventClient(Kubernetes kubernetesClient, ILogger logger)
        {
            this.client = kubernetesClient;
            this.logger = logger;
        }
        public async Task<V1Event> FindEventAsync(string namespaceParam, V1Service service, string message)
        {
            var events = await client.ListNamespacedEventAsync(namespaceParam);

            var res = events.Items.FirstOrDefault(p => p.InvolvedObject.Uid == service.Uid() && p.Message == message);
            return res;

        }

        public async Task<V1Event> CountUpOrCreateEvent(string namespaceParam, V1Service service, string message)
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

                //v1event.Count = v1event.Count++;
                //v1event = await client.ReplaceNamespacedEventAsync(v1event, v1event.Name(), v1event.Namespace());
            }
            else
            {
                logger.LogCritical("Create a new Event");
                v1event = await CreateNewEventAsync(namespaceParam, service, message);
            }
            return v1event;
        }

        public async Task<V1Event> CreateNewEventAsync(string namespaceParam, V1Service service, string message)
        {

            V1ObjectReference refer = new V1ObjectReference();
            refer.ApiVersion = service.ApiVersion;
            refer.Kind = "Service";
            refer.Name = service.Name();
            refer.NamespaceProperty = service.Namespace();
            refer.Uid = service.Uid();
            refer.ResourceVersion = service.ResourceVersion();

            V1Event newEvent = new V1Event { };
            //  newEvent.EventTime = DateTime.Now;
            newEvent.FirstTimestamp = DateTime.UtcNow;
            newEvent.LastTimestamp = DateTime.UtcNow;
            newEvent.InvolvedObject = refer;
            newEvent.Count = 1;
            newEvent.LastTimestamp = DateTime.UtcNow;
            newEvent.Message = message;
            newEvent.Metadata = new V1ObjectMeta();
            newEvent.Metadata.CreationTimestamp = DateTime.UtcNow;
            newEvent.Metadata.NamespaceProperty = namespaceParam;

            newEvent.Metadata.Name = service.Name() + Guid.NewGuid().ToString();

            newEvent.Reason = "Unhealthy";
            newEvent.Type = "Warning";

            var savedevent = await this.client.CreateNamespacedEventAsync(newEvent, namespaceParam);

            logger.LogCritical("Saved UID :" + savedevent.Uid());
            return savedevent;

            // {
            //     "action": null,
            //     "apiVersion": null,
            //     "count": 38,
            //     "eventTime": null,
            //     "firstTimestamp": "2020-09-08T04:34:33Z",
            //     "involvedObject": {
            //       "apiVersion": "v1",
            //       "fieldPath": "spec.containers{turquoise-api-healthmonitoring}",
            //       "kind": "Pod",
            //       "name": "turquoise-api-healthmonitoring-dev-986cff86c-cx4tz",
            //       "namespaceProperty": "turquoise-dev",
            //       "resourceVersion": "342311",
            //       "uid": "32de336e-e46f-491f-8e4a-f83433ea7e69"
            //     },
            //     "kind": null,
            //     "lastTimestamp": "2020-09-08T11:20:43Z",
            //     "message": "Readiness probe failed: Get http://10.244.5.15:15020/app-health/turquoise-api-healthmonitoring/readyz: net/http: request canceled (Client.Timeout exceeded while awaiting headers)",
            //     "metadata": {
            //       "annotations": null,
            //       "clusterName": null,
            //       "creationTimestamp": "2020-09-08T04:34:33Z",
            //       "deletionGracePeriodSeconds": null,
            //       "deletionTimestamp": null,
            //       "finalizers": null,
            //       "generateName": null,
            //       "generation": null,
            //       "labels": null,
            //       "managedFields": null,
            //       "name": "turquoise-api-healthmonitoring-dev-986cff86c-cx4tz.1632b4dbab96ac24",
            //       "namespaceProperty": "turquoise-dev",
            //       "ownerReferences": null,
            //       "resourceVersion": "436447",
            //       "selfLink": "/api/v1/namespaces/turquoise-dev/events/turquoise-api-healthmonitoring-dev-986cff86c-cx4tz.1632b4dbab96ac24",
            //       "uid": "7142af4f-d3f6-4be2-b1c5-4a29cb3f51a1"
            //     },
            //     "reason": "Unhealthy",
            //     "related": null,
            //     "reportingComponent": "",
            //     "reportingInstance": "",
            //     "series": null,
            //     "source": {
            //       "component": "kubelet",
            //       "host": "aks-agentpool-54824633-vmss000005"
            //     },
            //     "type": "Warning"
            //   },


        }
    }
}