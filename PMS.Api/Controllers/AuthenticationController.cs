using Microsoft.AspNetCore.Mvc;
using PMS.Contracts.Authentication;
using OneOf;
using PMS.Application.Common.Errors;
using ErrorOr;
using MediatR;
using PMS.Application.Authentication.Commands.Register;
using PMS.Application.Authentication.Common;
using PMS.Application.Authentication.Queries.Login;
using MapsterMapper;

namespace PMS.Api.Controllers;

[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public AuthenticationController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        
        ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

        return authResult.Match(
            authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
            errors => Problem(errors));
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginRequest request)
    {
        var query =_mapper.Map<LoginQuery>(request);

        var authResult = await _mediator.Send(query);

        return authResult.Match(
            authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
            errors => Problem(errors));

    }

    
  
}