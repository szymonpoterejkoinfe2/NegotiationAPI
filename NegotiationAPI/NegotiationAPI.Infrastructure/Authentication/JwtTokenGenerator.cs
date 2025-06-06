using Microsoft.IdentityModel.Tokens;
using NegotiationAPI.Application.Common.Interfaces.Authentication;
using NegotiationAPI.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NegotiationAPI.Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSettings _jwtSettings;

        public JwtTokenGenerator(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public string GenerateToken(Employee employee)
        {
            var signinClredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, employee.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, employee.FristName ),
                new Claim(JwtRegisteredClaimNames.FamilyName, employee.LastName )
            };

            var securityToken = new JwtSecurityToken(issuer: _jwtSettings.Issuer, audience: _jwtSettings.Audience, expires: DateTime.UtcNow.AddDays(_jwtSettings.ExpiryDays), claims: claims, signingCredentials: signinClredentials);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);

        }
    }
}
