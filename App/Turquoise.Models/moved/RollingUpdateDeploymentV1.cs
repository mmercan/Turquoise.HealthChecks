using Newtonsoft.Json;

namespace Turquoise.Models.moved
{
    public class RollingUpdateDeploymentV1
    {
        [JsonProperty(PropertyName = "maxSurge")]
        public string MaxSurge { get; set; }

        [JsonProperty(PropertyName = "maxUnavailable")]
        public string MaxUnavailable { get; set; }
    }
}