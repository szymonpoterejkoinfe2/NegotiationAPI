using ErrorOr;
using MediatR;
using NegotiationAPI.Application.Common.Interfaces.Persistance;
using NegotiationAPI.Domain.Errors;

namespace NegotiationAPI.Application.Authentication.Queries.Product.GetAllProductsQuery
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, ErrorOr<List<Domain.Entities.Product>>>
    {
        private readonly IProductRepository  _productRepository;

        public GetAllProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ErrorOr<List<Domain.Entities.Product>>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
        {
            var allProducts = _productRepository.GetAllProducts().ToList();

            if (allProducts.Count < 1)
            { 
                return Errors.Product.NoProducts;
            }

            return allProducts;
        }
    }
}
