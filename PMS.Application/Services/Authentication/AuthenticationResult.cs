using PMS.Domain.Entities;

namespace PMS.Application.Services.Authentication
{
    public record AuthenticationResult(
        User user,
        string Token
        );
}
