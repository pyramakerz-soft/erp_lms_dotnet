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
using System.Diagnostics;
using System.Drawing;

namespace LMS_CMS_PL.Controllers.Domains.LMS
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class SubjectController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        private readonly FileImageValidationService _fileImageValidationService;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;

        public SubjectController(DbContextFactoryService dbContextFactory, IMapper mapper, FileImageValidationService fileImageValidationService, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _fileImageValidationService = fileImageValidationService;
            _checkPageAccessService = checkPageAccessService;
        }

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Subject" }
        )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<Subject> subjects = await Unit_Of_Work.subject_Repository.Select_All_With_IncludesById<Subject>(
                    f => f.IsDeleted != true,
                    query => query.Include(emp => emp.Grade),
                    query => query.Include(emp => emp.SubjectCategory)
                    );

            if (subjects == null || subjects.Count == 0)
            {
                return NotFound();
            }

            List<SubjectGetDTO> subjectsDTO = mapper.Map<List<SubjectGetDTO>>(subjects);

            string serverUrl = $"{Request.Scheme}://{Request.Host}/";
            foreach (var subject in subjectsDTO)
            {
                if (!string.IsNullOrEmpty(subject.IconLink))
                {
                    subject.IconLink = $"{serverUrl}{subject.IconLink.Replace("\\", "/")}"; 
                }
            }

            return Ok(subjectsDTO);
        }

        ////////////////////////////////////////////////////////////////////////////////
        
        [HttpGet("GetByGrade/{gradeId}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Subject" }
        )]
        public async Task<IActionResult> GetByGrade(long gradeId)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<Subject> subjects = await Unit_Of_Work.subject_Repository.Select_All_With_IncludesById<Subject>(
                    f => f.IsDeleted != true && f.GradeID == gradeId,
                    query => query.Include(emp => emp.Grade),
                    query => query.Include(emp => emp.SubjectCategory)
                    );

            if (subjects == null || subjects.Count == 0)
            {
                return NotFound();
            }

            List<SubjectGetDTO> subjectsDTO = mapper.Map<List<SubjectGetDTO>>(subjects);

            string serverUrl = $"{Request.Scheme}://{Request.Host}/";
            foreach (var subject in subjectsDTO)
            {
                if (!string.IsNullOrEmpty(subject.IconLink))
                {
                    subject.IconLink = $"{serverUrl}{subject.IconLink.Replace("\\", "/")}"; 
                }
            }

            return Ok(subjectsDTO);
        }

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Subject" }
        )]
        public async Task<IActionResult> GetById(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (id == 0)
            {
                return BadRequest("Enter Subject ID");
            }

            Subject subject = await Unit_Of_Work.subject_Repository.FindByIncludesAsync(
                t => t.IsDeleted != true && t.ID == id, 
                query => query.Include(e => e.Grade),
                query => query.Include(e => e.SubjectCategory)
                );


            if (subject == null)
            {
                return NotFound();
            }

            SubjectGetDTO subjectDTO = mapper.Map<SubjectGetDTO>(subject);

            string serverUrl = $"{Request.Scheme}://{Request.Host}/";
            
            if (!string.IsNullOrEmpty(subjectDTO.IconLink))
            {
                subjectDTO.IconLink = $"{serverUrl}{subjectDTO.IconLink.Replace("\\", "/")}";
            }

            return Ok(subjectDTO);
        }

        [HttpPost] 
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Subject" }
        )]
        public async Task<IActionResult> Add([FromForm]SubjectAddDTO NewSubject)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (NewSubject == null)
            {
                return BadRequest("Subject cannot be null");
            }
            if (NewSubject.en_name == null)
            {
                return BadRequest("the name cannot be null");
            }
            if (NewSubject.ar_name == null)
            {
                return BadRequest("the name cannot be null");
            }
            if (NewSubject.GradeID != 0)
            {
                Grade grade = Unit_Of_Work.grade_Repository.First_Or_Default(g=>g.ID==NewSubject.GradeID&&g.IsDeleted!=true);
                if (grade == null)
                {
                    return BadRequest("No Grade with this ID");
                }
            }

            if (NewSubject.SubjectCategoryID != 0)
            {
                SubjectCategory subjectCategory = Unit_Of_Work.subjectCategory_Repository.First_Or_Default(g => g.ID == NewSubject.SubjectCategoryID && g.IsDeleted != true);
                if (subjectCategory == null)
                {
                    return BadRequest("No Subject Category with this ID");
                }
            }

            if (NewSubject.IconFile != null)
            {
                string returnFileInput = _fileImageValidationService.ValidateImageFile(NewSubject.IconFile);
                if (returnFileInput != null)
                {
                    return BadRequest(returnFileInput);
                }
            }

            var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/SubjectIcon");
            var subjectFolder = Path.Combine(baseFolder, NewSubject.en_name);
            if (!Directory.Exists(subjectFolder))
            {
                Directory.CreateDirectory(subjectFolder);
            }

            if (NewSubject.IconFile != null)
            {
                if (NewSubject.IconFile.Length > 0)
                {
                    var filePath = Path.Combine(subjectFolder, NewSubject.IconFile.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await NewSubject.IconFile.CopyToAsync(stream);
                    }
                }
            }

            Subject subject = mapper.Map<Subject>(NewSubject);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            subject.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                subject.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                subject.InsertedByUserId = userId;
            }

            subject.IconLink = Path.Combine("Uploads", "SubjectIcon", NewSubject.en_name, NewSubject.IconFile.FileName);

            Unit_Of_Work.subject_Repository.Add(subject);
            Unit_Of_Work.SaveChanges();
            return Ok(NewSubject);
        }

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Subject" }
        )]
        public async Task<IActionResult> Edit([FromForm] SubjectPutDTO EditSubject)
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

            if (EditSubject == null)
            {
                return BadRequest("Subject cannot be null");
            }

            if (EditSubject.GradeID != 0)
            {
                Grade grade = Unit_Of_Work.grade_Repository.Select_By_Id(EditSubject.GradeID);
                if (grade == null || grade.IsDeleted==true)
                {
                    return BadRequest("No Grade with this ID");
                }
            }

            if (EditSubject.SubjectCategoryID != 0)
            {
                SubjectCategory subjectCategory = Unit_Of_Work.subjectCategory_Repository.Select_By_Id(EditSubject.SubjectCategoryID);
                if (subjectCategory == null || subjectCategory.IsDeleted == true)
                {
                    return BadRequest("No Subject Category with this ID");
                }
            }

            if (EditSubject.IconFile != null)
            {
                string returnFileInput = _fileImageValidationService.ValidateImageFile(EditSubject.IconFile);
                if (returnFileInput != null)
                {
                    return BadRequest(returnFileInput);
                }
            }
             
            Subject SubjectExists = Unit_Of_Work.subject_Repository.Select_By_Id(EditSubject.ID);

            string iconLinkExists = SubjectExists.IconLink;
            string enNameExists = SubjectExists.en_name;
            if (SubjectExists == null || SubjectExists.IsDeleted == true)
            {
                return NotFound("No Subject with this ID");
            } 

            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Subject", roleId, userId, SubjectExists);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            if (EditSubject.IconFile != null)
            {
                var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/SubjectIcon");
                var oldSubjectFolder = Path.Combine(baseFolder, enNameExists);
                var subjectFolder = Path.Combine(baseFolder, EditSubject.en_name);

                string existingFilePath = Path.Combine(baseFolder, enNameExists);

                if (System.IO.File.Exists(existingFilePath))
                {
                    System.IO.File.Delete(existingFilePath); // Delete the old file
                }
                
                if (Directory.Exists(oldSubjectFolder))
                {
                    Directory.Delete(oldSubjectFolder, true);
                }

                if (!Directory.Exists(subjectFolder))
                {
                    Directory.CreateDirectory(subjectFolder);
                }

                if (EditSubject.IconFile.Length > 0)
                {
                    var filePath = Path.Combine(subjectFolder, EditSubject.IconFile.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await EditSubject.IconFile.CopyToAsync(stream);
                    }
                }

                EditSubject.IconLink = Path.Combine("Uploads", "SubjectIcon", EditSubject.en_name, EditSubject.IconFile.FileName);
            }
            else
            {
                if (EditSubject.en_name != enNameExists)
                {
                    var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/SubjectIcon");
                    var oldSubjectFolder = Path.Combine(baseFolder, enNameExists);
                    var newSubjectFolder = Path.Combine(baseFolder, EditSubject.en_name);

                    // Rename the folder if it exists
                    if (Directory.Exists(oldSubjectFolder))
                    {
                        if (!Directory.Exists(newSubjectFolder))
                        {
                            Directory.CreateDirectory(newSubjectFolder);
                        }
                         
                        var files = Directory.GetFiles(oldSubjectFolder);
                        foreach (var file in files)
                        {
                            var fileName = Path.GetFileName(file);
                            var destFile = Path.Combine(newSubjectFolder, fileName);
                            System.IO.File.Move(file, destFile);
                        }
                         
                        Directory.Delete(oldSubjectFolder);
                    }
                }
                EditSubject.IconLink = Path.Combine("Uploads", "SubjectIcon", EditSubject.en_name, Path.GetFileName(iconLinkExists));
            }

            mapper.Map(EditSubject, SubjectExists);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            SubjectExists.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                SubjectExists.UpdatedByOctaId = userId;
                if (SubjectExists.UpdatedByUserId != null)
                {
                    SubjectExists.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                SubjectExists.UpdatedByUserId = userId;
                if (SubjectExists.UpdatedByOctaId != null)
                {
                    SubjectExists.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.subject_Repository.Update(SubjectExists);
            Unit_Of_Work.SaveChanges();
            return Ok(EditSubject);
        }

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Subject" }
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
                return BadRequest("Enter Subject ID");
            }

            Subject subject = Unit_Of_Work.subject_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);


            if (subject == null)
            {
                return NotFound();
            } 

            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "Subject", roleId, userId, subject);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            subject.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            subject.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                subject.DeletedByOctaId = userId;
                if (subject.DeletedByUserId != null)
                {
                    subject.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                subject.DeletedByUserId = userId;
                if (subject.DeletedByOctaId != null)
                {
                    subject.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.subject_Repository.Update(subject);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
