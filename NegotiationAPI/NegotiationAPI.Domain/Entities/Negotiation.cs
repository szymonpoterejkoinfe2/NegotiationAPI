using NegotiationAPI.Domain.Enums;

namespace NegotiationAPI.Domain.Entities
{
    public class Negotiation
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ProductId { get; set; }
        public List<NegotiationAttempt> Attempts { get; set; } = new();
        public NegotiationStatus Status { get; set; } = Domain.Enums.NegotiationStatus.Waiting;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastRejectedAt { get; set; } = null;

        public bool CanProposeNewPrice() =>
        Status == NegotiationStatus.Waiting &&
        Attempts.Count < 3 &&
        (LastRejectedAt == null || DateTime.UtcNow - LastRejectedAt < TimeSpan.FromDays(7));
    }
}
