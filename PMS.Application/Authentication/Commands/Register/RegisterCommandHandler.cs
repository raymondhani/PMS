using ErrorOr;
using MediatR;
using PMS.Application.Authentication.Common;
using PMS.Application.Common.Interfaces.Authentication;
using PMS.Application.Common.Interfaces.Persistence;
using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler :
        IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        public readonly IJwtTokenGenerator _jwtTokenGenerator;
        public readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            // 1- Validate the user doesn't exist
            if (_userRepository.GetUserByEmail(command.Email) is not null)
            {
                return Domain.Common.Errors.Errors.User.DuplicateEmail;
            }

            // 2 - create user (generate Unique ID)
            var user = new User
            {
                FirstName = command.FirstName,
                LastName =  command.LastName,
                Email =     command.Email,
                Password =  command.Password
            };
            _userRepository.Add(user);

            // 3- Create the JWT Token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }
    }
}
