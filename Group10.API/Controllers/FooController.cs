using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Group10.Data.Contexts;
using Group10.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;


namespace Group10.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class FooController : ControllerBase
    {
        private readonly ILogger<FooController> _logger;
        private readonly Group10Context _context;
        private readonly UserManager<AppUser> _userManager;

        public FooController(ILogger<FooController> logger,
            Group10Context context,
            UserManager<AppUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }
        
        [HttpGet("1")]
        public ActionResult<string> Get(int id)
        {
            var foo = User.Identity?.Name;
            return "hi";
        }

        [HttpGet]
        [Authorize]
        public async Task<Foo> Get()
        {
            //await _context.AddAsync(new Catalog() {Id = 1, Name = "test"});
            //await _context.SaveChangesAsync();
            //var result = await _userManager.CreateAsync(new AppUser {Name = "test", Email = "a@gmail.com"});

            await _context.SaveChangesAsync();
                
            return new Foo{ bar = JsonSerializer.Serialize(_context.Users.ToList())};
        }
    }
}