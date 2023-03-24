using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Core.Domain.Enums;

namespace Core.Domain.Entities
{
    public class UserSettings
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int UserId { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Language Language { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Theme Theme { get; set; }
    }
}
