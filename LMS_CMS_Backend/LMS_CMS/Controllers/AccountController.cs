using AutoMapper;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LMS_CMS_PL.Controllers
{
    public interface IUser
    {
        int Id { get; }
        string User_Name { get; }
        // Add other common properties here
    }

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private UOW Unit_Of_Work;
        private readonly IConfiguration _configuration;

        public AccountController(UOW Unit_Of_Work, IConfiguration configuration)
        {
            this.Unit_Of_Work = Unit_Of_Work;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Login(string User_Name, string Password, string Type)
        {
            dynamic user = Type switch
            {
                "Emp" => Unit_Of_Work.employee_Repository.First_Or_Default(emp => emp.User_Name == User_Name && emp.Password == Password),
                "Stu" => Unit_Of_Work.student_Repository.First_Or_Default(stu => stu.User_Name == User_Name && stu.Password == Password),
                "Par" => Unit_Of_Work.parent_Repository.First_Or_Default(par => par.User_Name == User_Name && par.Password == Password),
                _ => null
            };

            if (user == null)
            {
                return NotFound();
            }

            var token = Generate_Jwt_Token(user.User_Name, user.ID.ToString());
            return Ok(new { Token = token });
        }

        private string Generate_Jwt_Token(string username, string userId)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["JWT:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("User_Name", username),
                new Claim("ID", userId)
            };

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
