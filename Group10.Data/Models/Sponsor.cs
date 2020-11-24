using System;
using System.Collections.Generic;

namespace Group10.Data.Models
{
    public class Sponsor
    {
        public int Id { get; set; }

        public List<Driver> Drivers { get; set; } = new List<Driver>();

        public string AppUserId { get; set; } = null!;
        public AppUser AppUser { get; set; } = null!;

        public int? CatalogId { get; set; }
        public Catalog Catalog { get; set; } = null!;
    }
}