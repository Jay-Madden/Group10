using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Claims;
using Group10.Data.Models;

namespace Group10.API.Security
{
    public interface IJwtAuthManager
    {
        public string GenerateToken(IEnumerable<Claim> claims, DateTime now);
    }
}