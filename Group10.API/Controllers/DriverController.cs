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

            //query database for list of drivers associated with current driver
            var sponsors = await _context.Drivers
                .Where(x => x.AppUserId == userId)
                .SelectMany(x => x.Sponsors)
                .ToListAsync();

            //create new model and populate it
            var model = new SponsorListModel();
            for (int i = 0; i < sponsors.Count(); i++)
            {
                var temp = sponsors[i];
                model.Sponsors.Add(temp.AppUserId);
            }

            //return list of current drivers for current sponsor
            return Ok(model);
        }

        [HttpGet("all_sponsors")]
        public async Task<IActionResult> GetAllSponsors()
        {
            //get user Id
            var userId = User.FindFirst(AppClaims.UserId)?.Value;

            //query database for list all available sponsors
            var sponsors = await _context.Sponsors
                .ToListAsync();

            //create new model and populate it
            var model = new DriverListModel();
            for (int i = 0; i < sponsors.Count(); i++)
            {
                var temp = sponsors[i];
                model.DriverIds.Add(temp.AppUserId);
            }

            //return list of current drivers for current sponsor
            return Ok(model);
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
            var driverNotifications = await (from messages in _context.Messages
                                             where messages.AppUserId == userId
                                             select messages.Messages).ToListAsync();

            var model = new DriverNotificationModel() { Notifications = driverNotifications };

            //return list of notifications */
            return Ok(model);
        }

        [HttpPut("petition_sponsor")]
        public async Task<IActionResult> PetitionSponsor(string sponsorId)
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

            //query database for list of sponsors associated with current driver
            var sponsors = await _context.Drivers
                .Where(x => x.AppUserId == userId)
                .SelectMany(x => x.Sponsors)
                .ToListAsync();

            //create new model and populate it
            var model = new SponsorListModel();
            for (int i = 0; i < sponsors.Count(); i++)
            {
                var temp = sponsors[i];
                model.Sponsors.Add(temp.AppUserId);
            }

            var sponsorList = model.Sponsors;
            var driverCatalog = new DriverCatalogModel();

            for(int i = 0; i < sponsorList.Count(); i++)
            {
                var AppUserId = sponsorList[i];

                var sponsor = await _context
                    .Sponsors
                    .SingleOrDefaultAsync(x => x.AppUserId == AppUserId);

                var products = await _context.Catalogs
                .Where(x => x.Id == sponsor.CatalogId)
                .SelectMany(x => x.Products)
                .ToListAsync();

                for(int j = 0; j < products.Count(); j++)
                {
                    driverCatalog.Products.Add(products[j]);
                }
                
            }

            return Ok(driverCatalog);
            
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
                .Where(x => x.Id == driver.Id)
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
