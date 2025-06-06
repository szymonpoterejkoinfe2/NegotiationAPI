using NegotiationAPI.Domain.Entities;

namespace NegotiationAPI.Application.Common.Interfaces.Persistance
{
    public interface IEmployeeRepository
    {
        public void Add(Employee employee);
        public Employee? GetEmployeeByEmail(string email);
    }
}
