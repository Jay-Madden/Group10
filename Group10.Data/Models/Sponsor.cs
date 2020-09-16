using System;
using System.Collections.Generic;

namespace Group10.Data.Models
{
    using Models;

    public class Sponsor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public List<Driver> Drivers {get; set;}

        public int CatalogId {get; set;}
        public Catalog Catalog {get; set;}
    }
}