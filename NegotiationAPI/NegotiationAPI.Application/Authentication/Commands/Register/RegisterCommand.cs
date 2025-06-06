using MediatR;
using NegotiationAPI.Application.Services.Authentication;

namespace NegotiationAPI.Application.Authentication.Commands.Register
{
    public record RegisterCommand
        (
        string FristName,
        string LastName,
        string Email,
        string Phone,
        string Password
        ) : IRequest<AuthenticationResult>;
}
