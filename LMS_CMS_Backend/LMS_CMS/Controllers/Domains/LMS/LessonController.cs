using AutoMapper;
using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_DAL.Models.Domains;
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
    public class LessonController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;

        public LessonController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Lesson" }
        )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<Lesson> lessons = await Unit_Of_Work.lesson_Repository.Select_All_With_IncludesById<Lesson>(
                    b => b.IsDeleted != true, 
                    query => query.Include(d => d.SemesterWorkingWeek).ThenInclude(d => d.Semester).ThenInclude(d=>d.AcademicYear),
                    query => query.Include(d => d.Subject)
                    );

            if (lessons == null || lessons.Count == 0)
            {
                return NotFound();
            }

            List<LessonGetDTO> lessonsDTO = mapper.Map<List<LessonGetDTO>>(lessons);

            foreach (var lesson in lessonsDTO)
            {
                lesson.Tags = new List<TagGetDTO>();
                List<LessonTag> lessonTags = Unit_Of_Work.lessonTag_Repository.FindBy(d => d.IsDeleted != true && d.LessonID == lesson.ID);
                foreach (var lessonTag in lessonTags)
                {
                    Tag tag = Unit_Of_Work.tag_Repository.First_Or_Default(d => d.ID == lessonTag.TagID);
                    TagGetDTO tagDTO = mapper.Map<TagGetDTO>(tag);

                    lesson.Tags.Add(tagDTO);
                }
            }

            return Ok(lessonsDTO);
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("GetByID/{id}")]
        //[Authorize_Endpoint_(
        //    allowedTypes: new[] { "octa", "employee" },
        //    pages: new[] { "Lesson" }
        //)]
        public async Task<IActionResult> GetByID(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            Lesson lesson = await Unit_Of_Work.lesson_Repository.FindByIncludesAsync(
                    b => b.IsDeleted != true && b.ID == id, 
                    query => query.Include(d => d.SemesterWorkingWeek).ThenInclude(d => d.Semester).ThenInclude(d=>d.AcademicYear),
                    query => query.Include(d => d.Subject)
                    );

            if (lesson == null)
            {
                return NotFound();
            }

            LessonGetDTO lessonDTO = mapper.Map<LessonGetDTO>(lesson);

            lessonDTO.Tags = new List<TagGetDTO>();
            List<LessonTag> lessonTags = Unit_Of_Work.lessonTag_Repository.FindBy(d => d.IsDeleted != true && d.LessonID == lessonDTO.ID);
            foreach (var lessonTag in lessonTags)
            {
                Tag tag = Unit_Of_Work.tag_Repository.First_Or_Default(d => d.ID == lessonTag.TagID);
                TagGetDTO tagDTO = mapper.Map<TagGetDTO>(tag);

                lessonDTO.Tags.Add(tagDTO);
            }
             
            return Ok(lessonDTO);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("GetBySubjectIDAndSemester/{SubjectId}/{SemesterId}")]
        //[Authorize_Endpoint_(
        //    allowedTypes: new[] { "octa", "employee" },
        //    pages: new[] { "Lesson" }
        //)]
        public async Task<IActionResult> GetBySubjectIDAsync(long SubjectId, long SemesterId)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            Subject subject = Unit_Of_Work.subject_Repository.First_Or_Default(d => d.IsDeleted != true && d.ID == SubjectId);
            if(subject == null)
            {
                return NotFound("No Subject with this ID");
            }
            
            Semester semester = Unit_Of_Work.semester_Repository.First_Or_Default(d => d.IsDeleted != true && d.ID == SemesterId);
            if(semester == null)
            {
                return NotFound("No Semester with this ID");
            }

            List<Lesson> lessons = await Unit_Of_Work.lesson_Repository.Select_All_With_IncludesById<Lesson>(
                    b => b.IsDeleted != true && b.SubjectID == SubjectId && b.SemesterWorkingWeek.SemesterID == SemesterId,
                    query => query.Include(d => d.SemesterWorkingWeek).ThenInclude(d => d.Semester).ThenInclude(d => d.AcademicYear),
                    query => query.Include(d => d.Subject)
                    );

            if (lessons == null || lessons.Count == 0)
            {
                return NotFound();
            }

            List<LessonGetDTO> lessonsDTO = mapper.Map<List<LessonGetDTO>>(lessons);

            foreach (var lesson in lessonsDTO)
            {
                lesson.Tags = new List<TagGetDTO>();
                List<LessonTag> lessonTags = Unit_Of_Work.lessonTag_Repository.FindBy(d => d.IsDeleted != true && d.LessonID == lesson.ID);
                foreach (var lessonTag in lessonTags)
                {
                    Tag tag = Unit_Of_Work.tag_Repository.First_Or_Default(d => d.ID == lessonTag.TagID);
                    TagGetDTO tagDTO = mapper.Map<TagGetDTO>(tag);

                    lesson.Tags.Add(tagDTO);
                }
            }

            return Ok(lessonsDTO);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        //[Authorize_Endpoint_(
        //    allowedTypes: new[] { "octa", "employee" },
        //    pages: new[] { "Lesson" }
        //)]
        public IActionResult Add(LessonAddDTO NewLesson)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (NewLesson == null)
            {
                return BadRequest("Lesson cannot be null");
            }

            Subject subject = Unit_Of_Work.subject_Repository.First_Or_Default(s => s.ID == NewLesson.SubjectID && s.IsDeleted != true);
            if (subject == null)
            {
                return BadRequest("No Subject with this ID");
            }

            SemesterWorkingWeek semesterWorkingWeek = Unit_Of_Work.semesterWorkingWeek_Repository.First_Or_Default(s => s.ID == NewLesson.SemesterWorkingWeekID && s.IsDeleted != true);
            if (semesterWorkingWeek == null)
            {
                return BadRequest("No Semester Working Week with this ID");
            }

            Lesson lesson = mapper.Map<Lesson>(NewLesson);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            lesson.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                lesson.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                lesson.InsertedByUserId = userId;
            }

            Unit_Of_Work.lesson_Repository.Add(lesson);
            Unit_Of_Work.SaveChanges();

            if(NewLesson.TagNames != null && NewLesson.TagNames.Count != 0)
            {
                foreach (var tagName in NewLesson.TagNames)
                {
                    Tag tag = new Tag();
                    tag.Name = tagName;

                    tag.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                    if (userTypeClaim == "octa")
                    {
                        tag.InsertedByOctaId = userId;
                    }
                    else if (userTypeClaim == "employee")
                    {
                        tag.InsertedByUserId = userId;
                    }

                    Unit_Of_Work.tag_Repository.Add(tag);
                    Unit_Of_Work.SaveChanges();

                    LessonTag lessonTag = new LessonTag();
                    lessonTag.TagID = tag.ID;
                    lessonTag.LessonID = lesson.ID;

                    lessonTag.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                    if (userTypeClaim == "octa")
                    {
                        lessonTag.InsertedByOctaId = userId;
                    }
                    else if (userTypeClaim == "employee")
                    {
                        lessonTag.InsertedByUserId = userId;
                    }

                    Unit_Of_Work.lessonTag_Repository.Add(lessonTag);
                    Unit_Of_Work.SaveChanges();
                }
            }
            return Ok(NewLesson);
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost("ImportLessonFrom")]
        //[Authorize_Endpoint_(
        //    allowedTypes: new[] { "octa", "employee" },
        //    pages: new[] { "Lesson" }
        //)]
        public IActionResult ImportLessonFrom(LessonImportDTO ImportLesson)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (ImportLesson == null)
            {
                return BadRequest("Lesson cannot be null");
            }
             
            SemesterWorkingWeek semesterWorkingWeek = Unit_Of_Work.semesterWorkingWeek_Repository.First_Or_Default(s => s.ID == ImportLesson.ToSemesterWorkingWeekID && s.IsDeleted != true);
            if (semesterWorkingWeek == null)
            {
                return BadRequest("No Semester Working Week with this ID");
            }

            Lesson LessonExists = Unit_Of_Work.lesson_Repository.First_Or_Default(b => b.ID == ImportLesson.FromLessonID && b.IsDeleted != true);
            if (LessonExists == null || LessonExists.IsDeleted == true)
            {
                return NotFound("No Lesson with this ID");
            }

            Lesson lesson = new Lesson();
            lesson.SubjectID = LessonExists.SubjectID;
            lesson.Order = LessonExists.Order;
            lesson.ArabicTitle = LessonExists.ArabicTitle;
            lesson.EnglishTitle = LessonExists.EnglishTitle;
            lesson.Details = LessonExists.Details;
            lesson.SemesterWorkingWeekID = ImportLesson.ToSemesterWorkingWeekID;
             
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            lesson.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                lesson.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                lesson.InsertedByUserId = userId;
            }

            Unit_Of_Work.lesson_Repository.Add(lesson);
            Unit_Of_Work.SaveChanges();

            List<LessonTag> lessonTags = Unit_Of_Work.lessonTag_Repository.FindBy(d => d.IsDeleted != true && d.LessonID == LessonExists.ID);
            if (lessonTags != null)
            {
                foreach (var item in lessonTags)
                {
                    LessonTag lessonTag = new LessonTag();
                    lessonTag.TagID = item.TagID;
                    lessonTag.LessonID = lesson.ID;

                    lessonTag.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                    if (userTypeClaim == "octa")
                    {
                        lessonTag.InsertedByOctaId = userId;
                    }
                    else if (userTypeClaim == "employee")
                    {
                        lessonTag.InsertedByUserId = userId;
                    }

                    Unit_Of_Work.lessonTag_Repository.Add(lessonTag);
                    Unit_Of_Work.SaveChanges();
                }
            }
             
            return Ok();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPut]
        //[Authorize_Endpoint_(
        //    allowedTypes: new[] { "octa", "employee" },
        //    allowEdit: 1,
        //    pages: new[] { "Lesson" }
        //)]
        public IActionResult Edit(LessonPutDTO EditedLesson)
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

            if (EditedLesson == null)
            {
                return BadRequest("Lesson cannot be null");
            }

            Subject subject = Unit_Of_Work.subject_Repository.First_Or_Default(s => s.ID == EditedLesson.SubjectID && s.IsDeleted != true);
            if (subject == null)
            {
                return BadRequest("No Subject with this ID");
            }

            SemesterWorkingWeek semesterWorkingWeek = Unit_Of_Work.semesterWorkingWeek_Repository.First_Or_Default(s => s.ID == EditedLesson.SemesterWorkingWeekID && s.IsDeleted != true);
            if (semesterWorkingWeek == null)
            {
                return BadRequest("No Semester Working Week with this ID");
            }

            Lesson LessonExists = Unit_Of_Work.lesson_Repository.First_Or_Default(b => b.ID == EditedLesson.ID && b.IsDeleted != true);
            if (LessonExists == null || LessonExists.IsDeleted == true)
            {
                return NotFound("No Lesson with this ID");
            }

            //if (userTypeClaim == "employee")
            //{
            //    IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Lesson", roleId, userId, LessonExists);
            //    if (accessCheck != null)
            //    {
            //        return accessCheck;
            //    }
            //}

            mapper.Map(EditedLesson, LessonExists);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            LessonExists.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                LessonExists.UpdatedByOctaId = userId;
                if (LessonExists.UpdatedByUserId != null)
                {
                    LessonExists.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                LessonExists.UpdatedByUserId = userId;
                if (LessonExists.UpdatedByOctaId != null)
                {
                    LessonExists.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.lesson_Repository.Update(LessonExists);
            Unit_Of_Work.SaveChanges();

            List<LessonTag> lessonTags = Unit_Of_Work.lessonTag_Repository.FindBy(d => d.IsDeleted != true && d.LessonID == EditedLesson.ID);
            if (lessonTags != null)
            {
                foreach (var lessonTag in lessonTags)
                { 
                    if (EditedLesson.TagIDs == null || !EditedLesson.TagIDs.Contains(lessonTag.TagID))
                    {
                        //Tag tag = Unit_Of_Work.tag_Repository.First_Or_Default(d => d.IsDeleted != true && d.ID == lessonTag.TagID);
                        //tag.IsDeleted = true;
                        //tag.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                        //if (userTypeClaim == "octa")
                        //{
                        //    tag.DeletedByOctaId = userId;
                        //    if (tag.DeletedByUserId != null)
                        //    {
                        //        tag.DeletedByUserId = null;
                        //    }
                        //}
                        //else if (userTypeClaim == "employee")
                        //{
                        //    tag.DeletedByUserId = userId;
                        //    if (tag.DeletedByOctaId != null)
                        //    {
                        //        tag.DeletedByOctaId = null;
                        //    }
                        //}

                        //Unit_Of_Work.tag_Repository.Update(tag);

                        lessonTag.IsDeleted = true;
                        lessonTag.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                        if (userTypeClaim == "octa")
                        {
                            lessonTag.DeletedByOctaId = userId;
                            if (lessonTag.DeletedByUserId != null)
                            {
                                lessonTag.DeletedByUserId = null;
                            }
                        }
                        else if (userTypeClaim == "employee")
                        {
                            lessonTag.DeletedByUserId = userId;
                            if (lessonTag.DeletedByOctaId != null)
                            {
                                lessonTag.DeletedByOctaId = null;
                            }
                        }

                        Unit_Of_Work.lessonTag_Repository.Update(lessonTag);
                    }
                    Unit_Of_Work.SaveChanges();
                }
            }

            if (EditedLesson.TagNames != null && EditedLesson.TagNames.Count != 0)
            {
                foreach (var tagName in EditedLesson.TagNames)
                {
                    Tag tag = new Tag();
                    tag.Name = tagName;

                    tag.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                    if (userTypeClaim == "octa")
                    {
                        tag.InsertedByOctaId = userId;
                    }
                    else if (userTypeClaim == "employee")
                    {
                        tag.InsertedByUserId = userId;
                    }

                    Unit_Of_Work.tag_Repository.Add(tag);
                    Unit_Of_Work.SaveChanges();

                    LessonTag lessonTag = new LessonTag();
                    lessonTag.TagID = tag.ID;
                    lessonTag.LessonID = EditedLesson.ID;

                    lessonTag.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                    if (userTypeClaim == "octa")
                    {
                        lessonTag.InsertedByOctaId = userId;
                    }
                    else if (userTypeClaim == "employee")
                    {
                        lessonTag.InsertedByUserId = userId;
                    }

                    Unit_Of_Work.lessonTag_Repository.Add(lessonTag);
                    Unit_Of_Work.SaveChanges();
                }
            }

            return Ok();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpDelete("{id}")]
        //[Authorize_Endpoint_(
        //    allowedTypes: new[] { "octa", "employee" },
        //    allowDelete: 1,
        //    pages: new[] { "Lesson" }
        //)]
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
                return BadRequest("Enter Lesson ID");
            }

            Lesson lesson = Unit_Of_Work.lesson_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);


            if (lesson == null)
            {
                return NotFound();
            }

            //if (userTypeClaim == "employee")
            //{
            //    IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "Lesson", roleId, userId, lesson);
            //    if (accessCheck != null)
            //    {
            //        return accessCheck;
            //    }
            //}

            lesson.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            lesson.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                lesson.DeletedByOctaId = userId;
                if (lesson.DeletedByUserId != null)
                {
                    lesson.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                lesson.DeletedByUserId = userId;
                if (lesson.DeletedByOctaId != null)
                {
                    lesson.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.lesson_Repository.Update(lesson);
            Unit_Of_Work.SaveChanges();

            List<LessonTag> lessonTags = Unit_Of_Work.lessonTag_Repository.FindBy(d => d.IsDeleted != true && d.LessonID == id);
            if(lessonTags != null)
            {
                foreach (var tagLesson in lessonTags)
                {
                    //Tag tag = Unit_Of_Work.tag_Repository.First_Or_Default(d => d.IsDeleted != true && d.ID == tagLesson.TagID);
                    //tag.IsDeleted = true;
                    //tag.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                    //if (userTypeClaim == "octa")
                    //{
                    //    tag.DeletedByOctaId = userId;
                    //    if (tag.DeletedByUserId != null)
                    //    {
                    //        tag.DeletedByUserId = null;
                    //    }
                    //}
                    //else if (userTypeClaim == "employee")
                    //{
                    //    tag.DeletedByUserId = userId;
                    //    if (tag.DeletedByOctaId != null)
                    //    {
                    //        tag.DeletedByOctaId = null;
                    //    }
                    //}

                    //Unit_Of_Work.tag_Repository.Update(tag);

                    tagLesson.IsDeleted = true;
                    tagLesson.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                    if (userTypeClaim == "octa")
                    {
                        tagLesson.DeletedByOctaId = userId;
                        if (tagLesson.DeletedByUserId != null)
                        {
                            tagLesson.DeletedByUserId = null;
                        }
                    }
                    else if (userTypeClaim == "employee")
                    {
                        tagLesson.DeletedByUserId = userId;
                        if (tagLesson.DeletedByOctaId != null)
                        {
                            tagLesson.DeletedByOctaId = null;
                        }
                    }

                    Unit_Of_Work.lessonTag_Repository.Update(tagLesson);
                }
                Unit_Of_Work.SaveChanges();
            }

            return Ok();
        }
    }
}
