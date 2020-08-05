using Newtonsoft.Json;

namespace Turquoise.Models
{
    public class OwnerReference
    {
        [JsonProperty(PropertyName = "apiVersion")]
        public string ApiVersion { get; set; }

        [JsonProperty(PropertyName = "blockOwnerDeletion")]
        public bool? BlockOwnerDeletion { get; set; }

        [JsonProperty(PropertyName = "controller")]
        public bool? Controller { get; set; }

        [JsonProperty(PropertyName = "kind")]
        public string Kind { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "uid")]
        public string Uid { get; set; }
    }
}