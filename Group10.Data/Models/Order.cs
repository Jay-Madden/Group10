using System;
using System.Collections.Generic;

namespace Group10.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<Product> Products { get; set; }

        public int DriverId { get; set; }
        public Driver Driver { get; set; }
    }
}