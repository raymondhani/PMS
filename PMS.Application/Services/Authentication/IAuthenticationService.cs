namespace PMS.Application.Services.Authentication
{
    public interface IAuthenticationService
    {
        AuthenticationResult Login(string username, string password);
        AuthenticationResult Register(string FirstName, string LastName, string Email, string Password);

    }
}
