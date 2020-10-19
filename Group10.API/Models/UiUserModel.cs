using System;
using Group10.Data.Enums;

namespace Group10.API.Models
{
    public class UiUserModel
    {
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;
        
        public Uri Picture { get; set; } = null!;

        public AppRoles Role { get; set; } 

    }
}