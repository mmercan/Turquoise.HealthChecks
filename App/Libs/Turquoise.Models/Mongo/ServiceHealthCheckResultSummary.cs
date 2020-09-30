using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Turquoise.Models.Mongo
{
    public class ServiceHealthCheckResultSummary
    {
        [BsonId]
        public string NameandNamespace { get { return Name + "." + Namespace; } }
        public string Uid { get; set; }
        public string Name { get; set; }
        public string Namespace { get; set; }

        public string HealthIsalive { get; set; }
        public DateTime HealthIsaliveSyncDateUTC { get; set; }
        public string HealthIsaliveAndWell { get; set; }
        public DateTime HealthIsaliveAndWellSyncDateUTC { get; set; }

    }
}