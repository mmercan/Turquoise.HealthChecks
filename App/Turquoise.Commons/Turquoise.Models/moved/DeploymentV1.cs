using Newtonsoft.Json;

namespace Turquoise.Models.moved
{
    public class DeploymentV1
    {

        public const string KubeApiVersion = "v1";
        public const string KubeKind = "Deployment";
        public const string KubeGroup = "apps";

        [JsonProperty(PropertyName = "apiVersion")]
        public string ApiVersion { get; set; }

        [JsonProperty(PropertyName = "kind")]
        public string Kind { get; set; }

        [JsonProperty(PropertyName = "metadata")]
        public ObjectMetaV1 Metadata { get; set; }

        [JsonProperty(PropertyName = "spec")]
        public DeploymentSpecV1 Spec { get; set; }

        [JsonProperty(PropertyName = "status")]
        public DeploymentStatusV1 Status { get; set; }

    }
}