using System;
using System.Collections.Generic;

namespace Group10.Data.Models
{
    public class Sponsor
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; }  = null!;
        public string Email { get; set; } = null!;

        public List<Driver> Drivers { get; set; } = null!;
        
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; } = null!;

        public int CatalogId { get; set; }
        public Catalog Catalog { get; set; } = null!;
    }
}