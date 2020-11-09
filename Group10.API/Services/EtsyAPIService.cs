using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Group10.API.Models;
using Microsoft.Extensions.Configuration;

namespace Group10.API.Services
{
    public class EtsyAPIService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string listingInfoUrl = "https://openapi.etsy.com/v2/listings/active?api_key=";
        private string etsyConnection;
        
        public EtsyAPIService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            etsyConnection = configuration["EtsyAPIConsumerKey"];
        }
        
        public async Task<EtsyAPIListingInfo?> getListingAsync()
        {
            
            using var client = _httpClientFactory.CreateClient();
            var jsonresponse = await client.GetAsync($"{listingInfoUrl}{etsyConnection}");
            var jsonResp = await jsonresponse.Content.ReadAsStringAsync();
            var listinginfo = JsonSerializer.Deserialize<EtsyAPIListingInfo>(jsonResp);

            return listinginfo;
        }

        public async Task<EtsyAPIimageInfo?> getImageAsync(string listingId)
        {
            string imageUrL = $"https://openapi.etsy.com/v2/listings/{listingId}/images?api_key=";

            using var client = _httpClientFactory.CreateClient();
            var jsonresponse = await client.GetAsync($"{imageUrL}{etsyConnection}");
            var jsonResp = await jsonresponse.Content.ReadAsStringAsync();
            var imageinfo = JsonSerializer.Deserialize<EtsyAPIimageInfo>(jsonResp);

            return imageinfo;
        }
    }
}