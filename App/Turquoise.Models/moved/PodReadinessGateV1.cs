using Newtonsoft.Json;

namespace Turquoise.Models.moved
{
    public class PodReadinessGateV1
    {
        [JsonProperty(PropertyName = "conditionType")]
        public string ConditionType { get; set; }
    }
}