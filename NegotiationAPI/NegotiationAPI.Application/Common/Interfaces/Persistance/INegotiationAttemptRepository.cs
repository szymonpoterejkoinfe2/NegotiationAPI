using NegotiationAPI.Domain.Entities;
using NegotiationAPI.Domain.Enums;

namespace NegotiationAPI.Application.Common.Interfaces.Persistance
{
    public interface INegotiationAttemptRepository
    {
        IEnumerable<NegotiationAttempt> GetPendingAttempts();
        IEnumerable<NegotiationAttempt> GetAttempts();
        NegotiationAttempt? GetNegotiationAttemptById(Guid negotiationAttemptId);
        bool DeleteNegotiationAttempt(Guid negotiationAttemptId);
        NegotiationAttempt? UpdateNegotiationAttemptResultState(Guid negotiationAttemptId, NegotiationResult result);
        Guid AddNegotiationAttempt(NegotiationAttempt negotiationAttempt);
    }
}
