using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DataSyncHub.Modules.Users.Core.Models.MongoDb
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }        
        public string? Username { get; set; }
        public string? Sex { get; set; }
        public string? Address { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Birthday { get; set; }
    }
}
