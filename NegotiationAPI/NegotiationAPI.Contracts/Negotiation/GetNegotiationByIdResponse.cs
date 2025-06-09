using NegotiationAPI.Domain.Entities;
using NegotiationAPI.Domain.Enums;

namespace NegotiationAPI.Contracts.Negotiation
{
    public record GetNegotiationByIdResponse (
         Guid Id,
         Guid ProductId,
         List<Domain.Entities.NegotiationAttempt> Attempts,
         NegotiationStatus Status,
         DateTime CreatedAt,
         DateTime? LastRejectedAt
        );
    
}
