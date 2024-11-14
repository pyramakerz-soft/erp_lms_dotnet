using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LMS_CMS_PL.Controllers
{

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
        public IActionResult Login([FromBody] LoginDTO UserInfo)
        {
            if (UserInfo == null)
            {
                return BadRequest("Data Can't be null");
            }
            if (UserInfo.Type == null)
            {
                return BadRequest("Type Can't be null");
            }
            if (UserInfo.User_Name.Length == 0)
            {
                return BadRequest("User_Name Can't be null");
            }
            if (UserInfo.Password.Length == 0)
            {
                return BadRequest("Password Can't be null");
            }
            if (UserInfo.Password.Length < 6 || UserInfo.Password.Length > 100)
            {
                return BadRequest("Password must be between 6 and 100 characters");
            }
            if (UserInfo.User_Name.Length >100)
            {
                return BadRequest("Use_Name cannot be longer than 100 characters");
            }

            dynamic user = UserInfo.Type switch
            {
                "employee" => Unit_Of_Work.employee_Repository.First_Or_Default(emp => emp.User_Name == UserInfo.User_Name && emp.Password == UserInfo.Password),
                "student" => Unit_Of_Work.student_Repository.First_Or_Default(stu => stu.User_Name == UserInfo.User_Name && stu.Password == UserInfo.Password),
                "parent" => Unit_Of_Work.parent_Repository.First_Or_Default(par => par.User_Name == UserInfo.User_Name && par.Password == UserInfo.Password),
                "pyramakerz" => Unit_Of_Work.pyramakerz_Repository.First_Or_Default(par => par.User_Name == UserInfo.User_Name && par.Password == UserInfo.Password),
                _ => null
            };

            if (user == null)
            {
                return BadRequest("Invalid user type or credentials.");
            }
            if(UserInfo.Type == "employee")
            {
                var tokenEmp = Generate_Jwt_Token(user.User_Name, user.ID.ToString(), UserInfo.Type, user.Domain_ID, user.Role_ID);
                return Ok(new { Token = tokenEmp });
            }

            var token = Generate_Jwt_Token(user.User_Name, user.ID.ToString() , UserInfo.Type);
            return Ok(new { Token = token });
        }

        private string Generate_Jwt_Token(string username, string userId ,string type, string? domainId = null, string? roleId = null)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["JWT:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("user_Name", username),
                new Claim("id", userId),
                new Claim("type", type),
            };
            if (!string.IsNullOrEmpty(domainId))
            {
                claims.Add(new Claim("domain", domainId));
            }

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
