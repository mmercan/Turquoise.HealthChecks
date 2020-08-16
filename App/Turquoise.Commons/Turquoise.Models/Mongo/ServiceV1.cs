using System;
using System.Collections.Generic;

namespace Turquoise.Models.Mongo
{
    public class ServiceV1
    {
        public string Name { get; set; }
        public string Namespace { get; set; }
        public List<Label> Labels { get; set; }
        public DateTime CreationTime { get; set; }
        public List<Label> LabelSelector { get; set; }

        public List<Label> Annotations { get; set; }
        public string Type { get; set; }
        public string SessionAffinity { get; set; }

        public string ClusterIP { get; set; }
        public List<string> InternalEndpoints { get; set; }
        public List<string> ExternalEndpoints { get; set; }
        public string Uid { get; set; }

        //TODO: Add Endpoints
        // Endpoints

        //TODO: Add PODS
        // Pods

        //TODO: Add Events
        //Events
    }
}