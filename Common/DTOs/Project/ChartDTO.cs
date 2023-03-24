

using System.Text.Json.Serialization;

namespace Common.DTOs
{
    public class ChartDTO
    {
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
        [JsonPropertyName("timeframe")]
        public string Timeframe { get; set; }
        [JsonPropertyName("indicators")]
        public List<IndicatorDTO> Indicators { get; set; }
    }
}
