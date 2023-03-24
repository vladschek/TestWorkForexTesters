using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Core.Domain.Enums;

namespace Core.Domain.Entities
{
    public class Indicator
    {
        [BsonRepresentation(BsonType.String)]
        public IndicatorName Name { get; set; }

        public string Parameters { get; set; }
    }
}
