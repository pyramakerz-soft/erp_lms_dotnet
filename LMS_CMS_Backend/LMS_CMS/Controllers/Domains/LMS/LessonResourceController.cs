using AutoMapper;
using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.UOW;
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
    public class LessonResourceController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;

        public LessonResourceController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("GetByLessonID/{id}")]
        //[Authorize_Endpoint_(
        //    allowedTypes: new[] { "octa", "employee" },
        //    pages: new[] { "Lesson Resource" }
        //)]
        public async Task<IActionResult> GetAsync(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            Lesson lesson = Unit_Of_Work.lesson_Repository.First_Or_Default(d => d.IsDeleted != true && d.ID == id);
            if(lesson == null)
            {
                return NotFound("No Lesson With this ID");
            }

            List<LessonResource> lessonResources = await Unit_Of_Work.lessonResource_Repository.Select_All_With_IncludesById<LessonResource>(
                    f => f.IsDeleted != true && f.LessonID == id,
                    query => query.Include(emp => emp.Lesson),
                    query => query.Include(emp => emp.LessonResourceType)
                    );

            if (lessonResources == null || lessonResources.Count == 0)
            {
                return NotFound();
            }

            List<LessonResourceGetDTO> lessonResourcesDTO = mapper.Map<List<LessonResourceGetDTO>>(lessonResources);

            string serverUrl = $"{Request.Scheme}://{Request.Host}/";
            foreach (var lessonResource in lessonResourcesDTO)
            {
                if (!string.IsNullOrEmpty(lessonResource.AttachmentLink) &&
                    lessonResource.AttachmentLink.StartsWith("Uploads", StringComparison.OrdinalIgnoreCase))
                {
                    lessonResource.AttachmentLink = $"{serverUrl}{lessonResource.AttachmentLink.Replace("\\", "/")}";
                }
            }

            foreach (var lessonResource in lessonResourcesDTO)
            {
                lessonResource.Classrooms = new List<ClassroomGetDTO>();
                List<LessonResourceClassroom> lessonResourceClassrooms = Unit_Of_Work.lessonResourceClassroom_Repository.FindBy(d => d.IsDeleted != true && d.LessonResourceID == lessonResource.ID);
                foreach (var lessonResourceClassroom in lessonResourceClassrooms)
                {
                    Classroom classroom = Unit_Of_Work.classroom_Repository.First_Or_Default(d => d.ID == lessonResourceClassroom.ClassroomID);
                    ClassroomGetDTO classroomGetDTO = mapper.Map<ClassroomGetDTO>(classroom);

                    lessonResource.Classrooms.Add(classroomGetDTO);
                }
            }

            return Ok(lessonResourcesDTO);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("GetById/{id}")]
        //[Authorize_Endpoint_(
        //    allowedTypes: new[] { "octa", "employee" },
        //    pages: new[] { "Lesson Resource" }
        //)]
        public async Task<IActionResult> GetById(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            LessonResource lessonResource = await Unit_Of_Work.lessonResource_Repository.FindByIncludesAsync(
                    f => f.IsDeleted != true && f.ID == id,
                    query => query.Include(emp => emp.Lesson),
                    query => query.Include(emp => emp.LessonResourceType)
                    );

            if (lessonResource == null)
            {
                return NotFound();
            }

            LessonResourceGetDTO lessonResourceDTO = mapper.Map<LessonResourceGetDTO>(lessonResource);

            string serverUrl = $"{Request.Scheme}://{Request.Host}/";
            if (!string.IsNullOrEmpty(lessonResourceDTO.AttachmentLink) &&
                lessonResourceDTO.AttachmentLink.StartsWith("Uploads", StringComparison.OrdinalIgnoreCase))
            {
                lessonResourceDTO.AttachmentLink = $"{serverUrl}{lessonResourceDTO.AttachmentLink.Replace("\\", "/")}";
            }

            lessonResourceDTO.Classrooms = new List<ClassroomGetDTO>();
            List<LessonResourceClassroom> lessonResourceClassrooms = Unit_Of_Work.lessonResourceClassroom_Repository.FindBy(d => d.IsDeleted != true && d.LessonResourceID == lessonResourceDTO.ID);
            foreach (var lessonResourceClassroom in lessonResourceClassrooms)
            {
                Classroom classroom = Unit_Of_Work.classroom_Repository.First_Or_Default(d => d.ID == lessonResourceClassroom.ClassroomID);
                ClassroomGetDTO classroomGetDTO = mapper.Map<ClassroomGetDTO>(classroom);

                lessonResourceDTO.Classrooms.Add(classroomGetDTO);
            }

            return Ok(lessonResourceDTO);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        //[Authorize_Endpoint_(
        //    allowedTypes: new[] { "octa", "employee" },
        //    pages: new[] { "Lesson Resource" }
        //)]
        public async Task<IActionResult> Add([FromForm] LessonResourceAddDTO NewLessonResource)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (NewLessonResource == null)
            {
                return BadRequest("Lesson Resource cannot be null");
            }

            Lesson lesson = Unit_Of_Work.lesson_Repository.First_Or_Default(g => g.ID == NewLessonResource.LessonID && g.IsDeleted != true);
            if (lesson == null)
            {
                return BadRequest("No Lesson with this ID");
            }

            LessonResourceType lessonResourceType = Unit_Of_Work.lessonResourceType_Repository.First_Or_Default(g => g.ID == NewLessonResource.LessonResourceTypeID && g.IsDeleted != true);
            if (lesson == null)
            {
                return BadRequest("No Lesson Resource Type with this ID");
            }

            if (NewLessonResource.AttachmentFile != null)
            {
                var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/LessonResource");
                var lessonResourceFolder = Path.Combine(baseFolder, NewLessonResource.EnglishTitle);
                if (!Directory.Exists(lessonResourceFolder))
                {
                    Directory.CreateDirectory(lessonResourceFolder);
                }

                if (NewLessonResource.AttachmentFile.Length > 0)
                {
                    var filePath = Path.Combine(lessonResourceFolder, NewLessonResource.AttachmentFile.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await NewLessonResource.AttachmentFile.CopyToAsync(stream);
                    }
                }
            }

            LessonResource lessonResource = mapper.Map<LessonResource>(NewLessonResource);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            lessonResource.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                lessonResource.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                lessonResource.InsertedByUserId = userId;
            }

            if (NewLessonResource.AttachmentFile != null)
            {
                lessonResource.AttachmentLink = Path.Combine("Uploads", "LessonResource", NewLessonResource.EnglishTitle, NewLessonResource.AttachmentFile.FileName);
            }

            Unit_Of_Work.lessonResource_Repository.Add(lessonResource);
            Unit_Of_Work.SaveChanges();

            if (NewLessonResource.Classes != null && NewLessonResource.Classes.Count != 0)
            {
                foreach (var classroom in NewLessonResource.Classes)
                { 
                    Classroom classroomExists = Unit_Of_Work.classroom_Repository.First_Or_Default(d => d.IsDeleted != true && d.ID ==  classroom);

                    if(classroomExists == null)
                    {
                        return NotFound("No Class with this ID");
                    }

                    LessonResourceClassroom lessonResourceClassroom = new LessonResourceClassroom();
                    lessonResourceClassroom.ClassroomID = classroom;
                    lessonResourceClassroom.LessonResourceID = lessonResource.ID;

                    lessonResourceClassroom.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                    if (userTypeClaim == "octa")
                    {
                        lessonResourceClassroom.InsertedByOctaId = userId;
                    }
                    else if (userTypeClaim == "employee")
                    {
                        lessonResourceClassroom.InsertedByUserId = userId;
                    }

                    Unit_Of_Work.lessonResourceClassroom_Repository.Add(lessonResourceClassroom);
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
        //    pages: new[] { "Lesson Resource" }
        //)]
        public async Task<IActionResult> Edit([FromForm] LessonResourcePutDTO EditLessonResource)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
            var userRoleClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            long.TryParse(userRoleClaim, out long roleId);

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (EditLessonResource == null)
            {
                return BadRequest("Lesson Resource cannot be null");
            }

            Lesson lesson = Unit_Of_Work.lesson_Repository.First_Or_Default(g => g.ID == EditLessonResource.LessonID && g.IsDeleted != true);
            if (lesson == null)
            {
                return BadRequest("No Lesson with this ID");
            }

            LessonResourceType lessonResourceType = Unit_Of_Work.lessonResourceType_Repository.First_Or_Default(g => g.ID == EditLessonResource.LessonResourceTypeID && g.IsDeleted != true);
            if (lesson == null)
            {
                return BadRequest("No Lesson Resource Type with this ID");
            }

            LessonResource lessonResourceExists = Unit_Of_Work.lessonResource_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == EditLessonResource.ID);
            if (lessonResourceExists == null)
            {
                return NotFound("No Lesson Resource with this ID");
            }

            //if (userTypeClaim == "employee")
            //{
            //    IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Lesson Resource", roleId, userId, lessonResourceExists);
            //    if (accessCheck != null)
            //    {
            //        return accessCheck;
            //    }
            //}

            string enNameExists = lessonResourceExists.EnglishTitle;
            string AttachmentLinkExists = lessonResourceExists.AttachmentLink;
            if (EditLessonResource.AttachmentFile != null)
            {
                var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/LessonResource");
                if (!string.IsNullOrEmpty(lessonResourceExists.AttachmentLink))
                {
                    if (AttachmentLinkExists != null && AttachmentLinkExists.StartsWith("Uploads", StringComparison.OrdinalIgnoreCase))
                    {
                        var oldLessonResourceFolder = Path.Combine(baseFolder, enNameExists);

                        string existingFilePath = Path.Combine(baseFolder, enNameExists);

                        if (System.IO.File.Exists(existingFilePath))
                        {
                            System.IO.File.Delete(existingFilePath); // Delete the old file
                        }

                        if (Directory.Exists(oldLessonResourceFolder))
                        {
                            Directory.Delete(oldLessonResourceFolder, true);
                        }
                    }

                }

                var lessonResourceFolder = Path.Combine(baseFolder, EditLessonResource.EnglishTitle);
                if (!Directory.Exists(lessonResourceFolder))
                {
                    Directory.CreateDirectory(lessonResourceFolder);
                }

                if (EditLessonResource.AttachmentFile.Length > 0)
                {
                    var filePath = Path.Combine(lessonResourceFolder, EditLessonResource.AttachmentFile.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await EditLessonResource.AttachmentFile.CopyToAsync(stream);
                    }
                }

                EditLessonResource.AttachmentLink = Path.Combine("Uploads", "LessonResource", EditLessonResource.EnglishTitle, EditLessonResource.AttachmentFile.FileName);
            }
            else
            {
                if (EditLessonResource.AttachmentLink == null)
                {
                    if (!string.IsNullOrEmpty(lessonResourceExists.AttachmentLink))
                    {
                        if (AttachmentLinkExists.StartsWith("Uploads", StringComparison.OrdinalIgnoreCase))
                        {
                            var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/LessonResource");
                            var oldLessonResourceFolder = Path.Combine(baseFolder, enNameExists);

                            string existingFilePath = Path.Combine(baseFolder, enNameExists);

                            if (System.IO.File.Exists(existingFilePath))
                            {
                                System.IO.File.Delete(existingFilePath); // Delete the old file
                            }

                            if (Directory.Exists(oldLessonResourceFolder))
                            {
                                Directory.Delete(oldLessonResourceFolder, true);
                            }
                        }
                    }
                }
                else
                {
                    string url = EditLessonResource.AttachmentLink.Replace("/", "\\");
                    if (AttachmentLinkExists != null)
                    {
                        if (AttachmentLinkExists.StartsWith("Uploads", StringComparison.OrdinalIgnoreCase))
                        {
                            if (EditLessonResource.EnglishTitle != enNameExists)
                            {
                                var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/LessonResource");
                                var oldLessonResourceFolder = Path.Combine(baseFolder, enNameExists);
                                var newLessonResourceFolder = Path.Combine(baseFolder, EditLessonResource.EnglishTitle);
                                if (url.EndsWith(AttachmentLinkExists, StringComparison.OrdinalIgnoreCase))
                                {
                                    // Rename the folder if it exists
                                    if (Directory.Exists(oldLessonResourceFolder))
                                    {
                                        if (!Directory.Exists(newLessonResourceFolder))
                                        {
                                            Directory.CreateDirectory(newLessonResourceFolder);
                                        }

                                        var files = Directory.GetFiles(oldLessonResourceFolder);
                                        foreach (var file in files)
                                        {
                                            var fileName = Path.GetFileName(file);
                                            var destFile = Path.Combine(newLessonResourceFolder, fileName);
                                            System.IO.File.Move(file, destFile);
                                        }

                                        Directory.Delete(oldLessonResourceFolder);
                                    }
                                    EditLessonResource.AttachmentLink = Path.Combine("Uploads", "LessonResource", EditLessonResource.EnglishTitle, Path.GetFileName(AttachmentLinkExists));
                                }
                                else
                                {
                                    EditLessonResource.AttachmentLink = EditLessonResource.AttachmentLink;

                                    string existingFilePath = Path.Combine(baseFolder, enNameExists);

                                    if (System.IO.File.Exists(existingFilePath))
                                    {
                                        System.IO.File.Delete(existingFilePath); // Delete the old file
                                    }

                                    if (Directory.Exists(oldLessonResourceFolder))
                                    {
                                        Directory.Delete(oldLessonResourceFolder, true);
                                    }
                                }
                            }
                            else
                            {
                                var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/LessonResource");
                                var oldLessonResourceFolder = Path.Combine(baseFolder, enNameExists);

                                string existingFilePath = Path.Combine(baseFolder, enNameExists);

                                if (System.IO.File.Exists(existingFilePath))
                                {
                                    System.IO.File.Delete(existingFilePath); // Delete the old file
                                }

                                if (Directory.Exists(oldLessonResourceFolder))
                                {
                                    Directory.Delete(oldLessonResourceFolder, true);
                                }

                                if (url.EndsWith(AttachmentLinkExists, StringComparison.OrdinalIgnoreCase))
                                {
                                    EditLessonResource.AttachmentLink = AttachmentLinkExists;
                                }
                                else
                                {
                                    EditLessonResource.AttachmentLink = EditLessonResource.AttachmentLink;
                                }
                            }
                        }
                        else
                        {
                            EditLessonResource.AttachmentLink = EditLessonResource.AttachmentLink;
                        }
                    }
                    else
                    {

                        EditLessonResource.AttachmentLink = EditLessonResource.AttachmentLink;
                    }
                }
            }

            mapper.Map(EditLessonResource, lessonResourceExists);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            lessonResourceExists.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                lessonResourceExists.UpdatedByOctaId = userId;
                if (lessonResourceExists.UpdatedByUserId != null)
                {
                    lessonResourceExists.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                lessonResourceExists.UpdatedByUserId = userId;
                if (lessonResourceExists.UpdatedByOctaId != null)
                {
                    lessonResourceExists.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.lessonResource_Repository.Update(lessonResourceExists);
            Unit_Of_Work.SaveChanges();

            List<LessonResourceClassroom> lessonResourceClassrooms = Unit_Of_Work.lessonResourceClassroom_Repository.FindBy(d => d.IsDeleted != true && d.LessonResourceID == EditLessonResource.ID);
             
            if (lessonResourceClassrooms != null)
            {
                foreach (var lessonResourceClassroom in lessonResourceClassrooms)
                {
                    if (EditLessonResource.Classes == null || !EditLessonResource.Classes.Contains(lessonResourceClassroom.ClassroomID))
                    {
                        lessonResourceClassroom.IsDeleted = true;
                        lessonResourceClassroom.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                        if (userTypeClaim == "octa")
                        {
                            lessonResourceClassroom.DeletedByOctaId = userId;
                            if (lessonResourceClassroom.DeletedByUserId != null)
                            {
                                lessonResourceClassroom.DeletedByUserId = null;
                            }
                        }
                        else if (userTypeClaim == "employee")
                        {
                            lessonResourceClassroom.DeletedByUserId = userId;
                            if (lessonResourceClassroom.DeletedByOctaId != null)
                            {
                                lessonResourceClassroom.DeletedByOctaId = null;
                            }
                        }

                        Unit_Of_Work.lessonResourceClassroom_Repository.Update(lessonResourceClassroom);
                    }
                    Unit_Of_Work.SaveChanges();
                }
            }

            if (EditLessonResource.NewClassRooms != null && EditLessonResource.NewClassRooms.Count != 0)
            {
                foreach (var classroom in EditLessonResource.NewClassRooms)
                {
                    Classroom classroomExists = Unit_Of_Work.classroom_Repository.First_Or_Default(d => d.IsDeleted != true && d.ID == classroom);

                    if (classroomExists == null)
                    {
                        return NotFound("No Class with this ID");
                    }

                    LessonResourceClassroom lessonResourceClassroom = new LessonResourceClassroom();
                    lessonResourceClassroom.ClassroomID = classroom;
                    lessonResourceClassroom.LessonResourceID = lessonResourceExists.ID;

                    lessonResourceClassroom.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                    if (userTypeClaim == "octa")
                    {
                        lessonResourceClassroom.InsertedByOctaId = userId;
                    }
                    else if (userTypeClaim == "employee")
                    {
                        lessonResourceClassroom.InsertedByUserId = userId;
                    }

                    Unit_Of_Work.lessonResourceClassroom_Repository.Add(lessonResourceClassroom);
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
        //    pages: new[] { "Lesson Resource" }
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
                return BadRequest("Enter Lesson Resource ID");
            }

            LessonResource lessonResource = Unit_Of_Work.lessonResource_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);

            if (lessonResource == null)
            {
                return NotFound();
            }

            //if (userTypeClaim == "employee")
            //{
            //    IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "Lesson Resource", roleId, userId, lessonResource);
            //    if (accessCheck != null)
            //    {
            //        return accessCheck;
            //    }
            //}

            lessonResource.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            lessonResource.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                lessonResource.DeletedByOctaId = userId;
                if (lessonResource.DeletedByUserId != null)
                {
                    lessonResource.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                lessonResource.DeletedByUserId = userId;
                if (lessonResource.DeletedByOctaId != null)
                {
                    lessonResource.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.lessonResource_Repository.Update(lessonResource);
            Unit_Of_Work.SaveChanges();

            List<LessonResourceClassroom> lessonResourceClassrooms = Unit_Of_Work.lessonResourceClassroom_Repository.FindBy(d => d.IsDeleted != true && d.LessonResourceID == id);
            if (lessonResourceClassrooms != null)
            {
                foreach (var lessonResourceClassroom in lessonResourceClassrooms)
                {
                    lessonResourceClassroom.IsDeleted = true;
                    lessonResourceClassroom.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                    if (userTypeClaim == "octa")
                    {
                        lessonResourceClassroom.DeletedByOctaId = userId;
                        if (lessonResourceClassroom.DeletedByUserId != null)
                        {
                            lessonResourceClassroom.DeletedByUserId = null;
                        }
                    }
                    else if (userTypeClaim == "employee")
                    {
                        lessonResourceClassroom.DeletedByUserId = userId;
                        if (lessonResourceClassroom.DeletedByOctaId != null)
                        {
                            lessonResourceClassroom.DeletedByOctaId = null;
                        }
                    }

                    Unit_Of_Work.lessonResourceClassroom_Repository.Update(lessonResourceClassroom);
                }
                Unit_Of_Work.SaveChanges();
            }

            return Ok();
        }
    }
}
