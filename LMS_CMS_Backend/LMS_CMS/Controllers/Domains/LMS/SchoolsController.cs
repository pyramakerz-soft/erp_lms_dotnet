using AutoMapper;
using LMS_CMS_BL.DTO.Bus;
using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.BusModule;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices.JavaScript;

namespace LMS_CMS_PL.Controllers.Domains.LMS
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class SchoolsController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly FileImageValidationService _fileImageValidationService;

        public SchoolsController(DbContextFactoryService dbContextFactory, IMapper mapper, FileImageValidationService fileImageValidationService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _fileImageValidationService = fileImageValidationService;
        }


        ///////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee", "parent"}
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

            List<School> Schools = await Unit_Of_Work.school_Repository.Select_All_With_IncludesById<School>(
                    bus => bus.IsDeleted != true,
                    query => query.Include(emp => emp.SchoolType));

            if (Schools == null || Schools.Count == 0)
            {
                return NotFound();
            }

            List<School_GetDTO>schoolDTO =mapper.Map<List<School_GetDTO>>(Schools);

            string serverUrl = $"{Request.Scheme}://{Request.Host}/";
            foreach (var school in schoolDTO)
            {
                if (!string.IsNullOrEmpty(school.ReportImage))
                {
                    school.ReportImage = $"{serverUrl}{school.ReportImage.Replace("\\", "/")}";
                }
            }


            return Ok(schoolDTO);
        }
        ///////////////////////////////////////////

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
             pages: new[] { "School", "Administrator" }
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

            School School = await Unit_Of_Work.school_Repository.FindByIncludesAsync(
                   bus => bus.ID == id && bus.IsDeleted != true,
                   query => query.Include(e => e.SchoolType));
            if (School == null || School.IsDeleted == true)
            {
                return NotFound("No School with this ID");
            }

            School_GetDTO schoolDTO = mapper.Map<School_GetDTO>(School);

            string serverUrl = $"{Request.Scheme}://{Request.Host}/";
            if (!string.IsNullOrEmpty(schoolDTO.ReportImage))
            {
                schoolDTO.ReportImage = $"{serverUrl}{schoolDTO.ReportImage.Replace("\\", "/")}";
            }
             
            return Ok(schoolDTO);
        }

        ///////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa" }
        )]
        public IActionResult Add(SchoolAddDTO newSchool)
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

            if (newSchool == null)
            {
                return BadRequest("School cannot be null");
            }
            if (newSchool.Name == null)
            {
                return BadRequest("the name cannot be null");
            }
            SchoolType schoolType = Unit_Of_Work.schoolType_Repository.First_Or_Default(s => s.ID == newSchool.SchoolTypeID);
            if (schoolType == null) 
            { 
                return BadRequest("there is no School Type with this id");
            }
            School school = mapper.Map<School>(newSchool);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            school.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                school.InsertedByOctaId = userId;
            }
            Unit_Of_Work.school_Repository.Add(school);
            Unit_Of_Work.SaveChanges();
            return Ok(newSchool);

        }
        ////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "School", "Administrator" }
        )]
        public async Task<IActionResult> Edit([FromForm] SchoolEditDTO newSchool)
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

            if (newSchool == null)
            {
                return BadRequest("School cannot be null");
            }
            if (newSchool.Name == null)
            {
                return BadRequest("the name cannot be null");
            }

            SchoolType schoolType = Unit_Of_Work.schoolType_Repository.First_Or_Default(s => s.ID == newSchool.SchoolTypeID);
            if (schoolType == null)
            {
                return BadRequest("there is no School Type with this id");
            }

            if (newSchool.ReportImageFile != null)
            {
                string returnFileInput = _fileImageValidationService.ValidateImageFile(newSchool.ReportImageFile);
                if (returnFileInput != null)
                {
                    return BadRequest(returnFileInput);
                }
            }

            School school = Unit_Of_Work.school_Repository.First_Or_Default(s => s.ID == newSchool.ID);

            string iconLinkExists = school.ReportImage;
            string nameExists = school.Name;
            if (school == null || school.IsDeleted == true)
            {
                return NotFound("No School with this ID");
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "School");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (school.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("School page doesn't exist");
                }
            }

            if (newSchool.ReportImageFile != null)
            {
                var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/SchoolHeaderImages");
                var oldSchoolFolder = Path.Combine(baseFolder, nameExists);
                var SchoolFolder = Path.Combine(baseFolder, newSchool.Name);

                string existingFilePath = Path.Combine(baseFolder, nameExists);

                if (System.IO.File.Exists(existingFilePath))
                {
                    System.IO.File.Delete(existingFilePath); // Delete the old file
                }

                if (Directory.Exists(oldSchoolFolder))
                {
                    Directory.Delete(oldSchoolFolder, true);
                }

                if (!Directory.Exists(SchoolFolder))
                {
                    Directory.CreateDirectory(SchoolFolder);
                }

                if (newSchool.ReportImageFile.Length > 0)
                {
                    var filePath = Path.Combine(SchoolFolder, newSchool.ReportImageFile.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await newSchool.ReportImageFile.CopyToAsync(stream);
                    }
                }

                newSchool.ReportImage = Path.Combine("Uploads", "SchoolHeaderImages", newSchool.Name, newSchool.ReportImageFile.FileName);
            }
            else
            {
                if(iconLinkExists != null)
                {
                    if (newSchool.Name != nameExists)
                    {
                        var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/SchoolHeaderImages");
                        var oldSubjectFolder = Path.Combine(baseFolder, nameExists);
                        var newSubjectFolder = Path.Combine(baseFolder, newSchool.Name);

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
                    newSchool.ReportImage = Path.Combine("Uploads", "SchoolHeaderImages", newSchool.Name, Path.GetFileName(iconLinkExists));
                }
            }

            mapper.Map(newSchool, school);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            school.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                school.UpdatedByOctaId = userId;
                if (school.UpdatedByUserId != null)
                {
                    school.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                school.UpdatedByUserId = userId;
                if (school.UpdatedByOctaId != null)
                {
                    school.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.school_Repository.Update(school);
            Unit_Of_Work.SaveChanges();
            return Ok(newSchool);
        }

        ////////////////////////////////////////////////////

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa" }
        )]
        public IActionResult Delete(long id)
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
           School school = Unit_Of_Work.school_Repository.Select_By_Id(id);
            school.DeletedByOctaId = userId;
            school.IsDeleted = true;
            Unit_Of_Work.school_Repository.Update(school);
            Unit_Of_Work.SaveChanges();
            return Ok();

        }
    }
}
