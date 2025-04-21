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
using System.Runtime.InteropServices;

namespace LMS_CMS_PL.Controllers.Domains.LMS
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class SectionController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;
        private readonly SchoolHeaderService _schoolHeaderService;

        public SectionController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService, SchoolHeaderService schoolHeaderService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
            _schoolHeaderService = schoolHeaderService;
        }

        ///////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee", "parent" },
            pages: new[] { "Section" }
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
            List<Section> Sections = await Unit_Of_Work.section_Repository.Select_All_With_IncludesById<Section>(
                    sem => sem.IsDeleted != true,
                    query => query.Include(emp => emp.school));

            if (Sections == null || Sections.Count == 0)
            {
                return NotFound();
            }

            List<SectionGetDTO> SectionDTO = mapper.Map<List<SectionGetDTO>>(Sections);

            return Ok(SectionDTO);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Section" }
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
            Section Sections = await Unit_Of_Work.section_Repository.FindByIncludesAsync(
                    sem => sem.IsDeleted != true && sem.ID == id,
                    query => query.Include(emp => emp.school));

            if (Sections == null)
            {
                return NotFound();
            }

            SectionGetDTO SectionDTO = mapper.Map<SectionGetDTO>(Sections);

            return Ok(SectionDTO);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("BySchoolID/{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Section" }
        )]
        public async Task<IActionResult> GetAsyncBySchoolID(long id)
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
            List<Section> Sections = await Unit_Of_Work.section_Repository.Select_All_With_IncludesById<Section>(
                    sem => sem.IsDeleted != true&&sem.SchoolID==id,
                    query => query.Include(emp => emp.school));

            if (Sections == null || Sections.Count == 0)
            {
                return NotFound();
            }

            List<SectionGetDTO> SectionDTO = mapper.Map<List<SectionGetDTO>>(Sections);

            return Ok(SectionDTO);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Section" }
        )]
        public async Task<IActionResult> Add(SectionAddDTO NewSection)
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
            if (NewSection == null)
            {
                return NotFound();
            }
            if (NewSection.Name == null)
            {
                return BadRequest("the name cannot be null");
            }
            School school = Unit_Of_Work.school_Repository.First_Or_Default(s=>s.ID==NewSection.SchoolID&&s.IsDeleted!=true);
            if (school == null)
            {
              return NotFound("there is no school with this id");
            }

            Section section = mapper.Map<Section>(NewSection);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            section.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                section.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                section.InsertedByUserId = userId;
            }

            Unit_Of_Work.section_Repository.Add(section);
            Unit_Of_Work.SaveChanges();
            return Ok(NewSection);
        }
        ////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Section" }
        )]
        public IActionResult Edit(SectionEditDTO newSection)
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

            if (newSection == null)
            {
                return BadRequest("Section cannot be null");
            }
            if (newSection.Name == null)
            {
                return BadRequest("the name cannot be null");
            }
            School school = Unit_Of_Work.school_Repository.First_Or_Default(s => s.ID == newSection.SchoolID && s.IsDeleted != true);
            if (school == null)
            {
                return NotFound("there is no school with this id");
            }
            Section section = Unit_Of_Work.section_Repository.First_Or_Default(s => s.ID == newSection.ID && s.IsDeleted != true);
            if (section == null)
            {
                return NotFound("there is no section with this id");
            } 

            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Section", roleId, userId, section);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            mapper.Map(newSection, section);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            section.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                section.UpdatedByOctaId = userId;
                if (section.UpdatedByUserId != null)
                {
                    section.UpdatedByUserId = null;
                }

            }
            else if (userTypeClaim == "employee")
            {
                section.UpdatedByUserId = userId;
                if (section.UpdatedByOctaId != null)
                {
                    section.UpdatedByOctaId = null;
                }
            }
            Unit_Of_Work.section_Repository.Update(section);
            Unit_Of_Work.SaveChanges();
            return Ok(newSection);

        }

        //////////////////////////////////////////////////////

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Section" }
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
            Section section = Unit_Of_Work.section_Repository.Select_By_Id(id);

            if (section == null || section.IsDeleted == true)
            {
                return NotFound("No Section with this ID");
            } 

            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "Section", roleId, userId, section);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            section.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            section.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                section.DeletedByOctaId = userId;
                if (section.DeletedByUserId != null)
                {
                    section.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                section.DeletedByUserId = userId;
                if (section.DeletedByOctaId != null)
                {
                    section.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.section_Repository.Update(section);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("GetClassAndStudentCountsInGradesGroupedBySectionByYearID")]
        public async Task<IActionResult> GetClassAndStudentCountsInGradesGroupedBySectionByYearID([FromQuery] long schoolId, [FromQuery] long yearId)
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

            List<Grade> grades = await Unit_Of_Work.grade_Repository.Select_All_With_IncludesById<Grade>(
                query => query.IsDeleted != true && query.Section.SchoolID == schoolId,
                query => query.Include(d => d.Section));


            var groupedGrades = grades.GroupBy(grade => grade.Section);

            List<SectionWithGradeAndCountsDTO> sectionWithGradeAndCounts = new List<SectionWithGradeAndCountsDTO>();

            foreach (var group in groupedGrades)
            {
                SectionWithGradeAndCountsDTO section = new SectionWithGradeAndCountsDTO();
                section.ID = group.Key.ID;
                section.Name = group.Key.Name;
                section.GradeWithStudentClassCount = new List<GradeWithStudentClassCountDTO>();
                GradeWithStudentClassCountDTO TotalCounts = new GradeWithStudentClassCountDTO();

                foreach (var grade in group)
                {
                    GradeWithStudentClassCountDTO gradeWithStudentClassCountDTO = new GradeWithStudentClassCountDTO();
                    int StudentCountPerGrade = 0;
                    int StudentSaudiCountPerGrade = 0;
                    int StudentNoonCountPerGrade = 0;

                    gradeWithStudentClassCountDTO.ID = grade.ID;
                    gradeWithStudentClassCountDTO.Name = grade.Name;

                    List<Classroom> classes = Unit_Of_Work.classroom_Repository.FindBy(d => d.IsDeleted != true && d.AcademicYearID == yearId && d.GradeID == grade.ID);
                    gradeWithStudentClassCountDTO.ClassCount = classes.Count;
                    TotalCounts.ClassCount = TotalCounts.ClassCount + gradeWithStudentClassCountDTO.ClassCount;

                    for (int i = 0; i < classes.Count; i++)
                    {
                        List<StudentAcademicYear> studentAcademicYears = await Unit_Of_Work.studentAcademicYear_Repository.Select_All_With_IncludesById<StudentAcademicYear>(
                            d => d.IsDeleted != true && d.ClassID == classes[i].ID && d.SchoolID == schoolId && d.GradeID == grade.ID,
                            query => query.Include(d => d.Student)
                            );

                        var studentCountPerClass = studentAcademicYears
                            .Select(d => d.StudentID) 
                            .Distinct()
                            .Count();

                        var studentSaudiCountPerClass = studentAcademicYears
                            .Where(d => d.Student.Nationality == 148)  
                            .Select(d => d.StudentID)                 
                            .Distinct()                               
                            .Count();
                        
                        var studentNoonCountPerClass = studentAcademicYears
                            .Where(d => d.Student.IsRegisteredInNoor == true)  
                            .Select(d => d.StudentID)                 
                            .Distinct()                               
                            .Count();

                        StudentCountPerGrade = StudentCountPerGrade + studentCountPerClass;
                        StudentSaudiCountPerGrade = StudentSaudiCountPerGrade + studentSaudiCountPerClass;
                        StudentNoonCountPerGrade = StudentNoonCountPerGrade + studentNoonCountPerClass;

                        gradeWithStudentClassCountDTO.StudentCount = gradeWithStudentClassCountDTO.StudentCount + studentCountPerClass;
                    }

                    gradeWithStudentClassCountDTO.StudentCount = StudentCountPerGrade;
                    TotalCounts.StudentCount = TotalCounts.StudentCount + gradeWithStudentClassCountDTO.StudentCount;

                    gradeWithStudentClassCountDTO.SaudiCount = StudentSaudiCountPerGrade;
                    TotalCounts.SaudiCount = TotalCounts.SaudiCount + gradeWithStudentClassCountDTO.SaudiCount;

                    gradeWithStudentClassCountDTO.NonSaudiCount = StudentCountPerGrade - StudentSaudiCountPerGrade;
                    TotalCounts.NonSaudiCount = TotalCounts.NonSaudiCount + gradeWithStudentClassCountDTO.NonSaudiCount;

                    gradeWithStudentClassCountDTO.StudentsAssignedToNoorCount = StudentNoonCountPerGrade;
                    TotalCounts.StudentsAssignedToNoorCount = TotalCounts.StudentsAssignedToNoorCount + gradeWithStudentClassCountDTO.StudentsAssignedToNoorCount;

                    section.GradeWithStudentClassCount.Add(gradeWithStudentClassCountDTO); 
                }

                section.TotalCounts = TotalCounts;
                sectionWithGradeAndCounts.Add(section);
            }

            string timeZoneId = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? "Egypt Standard Time"
                : "Africa/Cairo";

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

            School_GetDTO schoolDTO = _schoolHeaderService.GetSchoolHeader(Unit_Of_Work, schoolId, Request);

            return Ok(new
            {
                Sections = sectionWithGradeAndCounts,
                School = schoolDTO,
                Date = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone)
            });
        }
    }
}
