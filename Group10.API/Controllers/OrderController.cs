using Group10.API.Enums;
using Group10.API.Models;
using Group10.Data.Contexts;
using Group10.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Group10.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly Group10Context _context;

        public OrderController(Group10Context context)
        {
            _context = context;
        }

        [HttpPost("purchase_cart_order")]
        public async Task<IActionResult> PayForItems([FromBody] ListOfProductsModel listofproducts)
        {
            var driverId = User.FindFirst(AppClaims.UserId)?.Value;

            var product = new Product();
            var products = new List<Product>();
            var points = 0;

            var productIds = listofproducts.Products;

            //grab each product, query database, sum points
            for(int i = 0; i < productIds.Count(); i++)
            {
                var productId = productIds[i];

                product = await _context.Products
                    .SingleOrDefaultAsync(x => x.ProductId == productId);

                products.Add(product);

                points += (int)double.Parse(product.PricePts);
            }
            
            //get current points for driver
            var driver = await _context.Drivers
                .SingleOrDefaultAsync(x => x.AppUserId == driverId);

            //decrement the points attribute
            if (points < driver.Points)
            {
                driver.Points -= points;
                await _context.SaveChangesAsync();
                //return Ok("Points subtracted");
            }
            if(points > driver.Points)
            {
                return BadRequest("Insufficient points to finalize purchase");
            }


            //add the drivers order to coresponding tables
            var newOrder = new Order { DriverId = driver.Id, Products = products };

            _context.Add(newOrder);
            await _context.SaveChangesAsync();
            
            
            return Ok("Purchase successful");
            
        }

    }
}
