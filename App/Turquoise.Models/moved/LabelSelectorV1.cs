using System.Collections.Generic;
using Newtonsoft.Json;

namespace Turquoise.Models.moved
{
    public class LabelSelectorV1
    {
        [JsonProperty(PropertyName = "matchExpressions")]
        public IList<LabelSelectorRequirementV1> MatchExpressions { get; set; }

        [JsonProperty(PropertyName = "matchLabels")]
        public IDictionary<string, string> MatchLabels { get; set; }
    }
}