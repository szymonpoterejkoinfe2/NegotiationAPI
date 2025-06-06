using NegotiationAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegotiationAPI.Application.Services.Authentication
{
    public record AuthenticationResult
     (
        Employee Employee,
        string Token
    );
}
