using AutoMapper;
using LMS_CMS_BL.DTO.Registration;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.RegisterationModule;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers.Domains.Registeration
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class RegisterationFormTestController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public RegisterationFormTestController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" },
         pages: new[] { "Registration Confirmation", "Registration" }
         )]
        public async Task<IActionResult> GetAsync(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<RegisterationFormTest> tests = await Unit_Of_Work.registerationFormTest_Repository.Select_All_With_IncludesById<RegisterationFormTest>(
                    b => b.IsDeleted != true && b.RegisterationFormParentID==id,
                    query => query.Include(emp => emp.RegisterationFormParent),
                    query => query.Include(emp => emp.Test),
                    query => query.Include(emp => emp.TestState)

                    );

            if (tests == null || tests.Count == 0)
            {
                return NotFound();
            }

            List<RegisterationFormTestGetDTO> testDTO = mapper.Map<List<RegisterationFormTestGetDTO>>(tests);

            return Ok(testDTO);
        }
        //////////////////////////////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
      allowedTypes: new[] { "octa", "employee" },
      allowEdit: 1,
     pages: new[] { "Registration Confirmation", "Registration" }
  )]
        public IActionResult Edit(RegisterationFormTestEditDTO newTest)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
            var userRoleClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            long.TryParse(userRoleClaim, out long roleId);

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID, Type claim not found.");
            }

            if (newTest == null)
            {
                return BadRequest("Building cannot be null");
            }

            RegisterationFormTest registerationFormTest = Unit_Of_Work.registerationFormTest_Repository.First_Or_Default(r => r.ID == newTest.ID&&r.IsDeleted!=true);
            if (registerationFormTest == null)
            {
                return NotFound();
            }
            TestState state = Unit_Of_Work.testState_Repository.First_Or_Default(r => r.ID == newTest.StateID);
            if (state == null)
            {
                return NotFound("this state not exist");
            }
            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Registration Confirmation");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (registerationFormTest.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Registration Confirmation page doesn't exist");
                }
            }

            mapper.Map(newTest, registerationFormTest);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            registerationFormTest.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                registerationFormTest.UpdatedByOctaId = userId;
                if (registerationFormTest.UpdatedByUserId != null)
                {
                    registerationFormTest.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                registerationFormTest.UpdatedByUserId = userId;
                if (registerationFormTest.UpdatedByOctaId != null)
                {
                    registerationFormTest.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.registerationFormTest_Repository.Update(registerationFormTest);
            Unit_Of_Work.SaveChanges();
            return Ok(newTest);
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////
    

}
