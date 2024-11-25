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
            if (UserInfo.Type == null || !new[] { "employee", "student", "parent", "pyramakerz" }.Contains(UserInfo.Type.ToLower()))
            {
                return BadRequest("Invalid user type.");
            }
            if (UserInfo.User_Name.Length == 0)
            {
                return BadRequest("User_Name Can't be null");
            }
            if (UserInfo.Password.Length == 0)
            {
                return BadRequest("Password Can't be null");
            }

            dynamic user = UserInfo.Type switch
            {
                "employee" => Unit_Of_Work.employee_Repository.First_Or_Default(emp => emp.User_Name == UserInfo.User_Name && emp.Password == UserInfo.Password && emp.IsDeleted!=true),
                "student" => Unit_Of_Work.student_Repository.First_Or_Default(stu => stu.User_Name == UserInfo.User_Name && stu.Password == UserInfo.Password && stu.IsDeleted != true),
                "parent" => Unit_Of_Work.parent_Repository.First_Or_Default(par => par.User_Name == UserInfo.User_Name && par.Password == UserInfo.Password && par.IsDeleted != true),
                "pyramakerz" => Unit_Of_Work.pyramakerz_Repository.First_Or_Default(par => par.User_Name == UserInfo.User_Name && par.Password == UserInfo.Password && par.IsDeleted != true),
                _ => null
            };

            if (user == null)
            {
                return BadRequest("UserName or Password is Invalid");
            }

            if (UserInfo.Type == "employee" && user is Employee emp)
            {
                var tokenEmp = Generate_Jwt_Token(emp.User_Name, emp.ID.ToString(), UserInfo.Type, emp.Domain_ID.ToString(), emp.Role_ID.ToString());
                return Ok(new { Token = tokenEmp });
            }
            else if (UserInfo.Type == "student" && user is Student stu)
            {
                var token = Generate_Jwt_Token(stu.User_Name, stu.ID.ToString(), UserInfo.Type);
                return Ok(new { Token = token });
            }
            else if (UserInfo.Type == "parent" && user is Parent par)
            {
                var token = Generate_Jwt_Token(par.User_Name, par.ID.ToString(), UserInfo.Type);
                return Ok(new { Token = token });
            }
            else if (UserInfo.Type == "pyramakerz" && user is Pyramakerz pym)
            {
                var token = Generate_Jwt_Token(pym.User_Name, pym.ID.ToString(), UserInfo.Type);
                return Ok(new { Token = token });
            }

            return BadRequest("Unexpected user type.");
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
