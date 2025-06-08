using FluentValidation;

namespace NegotiationAPI.Application.CQRS.Queries.Negotiation.GetNegotiationById
{
    public class AddNegotiationQueryValidation : AbstractValidator<GetNegotiationByIdQuery>
    {
        public AddNegotiationQueryValidation()
        {
            RuleFor(x => x.NegotiationId).NotEmpty();
        }
    }
}
