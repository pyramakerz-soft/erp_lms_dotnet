using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_DAL.Models.Octa;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LMS_CMS_PL.Controllers.Domains
{

    [Route("api/with-domain/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly DbContextFactoryService _dbContextFactory;
        private readonly GenerateJWTService _generateJWT;
        private readonly UOW _Unit_Of_Work_Octa;

        public AccountController(DbContextFactoryService dbContextFactory, UOW unit_Of_Work_Octa, GenerateJWTService generateJWT)
        {
            _dbContextFactory = dbContextFactory;
            _Unit_Of_Work_Octa = unit_Of_Work_Octa;
            _generateJWT = generateJWT;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginDTO UserInfo)
        {
            // bool isPasswordValid = BCrypt.Net.BCrypt.Verify(enteredPassword, storedHashedPassword);

            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (UserInfo == null)
            {
                return BadRequest("Data Can't be null");
            }
            if (UserInfo.Type == null || !new[] { "employee", "student", "parent" }.Contains(UserInfo.Type.ToLower()))
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
                "employee" => Unit_Of_Work.employee_Repository.First_Or_Default(emp => emp.User_Name == UserInfo.User_Name && emp.IsDeleted != true),
                "student" => Unit_Of_Work.student_Repository.First_Or_Default(stu => stu.User_Name == UserInfo.User_Name && stu.IsDeleted != true),
                "parent" => Unit_Of_Work.parent_Repository.First_Or_Default(par => par.User_Name == UserInfo.User_Name && par.IsDeleted != true),
            };
            bool isMatch = BCrypt.Net.BCrypt.Verify(UserInfo.Password, user.Password);

            if (user == null)
            {
                return BadRequest("UserName or Password is Invalid");
            }
            if (isMatch == false)
            {
                return BadRequest("UserName or Password is Invalid");
            }
            if (UserInfo.Type == "employee" && user is Employee emp)
            {
                var tokenEmp = _generateJWT.Generate_Jwt_Token(emp.User_Name, emp.ID.ToString(), UserInfo.Type, emp.Role_ID.ToString());
                return Ok(new { Token = tokenEmp });
            }
            else if (UserInfo.Type == "student" && user is Student stu)
            {
                var token = _generateJWT.Generate_Jwt_Token(stu.User_Name, stu.ID.ToString(), UserInfo.Type);
                return Ok(new { Token = token });
            }
            else if (UserInfo.Type == "parent" && user is Parent par)
            {
                var token = _generateJWT.Generate_Jwt_Token(par.User_Name, par.ID.ToString(), UserInfo.Type);
                return Ok(new { Token = token });
            }

            return BadRequest("Unexpected user type.");
        }

    }
        
 }
