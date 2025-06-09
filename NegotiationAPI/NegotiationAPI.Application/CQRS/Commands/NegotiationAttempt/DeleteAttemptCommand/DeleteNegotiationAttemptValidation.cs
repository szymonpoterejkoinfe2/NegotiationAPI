using FluentValidation;

namespace NegotiationAPI.Application.CQRS.Commands.NegotiationAttempt.DeleteAttemptCommand
{
    public class DeleteNegotiationAttemptValidation : AbstractValidator<DeleteAttemptCommand>
    {
        public DeleteNegotiationAttemptValidation()
        {
            RuleFor(x => x.AttemptId).NotEmpty();
        }
    }
}
