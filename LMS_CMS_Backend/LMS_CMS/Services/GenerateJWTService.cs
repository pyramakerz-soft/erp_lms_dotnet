using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LMS_CMS_PL.Services
{
    public class GenerateJWTService
    {
        private readonly IConfiguration _configuration;

        public GenerateJWTService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Generate_Jwt_Token(string username, string userId, string type, string? roleId = null)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["JWT:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("user_Name", username),
                new Claim("id", userId),
                new Claim("type", type),
            };

            if (!string.IsNullOrEmpty(roleId))
            {
                claims.Add(new Claim("role", roleId));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["JWT:Issuer"],
                _configuration["JWT:Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: signIn
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
