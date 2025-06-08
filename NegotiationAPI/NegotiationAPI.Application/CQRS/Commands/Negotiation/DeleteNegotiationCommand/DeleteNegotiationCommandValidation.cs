using FluentValidation;

namespace NegotiationAPI.Application.CQRS.Commands.Negotiation.DeleteNegotiationCommand
{
    public class DeleteNegotiationCommandValidation : AbstractValidator<DeleteNegotiationCommand>
    {
        public DeleteNegotiationCommandValidation() 
        {
            RuleFor(x => x.NegotiationId).NotEmpty();
        }
    }
}
