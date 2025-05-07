using AutoMapper;
using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers.Domains.LMS
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class LessonLiveController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;

        public LessonLiveController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
        }

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" }
            //,
            //pages: new[] { "Lesson Live" }
        )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<LessonLive> lessonLives = await Unit_Of_Work.lessonLive_Repository.Select_All_With_IncludesById<LessonLive>(
                    b => b.IsDeleted != true,
                    query => query.Include(d => d.Classroom),
                    query => query.Include(d => d.WeekDay),
                    query => query.Include(d => d.Subject)
                    );

            if (lessonLives == null || lessonLives.Count == 0)
            {
                return NotFound();
            }

            List<LessonLiveGetDTO> lessonLivesDTO = mapper.Map<List<LessonLiveGetDTO>>(lessonLives);

            return Ok(lessonLivesDTO);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" }
            //,
            //pages: new[] { "Lesson Live" }
        )]
        public async Task<IActionResult> GetById(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (id == 0)
            {
                return BadRequest("Enter Lesson Live ID");
            }

            LessonLive lessonLive = await Unit_Of_Work.lessonLive_Repository.FindByIncludesAsync(
                t => t.IsDeleted != true && t.ID == id,
                query => query.Include(d => d.Classroom),
                query => query.Include(d => d.WeekDay),
                query => query.Include(e => e.Subject));


            if (lessonLive == null)
            {
                return NotFound();
            }

            LessonLiveGetDTO LessonLiveGetDTOs = mapper.Map<LessonLiveGetDTO>(lessonLive);

            return Ok(LessonLiveGetDTOs);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("ByStudentId/{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" ,"student" }
            //,
            //pages: new[] { "Lesson Live" }
        )]
        public async Task<IActionResult> GetByStudentId(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (id == 0)
            {
                return BadRequest("Enter student ID");
            }
            Student student = Unit_Of_Work.student_Repository.First_Or_Default(s=>s.ID== id);
            if (student == null)
            {
                return BadRequest("Enter student ID");
            }

            StudentAcademicYear studentAcademicYear = Unit_Of_Work.studentAcademicYear_Repository.First_Or_Default(s => s.StudentID == id && s.Classroom.AcademicYear.IsActive==true);
            if (studentAcademicYear == null)
            {
                return BadRequest("this student Has no class");
            }


            List<LessonLive> lessonLives = await Unit_Of_Work.lessonLive_Repository.Select_All_With_IncludesById<LessonLive>(
                    b => b.IsDeleted != true && b.ClassroomID== studentAcademicYear.ClassID,
                    query => query.Include(d => d.Classroom),
                    query => query.Include(d => d.WeekDay),
                    query => query.Include(d => d.Subject)
                    );

            if (lessonLives == null || lessonLives.Count == 0)
            {
                return NotFound();
            }

            List<LessonLiveGetDTO> lessonLivesDTO = mapper.Map<List<LessonLiveGetDTO>>(lessonLives);

            return Ok(lessonLivesDTO);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" }
            //,
            //pages: new[] { "Lesson Live" }
        )]
        public IActionResult Add(LessonLiveAddDTO NewLessonLive)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (NewLessonLive == null)
            {
                return BadRequest("Lesson Live cannot be null");
            }
              
            Subject subject = Unit_Of_Work.subject_Repository.First_Or_Default(s => s.ID == NewLessonLive.SubjectID && s.IsDeleted != true);
            if (subject == null)
            {
                return BadRequest("No Subject with this ID");
            } 

            Classroom classroom = Unit_Of_Work.classroom_Repository.First_Or_Default(s => s.ID == NewLessonLive.ClassroomID && s.IsDeleted != true);
            if (classroom == null)
            {
                return BadRequest("No Classroom with this ID");
            }
            
            Days day = Unit_Of_Work.days_Repository.First_Or_Default(s => s.ID == NewLessonLive.WeekDayID);
            if (day == null)
            {
                return BadRequest("No Day with this ID");
            }

            LessonLive lessonLive = mapper.Map<LessonLive>(NewLessonLive);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            lessonLive.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                lessonLive.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                lessonLive.InsertedByUserId = userId;
            }

            Unit_Of_Work.lessonLive_Repository.Add(lessonLive);
            Unit_Of_Work.SaveChanges();
            return Ok(NewLessonLive);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1
            //,
            //pages: new[] { "Lesson Live" }
        )]
        public IActionResult Edit(LessonLivePutDTO EditedLessonLive)
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

            if (EditedLessonLive == null)
            {
                return BadRequest("Lesson Live cannot be null");
            }

            Subject subject = Unit_Of_Work.subject_Repository.First_Or_Default(s => s.ID == EditedLessonLive.SubjectID && s.IsDeleted != true);
            if (subject == null)
            {
                return BadRequest("No Subject with this ID");
            }

            Classroom classroom = Unit_Of_Work.classroom_Repository.First_Or_Default(s => s.ID == EditedLessonLive.ClassroomID && s.IsDeleted != true);
            if (classroom == null)
            {
                return BadRequest("No Classroom with this ID");
            }

            Days day = Unit_Of_Work.days_Repository.First_Or_Default(s => s.ID == EditedLessonLive.WeekDayID);
            if (day == null)
            {
                return BadRequest("No Day with this ID");
            }

            LessonLive LessonLiveExists = Unit_Of_Work.lessonLive_Repository.First_Or_Default(b => b.ID == EditedLessonLive.ID && b.IsDeleted != true);
            if (LessonLiveExists == null || LessonLiveExists.IsDeleted == true)
            {
                return NotFound("No Lesson Live with this ID");
            }

            //if (userTypeClaim == "employee")
            //{
            //    IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Lesson Live", roleId, userId, LessonLiveExists);
            //    if (accessCheck != null)
            //    {
            //        return accessCheck;
            //    }
            //}

            mapper.Map(EditedLessonLive, LessonLiveExists);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            LessonLiveExists.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                LessonLiveExists.UpdatedByOctaId = userId;
                if (LessonLiveExists.UpdatedByUserId != null)
                {
                    LessonLiveExists.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                LessonLiveExists.UpdatedByUserId = userId;
                if (LessonLiveExists.UpdatedByOctaId != null)
                {
                    LessonLiveExists.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.lessonLive_Repository.Update(LessonLiveExists);
            Unit_Of_Work.SaveChanges();
            return Ok(EditedLessonLive);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1
            //,
            //pages: new[] { "Lesson Live" }
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
                return BadRequest("Enter Lesson Live ID");
            }

            LessonLive lessonLive = Unit_Of_Work.lessonLive_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);


            if (lessonLive == null)
            {
                return NotFound();
            }

            //if (userTypeClaim == "employee")
            //{
            //    IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "Lesson Live", roleId, userId, lessonLive);
            //    if (accessCheck != null)
            //    {
            //        return accessCheck;
            //    }
            //}

            lessonLive.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            lessonLive.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                lessonLive.DeletedByOctaId = userId;
                if (lessonLive.DeletedByUserId != null)
                {
                    lessonLive.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                lessonLive.DeletedByUserId = userId;
                if (lessonLive.DeletedByOctaId != null)
                {
                    lessonLive.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.lessonLive_Repository.Update(lessonLive);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
