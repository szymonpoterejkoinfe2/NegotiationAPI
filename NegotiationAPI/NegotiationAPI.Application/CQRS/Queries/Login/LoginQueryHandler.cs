using ErrorOr;
using MediatR;
using NegotiationAPI.Application.Common.Interfaces.Authentication;
using NegotiationAPI.Application.Common.Interfaces.Persistance;
using NegotiationAPI.Application.Services.Authentication;
using NegotiationAPI.Domain.Entities;
using NegotiationAPI.Domain.Errors;

namespace NegotiationAPI.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IEmployeeRepository _employeeRepository;

        public LoginQueryHandler(IEmployeeRepository employeeRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _employeeRepository = employeeRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            
            if (_employeeRepository.GetEmployeeByEmail(query.Email) is not Employee loginEmployee)
            {
                return Errors.Authentication.InvalidCredentials;
            }
            
            if (loginEmployee.Password != query.Password)
            {
                return new[] { Errors.Authentication.InvalidCredentials };
            }

            var token = _jwtTokenGenerator.GenerateToken(loginEmployee);

            return new AuthenticationResult(loginEmployee, token);
        }
    }
}
