using MediatR;

namespace NegotiationAPI.Application.Common.Events
{
    public class NegotiationStatusChangedEvent : INotification
    {
        public Guid NegotiationId { get; }
        public string NewStatus { get; }

        public NegotiationStatusChangedEvent(Guid negotiationId, string newStatus)
        {
            NegotiationId = negotiationId;
            NewStatus = newStatus;
        }
    }
}
