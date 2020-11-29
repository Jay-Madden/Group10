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
    public class SponsorController : ControllerBase
    {
        private readonly Group10Context _context;

        public SponsorController(Group10Context context)
        {
            _context = context;
        }

        //Returns a list of driver Ids that were claimed by a currently logged in sponsor.
        [HttpGet("drivers")]
        public async Task<IActionResult> GetListDrivers()
        {
            //get user Id
            var userId = User.FindFirst(AppClaims.UserId)?.Value;

            //query database for list of drivers associated with current driver
            var drivers = await _context.Sponsors
                .Where(x => x.AppUserId == userId)
                .SelectMany(x => x.Drivers)
                .Include(e => e.AppUser)
                .ToListAsync();

            //return list of current drivers for current sponsor
            return Ok(new {drivers});
        }

        [HttpGet("all_drivers")]
        public async Task<IActionResult> GetAllDrivers()
        {
            var userId = User.FindFirst(AppClaims.UserId)?.Value;
            //query database for list of all drivers 
            /*j
            var dr = await _context.Drivers
                .SelectMany(s => s.Sponsors)
                .Where(z => z.Drivers)
                .Select(x => x.AppUser.Email)
                .ToListAsync();
                */

            var drivers = await _context.Drivers
                .Where(x => !x.Sponsors
                    .Select(c => c.AppUserId)
                    .Contains(userId))
                .Select(d => new {d.AppUser.Email, d.AppUserId})
                .ToListAsync();
            
            return Ok(new {drivers});
        }

        //Provide number of points to increment and the driverId
        //Points > 0, int
        //driverId !NULL, string
        [HttpGet("givePoints")]
        public async Task<IActionResult> GivePoints(int points, string driverId)
        {
            //get sponsor Id
            var sponsorId = User.FindFirst(AppClaims.UserId)?.Value;

            //get sponsor email
            var sponsorUser = await _context
                .AppUser
                .SingleOrDefaultAsync(x => x.Id == sponsorId);
            var sponsorEmail = sponsorUser.Email;

            //get current points for driver
            var num = await _context.Drivers
                .SingleOrDefaultAsync(x => x.AppUserId == driverId);

            //increment the points attribute
            if (points > 0)
            {
                num.Points += points;
                await _context.SaveChangesAsync();
            }

            //set message to notify driver of incremented points
            var message = $"You have been awarded {points} points by {sponsorEmail}!";

            //add message to table
            var messages = new Message() { AppUserId = driverId, Messages = message };
            _context.Add(messages);
            await _context.SaveChangesAsync();

            return Ok();
        }

        //Provide number of points to decrement and the driverId
        //Points > 0, int
        //driverId !NULL, string
        [HttpGet("takePoints")]
        public async Task<IActionResult> TakePoints(int points, string driverId)
        {
            var sponsorId = User.FindFirst(AppClaims.UserId)?.Value;

            //get sponsor email
            var sponsorUser = await _context
                .AppUser
                .SingleOrDefaultAsync(x => x.Id == sponsorId);
            var sponsorEmail = sponsorUser.Email;

            //get current points for driver
            var num = await _context.Drivers
                .SingleOrDefaultAsync(x => x.AppUserId == driverId);

            //decrement the points attribute
            if (points > 0)
            {
                num.Points -= points;
                await _context.SaveChangesAsync();
            }

            //set message to notify driver of decremented points
            var message = $"{sponsorEmail} has removed {points} points.";

            //add message to message table
            var messages = new Message() { AppUserId = driverId, Messages = message };
            _context.Add(messages);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("claim_driver")]
        public async Task<IActionResult> ClaimDriver([FromQuery]string driverId)
        {
            //get sponsor Id
            var sponsorId = User.FindFirst(AppClaims.UserId)?.Value;

            //get the driver
            var driver = await _context.Drivers.FirstOrDefaultAsync(x => x.AppUserId == driverId);
            if (driver is null)
            {
                return BadRequest("Driver does not exist!");
            }

            //get the sponsor
            var sponsorList = await _context.Sponsors.FirstOrDefaultAsync(x => x.AppUserId == sponsorId);
            if (sponsorList is null)
            {
                return BadRequest("Sponsor does not exist!");
            }

            //add driver to sponsors list
            sponsorList.Drivers.Add(driver);

            //save changes
            //_context.Add(sponsorList);
            await _context.SaveChangesAsync();

            //get sponsor email
            var sponsorUser = await _context
                .AppUser
                .SingleOrDefaultAsync(x => x.Id == sponsorId);
            var sponsorEmail = sponsorUser.Email;

            //set message to notify driver of decremented points
            var message = $"{sponsorEmail} has claimed you as a driver!";

            //add message to message table
            var messages = new Message() { AppUserId = driverId, Messages = message };
            _context.Add(messages);
            await _context.SaveChangesAsync();

            return Ok("Driver added successfully!");
        }

        [HttpPut("remove_driver")]
        public async Task<IActionResult> RemoveDriver([FromQuery]string driverId)
        {
            //get sponsor Id
            var sponsorId = User.FindFirst(AppClaims.UserId)?.Value;

            //get the driver
            var driver = await _context.Drivers.Include(s => s.Sponsors).FirstOrDefaultAsync(x => x.AppUserId == driverId);
            if (driver is null)
            {
                return BadRequest("Driver does not exist!");
            }

            //get the sponsor
            var sponsorList = await _context.Sponsors
                .Include(s => s.Drivers)
                .FirstOrDefaultAsync(x => x.AppUserId == sponsorId);
            if (sponsorList is null)
            {
                return BadRequest("Sponsor does not exist!");
            }

            var drivers = await _context.Sponsors
                .SelectMany(x => x.Drivers)
                .ToListAsync();

            sponsorList.Drivers.Remove(driver);

            //save changes
            //_context.Add(sponsorList);
            await _context.SaveChangesAsync();

            //get sponsor email
            var sponsorUser = await _context.AppUser
                .SingleOrDefaultAsync(x => x.Id == sponsorId);
            
            var sponsorEmail = sponsorUser.Email;

            //set message to notify driver of decremented points
            var message = $"{sponsorEmail} has removed you as a driver!";

            //add message to message table
            var messages = new Message() { AppUserId = driverId, Messages = message };
            _context.Add(messages);
            await _context.SaveChangesAsync();

            return Ok("Driver added successfully!");
        }
        [HttpGet("getNotifications")]
        public async Task<IActionResult> GetNotifications()
        {
            //get user Id
            var userId = User.FindFirst(AppClaims.UserId)?.Value;

            //query db for list of notifications for current driver
            var driverNotifications = await _context.Messages
                .Where(m => m.AppUserId == userId)
                .Select(m => m.Messages)
                .ToListAsync();

            var model = new DriverNotificationModel() { Notifications = driverNotifications };

            //return list of notifications */
            return Ok(model);
        }

        [HttpPut("add_to_catalog")]
        public async Task<IActionResult> AddToCatalog([FromBody] AddToCatalogModel productId)
        {
            var add_product = productId.ProductId;

            var userId = User.FindFirst(AppClaims.UserId)?.Value;

            var sponsor = await _context
                .Sponsors
                .SingleOrDefaultAsync(x => x.AppUserId == userId);

            var catalog = await _context
                .Catalogs
                .SingleOrDefaultAsync(x => x.Id == sponsor.CatalogId);

            var product = await _context
                .Products
                .SingleOrDefaultAsync(x => x.ProductId == add_product);


            catalog.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok("Product Added to catalog");

        }

        [HttpPost("remove_from_catalog")]
        public async Task<IActionResult> RemoveFromCatalog([FromBody] AddToCatalogModel productId)
        {

            var remove_product = productId.ProductId;

            var userId = User.FindFirst(AppClaims.UserId)?.Value;

            var sponsor = await _context
                .Sponsors
                .SingleOrDefaultAsync(x => x.AppUserId == userId);

            var catalog = await _context
                .Catalogs
                .Include(x => x.Products)
                .SingleOrDefaultAsync(x => x.Id == sponsor.CatalogId);

            var product = await _context
                .Products
                .SingleOrDefaultAsync(x => x.ProductId == remove_product);


            catalog.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok("Product removed from catalog");
        }

        [HttpGet("get_catalog")]
        public async Task<IActionResult> SponsorCatalog()
        {

            //get user Id
            var userId = User.FindFirst(AppClaims.UserId)?.Value;

            var sponsor = await _context
                .Sponsors
                .SingleOrDefaultAsync(x => x.AppUserId == userId);

            //query database for list of sponsors associated with current driver
            var catalog = await _context.Catalogs
                .Where(x => x.Id == sponsor.CatalogId)
                .SelectMany(x => x.Products)
                .ToListAsync();

            //create new model and populate it
            var model = new DriverCatalogModel();
            for (int i = 0; i < catalog.Count(); i++)
            {
                var temp = catalog[i];
                model.Products.Add(temp);
            }

           
            return Ok(catalog);

        }
    }
}
