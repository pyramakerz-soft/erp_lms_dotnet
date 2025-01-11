using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.DTO.Registration;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.LMS;
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
    public class RegistrationFormInterviewController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        private readonly CancelInterviewDayMessageService _cancelInterviewDayMessage;
        IMapper mapper;

        public RegistrationFormInterviewController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("GetRegistrationFormInterviewByInterviewID/{interviewID}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Interview Registration", "Registration" }
        )]
        public async Task<IActionResult> GetRegistrationFormInterviewByInterviewID(long interviewID)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (interviewID == 0)
            {
                return BadRequest("Enter Interview ID");
            }

            InterviewTime interviewTime = Unit_Of_Work.interviewTime_Repository.First_Or_Default(
                a => a.ID == interviewID && a.IsDeleted != true
                );

            if (interviewTime == null)
            {
                return NotFound("No Interviews with this ID");
            }

            List<RegisterationFormInterview> registerationFormInterview = await Unit_Of_Work.registerationFormInterview_Repository.Select_All_With_IncludesById<RegisterationFormInterview>(
                    i => i.IsDeleted != true && i.InterviewTimeID == interviewID,
                    query => query.Include(r => r.RegisterationFormParent),
                    query => query.Include(r => r.InterviewState)
                    );

            if (registerationFormInterview == null || registerationFormInterview.Count == 0)
            {
                return NotFound();
            }

            List<RegisterationFormInterviewGetDTO> registerationFormInterviewGetDTOs = mapper.Map<List<RegisterationFormInterviewGetDTO>>(registerationFormInterview);

            for (int i = 0; i < registerationFormInterviewGetDTOs.Count; i++)
            {
                long gradeId = long.Parse(registerationFormInterviewGetDTOs[i].GradeID);
                if (gradeId != 0 || gradeId != null)
                {
                    Grade grade = Unit_Of_Work.grade_Repository.First_Or_Default(g => g.ID == gradeId && g.IsDeleted != true);
                    registerationFormInterviewGetDTOs[i].GradeName = grade.Name;
                }
            }

            return Ok(registerationFormInterviewGetDTOs);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("GetRegistrationFormInterviewByID/{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Interview Registration", "Registration" }
        )]
        public async Task<IActionResult> GetByID(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (id == 0)
            {
                return BadRequest("Enter Registration Form Interview ID");
            }

            RegisterationFormInterview registerationFormInterview = await Unit_Of_Work.registerationFormInterview_Repository.FindByIncludesAsync(
                t => t.IsDeleted != true && t.ID == id,
                    query => query.Include(r => r.RegisterationFormParent),
                    query => query.Include(e => e.InterviewState)
                );


            if (registerationFormInterview == null)
            {
                return NotFound();
            }

            RegisterationFormInterviewGetDTO registerationFormInterviewDTO = mapper.Map<RegisterationFormInterviewGetDTO>(registerationFormInterview);

            long gradeId = long.Parse(registerationFormInterviewDTO.GradeID);
            if (gradeId != 0 || gradeId != null)
            {
                Grade grade = Unit_Of_Work.grade_Repository.First_Or_Default(g => g.ID == gradeId && g.IsDeleted != true);
                registerationFormInterviewDTO.GradeName = grade.Name;
            }

            return Ok(registerationFormInterviewDTO);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee", "parent" },
            pages: new[] { "Interview Registration", "Registration" }
        )]
        public IActionResult Add(RegisterationFormInterviewAddDTO NewRegisterationFormInterview)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (NewRegisterationFormInterview == null)
            {
                return BadRequest("Interview Registration cannot be null");
            }

            if (NewRegisterationFormInterview.RegisterationFormParentID != 0)
            {
                RegisterationFormParent registrationFormParent = Unit_Of_Work.registerationFormParent_Repository.First_Or_Default(
                    b => b.ID == NewRegisterationFormInterview.RegisterationFormParentID && b.IsDeleted != true
                    );
                if (registrationFormParent == null)
                {
                    return BadRequest("No Registration Form Parent with this ID");
                }
            }
            else
            {
                return BadRequest("Registration Form Parent id cannot be null");
            }

            if (NewRegisterationFormInterview.InterviewTimeID != 0)
            {
                InterviewTime interviewTime = Unit_Of_Work.interviewTime_Repository.First_Or_Default(
                    b => b.ID == NewRegisterationFormInterview.InterviewTimeID && b.IsDeleted != true
                    );

                if (interviewTime.Reserved == interviewTime.Capacity)
                {
                    return BadRequest("There is No Space To Register an Interview");
                }

                if (interviewTime == null)
                {
                    return BadRequest("No Interview Time with this ID");
                }
            }
            else
            {
                return BadRequest("Interview Time id cannot be null");
            }

            RegisterationFormInterview RegisterationFormInterviewExists = Unit_Of_Work.registerationFormInterview_Repository.First_Or_Default(
                r => r.IsDeleted != true && r.RegisterationFormParentID == NewRegisterationFormInterview.RegisterationFormParentID
                );

            if( RegisterationFormInterviewExists != null )
            {
                return BadRequest("You have been already registered to an interview");
            }

            RegisterationFormInterview registerationFormInterview = mapper.Map<RegisterationFormInterview>(NewRegisterationFormInterview);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            registerationFormInterview.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);

            if (userTypeClaim == "octa")
            {
                registerationFormInterview.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                registerationFormInterview.InsertedByUserId = userId;
            }

            // Default is pending
            registerationFormInterview.InterviewStateID = 1;

            Unit_Of_Work.registerationFormInterview_Repository.Add(registerationFormInterview);
            Unit_Of_Work.SaveChanges();

            // Reserved++
            InterviewTime interviewTimeToedit = Unit_Of_Work.interviewTime_Repository.First_Or_Default(
                b => b.ID == registerationFormInterview.InterviewTimeID
                    );
            interviewTimeToedit.Reserved++;
            Unit_Of_Work.interviewTime_Repository.Update(interviewTimeToedit);
            Unit_Of_Work.SaveChanges();

            return Ok(NewRegisterationFormInterview);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Interview Registration", "Registration" }
        )]
        public IActionResult Edit(RegistrationFormInterviewPutDTO registrationFormInterviewPutDTO)
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

            if (registrationFormInterviewPutDTO == null)
            {
                return BadRequest("Registration Form Interview cannot be null");
            }

            if (registrationFormInterviewPutDTO.InterviewStateID != 0)
            {
                InterviewState interviewState = Unit_Of_Work.interviewState_Repository.First_Or_Default(
                    b => b.ID == registrationFormInterviewPutDTO.InterviewStateID
                    );
                if (interviewState == null)
                {
                    return BadRequest("No Interview State with this ID");
                }
            }
            else
            {
                return BadRequest("Interview State id cannot be null");
            }

            RegisterationFormInterview RegisterationFormInterviewExists = Unit_Of_Work.registerationFormInterview_Repository.Select_By_Id(registrationFormInterviewPutDTO.ID);
            if (RegisterationFormInterviewExists == null || RegisterationFormInterviewExists.IsDeleted == true)
            {
                return NotFound("No Registration Form Interview with this ID");
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Interview Registration");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (RegisterationFormInterviewExists.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Interview Registration page doesn't exist");
                }
            }

            mapper.Map(registrationFormInterviewPutDTO, RegisterationFormInterviewExists);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            RegisterationFormInterviewExists.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                RegisterationFormInterviewExists.UpdatedByOctaId = userId;
                if (RegisterationFormInterviewExists.UpdatedByUserId != null)
                {
                    RegisterationFormInterviewExists.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                RegisterationFormInterviewExists.UpdatedByUserId = userId;
                if (RegisterationFormInterviewExists.UpdatedByOctaId != null)
                {
                    RegisterationFormInterviewExists.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.registerationFormInterview_Repository.Update(RegisterationFormInterviewExists);
            Unit_Of_Work.SaveChanges();
            return Ok(registrationFormInterviewPutDTO);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpDelete("CanceledByParent/{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee", "parent" },
            allowDelete: 1,
            pages: new[] { "Interview Registration", "Registration" }
        )]
        public IActionResult CanceledByParent(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
            var userRoleClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            long.TryParse(userRoleClaim, out long roleId);

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (id == 0)
            {
                return BadRequest("Enter Interview Time Table ID");
            }

            RegisterationFormInterview registerationFormInterview = Unit_Of_Work.registerationFormInterview_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);

            if (registerationFormInterview == null)
            {
                return NotFound();
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Interview Registration");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Delete_For_Others == false)
                    {
                        if (registerationFormInterview.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Interview Registration page doesn't exist");
                }
            }

            // Reserved --
            InterviewTime interviewTime = Unit_Of_Work.interviewTime_Repository.First_Or_Default(
                b => b.ID == registerationFormInterview.InterviewTimeID
                    );
            interviewTime.Reserved--;
            Unit_Of_Work.interviewTime_Repository.Update( interviewTime );
            Unit_Of_Work.SaveChanges();

            registerationFormInterview.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            registerationFormInterview.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                registerationFormInterview.DeletedByOctaId = userId;
                if (registerationFormInterview.DeletedByUserId != null)
                {
                    registerationFormInterview.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                registerationFormInterview.DeletedByUserId = userId;
                if (registerationFormInterview.DeletedByOctaId != null)
                {
                    registerationFormInterview.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.registerationFormInterview_Repository.Update(registerationFormInterview);
            Unit_Of_Work.SaveChanges();

            return Ok();
        }
    }
}
