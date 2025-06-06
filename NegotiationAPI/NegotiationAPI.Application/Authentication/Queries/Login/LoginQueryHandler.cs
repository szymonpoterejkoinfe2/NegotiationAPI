using MediatR;
using NegotiationAPI.Application.Common.Interfaces.Authentication;
using NegotiationAPI.Application.Common.Interfaces.Persistance;
using NegotiationAPI.Application.Services.Authentication;
using NegotiationAPI.Domain.Entities;

namespace NegotiationAPI.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResult>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IEmployeeRepository _employeeRepository;

        public LoginQueryHandler(IEmployeeRepository employeeRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _employeeRepository = employeeRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<AuthenticationResult> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            
            if (_employeeRepository.GetEmployeeByEmail(query.Email) is not Employee loginEmployee)
            {
                throw new Exception("UserNotFound");
            }

            var token = _jwtTokenGenerator.GenerateToken(loginEmployee);

            return new AuthenticationResult(loginEmployee, token);
        }
    }
}
