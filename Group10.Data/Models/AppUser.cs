using Microsoft.AspNetCore.Identity;

namespace Group10.Data.Models
{
    public class AppUser : IdentityUser
    {
        public string AuthId { get; set; } = null!;
    }
}