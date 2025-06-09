using NegotiationAPI.Domain.Entities;
using NegotiationAPI.Domain.Enums;

namespace NegotiationAPI.Application.Common.Interfaces.Persistance
{
    public interface INegotiationRepository
    {
        IEnumerable<Negotiation> GetAllNegotiations();
        Negotiation? GetNegotiationById(Guid negotioationId);
        Guid AddNegotiation(Negotiation negotiation);
        bool DeleteNegotiation(Guid negotiationId);
        Negotiation? ChangeNegotiationStatus(Guid negotiationId, NegotiationStatus status);
        bool UpdateNegotiation(Negotiation negotiation);
        Negotiation? GetNegotiationByAttemptId(Guid attemptId);
    }
}
