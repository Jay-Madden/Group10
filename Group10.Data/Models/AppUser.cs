using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Group10.Data.Models
{
    public class AppUser : IdentityUser
    {
        public string AuthId { get; set; } = null!;
    }
}