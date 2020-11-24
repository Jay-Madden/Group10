using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Group10.API.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;


namespace Group10.API.Services
{
    public class EtsyAPIService : IEtsyAPIService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string listingInfoUrl = "https://openapi.etsy.com/v2/listings/active?limit=25&fields=listing_id,title,description,price&api_key=";
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
            var listinginfo = JsonConvert.DeserializeObject<EtsyAPIListingInfo>(jsonResp);

            return listinginfo;
        }

        public async Task<EtsyAPIimageInfo?> getImageAsync(int listingId)
        {
            string imageUrL = $"https://openapi.etsy.com/v2/listings/{listingId}/images?fields=url_570xN&api_key=";

            using var client = _httpClientFactory.CreateClient();
            var etsyResponse = await client.GetAsync($"{imageUrL}{etsyConnection}");
            await Task.Delay(50);
            var jsonResp = await etsyResponse.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<EtsyAPIimageInfo>(jsonResp);
        }
    }
}