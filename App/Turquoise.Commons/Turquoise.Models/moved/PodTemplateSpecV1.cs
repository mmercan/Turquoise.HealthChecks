using Newtonsoft.Json;

namespace Turquoise.Models.moved
{
    public class PodTemplateSpecV1
    {
        [JsonProperty(PropertyName = "metadata")]
        public ObjectMetaV1 Metadata { get; set; }

        [JsonProperty(PropertyName = "spec")]
        public PodSpecV1 Spec { get; set; }

    }
}

