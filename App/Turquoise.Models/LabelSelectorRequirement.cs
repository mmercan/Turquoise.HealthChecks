using System.Collections.Generic;
using Newtonsoft.Json;

namespace Turquoise.Models
{
    public class LabelSelectorRequirement
    {
        //
        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }

        [JsonProperty(PropertyName = "operator")]
        public string OperatorProperty { get; set; }

        [JsonProperty(PropertyName = "values")]
        public IList<string> Values { get; set; }
    }
}