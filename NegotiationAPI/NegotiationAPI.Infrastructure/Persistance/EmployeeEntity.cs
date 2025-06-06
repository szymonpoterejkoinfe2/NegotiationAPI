using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegotiationAPI.Infrastructure.Persistance
{
    public class EmployeeEntity
    {
        public Guid Id { get; set; }
        public string FristName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
