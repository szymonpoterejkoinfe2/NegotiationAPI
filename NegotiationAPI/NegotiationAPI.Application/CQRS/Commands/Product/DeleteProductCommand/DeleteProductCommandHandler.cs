using ErrorOr;
using MediatR;
using NegotiationAPI.Application.Common.Interfaces.Persistance;
using NegotiationAPI.Domain.Errors;

namespace NegotiationAPI.Application.CQRS.Commands.Product.DeleteProductCommand
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ErrorOr<Success>>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IProductRepository repository)
        {
            _productRepository = repository;
        }

        public async Task<ErrorOr<Success>> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var id = command.Id;

            var productToDelete = _productRepository.GetProductById(id);
            if (productToDelete is null)
            {
                return Errors.Product.NoProductWithGivenId;
            }

            var isDeleted = _productRepository.DeleteProduct(productToDelete);
            if(!isDeleted)
            {
                return Errors.Product.NotDeleted;
            }

            return new ErrorOr<Success>();
        }
    }
}
