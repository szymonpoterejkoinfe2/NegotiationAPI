using MapsterMapper;
using NegotiationAPI.Application.Common.Interfaces.Persistance;
using NegotiationAPI.Domain.Entities;
using NegotiationAPI.Infrastructure.Persistance.Entities;

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

        public Employee? GetRandomEmployee()
        {
            if (_employees.Count == 0)
                return null;

            var random = new Random();
            var index = random.Next(_employees.Count);
            var empEntity = _employees[index];

            return _mapper.Map<Employee>(empEntity);
        }

    }
}
