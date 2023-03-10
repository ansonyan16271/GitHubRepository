namespace Advance.NET7.MinimalApi.JwtDentity.Utility
{
    public class JWTTokenOptions
    {
        public string? Audience { get; set; }

        public string? SecurityKey { get; set; }

        public string? Issuer { get; set; }
    }
}
