using ErrorOr;
using MediatR;

namespace NegotiationAPI.Application.CQRS.Queries.NegotiationAttempt.GetPendingAttemptsQuery
{
    public record GetPendingAttemptsQuery () : IRequest<ErrorOr<List<Domain.Entities.NegotiationAttempt>>>;
}
