using System;
using Newtonsoft.Json;

namespace Turquoise.Models
{
    public class DeploymentCondition
    {
        [JsonProperty(PropertyName = "lastTransitionTime")]
        public DateTime? LastTransitionTime { get; set; }

        [JsonProperty(PropertyName = "lastUpdateTime")]
        public DateTime? LastUpdateTime { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "reason")]
        public string Reason { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

    }
}