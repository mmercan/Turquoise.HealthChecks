using System.Collections.Generic;
using Newtonsoft.Json;

namespace Turquoise.Models.moved
{
    public class LabelSelectorRequirementV1
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