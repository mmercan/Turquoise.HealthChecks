using Newtonsoft.Json;

namespace Turquoise.Models.moved
{
    public class DeploymentSpecV1
    {

        [JsonProperty(PropertyName = "minReadySeconds")]
        public int? MinReadySeconds { get; set; }

        [JsonProperty(PropertyName = "paused")]
        public bool? Paused { get; set; }

        [JsonProperty(PropertyName = "progressDeadlineSeconds")]
        public int? ProgressDeadlineSeconds { get; set; }

        [JsonProperty(PropertyName = "replicas")]
        public int? Replicas { get; set; }

        [JsonProperty(PropertyName = "revisionHistoryLimit")]
        public int? RevisionHistoryLimit { get; set; }

        [JsonProperty(PropertyName = "selector")]
        public LabelSelectorV1 Selector { get; set; }

        [JsonProperty(PropertyName = "strategy")]
        public DeploymentStrategyV1 Strategy { get; set; }

        [JsonProperty(PropertyName = "template")]
        public PodTemplateSpecV1 Template { get; set; }


    }
}