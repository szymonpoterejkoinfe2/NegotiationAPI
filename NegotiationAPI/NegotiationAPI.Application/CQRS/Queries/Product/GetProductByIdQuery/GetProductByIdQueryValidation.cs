using FluentValidation;

namespace NegotiationAPI.Application.Authentication.Queries.Product.GetProductByIdQuery
{
    public class GetProductByIdQueryValidation : AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdQueryValidation() 
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
