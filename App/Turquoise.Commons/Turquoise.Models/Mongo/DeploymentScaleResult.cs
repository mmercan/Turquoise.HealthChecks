using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Turquoise.Models.Mongo
{
    public class DeploymentScaleResult
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public DeploymentScaleResult()
        {
            this.Id = ObjectId.GenerateNewId();
        }
        public DateTime ScaleDateUTC { get; set; }
        public bool ScaledUp { get; set; }
        public bool ScaledDown { get; set; }
        public string ScheduleCrobTab { get; set; }

        public int TotalSeconds { get; set; }
    }
}