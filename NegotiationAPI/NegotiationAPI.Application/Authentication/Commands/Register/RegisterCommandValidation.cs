using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegotiationAPI.Application.Authentication.Commands.Register
{
    public class RegisterCommandValidation : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidation()
        {
            RuleFor(x => x.FristName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).MinimumLength(8);
        }
    }
}
