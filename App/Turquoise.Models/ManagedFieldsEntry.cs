using System;
using Newtonsoft.Json;

namespace Turquoise.Models
{
    public class ManagedFieldsEntry
    {
        [JsonProperty(PropertyName = "apiVersion")]
        public string ApiVersion { get; set; }

        [JsonProperty(PropertyName = "fieldsType")]
        public string FieldsType { get; set; }

        [JsonProperty(PropertyName = "fieldsV1")]
        public object FieldsV1 { get; set; }

        [JsonProperty(PropertyName = "manager")]
        public string Manager { get; set; }

        [JsonProperty(PropertyName = "operation")]
        public string Operation { get; set; }

        [JsonProperty(PropertyName = "time")]
        public DateTime? Time { get; set; }
    }
}