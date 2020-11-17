using System.Threading.Tasks;
using Group10.API.Models;

namespace Group10.API.Services
{
    public interface IEtsyAPIService
    {
        public Task<EtsyAPIListingInfo?> getListingAsync();

        public Task<EtsyAPIimageInfo?> getImageAsync(int listingId);
    }
}