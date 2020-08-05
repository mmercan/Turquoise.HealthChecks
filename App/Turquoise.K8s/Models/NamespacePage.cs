using System.Collections.Generic;
using k8s.Models;

namespace Turquoise.K8s.Models
{
    public class NamespacePage
    {
        public V1Namespace Namespace { get; set; }
        public List<NamespacePageDeployment> Deployments { get; set; }
        public List<V1Service> Services { get; set; }


    }

    public class NamespacePageDeployment
    {
        public V1Deployment Deployment { get; set; }
        public List<NamespacePageReplicaSet> ReplicaSets { get; set; }
    }

    public class NamespacePageReplicaSet
    {
        public V1ReplicaSet ReplicaSet { get; set; }
        public List<V1Pod> Pods { get; set; }
        public List<V1Service> Services { get; set; }
    }
}