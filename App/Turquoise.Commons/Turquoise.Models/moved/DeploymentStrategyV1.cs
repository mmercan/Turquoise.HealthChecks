using Newtonsoft.Json;

namespace Turquoise.Models.moved
{
    public class DeploymentStrategyV1
    {
        [JsonProperty(PropertyName = "rollingUpdate")]
        public RollingUpdateDeploymentV1 RollingUpdate { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }
}