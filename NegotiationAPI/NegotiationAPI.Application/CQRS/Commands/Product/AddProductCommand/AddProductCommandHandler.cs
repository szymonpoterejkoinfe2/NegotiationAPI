using ErrorOr;
using MediatR;
using NegotiationAPI.Application.Common.Interfaces.Persistance;

namespace NegotiationAPI.Application.CQRS.Commands.Product.AddProductCommand
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, ErrorOr<Success>>
    {
        private readonly IProductRepository _productRepository;

        public AddProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ErrorOr<Success>> Handle(AddProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Domain.Entities.Product()
            { 
                Id =  Guid.NewGuid(),
                Name = command.Name,
                Price = command.Price,
                Description = command.Description
            };

            _productRepository.AddProduct(product);

            return new ErrorOr<Success>();
        }
    }
}
