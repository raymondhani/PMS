using ErrorOr;
using PMS.Application.Common.Errors;

namespace PMS.Application.Services.Authentication
{
    public interface IAuthenticationService
    {
        ErrorOr<AuthenticationResult> Login(string username, string password);
        ErrorOr<AuthenticationResult> Register(string FirstName, string LastName, string Email, string Password);

    }
}
    