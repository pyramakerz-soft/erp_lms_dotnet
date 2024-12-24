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

namespace LMS_CMS_PL.Controllers.Domains.LMS
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class SubjectController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public SubjectController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Subjects", "Administrator" }
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

            if (NewSubject.GradeID != 0)
            {
                Grade grade = Unit_Of_Work.grade_Repository.Select_By_Id(NewSubject.GradeID);
                if (grade == null)
                {
                    return BadRequest("No Grade with this ID");
                }
            }

            if (NewSubject.SubjectCategoryID != 0)
            {
                SubjectCategory subjectCategory = Unit_Of_Work.subjectCategory_Repository.Select_By_Id(NewSubject.SubjectCategoryID);
                if (subjectCategory == null)
                {
                    return BadRequest("No Subject Category with this ID");
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
            pages: new[] { "Subjects", "Administrator" }
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
                if (grade == null)
                {
                    return BadRequest("No Grade with this ID");
                }
            }

            if (EditSubject.SubjectCategoryID != 0)
            {
                SubjectCategory subjectCategory = Unit_Of_Work.subjectCategory_Repository.Select_By_Id(EditSubject.SubjectCategoryID);
                if (subjectCategory == null)
                {
                    return BadRequest("No Subject Category with this ID");
                }
            }

            Subject SubjectExists = Unit_Of_Work.subject_Repository.Select_By_Id(EditSubject.ID);
            string iconLinkExists = SubjectExists.IconLink;
            if (SubjectExists == null || SubjectExists.IsDeleted == true)
            {
                return NotFound("No Subject with this ID");
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Subjects");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (SubjectExists.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Subjects page doesn't exist");
                }
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

            if (EditSubject.IconFile != null)
            {
                var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/SubjectIcon");
                var subjectFolder = Path.Combine(baseFolder, EditSubject.en_name);
                if (!Directory.Exists(subjectFolder))
                {
                    Directory.CreateDirectory(subjectFolder);
                }

                if (EditSubject.IconFile != null)
                {
                    string existingFilePath = Path.Combine(Directory.GetCurrentDirectory(), iconLinkExists);

                    if (System.IO.File.Exists(existingFilePath))
                    {
                        System.IO.File.Delete(existingFilePath); // Delete the old file
                    }

                    if (EditSubject.IconFile.Length > 0)
                    {
                        var filePath = Path.Combine(subjectFolder, EditSubject.IconFile.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await EditSubject.IconFile.CopyToAsync(stream);
                        }
                    }
                }

                SubjectExists.IconLink = Path.Combine("Uploads", "SubjectIcon", EditSubject.en_name, EditSubject.IconFile.FileName);
            }
            else
            {
                SubjectExists.IconLink = EditSubject.IconLink;
            }

            Unit_Of_Work.subject_Repository.Update(SubjectExists);
            Unit_Of_Work.SaveChanges();
            return Ok(EditSubject);
        }
    }
}
