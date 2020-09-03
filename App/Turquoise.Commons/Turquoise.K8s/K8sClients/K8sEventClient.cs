using System;
using k8s;
using k8s.Models;

namespace Turquoise.K8s.K8sClients
{
    public class K8sEventClient
    {
        private Kubernetes client;
        public K8sEventClient(Kubernetes kubernetesClient)
        {
            this.client = kubernetesClient;
        }


        public void CreateNewEvent(string namespaceParam)
        {

            V1ObjectReference refer = new V1ObjectReference();
            //     refer.
            V1Event newEvent = new V1Event { };


            //newEvent.EventTime=DateTime.UtcNow;
            //newEvent.InvolvedObject
            //this.client.CreateNamespacedEvent()

        }
    }
}