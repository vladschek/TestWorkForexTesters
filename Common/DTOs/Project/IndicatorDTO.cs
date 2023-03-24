using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class IndicatorDTO
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("parameters")]
        public string Parameters { get; set; }
    }
}