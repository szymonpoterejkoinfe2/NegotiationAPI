using MediatR;
using NegotiationAPI.Application.Services.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegotiationAPI.Application.Authentication.Queries.Login
{
    public record LoginQuery(
        string Email,
        string Password
        ) : IRequest<AuthenticationResult>;
   
}
