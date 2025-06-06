using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using NegotiationAPI.Application.Authentication.Commands.Register;
using NegotiationAPI.Application.Authentication.Queries.Login;
using NegotiationAPI.Application.Services.Authentication;
using NegotiationAPI.Contracts.Authentication;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NegotiationAPI.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        public AuthenticationController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
        {
               
            var command = _mapper.Map<RegisterCommand>(request);
            AuthenticationResult authResult = await _mediator.Send(command);

            return Ok(_mapper.Map<AuthenticationResponse>(authResult));
               
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginRequest request)
        {
            var query = _mapper.Map<LoginQuery>(request);
            AuthenticationResult authResult = await _mediator.Send(query);

            return Ok(_mapper.Map<AuthenticationResponse>(authResult));
        }

    }
}

