using LMS_CMS_BL.DTO;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Octa;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LMS_CMS_PL.Controllers.Octa
{
    [Route("api/[controller]")]
    [ApiController]
    public class OctaAccountController : ControllerBase
    {
        private readonly GenerateJWTService _generateJWT;
        private readonly UOW _Unit_Of_Work_Octa;

        public OctaAccountController(UOW unit_Of_Work_Octa, GenerateJWTService generateJWT)
        {
            _Unit_Of_Work_Octa = unit_Of_Work_Octa;
            _generateJWT = generateJWT;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginDTO UserInfo)
        {
            // bool isPasswordValid = BCrypt.Net.BCrypt.Verify(enteredPassword, storedHashedPassword);

            Console.WriteLine(UserInfo);
            if (UserInfo == null)
            {
                return BadRequest("Data Can't be null");
            }
            if (UserInfo.Type != "octa")
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

            LMS_CMS_DAL.Models.Octa.Octa user = _Unit_Of_Work_Octa.octa_Repository.First_Or_Default_Octa(par => par.User_Name == UserInfo.User_Name && par.Password == UserInfo.Password);

            if (user == null)
            {
                return BadRequest("UserName or Password is Invalid");
            }

            var token = _generateJWT.Generate_Jwt_Token(user.User_Name, user.ID.ToString(), UserInfo.Type);
            return base.Ok(new { Token = token });

        }
    }
}