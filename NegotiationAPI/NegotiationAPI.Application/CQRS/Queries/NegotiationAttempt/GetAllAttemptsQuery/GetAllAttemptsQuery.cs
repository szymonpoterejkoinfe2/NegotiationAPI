using ErrorOr;
using MediatR;

namespace NegotiationAPI.Application.CQRS.Queries.NegotiationAttempt.GetAllAttemptsQuery
{
    public record GetAllAttemptsQuery() :IRequest<ErrorOr<List<Domain.Entities.NegotiationAttempt>>>;
}
