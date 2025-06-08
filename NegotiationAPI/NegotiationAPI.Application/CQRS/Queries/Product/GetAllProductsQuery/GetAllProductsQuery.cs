using ErrorOr;
using MediatR;

namespace NegotiationAPI.Application.Authentication.Queries.Product.GetAllProductsQuery
{
    public record GetAllProductsQuery
    (
    ) : IRequest<ErrorOr<List<Domain.Entities.Product>>>;
}
