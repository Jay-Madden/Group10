using System.Collections.Generic;
using System.Threading.Tasks;
using Group10.API.Models;
using Microsoft.AspNetCore.Mvc;
using Group10.API.Services;
using Group10.Data.Contexts;
using Group10.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Group10.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly Group10Context _context;
        private readonly IEtsyAPIService _etsyService;

        public CatalogController(IEtsyAPIService etsyService, Group10Context context)
        {
            _etsyService = etsyService;
            _context = context;
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
                    id = result.listing_id.ToString(),
                    price = result.price,
                    description = result.description,
                    title = result.title,
                    image = imageUrl.results[0].url_570xN
                });
            }

            foreach (var list in relevantInfo)
            {
                if(list.description is null || list.id is null || list.title is null || list.price is null || list.image is null)
                {
                    continue;
                }
                var product = new Product
                {
                    imageurl = list.image,
                    Description = list.description,
                    ProductId = list.id,
                    Name = list.title,
                    PriceUsd = list.price,
                    PricePts = list.price
                };

                var product2 = await _context
                    .Products
                    .SingleOrDefaultAsync(x => x.ProductId == product.ProductId);

                if (product2 is null)
                {
                    _context.Add(product);
                }
            }
            await _context.SaveChangesAsync();
            var products = await _context.Products.ToListAsync();
            return Ok(new {products});
        }
    }
}