using ErrorOr;
using MediatR;

namespace NegotiationAPI.Application.CQRS.Commands.NegotiationAttempt.DeleteAttemptCommand
{
    public record DeleteAttemptCommand
        (
           Guid AttemptId
        ) : IRequest<ErrorOr<Success>>;
}
