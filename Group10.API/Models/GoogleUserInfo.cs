using System;
using System.Text.Json.Serialization;

namespace Group10.API.Models
{
    public class GoogleUserInfo
    {
        [JsonPropertyName("sub")]
        public string? sub { get; set; }

        [JsonPropertyName("name")]
        public string? name { get; set; }

        [JsonPropertyName("given_name")]
        public string? given_name { get; set; }

        [JsonPropertyName("family_name")]
        public string? family_name { get; set; }

        [JsonPropertyName("picture")]
        public Uri? picture { get; set; }

        [JsonPropertyName("email")]
        public string? email { get; set; }

        [JsonPropertyName("email_verified")]
        public bool? email_verified { get; set; }

        [JsonPropertyName("locale")]
        public string? locale { get; set; } 
    }
}