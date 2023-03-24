using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class UserSettingsDTO
    {
        [JsonPropertyName("userId")]
        public int UserId { get; set; }
        [JsonPropertyName("language")]
        public string Language { get; set; }
        [JsonPropertyName("theme")]
        public string Theme { get; set; }
    }
}
