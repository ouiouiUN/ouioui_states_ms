using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace States_ms.Models
{
    public class State
    {
        [BsonId]
        public ObjectId internalId { get; set; }
        public long stateId { get; set; }
        public string userId { get; set; }
        public string mediaId { get; set; }
        public bool active { get; set; }
        public DateTime createdOn { get; set; }
        public string stateText { get; set; }
    }

    
}