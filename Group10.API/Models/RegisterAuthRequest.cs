namespace Group10.API.Models
{
    public class RegisterAuthRequest
    {
        public string AccessToken { get; set; } = null!;
        
        public string UserRole { get; set; } = null!;
        
        public GoogleUserInfo GoogleUserInfo { get; set; } = null!;
    }
}