using NegotiationAPI.Domain.Enums;

namespace NegotiationAPI.Domain.Entities
{
    public class NegotiationAttempt
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public double ProposedPrice { get; set; }
        public DateTime ProposedAt { get; set; } = DateTime.UtcNow;
        public NegotiationResult Result { get; set; } = NegotiationResult.AwaitingResponse;
    }
}
