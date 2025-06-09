using ErrorOr;
using MediatR;

namespace NegotiationAPI.Application.CQRS.Commands.NegotiationAttempt.RejectAttemptCommand
{
    public record RejectAttemptCommand
        (
            Guid AttemptId
        ) : IRequest<ErrorOr<Success>>;

}
