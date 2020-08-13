using Newtonsoft.Json;

namespace Turquoise.Models
{
    public class DeploymentSpec
    {
        // [JsonProperty(PropertyName = "minReadySeconds")]
        // public int? MinReadySeconds { get; set; }

        // [JsonProperty(PropertyName = "paused")]
        // public bool? Paused { get; set; }

        [JsonProperty(PropertyName = "progressDeadlineSeconds")]
        public int? ProgressDeadlineSeconds { get; set; }

        [JsonProperty(PropertyName = "replicas")]
        public int? Replicas { get; set; }

        [JsonProperty(PropertyName = "revisionHistoryLimit")]
        public int? RevisionHistoryLimit { get; set; }

        // [JsonProperty(PropertyName = "selector")]
        // public V1LabelSelector Selector { get; set; }

        // [JsonProperty(PropertyName = "strategy")]
        // public V1DeploymentStrategy Strategy { get; set; }

        // [JsonProperty(PropertyName = "template")]
        // public V1PodTemplateSpec Template { get; set; }

    }
}