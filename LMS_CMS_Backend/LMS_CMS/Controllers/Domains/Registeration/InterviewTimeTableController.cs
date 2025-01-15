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
using System.Linq;

namespace LMS_CMS_PL.Controllers.Domains.Registeration
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class InterviewTimeTableController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        private readonly CancelInterviewDayMessageService _cancelInterviewDayMessage;
        IMapper mapper;

        public InterviewTimeTableController(DbContextFactoryService dbContextFactory, IMapper mapper, CancelInterviewDayMessageService cancelInterviewDayMessage)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            this._cancelInterviewDayMessage = cancelInterviewDayMessage;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Interview Time Table", "Registration" }
        )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<InterviewTime> interviewTimes = await Unit_Of_Work.interviewTime_Repository.Select_All_With_IncludesById<InterviewTime>(
                    i => i.IsDeleted != true,
                    query => query.Include(r => r.AcademicYear)
                    );

            if (interviewTimes == null || interviewTimes.Count == 0)
            {
                return NotFound();
            }

            List<InterviewTimeTableGetDTO> interviewTimeTableGetDTOsDTO = mapper.Map<List<InterviewTimeTableGetDTO>>(interviewTimes);

            return Ok(interviewTimeTableGetDTOsDTO);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("GetInterviewTableByID/{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Interview Time Table", "Registration" }
        )]
        public async Task<IActionResult> GetByID(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (id == 0)
            {
                return BadRequest("Enter Interview Time ID");
            }

            InterviewTime interviewTime = await Unit_Of_Work.interviewTime_Repository.FindByIncludesAsync(
                    i => i.IsDeleted != true && i.ID == id,
                    query => query.Include(r => r.AcademicYear)
                    );

            if (interviewTime == null)
            {
                return NotFound("No Interview Time with this ID");
            }

            InterviewTimeTableGetDTO interviewTimeTableGetDTO = mapper.Map<InterviewTimeTableGetDTO>(interviewTime);

            return Ok(interviewTimeTableGetDTO);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("GetInterviewTableWithYearID/{yearID}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee", "parent" },
            pages: new[] { "Interview Time Table", "Registration" }
        )]
        public async Task<IActionResult> GetInterviewTableWithYearID(long yearID)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (yearID == 0)
            {
                return BadRequest("Enter Academic Year ID");
            }

            AcademicYear academicYear = Unit_Of_Work.academicYear_Repository.First_Or_Default(
                a => a.ID == yearID && a.IsDeleted != true
                );

            if (academicYear == null)
            {
                return NotFound("No Academic Year with this ID");
            }

            List<InterviewTime> interviewTimes = await Unit_Of_Work.interviewTime_Repository.Select_All_With_IncludesById<InterviewTime>(
                    i => i.IsDeleted != true && i.AcademicYearID == yearID,
                    query => query.Include(r => r.AcademicYear)
                    );

            if (interviewTimes == null || interviewTimes.Count == 0)
            {
                return NotFound();
            }

            List<InterviewTimeTableGetDTO> interviewTimeTableGetDTOsDTO = mapper.Map<List<InterviewTimeTableGetDTO>>(interviewTimes);

            return Ok(interviewTimeTableGetDTOsDTO);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("GetInterviewTableWithSchoolID/{schoolID}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Interview Time Table", "Registration" }
        )]
        public async Task<IActionResult> GetInterviewTableWithSchoolID(long schoolID)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (schoolID == 0)
            {
                return BadRequest("Enter School ID");
            }

            School school = Unit_Of_Work.school_Repository.First_Or_Default(
                a => a.ID == schoolID && a.IsDeleted !=true
                );

            if (school == null)
            {
                return NotFound("No School with this ID");
            }

            List<InterviewTime> interviewTimes = await Unit_Of_Work.interviewTime_Repository.Select_All_With_IncludesById<InterviewTime>(
                    i => i.IsDeleted != true && i.AcademicYear.SchoolID == schoolID,
                    query => query.Include(r => r.AcademicYear)
                    );

            if (interviewTimes == null || interviewTimes.Count == 0)
            {
                return NotFound();
            }

            List<InterviewTimeTableGetDTO> interviewTimeTableGetDTOsDTO = mapper.Map<List<InterviewTimeTableGetDTO>>(interviewTimes);

            return Ok(interviewTimeTableGetDTOsDTO);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Interview Time Table", "Registration" }
        )]
        public IActionResult Add(InterviewTimeTableAddDTO NewInterviewTimeTable)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (NewInterviewTimeTable == null)
            {
                return BadRequest("Interview Time Table cannot be null");
            }

            if (NewInterviewTimeTable.AcademicYearID != 0)
            {
                AcademicYear academicYear = Unit_Of_Work.academicYear_Repository.First_Or_Default(
                    b => b.ID == NewInterviewTimeTable.AcademicYearID && b.IsDeleted != true
                    );
                if (academicYear == null)
                {
                    return BadRequest("No Academic Year with this ID");
                }
            }
            else
            {
                return BadRequest("Academic Year id cannot be null");
            }

            if (NewInterviewTimeTable.FromDate > NewInterviewTimeTable.ToDate)
            {
                return BadRequest("FromDate cannot be later than ToDate.");
            }

            if (NewInterviewTimeTable.FromTime > NewInterviewTimeTable.ToTime)
            {
                return BadRequest("FromTime cannot be later than ToTime.");
            }

            List<DateOnly> dateList = new List<DateOnly>();
            DateOnly currentDate = NewInterviewTimeTable.FromDate;

            while (currentDate <= NewInterviewTimeTable.ToDate)
            {
                DayOfWeek currentDayOfWeek = currentDate.DayOfWeek;
                DaysEnum currentDayOfWeekEnum = (DaysEnum)currentDayOfWeek;

                if (NewInterviewTimeTable.Days.Contains(currentDayOfWeekEnum))
                {
                    dateList.Add(currentDate);
                }

                currentDate = currentDate.AddDays(1);
            }

            if(dateList.Count == 0)
            {
                return BadRequest("No Dates in this Period of Time");
            }

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");

            for (int i = 0; i < dateList.Count; i++)
            {
                InterviewTime interviewTime = new InterviewTime();
                interviewTime.Date = dateList[i].ToString();
                interviewTime.FromTime = NewInterviewTimeTable.FromTime.ToString();
                interviewTime.ToTime = NewInterviewTimeTable.ToTime.ToString();
                interviewTime.Capacity = NewInterviewTimeTable.Capacity;
                interviewTime.Reserved = 0;
                interviewTime.AcademicYearID = NewInterviewTimeTable.AcademicYearID;
                interviewTime.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                if (userTypeClaim == "octa")
                {
                    interviewTime.InsertedByOctaId = userId;
                }
                else if (userTypeClaim == "employee")
                {
                    interviewTime.InsertedByUserId = userId;
                }
                Unit_Of_Work.interviewTime_Repository.Add(interviewTime);
            }

            Unit_Of_Work.SaveChanges();
            return Ok(new { dates = dateList });
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Interview Time Table", "Registration" }
        )]
        public IActionResult Edit(InterviewTimeTablePutDTO EditedInterviewTimeTable)
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

            if (EditedInterviewTimeTable == null)
            {
                return BadRequest("Interview Time Table cannot be null");
            }

            if (EditedInterviewTimeTable.AcademicYearID != 0)
            {
                AcademicYear academicYear = Unit_Of_Work.academicYear_Repository.First_Or_Default(
                    b => b.ID == EditedInterviewTimeTable.AcademicYearID && b.IsDeleted != true
                    );
                if (academicYear == null)
                {
                    return BadRequest("No Academic Year with this ID");
                }
            }
            else
            {
                return BadRequest("Academic Year id cannot be null");
            }

            if (EditedInterviewTimeTable.FromTime > EditedInterviewTimeTable.ToTime)
            {
                return BadRequest("FromTime cannot be later than ToTime.");
            }

            InterviewTime InterviewTimeTableExists = Unit_Of_Work.interviewTime_Repository.Select_By_Id(EditedInterviewTimeTable.ID);
            if (InterviewTimeTableExists == null || InterviewTimeTableExists.IsDeleted == true)
            {
                return NotFound("No Interview Time Table with this ID");
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Interview Time Table");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (InterviewTimeTableExists.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Interview Time Table page doesn't exist");
                }
            }

            mapper.Map(EditedInterviewTimeTable, InterviewTimeTableExists);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            InterviewTimeTableExists.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                InterviewTimeTableExists.UpdatedByOctaId = userId;
                if (InterviewTimeTableExists.UpdatedByUserId != null)
                {
                    InterviewTimeTableExists.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                InterviewTimeTableExists.UpdatedByUserId = userId;
                if (InterviewTimeTableExists.UpdatedByOctaId != null)
                {
                    InterviewTimeTableExists.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.interviewTime_Repository.Update(InterviewTimeTableExists);
            Unit_Of_Work.SaveChanges();
            return Ok(EditedInterviewTimeTable);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////
        
        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Interview Time Table", "Registration" }
        )]
        public IActionResult Delete(long id)
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

            InterviewTime interviewTime = Unit_Of_Work.interviewTime_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);

            if (interviewTime == null)
            {
                return NotFound();
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Interview Time Table");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Delete_For_Others == false)
                    {
                        if (interviewTime.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Interview Time Table page doesn't exist");
                }
            }

            interviewTime.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            interviewTime.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                interviewTime.DeletedByOctaId = userId;
                if (interviewTime.DeletedByUserId != null)
                {
                    interviewTime.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                interviewTime.DeletedByUserId = userId;
                if (interviewTime.DeletedByOctaId != null)
                {
                    interviewTime.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.interviewTime_Repository.Update(interviewTime);
            Unit_Of_Work.SaveChanges();

            _cancelInterviewDayMessage.CancelInterviewDayMessage(id, HttpContext);

            List<RegisterationFormInterview> registerationFormInterviewToBeCanceled = Unit_Of_Work.registerationFormInterview_Repository.FindBy(
                r => r.IsDeleted != true && r.InterviewTimeID == id
                );

            if(registerationFormInterviewToBeCanceled.Count > 0)
            {
                for(int i = 0; i < registerationFormInterviewToBeCanceled.Count; i++)
                {
                    registerationFormInterviewToBeCanceled[i].IsDeleted = true;
                    registerationFormInterviewToBeCanceled[i].DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                    if (userTypeClaim == "octa")
                    {
                        registerationFormInterviewToBeCanceled[i].DeletedByOctaId = userId;
                        if (registerationFormInterviewToBeCanceled[i].DeletedByUserId != null)
                        {
                            registerationFormInterviewToBeCanceled[i].DeletedByUserId = null;
                        }
                    }
                    else if (userTypeClaim == "employee")
                    {
                        registerationFormInterviewToBeCanceled[i].DeletedByUserId = userId;
                        if (registerationFormInterviewToBeCanceled[i].DeletedByOctaId != null)
                        {
                            registerationFormInterviewToBeCanceled[i].DeletedByOctaId = null;
                        }
                    }

                    Unit_Of_Work.registerationFormInterview_Repository.Update(registerationFormInterviewToBeCanceled[i]);
                }

                Unit_Of_Work.SaveChanges();
            }

            return Ok();
        }
    }
}
