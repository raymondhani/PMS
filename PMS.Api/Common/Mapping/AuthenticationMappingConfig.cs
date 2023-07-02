using Mapster;
using Microsoft.AspNetCore.Routing.Constraints;
using PMS.Application.Authentication.Commands.Register;
using PMS.Application.Authentication.Common;
using PMS.Application.Authentication.Queries.Login;
using PMS.Contracts.Authentication;

namespace PMS.Api.Common.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, RegisterCommand>();

            config.NewConfig<LoginRequest, LoginQuery>();

            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest, src => src.User);
        }
    }
}
