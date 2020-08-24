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


        public string ServiceUid { get; set; }
        public string ServiceName { get; set; }
        public string ServiceNamespace { get; set; }
        public BsonDocument Result { get; set; }
    }
}