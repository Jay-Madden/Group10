using System.Text.Json.Serialization;

namespace Group10.API.Models
{
    public class EtsyAPIimageInfo
    {
        [JsonPropertyName("listing_image_id")]
        public string? listingimageid { get; set; }
        
        [JsonPropertyName("url_170x135")]
        public string? url170x135 { get; set; }
    }
}