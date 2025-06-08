using ErrorOr;
using MediatR;
using NegotiationAPI.Domain.Enums;

namespace NegotiationAPI.Application.CQRS.Commands.Negotiation.ChangeNegotiationStatusCommand
{
    public record ChangeNegotiationStatusCommand
        (
        Guid NegotiationId,
        NegotiationStatus Status
        ) : IRequest<ErrorOr<NegotiationAPI.Domain.Entities.Negotiation>>;
    
}
