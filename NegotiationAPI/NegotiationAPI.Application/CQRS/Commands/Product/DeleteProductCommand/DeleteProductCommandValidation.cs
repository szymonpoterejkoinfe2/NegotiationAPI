using FluentValidation;

namespace NegotiationAPI.Application.CQRS.Commands.Product.DeleteProductCommand
{
    public class DeleteProductCommandValidation : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidation() 
        { 
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
