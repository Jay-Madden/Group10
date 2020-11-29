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
    public class DriverController : ControllerBase
    {

        private readonly Group10Context _context;

        public DriverController(Group10Context context)
        {
            _context = context;
        }

        //Returns a list of current sponsors in db context.
        [HttpGet("sponsors")]
        public async Task<IActionResult> GetListSponsors()
        {
            //get user Id
            var userId = User.FindFirst(AppClaims.UserId)?.Value;

            //query database for list of sponsors associated with current driver
            var sponsors = await _context.Drivers
                .Where(x => x.AppUserId == userId)
                .SelectMany(x => x.Sponsors)
                .Select(s => new {s.AppUserId, s.AppUser.Email})
                .ToListAsync();

            //return list of current drivers for current sponsor
            return Ok(new {sponsors});
        }

        [HttpGet("all_sponsors")]
        public async Task<IActionResult> GetAllSponsors()
        {

            //query database for list all available sponsors
            var sponsors = await _context.Sponsors
                .Select(x => new { x.AppUserId, x.AppUser.Email})
                .ToListAsync();
            
            return Ok(new {sponsors});
        }

        [HttpGet("points")]
        public async Task<IActionResult> Points()
        {
            //get user Id
            var userId = User.FindFirst(AppClaims.UserId)?.Value;

            //query db for point total of current driver
            var driver = await _context.Drivers
                .SingleOrDefaultAsync(x => x.AppUserId == userId);

            var model = new DriverPointsModel() { Points = driver.Points };

            //return point
            return Ok(model);
        }

        [HttpGet("getNotifications")]
        public async Task<IActionResult> GetNotifications()
        {
            //get user Id
            var userId = User.FindFirst(AppClaims.UserId)?.Value;

            //query db for list of notifications for current driver
            var driverNotifications = await (_context.Messages
                .Where(messages => messages.AppUserId == userId)
                .Select(messages => messages.Messages))
                .ToListAsync();

            var model = new DriverNotificationModel() { Notifications = driverNotifications };

            //return list of notifications */
            return Ok(model);
        }

        [HttpPut("petition_sponsor")]
        public async Task<IActionResult> PetitionSponsor([FromQuery] string sponsorId)
        {
            //get user email
            var userId = User.FindFirst(AppClaims.UserId)?.Value;

            //get sponsor email
            var driverUser = await _context
                .AppUser
                .SingleOrDefaultAsync(x => x.Id == userId);
            var driverEmail = driverUser.Email;


            //set message to notify driver of decremented points
            var message = $"{driverEmail} wishes to join your driver roster.";

            //add message to message table
            var messages = new Message() { AppUserId = sponsorId, Messages = message };
            _context.Add(messages);
            await _context.SaveChangesAsync();

            return Ok("Petition accepted. Please wait for confirmation by your chosen sponsor.");
        }

        [HttpGet("get_sponsor(s)_catalog")]
        public async Task<IActionResult> DriverCatalog()
        {
            //get user Id
            var userId = User.FindFirst(AppClaims.UserId)?.Value;

            var items = await _context.Drivers
                .Where(x => x.AppUserId == userId)
                .SelectMany(x => x.Sponsors)
                .SelectMany(x => x.Catalog.Products)
                .ToListAsync();
            
            return Ok(new {products=items});
        }

        [HttpGet("get_orders")]
        public async Task<IActionResult> PastOrders()
        {

            //get user Id
            var userId = User.FindFirst(AppClaims.UserId)?.Value;

            var driver = await _context
                .Drivers
                .SingleOrDefaultAsync(x => x.AppUserId == userId);

            //query database for list of sponsors associated with current driver
            var order = await _context.Orders
                .Where(x => x.DriverId == driver.Id)
                .SelectMany(x => x.Products)
                .ToListAsync();

            //create new model and populate it
            var model = new DriverCatalogModel();
            for (int i = 0; i < order.Count(); i++)
            {
                var temp = order[i];
                model.Products.Add(temp);
            }


            return Ok(model);

        }
    }
}
