using NegotiationAPI.Domain.Enums;


namespace NegotiationAPI.Infrastructure.Persistance.Entities
{
    public class NegotiationAttemptEntity
    {
        public Guid Id { get; set; }
        public double ProposedPrice { get; set; }
        public DateTime ProposedAt { get; set; }
        public NegotiationResult Result { get; set; }
    }
}
