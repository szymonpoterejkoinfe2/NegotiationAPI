using ErrorOr;
using MediatR;
using NegotiationAPI.Application.Common.Interfaces.Persistance;
using NegotiationAPI.Domain.Errors;

namespace NegotiationAPI.Application.Authentication.Queries.Product.GetProductByIdQuery
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ErrorOr<Domain.Entities.Product>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ErrorOr<Domain.Entities.Product>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var id = query.Id;
            var product = _productRepository.GetProductById(id);

            if (product is null) 
            { 
                return Errors.Product.NoProductWithGivenId;
            }

            return product;
        }
    }
}
