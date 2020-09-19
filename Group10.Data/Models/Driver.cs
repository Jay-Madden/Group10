using System;
using System.Collections.Generic;

namespace Group10.Data.Models
{
    public class Driver
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Points { get; set; }
        public List<Claims> Claims { get; set; } = null!;
        public List<Order> Orders { get; set; } = null!;

        public int SponsorId { get; set; }
        public Sponsor Sponsor { get; set; } = null!;

    }
}