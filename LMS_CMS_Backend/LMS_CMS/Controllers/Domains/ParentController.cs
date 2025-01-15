using AutoMapper;
using Azure.Core;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.RegisterationModule;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace LMS_CMS_PL.Controllers.Domains
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    public class ParentController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly IEmailService _emailService;

        public ParentController(DbContextFactoryService dbContextFactory, IMapper mapper , IEmailService emailService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            this._emailService = emailService;
        }


        [HttpGet("{Id}")]
        [Authorize]
        public IActionResult GetByID(long Id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            Parent parent = Unit_Of_Work.parent_Repository.Select_By_Id(Id);

            if (parent == null || parent.IsDeleted == true)
            {
                return NotFound("No Parent found");
            }

            ParentGetDTO employeeDTO = mapper.Map<ParentGetDTO>(parent);

            return Ok(employeeDTO);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        public async Task<IActionResult> AddAsync(ParentDTO UserInfo)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (UserInfo == null)
            {
                return BadRequest("Data Can't be null");
            }
            if (UserInfo.Email == null)
            {
                return BadRequest("Email Can't be null");
            }
            if (UserInfo.User_Name == null)
            {
                return BadRequest("User_Name Can't be null");
            }
            if (UserInfo.Password.Length == 0)
            {
                return BadRequest("Password Can't be null");
            }

            Parent parent =Unit_Of_Work.parent_Repository.First_Or_Default(p=>p.User_Name==UserInfo.ar_name);
            if (parent !=null) 
            {
                return BadRequest("This vv Already Token");
            }

            Parent parent1 = Unit_Of_Work.parent_Repository.First_Or_Default(p => p.Email == UserInfo.Email);
            if (parent1 != null)
            {
                return BadRequest("This Email Already Token");
            }

            Parent parent2 = Unit_Of_Work.parent_Repository.First_Or_Default(p => p.User_Name == UserInfo.ar_name);
            if (parent != null)
            {
                return BadRequest("This UserName Already Token");
            }

            //Validation
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (UserInfo.Email != null && !Regex.IsMatch(UserInfo.Email, pattern))
            {
                return BadRequest("Email Is Not Valid");
            }
            if (UserInfo.Password.Length < 6)
            {
                return BadRequest("Password must be between 6 and 100 characters");
            }

            //AddParent
            var confirmationCode = new Random().Next(100000, 999999).ToString();
            Parent NewParent =mapper.Map<Parent>(UserInfo);
            NewParent.Password = BCrypt.Net.BCrypt.HashPassword(NewParent.Password);
            Unit_Of_Work.parent_Repository.Add(NewParent);
            Unit_Of_Work.SaveChanges();

            //await _emailService.SendEmailAsync(NewParent.Email, "Confirm your account",
            // $"Your confirmation code is: {confirmationCode}");

            List<RegisterationFormParent> registerationFormParent = Unit_Of_Work.registerationFormParent_Repository.FindBy(r=>r.Email == UserInfo.Email);
            if (registerationFormParent.Count != 0) 
            {
                foreach (var item in registerationFormParent)
                {
                item.ParentID = NewParent.ID;
                Unit_Of_Work.registerationFormParent_Repository.Update(item);
                Unit_Of_Work.SaveChanges();
                }
            }
            return Ok(UserInfo);
        }
    }
}
