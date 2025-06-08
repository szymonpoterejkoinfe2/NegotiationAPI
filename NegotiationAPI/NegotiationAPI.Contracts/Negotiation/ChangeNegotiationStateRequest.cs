using NegotiationAPI.Domain.Enums;

namespace NegotiationAPI.Contracts.Negotiation
{
    public record ChangeNegotiationStateRequest
    (
        NegotiationStatus NegotiationStatus    
    );
}
