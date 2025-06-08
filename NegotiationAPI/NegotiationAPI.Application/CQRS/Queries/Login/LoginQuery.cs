using ErrorOr;
using MediatR;
using NegotiationAPI.Application.Services.Authentication;

namespace NegotiationAPI.Application.Authentication.Queries.Login
{
    public record LoginQuery(
        string Email,
        string Password
        ) : IRequest<ErrorOr<AuthenticationResult>>;


}
