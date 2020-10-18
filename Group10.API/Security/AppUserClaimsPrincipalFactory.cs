using System.Security.Claims;
using System.Threading.Tasks;
using Group10.API.Enums;
using Group10.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Group10.API.Security
{
    /*
    public class AppUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser> 
    {     
        public AppUserClaimsPrincipalFactory(
            UserManager<AppUser> userManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, optionsAccessor)     
        {
        }
      
        public async override Task<ClaimsPrincipal> CreateAsync(AppUser user)
        {
            var principal = (await base.CreateAsync(user));
            ((ClaimsIdentity)principal.Identity!).AddClaim(new Claim(AppClaims.UserId, "test"));
            return principal;
        }
    }
    */
}