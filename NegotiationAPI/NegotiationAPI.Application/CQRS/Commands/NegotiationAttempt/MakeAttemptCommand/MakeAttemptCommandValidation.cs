using FluentValidation;

namespace NegotiationAPI.Application.CQRS.Commands.NegotiationAttempt.MakeAttemptCommand
{
    public class MakeAttemptCommandValidation : AbstractValidator<MakeAttemptCommand>
    {
        public MakeAttemptCommandValidation()
        {
            RuleFor(x => x.NegotiationId).NotEmpty();
            RuleFor(x => x.ProposedPrice).GreaterThan(0);
        }
    }
}
