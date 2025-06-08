using NegotiationAPI.Domain.Enums;

namespace NegotiationAPI.Domain.Entities
{
    public class NegotiationAttempt
    {
        public Guid Id { get; set; }
        public double ProposedPrice { get; set; }
        public DateTime ProposedAt { get; set; }
        public NegotiationResult Result { get; set; }
    }
}
