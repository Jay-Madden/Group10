using System;
using System.Collections.Generic;

namespace Group10.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PriceUsd { get; set; }
        public string PricePts { get; set; }
        public string Description { get; set; }

        public List<Order> Orders { get; set; }

        public int CatalogId { get; set; }
        public Catalog Catalog { get; set; }

    }
}