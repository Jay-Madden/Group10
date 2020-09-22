using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group10.Data.Contexts;
using Group10.Data.Models;


namespace Group10.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class FooController : ControllerBase
    {
        private readonly ILogger<FooController> _logger;
        private readonly Group10Context _context;

        public FooController(ILogger<FooController> logger, Group10Context context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<Foo> Get()
        {
            await _context.AddAsync(new Catalog() {Id = 1, Name = "test"});
            await _context.SaveChangesAsync();
            return new Foo { bar = 4 };
        }
    }
}