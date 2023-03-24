
using System.Text.Json.Serialization;

namespace Common.DTOs
{

    public class ProjectResponseDTO
    {
        [JsonPropertyName("userId")]
        public int UserId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("charts")]
        public List<ChartDTO> Charts { get; set; }
    }
}
