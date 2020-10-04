using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Turquoise.Models.Mongo
{
    public class DeploymentScaleHistory
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public DeploymentScaleHistory()
        {
            this.Id = ObjectId.GenerateNewId();
        }


        public string Uid { get; set; }

        public string Name { get; set; }
        public string Namespace { get; set; }
        public string Schedule { get; set; }
        public DateTime ScaledUtc { get; set; }
        public int OldScaleNumber { get; set; }
        public int NewScaleNumber { get; set; }

        public string Status { get; set; }
    }
}