using FluentValidation;

namespace NegotiationAPI.Application.CQRS.Commands.Negotiation.ChangeNegotiationStatusCommand
{
    public class ChangeNegotiationStatusCommandValidation : AbstractValidator<ChangeNegotiationStatusCommand>
    {
        public ChangeNegotiationStatusCommandValidation() 
        {
            RuleFor(x => x.NegotiationId).NotEmpty();
            RuleFor(x => x.Status)
            .IsInEnum();
        }
    }
}
