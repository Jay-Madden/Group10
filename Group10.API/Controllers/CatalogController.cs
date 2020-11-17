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
            List<RelevantCatalogInfo> relevantinfo = new List<RelevantCatalogInfo>();
            
            var listing = await _etsyService.getListingAsync();

            if (listing is null || listing.results is null)
            {
                return BadRequest("No listings found in catalog");
            }
            foreach (var result in listing.results)
            {
                RelevantCatalogInfo tmp = new RelevantCatalogInfo
                {
                    price = result.price,
                    descrption = result.description,
                    title = result.title
                };
                
                
                var imageurl = await _etsyService.getImageAsync(result.listing_id);
                if (imageurl is null || imageurl.results is null)
                {
                    return BadRequest("No image associated with listing id");
                }

                tmp.url_170x135 = imageurl.results[0].url_170x135;

                relevantinfo.Add(tmp);
            }

            return Ok(relevantinfo);
            
        }
        
    }
}