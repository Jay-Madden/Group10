using System.Collections.Generic;
using System.Threading.Tasks;
using Group10.API.Models;
using Microsoft.AspNetCore.Mvc;
using Group10.API.Services;

namespace Group10.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IEtsyAPIService _etsyService;
        public CatalogController(IEtsyAPIService etsyService)
        {
            _etsyService = etsyService;
        }
        
        [HttpGet("info")]
        public async Task<IActionResult> Info()
        {
            List<RelevantCatalogInfo> relevantInfo = new List<RelevantCatalogInfo>();
            
            var listing = await _etsyService.getListingAsync();
            if (listing?.results is null)
            {
                return BadRequest("No listings found in catalog");
            }
            
            foreach (var result in listing.results)
            {
                var imageUrl = await _etsyService.getImageAsync(result.listing_id);
                if (imageUrl?.results is null or { Count: < 1 })
                {
                    continue;
                }
                
                relevantInfo.Add(new RelevantCatalogInfo
                {
                    price = result.price,
                    description = result.description,
                    title = result.title, 
                    image = imageUrl.results[0].url_170x135
                });
            }

            return Ok(relevantInfo);
        }
    }
}