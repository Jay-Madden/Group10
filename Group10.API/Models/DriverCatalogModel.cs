using System;
using Group10.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group10.API.Models
{
    public class DriverCatalogModel
    {

        public List<Product> Products { get; set; } = new List<Product>();

    }
}
