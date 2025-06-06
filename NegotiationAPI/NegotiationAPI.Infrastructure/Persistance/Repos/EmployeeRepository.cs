using MapsterMapper;
using NegotiationAPI.Application.Common.Interfaces.Persistance;
using NegotiationAPI.Domain.Entities;

namespace NegotiationAPI.Infrastructure.Persistance.Repos
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private static readonly List<EmployeeEntity> _employees = new();
        private  readonly IMapper _mapper;

        public EmployeeRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void Add(Employee employee)
        {
            var empEntity = _mapper.Map<EmployeeEntity>(employee);
            _employees.Add(empEntity);
        }

        public Employee? GetEmployeeByEmail(string email)
        {
            var empEntity = _employees.SingleOrDefault(e => e.Email == email);

            if (empEntity is null) 
            { 
                return null;
            }

            return _mapper.Map<Employee>(empEntity);
        }
    }
}
