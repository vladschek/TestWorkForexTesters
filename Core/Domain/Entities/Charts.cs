using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Core.Domain.Enums;

namespace Core.Domain.Entities
{
    public class Chart
    {
        [BsonRepresentation(BsonType.String)]
        public Symbol Symbol { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Timeframe Timeframe { get; set; }

        public List<Indicator> Indicators { get; set; }
    }

}
