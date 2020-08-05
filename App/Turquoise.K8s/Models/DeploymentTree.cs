using k8s.Models;

namespace Turquoise.K8s.Models
{
    public class DeploymentTree
    {
        public V1Deployment deployment { get; set; }
        public V1ReplicaSet deplicaset { get; set; }
        public V1Pod pod { get; set; }
        public V1Service service { get; set; }
    }
}