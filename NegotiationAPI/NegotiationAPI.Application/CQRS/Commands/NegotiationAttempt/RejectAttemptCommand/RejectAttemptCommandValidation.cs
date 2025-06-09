using FluentValidation;

namespace NegotiationAPI.Application.CQRS.Commands.NegotiationAttempt.RejectAttemptCommand
{
    public class RejectAttemptCommandValidation : AbstractValidator<RejectAttemptCommand>
    {
        public RejectAttemptCommandValidation()
        {
            RuleFor(x => x.AttemptId).NotEmpty();
        }
    }
}
