using System;
using System.Collections.Generic;
using k8s.Models;
namespace Turquoise.K8s.Models
{
    public class NamespaceDTO
    {
        public Dictionary<string, string> Labels { get; set; }
        public DateTime CreationTimestamp { get; set; }
        public string Name { get; set; }
        public Guid Uid { get; set; }
        public List<DeploymentDTO> Deployments { get; set; }

    }

    public class DeploymentDTO
    {

        public List<DeploymentDTO> ReplicaSets { get; set; }
    }

    public class ReplicaSetDTO
    {
        List<PodDTO> Pods { get; set; }
        List<ServiceDTO> Services { get; set; }
    }

    public class PodDTO
    {

    }

    public class ServiceDTO
    {

    }
}

