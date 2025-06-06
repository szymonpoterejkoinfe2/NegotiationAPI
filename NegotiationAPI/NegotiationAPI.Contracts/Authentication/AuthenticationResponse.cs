using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegotiationAPI.Contracts.Authentication
{
    public class AuthenticationResponse
    {
        public Guid Id { get; set; }
        public string FristName { get; set; }  = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
