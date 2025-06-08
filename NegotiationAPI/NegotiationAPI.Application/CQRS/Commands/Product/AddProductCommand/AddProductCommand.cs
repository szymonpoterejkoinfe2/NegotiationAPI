using ErrorOr;
using MediatR;

namespace NegotiationAPI.Application.CQRS.Commands.Product.AddProductCommand
{
    public record AddProductCommand 
    (
        string Name,
        double Price,
        string Description 
    ) : IRequest<ErrorOr<Success>>;
}
