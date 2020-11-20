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
                .ToListAsync();

            //create new model and populate it
            var model = new DriverListModel
            {
                DriverIds = drivers.Select(x => x.AppUserId).ToList()
            };

            //return list of current drivers for current sponsor
            return Ok(model);
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

            //get current points for driver
            var num = await _context.Drivers
                .SingleOrDefaultAsync(x => x.AppUserId == driverId);

            //increment the points attribute
            if (points <= 0)
            {
                return BadRequest("Cannot give negative points");
            }
            
            num.Points += points;

            //set message to notify driver of incremented points
            var message = $"You have been awarded {points} points by {sponsorUser.Email}!";

            //add message to table
            var messages = new Message()
            {
                AppUserId = driverId,
                Messages = message
            };
            _context.Add(messages);
            
            await _context.SaveChangesAsync();

            return Ok();
        }

        /// Provide number of points to decrement and the driverId
        /// Points > 0, int
        /// driverId !NULL, string
        [HttpGet("takePoints")]
        public async Task<IActionResult> TakePoints(int points, string driverId)
        {
            var sponsorId = User.FindFirst(AppClaims.UserId)?.Value;

            //get sponsor email
            var sponsorUser = await _context.AppUser
                .SingleOrDefaultAsync(x => x.Id == sponsorId);

            //get current points for driver
            var num = await _context.Drivers
                .SingleOrDefaultAsync(x => x.AppUserId == driverId);

            //decrement the points attribute
            if (points <= 0)
            {
                return BadRequest("Cannot remove negative points");
            }
            num.Points -= points;

            //set message to notify driver of decremented points
            var message = $"{sponsorUser.Email} has removed {points} points.";

            //add message to message table
            var messages = new Message()
            {
                AppUserId = driverId, 
                Messages = message
            };
            _context.Add(messages);
            
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("claim_driver")]
        public async Task<IActionResult> ClaimDriver(string driverId)
        {

            //get sponsor Id
            var sponsorId =  User.FindFirst(AppClaims.UserId)?.Value;

            //get the driver
            var driver = await _context.Drivers.FirstOrDefaultAsync(x => x.AppUserId == driverId);
            if(driver is null)
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

            //get sponsor name
            var sponsorName = await _context.Sponsors
                .SingleOrDefaultAsync(x => x.AppUserId == sponsorId);

            //set message to notify driver of decremented points
            var message = $"{sponsorName} has claimed you as a driver!";

            //add message to message table
            var messages = new Message()
            {
                AppUserId = driverId,
                Messages = message
            };
            _context.Add(messages);
            
            await _context.SaveChangesAsync();

            return Ok("Driver added successfully!");
        }

    }
}
