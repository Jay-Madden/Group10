using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Group10.API.Enums;
using Group10.API.Models;
using Group10.API.Security;
using Group10.Data.Contexts;
using Group10.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Group10.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly Group10Context _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IJwtAuthManager _jwtAuthManager;
        private const string AccessTokenInfoUrl = "https://www.googleapis.com/oauth2/v1/tokeninfo?access_token=";

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            Group10Context context,
            IHttpClientFactory httpClientFactory,
            IJwtAuthManager jwtAuthManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _httpClientFactory = httpClientFactory;
            _jwtAuthManager = jwtAuthManager;
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> Register()
        {
            var userId = User.FindFirst(AppClaims.UserId)?.Value;
            if (userId is null)
            {
                return BadRequest("Specified UserId not found");
            }
            var user = await _context.AppUser.SingleOrDefaultAsync(x => x.Id == userId);
            if (user is null)
            {
                return BadRequest("Specified User not found");
            }
            
            return Ok(new{user});
        }
        /*
        [HttpPost("get")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] LoginAuthRequest registerRequest)
        {
            
        }
        */
        
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginAuthRequest loginRequest)
        {
            
            GoogleApiTokenInfo tokenInfo = await ValidateGoogleTokenAsync(loginRequest);

            if (tokenInfo.UserId is null)
            {
                return BadRequest("The access token was invalid");
            }
            
            var user = await _context.AppUser
                .SingleOrDefaultAsync(x => x.AuthId == tokenInfo.UserId);

            if (user is null)
            {
                var newUser = new AppUser
                {
                    AuthId = tokenInfo.UserId, 
                    UserName = loginRequest.GoogleUserInfo.name, 
                    Email = loginRequest.GoogleUserInfo.email,
                    
                };
                var createdUser = await _userManager.CreateAsync(newUser);
                return Ok(createdUser);
            }


            await _signInManager.SignInAsync(user, false);

            var token = _jwtAuthManager.GenerateToken(user, DateTime.Now);
            
            return Ok(new{ token });
        }

        private async Task<GoogleApiTokenInfo> ValidateGoogleTokenAsync(LoginAuthRequest loginRequest)
        {
            using var client = _httpClientFactory.CreateClient();

            var httpReq = await client.GetAsync($"{AccessTokenInfoUrl}{loginRequest.AccessToken}");
            var jsonResp = await httpReq.Content.ReadAsStringAsync();
            var tokenInfo = JsonSerializer.Deserialize<GoogleApiTokenInfo>(jsonResp);
            return tokenInfo!;
        }
    }
}