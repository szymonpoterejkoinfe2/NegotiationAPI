using ErrorOr;
using MediatR;
using NegotiationAPI.Application.Common.Interfaces.Authentication;
using NegotiationAPI.Application.Common.Interfaces.Persistance;
using NegotiationAPI.Application.Services.Authentication;
using NegotiationAPI.Domain.Entities;
using NegotiationAPI.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NegotiationAPI.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IEmployeeRepository _employeeRepository;

        public RegisterCommandHandler(IEmployeeRepository employeeRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _employeeRepository = employeeRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {

            if (_employeeRepository.GetEmployeeByEmail(command.Email) is not null)
            {
                return Errors.Authentication.DuplicateEmail;
            }

            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                FristName = command.FristName,
                LastName = command.LastName,
                Email = command.Email,
                Password = command.Password
            };
            _employeeRepository.Add(employee);

            var token = _jwtTokenGenerator.GenerateToken(employee);
            return new AuthenticationResult(employee, token);
        }
    }
}
