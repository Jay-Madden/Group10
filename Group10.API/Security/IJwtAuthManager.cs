using System;
using System.Security.Claims;
using Group10.Data.Models;

namespace Group10.API.Security
{
    public interface IJwtAuthManager
    {
        public string GenerateToken(AppUser user, DateTime now);
    }
}