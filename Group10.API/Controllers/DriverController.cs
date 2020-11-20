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

            //query database for list of sponsors for current driver
            var list = await _context.Drivers
                .Where(x => x.AppUser.Id == userId)
                .SelectMany(x => x.Sponsors)
                .ToListAsync();
            
            //return list of sponsors
            return Ok();
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
            return Ok();
        }



    }
}
