using ErrorOr;
using MediatR;

namespace NegotiationAPI.Application.CQRS.Queries.Negotiation.GetAllNegotiationsQuery
{
    public record GetAllNegotiationsQuery(
        
        ) : IRequest<ErrorOr<List<Domain.Entities.Negotiation>>>;
    
}
