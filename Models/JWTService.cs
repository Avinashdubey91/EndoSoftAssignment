using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EndoSoftAssignment.Models
{
    public class JWTService
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int TokenDuration { get; set; }

        private readonly IConfiguration _configuration;

        public JWTService(IConfiguration configuration)
        {
            _configuration = configuration;
            this.SecretKey = _configuration.GetSection("JWT").GetSection("Key").Value;
            this.TokenDuration = Int32.Parse(_configuration.GetSection("JWT").GetSection("Duration").Value);
            this.Issuer = _configuration.GetSection("JWT").GetSection("Issuer").Value;
            this.Audience = _configuration.GetSection("JWT").GetSection("Audience").Value;
        }

        public string GenerateJWTToken(string UserId, string FirstName, string LastName, string Address, string MobileNo)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.SecretKey));

            var signature = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var payload = new[]
            {
                new Claim("UserId", UserId),
                new Claim("FirstName", FirstName),
                new Claim("LastName", LastName),
                new Claim("Address", Address),
                new Claim("MobileNo", MobileNo)
            };

            var jwtToken = new JwtSecurityToken(
                issuer: Issuer,
                audience:Audience,
                claims: payload,
                expires: DateTime.Now.AddMinutes(TokenDuration),
                signingCredentials: signature
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
