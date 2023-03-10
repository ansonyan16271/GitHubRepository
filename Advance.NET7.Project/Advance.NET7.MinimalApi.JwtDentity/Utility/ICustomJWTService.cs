namespace Advance.NET7.MinimalApi.JwtDentity.Utility
{
    public interface ICustomJWTService
    {
        string GetToken(CurrentUser user);
    }
}
