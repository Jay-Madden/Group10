using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Group10.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FooController: ControllerBase
    {
        private readonly ILogger<FooController> _logger;

        public FooController(ILogger<FooController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Foo Get()
        {
            return new Foo{ bar= 4};
        }
    }
}

