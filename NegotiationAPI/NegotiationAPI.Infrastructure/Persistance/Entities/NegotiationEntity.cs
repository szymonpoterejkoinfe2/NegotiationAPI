using NegotiationAPI.Domain.Enums;

namespace NegotiationAPI.Infrastructure.Persistance.Entities
{
    public class NegotiationEntity
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public List<NegotiationAttemptEntity> Attempts { get; set; } = new();
        public NegotiationStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastRejectedAt { get; set; }
    }
}
