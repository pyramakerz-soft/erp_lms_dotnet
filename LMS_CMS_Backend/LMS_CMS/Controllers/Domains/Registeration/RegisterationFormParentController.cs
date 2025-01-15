 using AutoMapper;
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
using System.Collections.Generic;
using System.Diagnostics;

namespace LMS_CMS_PL.Controllers.Domains.Registeration
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class RegisterationFormParentController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public RegisterationFormParentController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        ///////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Registration Confirmation", "Registration" }
        )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }
            List<RegisterationFormParent> registerationFormParents = await Unit_Of_Work.registerationFormParent_Repository.Select_All_With_IncludesById<RegisterationFormParent>(
                    r => r.IsDeleted != true,
                    query => query.Include(emp => emp.RegisterationFormState),
                    query => query.Include(emp => emp.RegistrationForm),
                    query => query.Include(emp => emp.Parent));

            if (registerationFormParents == null || registerationFormParents.Count == 0)
            {
                return NotFound();
            }

            List<RegisterationFormParentGetDTO> registerationFormParentDTO = mapper.Map<List<RegisterationFormParentGetDTO>>(registerationFormParents);

            for(int i = 0; i < registerationFormParentDTO.Count; i++)
            {
                long gradeId = long.Parse(registerationFormParentDTO[i].GradeID);
                if (gradeId != 0 || gradeId != null)
                {
                    Grade grade = Unit_Of_Work.grade_Repository.First_Or_Default(g => g.ID == gradeId && g.IsDeleted != true);
                    registerationFormParentDTO[i].GradeName = grade.Name;
                }
                long academicYearId = long.Parse(registerationFormParentDTO[i].AcademicYearID);
                if (academicYearId != 0 || academicYearId != null)
                {
                    AcademicYear academicYear = Unit_Of_Work.academicYear_Repository.First_Or_Default(g => g.ID == academicYearId && g.IsDeleted != true);
                    registerationFormParentDTO[i].AcademicYearName = academicYear.Name;
                }
            }

            return Ok(registerationFormParentDTO);
        }

        ///////////////////////////////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Registration Confirmation", "Registration" }
        )]
        public async Task<IActionResult> Edit(RegisterationFormParentPutDTO editedregisterationFormParentPutDTO)
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

            if (editedregisterationFormParentPutDTO == null)
            {
                return BadRequest("Registration Form Parent cannot be null");
            }

            RegisterationFormState registerationFormState = Unit_Of_Work.registerationFormState_Repository.First_Or_Default(b => b.ID == editedregisterationFormParentPutDTO.RegisterationFormStateID);
            if (registerationFormState == null)
            {
                return BadRequest("No Registration Form State with this ID");
            }

            RegisterationFormParent existsRegisterationFormParent = Unit_Of_Work.registerationFormParent_Repository.First_Or_Default(b => b.ID == editedregisterationFormParentPutDTO.ID && b.IsDeleted != true);
            if (existsRegisterationFormParent == null)
            {
                return BadRequest("No Registration Form Parent with this ID");
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Registration Confirmation");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (existsRegisterationFormParent.InsertedByUserId != userId)
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

            mapper.Map(editedregisterationFormParentPutDTO, existsRegisterationFormParent);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            existsRegisterationFormParent.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                existsRegisterationFormParent.UpdatedByOctaId = userId;
                if (existsRegisterationFormParent.UpdatedByUserId != null)
                {
                    existsRegisterationFormParent.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                existsRegisterationFormParent.UpdatedByUserId = userId;
                if (existsRegisterationFormParent.UpdatedByOctaId != null)
                {
                    existsRegisterationFormParent.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.registerationFormParent_Repository.Update(existsRegisterationFormParent);
            Unit_Of_Work.SaveChanges();
            return Ok(editedregisterationFormParentPutDTO);
        }

        ///////////////////////////////////////////////////////////////////////////////////

        [HttpGet("GetByParentID/{parentID}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee", "parent" },
            pages: new[] { "Registration Confirmation", "Registration" }
        )]
        public async Task<IActionResult> GetByParentID(long parentID)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }
            List<RegisterationFormParent> registerationFormParents = await Unit_Of_Work.registerationFormParent_Repository.Select_All_With_IncludesById<RegisterationFormParent>(
                    r => r.IsDeleted != true && r.ParentID == parentID,
                    query => query.Include(emp => emp.RegisterationFormState),
                    query => query.Include(emp => emp.RegistrationForm),
                    query => query.Include(emp => emp.Parent));

            if (registerationFormParents == null || registerationFormParents.Count == 0)
            {
                return NotFound();
            }

            List<RegisterationFormParentGetDTO> registerationFormParentDTO = mapper.Map<List<RegisterationFormParentGetDTO>>(registerationFormParents);

            for (int i = 0; i < registerationFormParentDTO.Count; i++)
            {
                long gradeId = long.Parse(registerationFormParentDTO[i].GradeID);
                if (gradeId != 0 || gradeId != null)
                {
                    Grade grade = Unit_Of_Work.grade_Repository.First_Or_Default(g => g.ID == gradeId && g.IsDeleted != true);
                    registerationFormParentDTO[i].GradeName = grade.Name;
                }

                long academicYearId = long.Parse(registerationFormParentDTO[i].AcademicYearID);
                if (academicYearId != 0 || academicYearId != null)
                {
                    AcademicYear academicYear = Unit_Of_Work.academicYear_Repository.First_Or_Default(g => g.ID == academicYearId && g.IsDeleted != true);
                    registerationFormParentDTO[i].AcademicYearName = academicYear.Name;
                }
            }

            return Ok(registerationFormParentDTO);
        }
        
        ///////////////////////////////////////////////////////////////////////////////////

        [HttpGet("GetByID/{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee", "parent" },
            pages: new[] { "Registration Confirmation", "Registration" }
        )]
        public async Task<IActionResult> GetByID(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }
            RegisterationFormParent registerationFormParent = await Unit_Of_Work.registerationFormParent_Repository.FindByIncludesAsync(
                    r => r.IsDeleted != true && r.ID == id,
                    query => query.Include(emp => emp.RegisterationFormState),
                    query => query.Include(emp => emp.RegistrationForm),
                    query => query.Include(emp => emp.Parent));

            if (registerationFormParent == null)
            {
                return NotFound();
            }

            RegisterationFormParentGetDTO registerationFormParentDTO = mapper.Map<RegisterationFormParentGetDTO>(registerationFormParent);

            long gradeId = long.Parse(registerationFormParentDTO.GradeID);
            if (gradeId != 0 || gradeId != null)
            {
                Grade grade = Unit_Of_Work.grade_Repository.First_Or_Default(g => g.ID == gradeId && g.IsDeleted != true);
                registerationFormParentDTO.GradeName = grade.Name;
            }

            long academicYearId = long.Parse(registerationFormParentDTO.AcademicYearID);
            if (academicYearId != 0 || academicYearId != null)
            {
                AcademicYear academicYear = Unit_Of_Work.academicYear_Repository.First_Or_Default(g => g.ID == academicYearId && g.IsDeleted != true);
                registerationFormParentDTO.AcademicYearName = academicYear.Name;
            }

            return Ok(registerationFormParentDTO);
        }

        ///////////////////////////////////////////////////////////////////////////////////

        [HttpGet("GetByParentIDIncludeRegistrationFormInterview/{parentID}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee", "parent" },
            pages: new[] { "Registration Confirmation", "Registration", "Interview Registration" }
        )]
        public async Task<IActionResult> GetByParentIDIncludeRegistrationFormInterview(long parentID)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }
            List<RegisterationFormParent> registerationFormParents = await Unit_Of_Work.registerationFormParent_Repository.Select_All_With_IncludesById<RegisterationFormParent>(
                    r => r.IsDeleted != true && r.ParentID == parentID,
                    query => query.Include(emp => emp.RegisterationFormState),
                    query => query.Include(emp => emp.RegistrationForm),
                    query => query.Include(emp => emp.Parent));

            if (registerationFormParents == null || registerationFormParents.Count == 0)
            {
                return NotFound();
            }

            List<RegistrationFormParentIncludeRegistrationFormInterviewGetDTO> registerationFormParentDTO = mapper.Map<List<RegistrationFormParentIncludeRegistrationFormInterviewGetDTO>>(registerationFormParents);

            for (int i = 0; i < registerationFormParentDTO.Count; i++)
            {
                long gradeId = long.Parse(registerationFormParentDTO[i].GradeID);
                if (gradeId != 0 || gradeId != null)
                {
                    Grade grade = Unit_Of_Work.grade_Repository.First_Or_Default(g => g.ID == gradeId && g.IsDeleted != true);
                    registerationFormParentDTO[i].GradeName = grade.Name;
                }

                long academicYearId = long.Parse(registerationFormParentDTO[i].AcademicYearID);
                if (academicYearId != 0 || academicYearId != null)
                {
                    AcademicYear academicYear = Unit_Of_Work.academicYear_Repository.First_Or_Default(g => g.ID == academicYearId && g.IsDeleted != true);
                    registerationFormParentDTO[i].AcademicYearName = academicYear.Name;
                }

                RegisterationFormInterview registerationFormInterview = await Unit_Of_Work.registerationFormInterview_Repository.FindByIncludesAsync(
                    r => r.RegisterationFormParentID == registerationFormParentDTO[i].ID && r.IsDeleted != true,
                    query => query.Include(r => r.InterviewTime),
                    query => query.Include(e => e.InterviewState)
                    );

                if(registerationFormInterview != null)
                {
                    registerationFormParentDTO[i].RegistrationFormInterviewStateID = registerationFormInterview.InterviewStateID;
                    registerationFormParentDTO[i].RegistrationFormInterviewStateName = registerationFormInterview.InterviewState.Name;
                    registerationFormParentDTO[i].InterviewTimeID = registerationFormInterview.InterviewTimeID;
                    registerationFormParentDTO[i].InterviewTimeDate = registerationFormInterview.InterviewTime.Date;
                    registerationFormParentDTO[i].InterviewTimeFromTime = registerationFormInterview.InterviewTime.FromTime;
                    registerationFormParentDTO[i].InterviewTimeToTime = registerationFormInterview.InterviewTime.ToTime;
                }
            }

            return Ok(registerationFormParentDTO);
        }

        ///////////////////////////////////////////////////////////////////////////////////

        [HttpGet("GetByStateID/{stateID}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Registration Confirmation", "Registration" }
        )]
        public async Task<IActionResult> GetByStateID(long stateID)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }
            List<RegisterationFormParent> registerationFormParents = await Unit_Of_Work.registerationFormParent_Repository.Select_All_With_IncludesById<RegisterationFormParent>(
                    r => r.IsDeleted != true && r.RegisterationFormStateID == stateID,
                    query => query.Include(emp => emp.RegisterationFormState),
                    query => query.Include(emp => emp.RegistrationForm),
                    query => query.Include(emp => emp.Parent));

            if (registerationFormParents == null || registerationFormParents.Count == 0)
            {
                return NotFound();
            }

            List<RegisterationFormParentGetDTO> registerationFormParentDTO = mapper.Map<List<RegisterationFormParentGetDTO>>(registerationFormParents);

            for (int i = 0; i < registerationFormParentDTO.Count; i++)
            {
                long gradeId = long.Parse(registerationFormParentDTO[i].GradeID);
                if (gradeId != 0 || gradeId != null)
                {
                    Grade grade = Unit_Of_Work.grade_Repository.First_Or_Default(g => g.ID == gradeId && g.IsDeleted != true);
                    registerationFormParentDTO[i].GradeName = grade.Name;
                }

                long academicYearId = long.Parse(registerationFormParentDTO[i].AcademicYearID);
                if (academicYearId != 0 || academicYearId != null)
                {
                    AcademicYear academicYear = Unit_Of_Work.academicYear_Repository.First_Or_Default(g => g.ID == academicYearId && g.IsDeleted != true);
                    registerationFormParentDTO[i].AcademicYearName = academicYear.Name;
                }
            }

            return Ok(registerationFormParentDTO);
        }

        ///////////////////////////////////////////////////////////////////////////////////

        [HttpGet("GetBySchoolID/{schoolID}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Registration Confirmation", "Registration" }
        )]
        public async Task<IActionResult> GetBySchoolID(long schoolID)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            List<RegisterationFormSubmittion> registerationFormSubmittions = Unit_Of_Work.registerationFormSubmittion_Repository.FindBy(
                r => r.CategoryFieldID == 7 && r.TextAnswer == schoolID.ToString()
            );

            List<long> registerationFormParentID_WithSchoolId = new List<long>();

            for ( int i = 0; i < registerationFormSubmittions.Count; i++ )
            {
                registerationFormParentID_WithSchoolId.Add(registerationFormSubmittions[i].RegisterationFormParentID);
            }

            List<RegisterationFormParent> registerationFormParents = new List<RegisterationFormParent>();

            for(int i = 0; i < registerationFormParentID_WithSchoolId.Count; i++)
            {
                RegisterationFormParent registerationFormParents_WithID = await Unit_Of_Work.registerationFormParent_Repository.FindByIncludesAsync(
                        r => r.IsDeleted != true && r.ID == registerationFormParentID_WithSchoolId[i],
                        query => query.Include(emp => emp.RegisterationFormState),
                        query => query.Include(emp => emp.RegistrationForm),
                        query => query.Include(emp => emp.Parent));

                registerationFormParents.Add(registerationFormParents_WithID);
            }

            if (registerationFormParents == null || registerationFormParents.Count == 0)
            {
                return NotFound();
            }

            List<RegisterationFormParentGetDTO> registerationFormParentDTO = mapper.Map<List<RegisterationFormParentGetDTO>>(registerationFormParents);

            for (int i = 0; i < registerationFormParentDTO.Count; i++)
            {
                long gradeId = long.Parse(registerationFormParentDTO[i].GradeID);
                if (gradeId != 0 || gradeId != null)
                {
                    Grade grade = Unit_Of_Work.grade_Repository.First_Or_Default(g => g.ID == gradeId && g.IsDeleted != true);
                    registerationFormParentDTO[i].GradeName = grade.Name;
                }

                long academicYearId = long.Parse(registerationFormParentDTO[i].AcademicYearID);
                if (academicYearId != 0 || academicYearId != null)
                {
                    AcademicYear academicYear = Unit_Of_Work.academicYear_Repository.First_Or_Default(g => g.ID == academicYearId && g.IsDeleted != true);
                    registerationFormParentDTO[i].AcademicYearName = academicYear.Name;
                }
            }

            return Ok(registerationFormParentDTO);
        }

        ///////////////////////////////////////////////////////////////////////////////////

        [HttpGet("GetByAcademicYearID/{academicYearID}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Registration Confirmation", "Registration" }
        )]
        public async Task<IActionResult> GetByAcademicYearID(long academicYearID)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            List<RegisterationFormSubmittion> registerationFormSubmittions = Unit_Of_Work.registerationFormSubmittion_Repository.FindBy(
                r => r.CategoryFieldID == 8 && r.TextAnswer == academicYearID.ToString()
            );

            List<long> registerationFormParentID_WithAcademicYearId = new List<long>();

            for (int i = 0; i < registerationFormSubmittions.Count; i++)
            {
                registerationFormParentID_WithAcademicYearId.Add(registerationFormSubmittions[i].RegisterationFormParentID);
            }

            List<RegisterationFormParent> registerationFormParents = new List<RegisterationFormParent>();

            for (int i = 0; i < registerationFormParentID_WithAcademicYearId.Count; i++)
            {
                RegisterationFormParent registerationFormParents_WithID = await Unit_Of_Work.registerationFormParent_Repository.FindByIncludesAsync(
                        r => r.IsDeleted != true && r.ID == registerationFormParentID_WithAcademicYearId[i],
                        query => query.Include(emp => emp.RegisterationFormState),
                        query => query.Include(emp => emp.RegistrationForm),
                        query => query.Include(emp => emp.Parent));

                registerationFormParents.Add(registerationFormParents_WithID);
            }

            if (registerationFormParents == null || registerationFormParents.Count == 0)
            {
                return NotFound();
            }

            List<RegisterationFormParentGetDTO> registerationFormParentDTO = mapper.Map<List<RegisterationFormParentGetDTO>>(registerationFormParents);

            for (int i = 0; i < registerationFormParentDTO.Count; i++)
            {
                long gradeId = long.Parse(registerationFormParentDTO[i].GradeID);
                if (gradeId != 0 || gradeId != null)
                {
                    Grade grade = Unit_Of_Work.grade_Repository.First_Or_Default(g => g.ID == gradeId && g.IsDeleted != true);
                    registerationFormParentDTO[i].GradeName = grade.Name;
                }

                long academicYearId = long.Parse(registerationFormParentDTO[i].AcademicYearID);
                if (academicYearId != 0 || academicYearId != null)
                {
                    AcademicYear academicYear = Unit_Of_Work.academicYear_Repository.First_Or_Default(g => g.ID == academicYearId && g.IsDeleted != true);
                    registerationFormParentDTO[i].AcademicYearName = academicYear.Name;
                }
            }

            return Ok(registerationFormParentDTO);
        }
    }
}
