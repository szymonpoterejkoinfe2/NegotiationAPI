using ErrorOr;
using MediatR;

namespace NegotiationAPI.Application.CQRS.Commands.NegotiationAttempt.AcceptAttemptCommand
{
    public record AcceptAttemptCommand
        (
        Guid AttemptId
        ) : IRequest<ErrorOr<Success>>;
}
