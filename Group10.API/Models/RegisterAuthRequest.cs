namespace Group10.API.Models
{
    public class RegisterAuthRequest : AuthRequest
    {
        public string UserRole { get; } = null!;
    }
}