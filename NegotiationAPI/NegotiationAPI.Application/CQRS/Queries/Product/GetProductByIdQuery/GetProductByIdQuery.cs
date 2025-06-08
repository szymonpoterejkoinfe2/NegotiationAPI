using ErrorOr;
using MediatR;

namespace NegotiationAPI.Application.Authentication.Queries.Product.GetProductByIdQuery
{
    public record GetProductByIdQuery
    (
        Guid Id
    ) : IRequest<ErrorOr<Domain.Entities.Product>>;
}
