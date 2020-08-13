using System.Collections.Generic;
using Newtonsoft.Json;

namespace Turquoise.Models.moved
{
    public class DeploymentStatusV1
    {
        [JsonProperty(PropertyName = "availableReplicas")]
        public int? AvailableReplicas { get; set; }

        [JsonProperty(PropertyName = "collisionCount")]
        public int? CollisionCount { get; set; }

        [JsonProperty(PropertyName = "conditions")]
        public IList<DeploymentConditionV1> Conditions { get; set; }

        [JsonProperty(PropertyName = "observedGeneration")]
        public long? ObservedGeneration { get; set; }

        [JsonProperty(PropertyName = "readyReplicas")]
        public int? ReadyReplicas { get; set; }

        [JsonProperty(PropertyName = "replicas")]
        public int? Replicas { get; set; }

        [JsonProperty(PropertyName = "unavailableReplicas")]
        public int? UnavailableReplicas { get; set; }

        [JsonProperty(PropertyName = "updatedReplicas")]
        public int? UpdatedReplicas { get; set; }
    }
}