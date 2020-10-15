using System.Text.Json.Serialization;

namespace Group10.API.Models
{
    public class GoogleApiTokenError
    {
        [JsonPropertyName("error")]
        public string? error { get; set; }
        
        [JsonPropertyName("error_description")]
        public string? ErrorDescription { get; set; }
        
    }
}