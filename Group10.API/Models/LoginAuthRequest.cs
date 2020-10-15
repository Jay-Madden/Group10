namespace Group10.API.Models
{
    public class LoginAuthRequest
    {
        public string AccessToken { get; set; } = null!;

        public GoogleUserInfo GoogleUserInfo { get; set; } = null!;
    }
}