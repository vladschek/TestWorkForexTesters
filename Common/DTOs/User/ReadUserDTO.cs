
using Common.DTOs.Subscription;
using Common.DTOs;
using System.Text.Json.Serialization;

namespace Common.DTOs
{
    public class ReadUserDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("subscriptionId")]
        public int SubscriptionId { get; set; }
        [JsonPropertyName("subscription")]
        public ReadSubscriptionDTO Subscription { get; set; }
        [JsonPropertyName("projects")]
        public List<ProjectResponseDTO> Projects { get; set; }
        [JsonPropertyName("userSettings")]
        public UserSettingsDTO UserSettings { get; set; }
    }
}
