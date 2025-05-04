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
using System;

namespace LMS_CMS_PL.Controllers.Domains.LMS
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class LessonActivityController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;

        public LessonActivityController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("GetByLessonID/{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Lesson Activity" }
        )]
        public async Task<IActionResult> GetAsync(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            Lesson lesson = Unit_Of_Work.lesson_Repository.First_Or_Default(d => d.IsDeleted != true && d.ID == id);
            if (lesson == null)
            {
                return NotFound("No Lesson With this ID");
            }

            List<LessonActivity> lessonActivities = await Unit_Of_Work.lessonActivity_Repository.Select_All_With_IncludesById<LessonActivity>(
                    f => f.IsDeleted != true && f.LessonID == id,
                    query => query.Include(emp => emp.Lesson),
                    query => query.Include(emp => emp.LessonActivityType)
                    );

            if (lessonActivities == null || lessonActivities.Count == 0)
            {
                return NotFound();
            }

            List<LessonActivityGetDTO> lessonActivitiesDTO = mapper.Map<List<LessonActivityGetDTO>>(lessonActivities);

            string serverUrl = $"{Request.Scheme}://{Request.Host}/";
            foreach (var lessonActivity in lessonActivitiesDTO)
            {
                if (!string.IsNullOrEmpty(lessonActivity.AttachmentLink) &&
                    lessonActivity.AttachmentLink.StartsWith("Uploads", StringComparison.OrdinalIgnoreCase))
                {
                    lessonActivity.AttachmentLink = $"{serverUrl}{lessonActivity.AttachmentLink.Replace("\\", "/")}";
                }
            }

            return Ok(lessonActivitiesDTO);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("GetById/{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Lesson Activity" }
        )]
        public async Task<IActionResult> GetById(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            LessonActivity lessonActivitie = await Unit_Of_Work.lessonActivity_Repository.FindByIncludesAsync(
                    f => f.IsDeleted != true && f.ID == id,
                    query => query.Include(emp => emp.Lesson),
                    query => query.Include(emp => emp.LessonActivityType)
                    );

            if (lessonActivitie == null)
            {
                return NotFound();
            }

            LessonActivityGetDTO lessonActivitieDTO = mapper.Map<LessonActivityGetDTO>(lessonActivitie);

            string serverUrl = $"{Request.Scheme}://{Request.Host}/";
            if (!string.IsNullOrEmpty(lessonActivitieDTO.AttachmentLink) &&
                lessonActivitieDTO.AttachmentLink.StartsWith("Uploads", StringComparison.OrdinalIgnoreCase))
            {
                lessonActivitieDTO.AttachmentLink = $"{serverUrl}{lessonActivitieDTO.AttachmentLink.Replace("\\", "/")}";
            } 

            return Ok(lessonActivitieDTO);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Lesson Activity" }
        )]
        public async Task<IActionResult> Add([FromForm] LessonActivityAddDTO NewLessonActivity)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (NewLessonActivity == null)
            {
                return BadRequest("Lesson Activity cannot be null");
            }
             
            Lesson lesson = Unit_Of_Work.lesson_Repository.First_Or_Default(g => g.ID == NewLessonActivity.LessonID && g.IsDeleted != true);
            if (lesson == null)
            {
                return BadRequest("No Lesson with this ID");
            } 
            
            LessonActivityType lessonActivityType = Unit_Of_Work.lessonActivityType_Repository.First_Or_Default(g => g.ID == NewLessonActivity.LessonActivityTypeID && g.IsDeleted != true);
            if (lesson == null)
            {
                return BadRequest("No Lesson Activity Type with this ID");
            } 
             
            if(NewLessonActivity.AttachmentFile != null)
            {
                var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/LessonActivity");
                var lessonActivityFolder = Path.Combine(baseFolder, NewLessonActivity.EnglishTitle);
                if (!Directory.Exists(lessonActivityFolder))
                {
                    Directory.CreateDirectory(lessonActivityFolder);
                }

                if (NewLessonActivity.AttachmentFile.Length > 0)
                {
                    var filePath = Path.Combine(lessonActivityFolder, NewLessonActivity.AttachmentFile.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await NewLessonActivity.AttachmentFile.CopyToAsync(stream);
                    }
                }
            }

            LessonActivity lessonActivity = mapper.Map<LessonActivity>(NewLessonActivity);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            lessonActivity.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                lessonActivity.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                lessonActivity.InsertedByUserId = userId;
            }

            if (NewLessonActivity.AttachmentFile != null)
            {
                lessonActivity.AttachmentLink = Path.Combine("Uploads", "LessonActivity", NewLessonActivity.EnglishTitle, NewLessonActivity.AttachmentFile.FileName);
            }

            Unit_Of_Work.lessonActivity_Repository.Add(lessonActivity);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Lesson Activity" }
        )]
        public async Task<IActionResult> Edit([FromForm] LessonActivityPutDTO EditLessonActivity)
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

            if (EditLessonActivity == null)
            {
                return BadRequest("Lesson Activity cannot be null");
            }

            Lesson lesson = Unit_Of_Work.lesson_Repository.First_Or_Default(g => g.ID == EditLessonActivity.LessonID && g.IsDeleted != true);
            if (lesson == null)
            {
                return BadRequest("No Lesson with this ID");
            }

            LessonActivityType lessonActivityType = Unit_Of_Work.lessonActivityType_Repository.First_Or_Default(g => g.ID == EditLessonActivity.LessonActivityTypeID && g.IsDeleted != true);
            if (lesson == null)
            {
                return BadRequest("No Lesson Activity Type with this ID");
            }

            LessonActivity lessonActivityExists = Unit_Of_Work.lessonActivity_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == EditLessonActivity.ID);
            if (lessonActivityExists == null)
            {
                return NotFound("No Lesson Activity with this ID");
            }

            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Lesson Activity", roleId, userId, lessonActivityExists);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            string enNameExists = lessonActivityExists.EnglishTitle;
            string AttachmentLinkExists = lessonActivityExists.AttachmentLink;
            if(EditLessonActivity.AttachmentFile != null)
            {
                var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/LessonActivity");
                if (!string.IsNullOrEmpty(lessonActivityExists.AttachmentLink))
                {
                    if (AttachmentLinkExists != null && AttachmentLinkExists.StartsWith("Uploads", StringComparison.OrdinalIgnoreCase))
                    {
                        var oldLessonActivityFolder = Path.Combine(baseFolder, enNameExists);

                        string existingFilePath = Path.Combine(baseFolder, enNameExists);

                        if (System.IO.File.Exists(existingFilePath))
                        {
                            System.IO.File.Delete(existingFilePath); // Delete the old file
                        }

                        if (Directory.Exists(oldLessonActivityFolder))
                        {
                            Directory.Delete(oldLessonActivityFolder, true);
                        }
                    }

                }

                var lessonActivityFolder = Path.Combine(baseFolder, EditLessonActivity.EnglishTitle);
                if (!Directory.Exists(lessonActivityFolder))
                {
                    Directory.CreateDirectory(lessonActivityFolder);
                }

                if (EditLessonActivity.AttachmentFile.Length > 0)
                {
                    var filePath = Path.Combine(lessonActivityFolder, EditLessonActivity.AttachmentFile.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await EditLessonActivity.AttachmentFile.CopyToAsync(stream);
                    }
                }

                EditLessonActivity.AttachmentLink = Path.Combine("Uploads", "LessonActivity", EditLessonActivity.EnglishTitle, EditLessonActivity.AttachmentFile.FileName); 
            }
            else
            { 
                if(EditLessonActivity.AttachmentLink == null)
                {
                    if (!string.IsNullOrEmpty(lessonActivityExists.AttachmentLink))
                    {
                        if(AttachmentLinkExists.StartsWith("Uploads", StringComparison.OrdinalIgnoreCase))
                        {
                            var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/LessonActivity");
                            var oldLessonActivityFolder = Path.Combine(baseFolder, enNameExists); 

                            string existingFilePath = Path.Combine(baseFolder, enNameExists);

                            if (System.IO.File.Exists(existingFilePath))
                            {
                                System.IO.File.Delete(existingFilePath); // Delete the old file
                            }

                            if (Directory.Exists(oldLessonActivityFolder))
                            {
                                Directory.Delete(oldLessonActivityFolder, true);
                            }
                        }
                    }
                }
                else
                {
                    string url = EditLessonActivity.AttachmentLink.Replace("/", "\\");
                    if (AttachmentLinkExists != null)
                    {
                        if (AttachmentLinkExists.StartsWith("Uploads", StringComparison.OrdinalIgnoreCase))
                        {
                            if (EditLessonActivity.EnglishTitle != enNameExists)
                            {
                                var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/LessonActivity");
                                var oldLessonActivityFolder = Path.Combine(baseFolder, enNameExists);
                                var newLessonActivityFolder = Path.Combine(baseFolder, EditLessonActivity.EnglishTitle);
                                if (url.EndsWith(AttachmentLinkExists, StringComparison.OrdinalIgnoreCase))
                                { 
                                    // Rename the folder if it exists
                                    if (Directory.Exists(oldLessonActivityFolder))
                                    {
                                        if (!Directory.Exists(newLessonActivityFolder))
                                        {
                                            Directory.CreateDirectory(newLessonActivityFolder);
                                        }

                                        var files = Directory.GetFiles(oldLessonActivityFolder);
                                        foreach (var file in files)
                                        {
                                            var fileName = Path.GetFileName(file);
                                            var destFile = Path.Combine(newLessonActivityFolder, fileName);
                                            System.IO.File.Move(file, destFile);
                                        }

                                        Directory.Delete(oldLessonActivityFolder);
                                    }
                                    EditLessonActivity.AttachmentLink = Path.Combine("Uploads", "LessonActivity", EditLessonActivity.EnglishTitle, Path.GetFileName(AttachmentLinkExists));
                                }
                                else
                                {
                                    EditLessonActivity.AttachmentLink = EditLessonActivity.AttachmentLink;  

                                    string existingFilePath = Path.Combine(baseFolder, enNameExists);

                                    if (System.IO.File.Exists(existingFilePath))
                                    {
                                        System.IO.File.Delete(existingFilePath); // Delete the old file
                                    }

                                    if (Directory.Exists(oldLessonActivityFolder))
                                    {
                                        Directory.Delete(oldLessonActivityFolder, true);
                                    }
                                }
                            }
                            else
                            {
                                var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/LessonActivity");
                                var oldLessonActivityFolder = Path.Combine(baseFolder, enNameExists); 

                                string existingFilePath = Path.Combine(baseFolder, enNameExists);

                                if (System.IO.File.Exists(existingFilePath))
                                {
                                    System.IO.File.Delete(existingFilePath); // Delete the old file
                                }

                                if (Directory.Exists(oldLessonActivityFolder))
                                {
                                    Directory.Delete(oldLessonActivityFolder, true);
                                }

                                if (url.EndsWith(AttachmentLinkExists, StringComparison.OrdinalIgnoreCase))
                                {
                                    EditLessonActivity.AttachmentLink = AttachmentLinkExists;
                                }
                                else{
                                    EditLessonActivity.AttachmentLink = EditLessonActivity.AttachmentLink;
                                }
                            }
                        }
                        else
                        {
                            EditLessonActivity.AttachmentLink = EditLessonActivity.AttachmentLink;
                        }
                    }
                    else
                    {

                        EditLessonActivity.AttachmentLink = EditLessonActivity.AttachmentLink;
                    }
                }
            } 

            mapper.Map(EditLessonActivity, lessonActivityExists);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            lessonActivityExists.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                lessonActivityExists.UpdatedByOctaId = userId;
                if (lessonActivityExists.UpdatedByUserId != null)
                {
                    lessonActivityExists.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                lessonActivityExists.UpdatedByUserId = userId;
                if (lessonActivityExists.UpdatedByOctaId != null)
                {
                    lessonActivityExists.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.lessonActivity_Repository.Update(lessonActivityExists);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Lesson Activity" }
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
                return BadRequest("Enter Lesson Activity ID");
            }

            LessonActivity lessonActivity = Unit_Of_Work.lessonActivity_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);

            if (lessonActivity == null)
            {
                return NotFound();
            }

            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "Lesson Activity", roleId, userId, lessonActivity);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            lessonActivity.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            lessonActivity.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                lessonActivity.DeletedByOctaId = userId;
                if (lessonActivity.DeletedByUserId != null)
                {
                    lessonActivity.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                lessonActivity.DeletedByUserId = userId;
                if (lessonActivity.DeletedByOctaId != null)
                {
                    lessonActivity.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.lessonActivity_Repository.Update(lessonActivity);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
