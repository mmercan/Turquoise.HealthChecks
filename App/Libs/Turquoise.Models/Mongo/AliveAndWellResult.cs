using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Turquoise.Models.Mongo
{
    public class AliveAndWellResult
    {

        [BsonId]
        public ObjectId Id { get; set; }

        public AliveAndWellResult()
        {
            this.Id = ObjectId.GenerateNewId();
        }

        public DateTime CreationTime { get; set; }
        public string ServiceUid { get; set; }
        public string ServiceName { get; set; }
        public string ServiceNamespace { get; set; }

        public string Status { get; set; }
        public BsonDocument Result { get; set; }

        public string StringResult { get; set; }

        public string BsonException { get; set; }

        public string CheckedUrl { get; set; }
        // public bool Equals(AliveAndWellResult other)
        // {

        //     //Check whether the compared object is null.
        //     if (Object.ReferenceEquals(other, null)) return false;

        //     //Check whether the compared object references the same data.
        //     if (Object.ReferenceEquals(this, other)) return true;

        //     //Check whether the products' properties are equal.
        //     // return ServiceNamespace.Equals(other.ServiceNamespace) && ServiceName.Equals(other.ServiceName);

        //     return ServiceUid.Equals(other.ServiceUid);
        // }

        // public override int GetHashCode()
        // {
        //     int hasServiceUid = ServiceUid == null ? 0 : ServiceUid.GetHashCode();
        //     return hasServiceUid;
        // }

    }
}