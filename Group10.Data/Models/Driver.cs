using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Group10.Data.Models
{
    public class Driver
    {
        
        public int Id { get; set; }

        public int Points { get; set; }
        public List<Order> Orders { get; set; } = null!;

        public string AppUserId { get; set; } = null!;
        public AppUser AppUser { get; set; } = null!;

        public List<Sponsor> Sponsors { get; set; } = new List<Sponsor>();

        public List<Message> Messages { get; set; } = null!;
    }
}