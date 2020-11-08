using System;

namespace Group10.API.Models
{
    public class UserModel
    {
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;
        
        public Uri Picture { get; set; } = null!;

        public string Role { get; set; } = null!;

    }
}