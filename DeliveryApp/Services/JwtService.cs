using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DeliveryApp.Services
{
    public class JwtService
    {
        private readonly string _jwtSecret;
        private readonly string _issuer;

        public JwtService(IConfiguration config)
        {
            _jwtSecret = config["Jwt:Secret"] ?? throw new InvalidOperationException("JWT secret key is missing in configuration.");
            _issuer = config["Jwt:Issuer"] ?? throw new InvalidOperationException("JWT issuer is missing in configuration.");
        }

        public string GenerateToken(string username)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, username)
        };

            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
            var creds = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_issuer, _issuer, claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
