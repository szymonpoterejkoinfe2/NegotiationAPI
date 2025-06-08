using ErrorOr;
using MediatR;

namespace NegotiationAPI.Application.CQRS.Commands.Negotiation.AddNegotiationCommand
{
    public record AddNegotiationCommand 
    (
        Guid ProductId
    ) : IRequest<ErrorOr<Domain.Entities.Negotiation>>;
}
