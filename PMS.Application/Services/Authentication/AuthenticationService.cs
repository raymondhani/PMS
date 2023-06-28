using ErrorOr;
using PMS.Application.Common.Interfaces.Authentication;
using PMS.Application.Common.Interfaces.Persistence;
using PMS.Domain.Entities;
using PMS.Domain.Common.Errors;
using System.ComponentModel;

namespace PMS.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        public readonly IJwtTokenGenerator _jwtTokenGenerator;
        public readonly IUserRepository _userRepository;
        public AuthenticationService(
            IJwtTokenGenerator jwtTokenGenerator,
            IUserRepository userRepository = null!)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public ErrorOr<AuthenticationResult> Login(string email, string password)
        {
            // 1 - Validate the user exists
            if(_userRepository.GetUserByEmail(email) is not User user)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            // 2 - Validate the password is correct
            if(user.Password !=password)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            // 3 - Create the JWT Token and return it to the user  
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }

        public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
        {
            // 1- Validate the user doesn't exist
            if(_userRepository.GetUserByEmail(email) is not null) 
            {
                return Errors.User.DuplicateEmail;
            }

            // 2 - create user (generate Unique ID)
            var user = new User
            {
                FirstName= firstName,
                LastName= lastName,
                Email= email,
                Password= password
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
