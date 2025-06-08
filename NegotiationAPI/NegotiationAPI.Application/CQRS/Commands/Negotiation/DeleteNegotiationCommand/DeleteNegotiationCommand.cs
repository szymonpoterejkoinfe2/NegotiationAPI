using ErrorOr;
using MediatR;

namespace NegotiationAPI.Application.CQRS.Commands.Negotiation.DeleteNegotiationCommand
{
    public record DeleteNegotiationCommand
        (
            Guid NegotiationId
        ) : IRequest<ErrorOr<Success>>;
    
}
