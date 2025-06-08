using FluentValidation;

namespace NegotiationAPI.Application.CQRS.Commands.Negotiation.AddNegotiationCommand
{
    public class AddNegotiationCommandValidation : AbstractValidator<AddNegotiationCommand>
    {
        public AddNegotiationCommandValidation()
        {
                RuleFor(x => x.ProductId).NotEmpty();   
        }
    }
}
