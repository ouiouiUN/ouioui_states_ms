using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace States_ms.Models
{
    public class State
    {
        [BsonId]
        public ObjectId internalId { get; set; }
        public long stateId { get; set; }
        public string userId { get; set; }
        public string mediaId { get; set; }
    }

    
}