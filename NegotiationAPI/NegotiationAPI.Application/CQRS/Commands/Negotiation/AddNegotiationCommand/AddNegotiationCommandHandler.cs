using ErrorOr;
using MediatR;
using NegotiationAPI.Application.Common.Interfaces.Persistance;
using NegotiationAPI.Domain.Errors;

namespace NegotiationAPI.Application.CQRS.Commands.Negotiation.AddNegotiationCommand
{
    public class AddNegotiationCommandHandler : IRequestHandler<AddNegotiationCommand, ErrorOr<Domain.Entities.Negotiation>>
    {
        private readonly INegotiationRepository _negotiationRepository;
        private readonly IProductRepository _productRepository;

        public AddNegotiationCommandHandler(INegotiationRepository negotiationRepository, IProductRepository productRepository)
        {
            _negotiationRepository = negotiationRepository;
            _productRepository = productRepository;
        }

        public async Task<ErrorOr<Domain.Entities.Negotiation>> Handle(AddNegotiationCommand command, CancellationToken cancellationToken)
        {
            var productId = command.ProductId;

            var product = _productRepository.GetProductById(productId);

            if (product is null)
            {
                return Errors.Product.NoProductWithGivenId;
            }

            var negotiation = new Domain.Entities.Negotiation()
            {
                ProductId = productId
            };

            _negotiationRepository.AddNegotiation(negotiation);


            return negotiation;
        }
    }
}
