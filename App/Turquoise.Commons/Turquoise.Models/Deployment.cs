using Newtonsoft.Json;

namespace Turquoise.Models
{
    public class Deployment
    {

        public const string KubeApiVersion = "v1";
        public const string KubeKind = "Deployment";
        public const string KubeGroup = "apps";

        [JsonProperty(PropertyName = "apiVersion")]
        public string ApiVersion { get; set; }

        [JsonProperty(PropertyName = "kind")]
        public string Kind { get; set; }

        [JsonProperty(PropertyName = "metadata")]
        public ObjectMeta Metadata { get; set; }

        [JsonProperty(PropertyName = "spec")]
        public DeploymentSpec Spec { get; set; }

        [JsonProperty(PropertyName = "status")]
        public DeploymentStatus Status { get; set; }

    }
}