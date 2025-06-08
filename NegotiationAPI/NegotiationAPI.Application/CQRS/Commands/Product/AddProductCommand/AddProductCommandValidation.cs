using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegotiationAPI.Application.CQRS.Commands.Product.AddProductCommand
{
    public class AddProductCommandValidation : AbstractValidator<AddProductCommand>
    {
        public AddProductCommandValidation()
        {
            RuleFor(x => x.Name).NotEmpty();            
            RuleFor(x => x.Description).NotEmpty();            
            RuleFor(x => x.Price).NotEmpty().GreaterThan(0.0);            
        }
    }

}
