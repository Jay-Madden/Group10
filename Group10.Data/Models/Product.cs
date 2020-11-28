using System;
using System.Collections.Generic;

namespace Group10.Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string ProductId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string PriceUsd { get; set; } = null!;
        public string PricePts { get; set; } = null!;
        public string Description { get; set; } = null!;

        public List<Order> Orders { get; set; } = new List<Order>();

        public List<Catalog> Catalogs { get; set; } = new List<Catalog>();
    }
}