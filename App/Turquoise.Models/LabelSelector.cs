using System.Collections.Generic;
using Newtonsoft.Json;

namespace Turquoise.Models
{
    public class LabelSelector
    {
        [JsonProperty(PropertyName = "matchExpressions")]
        public IList<LabelSelectorRequirement> MatchExpressions { get; set; }

        [JsonProperty(PropertyName = "matchLabels")]
        public IDictionary<string, string> MatchLabels { get; set; }
    }
}