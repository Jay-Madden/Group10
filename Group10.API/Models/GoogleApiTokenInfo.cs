using System.Text.Json.Serialization;

namespace Group10.API.Models
{
    public class GoogleApiTokenInfo
    {
        [JsonPropertyName("issued_to")]
        public string? IssuedTo { get; set; }

        [JsonPropertyName("audience")]
        public string? Audience { get; set; }

        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }

        [JsonPropertyName("scope")]
        public string? Scope { get; set; }

        [JsonPropertyName("expires_in")]
        public long? ExpiresIn { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("verified_email")]
        public bool? VerifiedEmail { get; set; }

        [JsonPropertyName("access_type")]
        public string? AccessType { get; set; }    }
}