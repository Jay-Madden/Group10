using System;
using System.Collections.Generic;

namespace Group10.Data.Models
{
    public class Driver
    {
        public int Id { get; set; }
        public int Points { get; set; }
        public List<Order> Orders { get; set; } = null!;
        
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; } = null!;

        public int SponsorId { get; set; }
        public Sponsor Sponsor { get; set; } = null!;
    }
}