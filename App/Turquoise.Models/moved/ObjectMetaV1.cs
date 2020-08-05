using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Turquoise.Models.moved
{
    public class ObjectMetaV1
    {
        [JsonProperty(PropertyName = "resourceVersion")]
        public string ResourceVersion { get; set; }

        [JsonProperty(PropertyName = "ownerReferences")]
        public IList<OwnerReferenceV1> OwnerReferences { get; set; }

        [JsonProperty(PropertyName = "namespace")]
        public string NamespaceProperty { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "managedFields")]
        public IList<ManagedFieldsEntryV1> ManagedFields { get; set; }

        [JsonProperty(PropertyName = "labels")]
        public IDictionary<string, string> Labels { get; set; }

        [JsonProperty(PropertyName = "generation")]
        public long? Generation { get; set; }

        [JsonProperty(PropertyName = "generateName")]
        public string GenerateName { get; set; }

        [JsonProperty(PropertyName = "finalizers")]
        public IList<string> Finalizers { get; set; }

        [JsonProperty(PropertyName = "deletionTimestamp")]
        public DateTime? DeletionTimestamp { get; set; }

        [JsonProperty(PropertyName = "deletionGracePeriodSeconds")]
        public long? DeletionGracePeriodSeconds { get; set; }

        [JsonProperty(PropertyName = "creationTimestamp")]
        public DateTime? CreationTimestamp { get; set; }

        [JsonProperty(PropertyName = "clusterName")]
        public string ClusterName { get; set; }

        [JsonProperty(PropertyName = "annotations")]
        public IDictionary<string, string> Annotations { get; set; }

        [JsonProperty(PropertyName = "selfLink")]
        public string SelfLink { get; set; }

        [JsonProperty(PropertyName = "uid")]
        public string Uid { get; set; }
    }
}