using AutoMapper;
using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.BusModule;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers.Domains.LMS
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class SemesterController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public SemesterController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        ///////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Semester", "Administrator" }
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
            List<Semester> Semesters = await Unit_Of_Work.semester_Repository.Select_All_With_IncludesById<Semester>(
                    sem => sem.IsDeleted != true,
                    query => query.Include(emp => emp.AcademicYear));

            if (Semesters == null || Semesters.Count == 0)
            {
                return NotFound();
            }

            List<Semester_GetDTO> SemesterDTO = mapper.Map<List<Semester_GetDTO>>(Semesters);

            return Ok(SemesterDTO);
        }

        //////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("GetByAcademicYear/{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Semester", "Administrator" }
        )]
        public async Task<IActionResult> GetAsyncByAcademicYear(long id)
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
            List<Semester> Semesters = await Unit_Of_Work.semester_Repository.Select_All_With_IncludesById<Semester>(
                    sem => sem.IsDeleted != true && sem.AcademicYearID == id,
                    query => query.Include(emp => emp.AcademicYear));

            if (Semesters == null || Semesters.Count == 0)
            {
                return NotFound();
            }

            List<Semester_GetDTO> SemesterDTO = mapper.Map<List<Semester_GetDTO>>(Semesters);

            return Ok(SemesterDTO);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("id")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Semester", "Administrator" }
        )]
        public async Task<IActionResult> GetAsyncByID(long id)
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
            Semester Semesters = await Unit_Of_Work.semester_Repository.FindByIncludesAsync(
                    sem => sem.IsDeleted != true && sem.ID == id,
                    query => query.Include(emp => emp.AcademicYear));

            if (Semesters == null)
            {
                return NotFound();
            }

            Semester_GetDTO SemesterDTO = mapper.Map<Semester_GetDTO>(Semesters);

            return Ok(SemesterDTO);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Semester", "Administrator" }
        )]
        public async Task<IActionResult> Add(SemesterAddDTO NewSemester)
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
            if (NewSemester == null)
            {
                return NotFound();
            }
            if (NewSemester.Name == null)
            {
                return BadRequest("the name cannot be null");
            }
            AcademicYear academicYear=Unit_Of_Work.academicYear_Repository.First_Or_Default(a=>a.ID==NewSemester.AcademicYearID&&a.IsDeleted!=true);
            if (academicYear == null) {
              return NotFound("there is no AcademicYear whit this id");
            }
            Semester semester = mapper.Map<Semester>(NewSemester);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            semester.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                semester.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                semester.InsertedByUserId = userId;
            }

            Unit_Of_Work.semester_Repository.Add(semester);
            Unit_Of_Work.SaveChanges();
            return Ok(NewSemester);
        }

        ////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Semester", "Administrator" }
        )]
        public IActionResult Edit(Semester_GetDTO newSemester)
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

            if (newSemester == null)
            {
                return BadRequest("Semester cannot be null");
            }
            if (newSemester.Name == null)
            {
                return BadRequest("the name cannot be null");
            }
            AcademicYear academicYear = Unit_Of_Work.academicYear_Repository.First_Or_Default(a => a.ID == newSemester.AcademicYearID && a.IsDeleted != true);
            if (academicYear == null)
            {
                return NotFound("there is no AcademicYear whit this id");
            }
            Semester semester =Unit_Of_Work.semester_Repository.First_Or_Default(s=>s.ID==newSemester.ID && s.IsDeleted != true);
            if (semester == null)
            {
                return NotFound("there is no semester with this id");
            }
            mapper.Map(newSemester, semester);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            semester.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                semester.UpdatedByOctaId = userId;
                if (semester.UpdatedByUserId != null)
                {
                    semester.UpdatedByUserId = null;
                }

            }
            else if (userTypeClaim == "employee")
            {
                semester.UpdatedByUserId = userId;
                if (semester.UpdatedByOctaId != null)
                {
                    semester.UpdatedByOctaId = null;
                }
            }
            Unit_Of_Work.semester_Repository.Update(semester);
            Unit_Of_Work.SaveChanges();
            return Ok(newSemester);

        }

        //////////////////////////////////////////////////////

        [HttpDelete]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Semester", "Administrator" }
        )]
        public IActionResult delete(long id)
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

            if (id == null)
            {
                return BadRequest("id cannot be null");
            }
            Semester semester = Unit_Of_Work.semester_Repository.Select_By_Id(id);

            if (semester == null || semester.IsDeleted == true)
            {
                return NotFound("No semester with this ID");
            }
            semester.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            semester.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                semester.DeletedByOctaId = userId;
                if (semester.DeletedByUserId != null)
                {
                    semester.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                semester.DeletedByUserId = userId;
                if (semester.DeletedByOctaId != null)
                {
                    semester.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.semester_Repository.Update(semester);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
