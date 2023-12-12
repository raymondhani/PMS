using ErrorOr;
using MediatR;
using PMS.Application.Authentication.Common;
using PMS.Application.Authentication.Queries.Login;
using PMS.Application.Common.Interfaces.Authentication;
using PMS.Application.Common.Interfaces.Persistence;
using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Queries.Login.Login
{
    public class LoginQueryHandler :
        IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        public readonly IJwtTokenGenerator _jwtTokenGenerator;
        public readonly IUserRepository _userRepository;

        public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            // 1 - Validate the user exists
            if (_userRepository.GetUserByEmail(query.Email) is not User user)
            {
                return Domain.Common.Errors.Errors.Authentication.InvalidCredentials;
            }

            // 2 - Validate the password is correct
            if (user.Password != query.Password)
            {
                return Domain.Common.Errors.Errors.Authentication.InvalidCredentials;
            }

            // 3 - Create the JWT Token and return it to the user  
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }
    }
}
