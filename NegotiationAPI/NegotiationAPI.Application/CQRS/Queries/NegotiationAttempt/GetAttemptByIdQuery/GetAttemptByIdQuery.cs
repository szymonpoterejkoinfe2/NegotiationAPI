using ErrorOr;
using MediatR;

namespace NegotiationAPI.Application.CQRS.Queries.NegotiationAttempt.GetAttemptByIdQuery
{
    public record GetAttemptByIdQuery
        (
            Guid AttemptId
        ) : IRequest<ErrorOr<Domain.Entities.NegotiationAttempt?>>;
}
