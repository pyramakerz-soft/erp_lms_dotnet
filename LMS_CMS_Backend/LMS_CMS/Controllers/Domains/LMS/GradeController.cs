using AutoMapper;
using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.BusModule;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Unicode;
using LMS_CMS_DAL.Models.Octa;
using System.Runtime.InteropServices;

namespace LMS_CMS_PL.Controllers.Domains.LMS
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class GradeController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;
        private readonly SchoolHeaderService _schoolHeaderService;

        public GradeController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService, SchoolHeaderService schoolHeaderService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
            _schoolHeaderService = schoolHeaderService;
        }

        ///////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" }
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
            List<Grade> Grades = await Unit_Of_Work.grade_Repository.Select_All_With_IncludesById<Grade>(
                    sem => sem.IsDeleted != true,
                    query => query.Include(emp => emp.Section));

            if (Grades == null || Grades.Count == 0)
            {
                return NotFound();
            }

            List<GradeGetDTO> GradeDTO = mapper.Map<List<GradeGetDTO>>(Grades);

            return Ok(GradeDTO);
        }
        ///////////////////////////////////////////////////////////////////////////////////

        [HttpGet("GetBySection/{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee", "parent" }
        )]
        public async Task<IActionResult> GetAsync(long id)
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
            List<Grade> Grades = await Unit_Of_Work.grade_Repository.Select_All_With_IncludesById<Grade>(
                    sem => sem.IsDeleted != true&&sem.SectionID==id,
                    query => query.Include(emp => emp.Section));

            if (Grades == null || Grades.Count == 0)
            {
                return NotFound();
            }

            List<GradeGetDTO> GradeDTO = mapper.Map<List<GradeGetDTO>>(Grades);

            return Ok(GradeDTO);
        }

        ///////////////////////////////////////////////////////////////////////////////////

        [HttpGet("GetBySchool/{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" }
        )]
        public async Task<IActionResult> GetBySchool(long id)
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

            if(id == 0 || id == null)
            {
                return BadRequest("Please Enter School ID");
            }

            School school = Unit_Of_Work.school_Repository.First_Or_Default( d => d.IsDeleted != true && d.ID == id);

            if (school == null) 
            {
                return NotFound("There is no School with this ID");
            }

            List<Grade> Grades = await Unit_Of_Work.grade_Repository.Select_All_With_IncludesById<Grade>(
                    sem => sem.IsDeleted != true && sem.Section.SchoolID == id,
                    query => query.Include(emp => emp.Section));

            if (Grades == null || Grades.Count == 0)
            {
                return NotFound();
            }

            List<GradeGetDTO> GradeDTO = mapper.Map<List<GradeGetDTO>>(Grades);

            return Ok(GradeDTO);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Grade" }
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
            Grade grade = await Unit_Of_Work.grade_Repository.FindByIncludesAsync(
                    sem => sem.IsDeleted != true && sem.ID == id,
                    query => query.Include(emp => emp.Section));

            if (grade == null)
            {
                return NotFound("there is no Grade With This Id");
            }

            GradeGetDTO gradeDTO = mapper.Map<GradeGetDTO>(grade);

            return Ok(gradeDTO);
        }

        //////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Grade" }
        )]
        public async Task<IActionResult> Add(GradeAddDTO Newgrade)
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
            if (Newgrade == null)
            {
                return BadRequest("Grade is empty");
            }
            if (Newgrade.SectionID == null)
            {
                return BadRequest("section id can not be null");
            }
            if (Newgrade.Name == null)
            {
                return BadRequest("the name cannot be null");
            }
            Section section=Unit_Of_Work.section_Repository.First_Or_Default(s=>s.ID== Newgrade.SectionID&&s.IsDeleted!=true);
            if (section == null)
            {
                return BadRequest("this section not found");
            }
            Grade grade = mapper.Map<Grade>(Newgrade);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            grade.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                grade.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                grade.InsertedByUserId = userId;
            }
            Unit_Of_Work.grade_Repository.Add(grade);
            Unit_Of_Work.SaveChanges();
            return Ok(Newgrade);
        }

        ////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Grade" }
        )]
        public async Task<IActionResult> EditAsync(GradeEditDTO newGrade)
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

            if (newGrade == null)
            {
                return BadRequest("Semester cannot be null");
            }
            if (newGrade.SectionID == null)
            {
                return BadRequest("section id can not be null");
            }
            if (newGrade.Name == null)
            {
                return BadRequest("the name cannot be null");
            }

            Section section = Unit_Of_Work.section_Repository.First_Or_Default(s => s.ID == newGrade.SectionID&&s.IsDeleted!=true);
            if (section == null)
            {
                return BadRequest("this section not found");
            }

            Grade grade = Unit_Of_Work.grade_Repository.First_Or_Default(s => s.ID == newGrade.ID && s.IsDeleted != true);
            if (grade == null)
            {
                return BadRequest("this grade not exist");
            } 

            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Grade", roleId, userId, grade);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            mapper.Map(newGrade, grade);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            grade.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                grade.UpdatedByOctaId = userId;
                if (grade.UpdatedByUserId != null)
                {
                    grade.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                grade.UpdatedByUserId = userId;
                if (grade.UpdatedByOctaId != null)
                {
                    grade.UpdatedByOctaId = null;
                }
            }
            Unit_Of_Work.grade_Repository.Update(grade);
            Unit_Of_Work.SaveChanges();
            return Ok(newGrade);
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Grade" }
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

            if (id == null)
            {
                return BadRequest("id cannot be null");
            }
            Grade grade = Unit_Of_Work.grade_Repository.Select_By_Id(id); 

            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "Grade", roleId, userId, grade);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            if (grade == null || grade.IsDeleted == true)
            {
                return NotFound("No grade with this ID");
            }

            grade.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            grade.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                grade.DeletedByOctaId = userId;
                if (grade.DeletedByUserId != null)
                {
                    grade.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                grade.DeletedByUserId = userId;
                if (grade.DeletedByOctaId != null)
                {
                    grade.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.grade_Repository.Update(grade);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("GetGradeAndStudentCountByYearID")]
        public async Task<IActionResult> GetGradeAndStudentCountByYearID([FromQuery] long schoolId, [FromQuery] long yearId)
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

            if (schoolId == null || schoolId == 0)
            {
                return BadRequest("School Id can't be null");
            }

            if (yearId == null || yearId == 0)
            {
                return BadRequest("Academic Year Id can't be null");
            }  

            AcademicYear year = Unit_Of_Work.academicYear_Repository.First_Or_Default(
                d => d.IsDeleted != true && d.ID == yearId
                );
            if (year == null)
            {
                return NotFound("No Academic Year with this Id");
            }


            // Get All Classes Where Academic Year 
            List<Classroom> classrooms = await Unit_Of_Work.classroom_Repository.Select_All_With_IncludesById<Classroom>(
                query => query.IsDeleted != true && query.AcademicYearID == yearId,
                query => query.Include(d => d.AcademicYear), 
                query => query.Include(d => d.Grade).ThenInclude(d => d.Section));

        //public class GradeWithStudentClassCountDTO
        //{
        //    public long ID { get; set; }
        //    public string Name { get; set; }
        //    public int ClassCount { get; set; }
        //    public int SaudiCount { get; set; }
        //    public int NonSaudiCount { get; set; }
        //    public int StudentCount { get; set; }
        //    public int StudentsAssignedToNoorCount { get; set; }
        //}
            var result = classrooms
                .GroupBy(c => new { c.Grade, c.Grade.Section })  // Group by Grade and Section
                .Select(g => new
                {
                    Grade = g.Key.Grade,
                    Section = g.Key.Section,
                    ClassCount = g.Count()  // Count of classrooms for this grade-section combination
                })
                .ToList();
             

            string timeZoneId = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? "Egypt Standard Time"
                : "Africa/Cairo";

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

            School_GetDTO schoolDTO = _schoolHeaderService.GetSchoolHeader(Unit_Of_Work, schoolId, Request);

            return Ok(new
            {
                //Grades = gradeDTOs, 
                School = schoolDTO, 
                Date = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone)
            });
        }
    }
}
