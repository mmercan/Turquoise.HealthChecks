using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Turquoise.Models.Mongo
{
    public class ServiceV1
    {

        public ServiceV1()
        {
            //   this._id = ObjectId.GenerateNewId();
        }

        public string Uid { get; set; }

        [BsonId]
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



        public string IngressUrl { get; set; }

        public string VirtualServiceUrl { get; set; }

        //TODO: Add Endpoints
        // Endpoints

        //TODO: Add PODS
        // Pods

        //TODO: Add Events
        //Events
    }
}