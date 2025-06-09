using FluentValidation;

namespace NegotiationAPI.Application.CQRS.Queries.NegotiationAttempt.GetAttemptByIdQuery
{
    public class GetNegotitationAttemptByIdValidation : AbstractValidator<GetAttemptByIdQuery>
    {
        public GetNegotitationAttemptByIdValidation()
        {
            RuleFor(x => x.AttemptId).NotEmpty();
        }
    }
}
