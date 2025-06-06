using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegotiationAPI.Domain.Entities
{
    public class Employee
    {
        public Guid Id { get; set; } 
        public string FristName { get; set; } 
        public string LastName { get; set; } 
        public string Email { get; set; } 
        public string Password { get; set; } 
    }
}
