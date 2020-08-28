using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Turquoise.Models.Mongo
{
    public class NamespaceV1
    {

        public NamespaceV1()
        {
            // this._id = ObjectId.GenerateNewId();
        }

        public string Uid { get; set; }

        [BsonId]
        public string Name { get; set; }
        public List<Label> Labels { get; set; }
        public DateTime CreationTime { get; set; }
        public string Status { get; set; }

        public DateTime LatestSyncDateUTC { get; set; }

        public int DeploymentCount { get; set; }
        public int ServiceCount { get; set; }
        public int ErrorCount { get; set; }
        public int WarningCount { get; set; }
    }

}