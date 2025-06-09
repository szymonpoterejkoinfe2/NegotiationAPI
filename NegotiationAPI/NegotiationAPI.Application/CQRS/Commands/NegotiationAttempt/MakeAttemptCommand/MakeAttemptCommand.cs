using ErrorOr;
using MediatR;

namespace NegotiationAPI.Application.CQRS.Commands.NegotiationAttempt.MakeAttemptCommand
{
    public record MakeAttemptCommand
        (
            Guid NegotiationId,
            double ProposedPrice
        )
        : IRequest<ErrorOr<Guid>>;
}
