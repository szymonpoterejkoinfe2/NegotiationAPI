using NegotiationAPI.Domain.Entities;

namespace NegotiationAPI.Application.Services.Authentication
{
    public record AuthenticationResult
     (
        Employee Employee,
        string Token
    );
}
