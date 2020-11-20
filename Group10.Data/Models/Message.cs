using System;
using System.Collections.Generic;

namespace Group10.Data.Models
{
    public class Message
    {

        public int Id { get; set; }

        public string AppUserId { get; set; } = null!;

        public AppUser AppUser { get; set; } = null!;
        public string Messages { get; set; } = null!;




    }

}
