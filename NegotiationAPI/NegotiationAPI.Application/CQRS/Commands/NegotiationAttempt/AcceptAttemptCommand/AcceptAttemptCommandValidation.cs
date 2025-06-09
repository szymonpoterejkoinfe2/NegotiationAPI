using FluentValidation;

namespace NegotiationAPI.Application.CQRS.Commands.NegotiationAttempt.AcceptAttemptCommand
{
    internal class AcceptAttemptCommandValidation : AbstractValidator<AcceptAttemptCommand>
    {
        public AcceptAttemptCommandValidation()
        {
            RuleFor(x => x.AttemptId).NotEmpty();   
        }
    }
}
