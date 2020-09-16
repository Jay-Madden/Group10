using System;
using System.Collections.Generic;

namespace Group10.Data.Models
{
    public class Driver
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int Points { get; set; }
        public List<Claims> Claims { get; set; }
        public List<Order> Orders { get; set; }

        public int SponsorId { get; set; }
        public Sponsor Sponsor { get; set; }

    }
}