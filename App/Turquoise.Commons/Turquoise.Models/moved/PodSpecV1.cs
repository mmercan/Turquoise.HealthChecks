using System.Collections.Generic;
using Newtonsoft.Json;

namespace Turquoise.Models.moved
{
    public class PodSpecV1
    {
        [JsonProperty(PropertyName = "preemptionPolicy")]
        public string PreemptionPolicy { get; set; }

        [JsonProperty(PropertyName = "priority")]
        public int? Priority { get; set; }

        [JsonProperty(PropertyName = "priorityClassName")]
        public string PriorityClassName { get; set; }

        [JsonProperty(PropertyName = "readinessGates")]
        public IList<PodReadinessGateV1> ReadinessGates { get; set; }

        [JsonProperty(PropertyName = "restartPolicy")]
        public string RestartPolicy { get; set; }

        [JsonProperty(PropertyName = "runtimeClassName")]
        public string RuntimeClassName { get; set; }

        [JsonProperty(PropertyName = "schedulerName")]
        public string SchedulerName { get; set; }

        //  [JsonProperty(PropertyName = "securityContext")]
        //  public PodSecurityContextV1 SecurityContext { get; set; }

        [JsonProperty(PropertyName = "serviceAccount")]
        public string ServiceAccount { get; set; }

        [JsonProperty(PropertyName = "serviceAccountName")]
        public string ServiceAccountName { get; set; }

        [JsonProperty(PropertyName = "shareProcessNamespace")]
        public bool? ShareProcessNamespace { get; set; }

        [JsonProperty(PropertyName = "subdomain")]
        public string Subdomain { get; set; }

        [JsonProperty(PropertyName = "terminationGracePeriodSeconds")]
        public long? TerminationGracePeriodSeconds { get; set; }

        [JsonProperty(PropertyName = "tolerations")]
        public IList<TolerationV1> Tolerations { get; set; }

        [JsonProperty(PropertyName = "overhead")]
        public IDictionary<string, ResourceQuantity> Overhead { get; set; }

        [JsonProperty(PropertyName = "nodeSelector")]
        public IDictionary<string, string> NodeSelector { get; set; }

        [JsonProperty(PropertyName = "nodeName")]
        public string NodeName { get; set; }

        [JsonProperty(PropertyName = "initContainers")]
        public IList<ContainerV1> InitContainers { get; set; }

        [JsonProperty(PropertyName = "activeDeadlineSeconds")]
        public long? ActiveDeadlineSeconds { get; set; }

        [JsonProperty(PropertyName = "affinity")]
        public AffinityV1 Affinity { get; set; }

        [JsonProperty(PropertyName = "automountServiceAccountToken")]
        public bool? AutomountServiceAccountToken { get; set; }

        [JsonProperty(PropertyName = "containers")]
        public IList<ContainerV1> Containers { get; set; }

        [JsonProperty(PropertyName = "dnsConfig")]
        public PodDNSConfigV1 DnsConfig { get; set; }

        [JsonProperty(PropertyName = "dnsPolicy")]
        public string DnsPolicy { get; set; }

        [JsonProperty(PropertyName = "topologySpreadConstraints")]
        public IList<TopologySpreadConstraintV1> TopologySpreadConstraints { get; set; }

        [JsonProperty(PropertyName = "enableServiceLinks")]
        public bool? EnableServiceLinks { get; set; }

        [JsonProperty(PropertyName = "hostAliases")]
        public IList<HostAliasV1> HostAliases { get; set; }

        [JsonProperty(PropertyName = "hostIPC")]
        public bool? HostIPC { get; set; }

        [JsonProperty(PropertyName = "hostNetwork")]
        public bool? HostNetwork { get; set; }

        [JsonProperty(PropertyName = "hostPID")]
        public bool? HostPID { get; set; }

        [JsonProperty(PropertyName = "hostname")]
        public string Hostname { get; set; }

        [JsonProperty(PropertyName = "imagePullSecrets")]
        public IList<LocalObjectReferenceV1> ImagePullSecrets { get; set; }

        [JsonProperty(PropertyName = "ephemeralContainers")]
        public IList<EphemeralContainerV1> EphemeralContainers { get; set; }

        [JsonProperty(PropertyName = "volumes")]
        public IList<VolumeV1> Volumes { get; set; }

    }
}