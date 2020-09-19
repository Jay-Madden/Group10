using System;
using System.Collections.Generic;

namespace Group10.Data.Models
{
    public class Catalog
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        
        public List<Product> Products { get; set; } = null!;
        public List<Sponsor> Sponsors { get; set; } = null!;
    }
}
