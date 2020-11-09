using System.Text.Json.Serialization;

namespace Group10.API.Models
{
    public class EtsyAPIListingInfo
    {
        [JsonPropertyName("listing_id")]
        public string? listingid { get; set; }
        
        [JsonPropertyName("price")]
        public string? Price { get; set; }
    }
}