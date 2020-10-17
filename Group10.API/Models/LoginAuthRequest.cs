namespace Group10.API.Models
{
    public class LoginAuthRequest : AuthRequest
    {
        public GoogleUserInfo GoogleUserInfo { get; } = null!;
    }
}