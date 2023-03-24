using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Domain.Entities
{
    public class Project
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public int UserId { get; set; }

        public string Name { get; set; }

        public List<Chart> Charts { get; set; }
    }
}
