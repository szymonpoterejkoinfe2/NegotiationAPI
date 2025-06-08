using ErrorOr;
using MediatR;

namespace NegotiationAPI.Application.CQRS.Queries.Negotiation.GetNegotiationById
{
    public record GetNegotiationByIdQuery
        (
        Guid NegotiationId
        ) : IRequest<ErrorOr<Domain.Entities.Negotiation>>;
   


}
