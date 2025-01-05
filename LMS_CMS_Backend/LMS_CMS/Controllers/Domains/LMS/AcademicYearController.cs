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

namespace LMS_CMS_PL.Controllers.Domains.LMS
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]

    public class AcademicYearController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public AcademicYearController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }
        //////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Academic Year", "Administrator" }
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

            List<AcademicYear> academicYear= await Unit_Of_Work.academicYear_Repository.Select_All_With_IncludesById<AcademicYear>(
                    sem => sem.IsDeleted != true,
                    query => query.Include(emp => emp.School));

            if (academicYear == null || academicYear.Count == 0)
            {
                return NotFound();
            }

            List<AcademicYearGet> AcademicYearDTOs = mapper.Map<List<AcademicYearGet>>(academicYear);

            return Ok(AcademicYearDTOs);
        }
        //////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Academic Year", "Administrator" }
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
            AcademicYear academicYear = await Unit_Of_Work.academicYear_Repository.FindByIncludesAsync(
                    sem => sem.IsDeleted != true && sem.ID == id,
                    query => query.Include(emp => emp.School));

            if (academicYear == null)
            {
                return NotFound();
            }

            AcademicYearGet academicYearDTO = mapper.Map<AcademicYearGet>(academicYear);

            return Ok(academicYearDTO);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Academic Year", "Administrator" }
        )]
        public async Task<IActionResult> Add(AcademicYearAddDTO NewAcademicYear)
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
            if (NewAcademicYear == null)
            {
                return BadRequest("AcademicYear can not be null");
            }
            if (NewAcademicYear.Name == null)
            {
                return BadRequest("the name cannot be null");
            }
            School school = Unit_Of_Work.school_Repository.First_Or_Default(s=>s.ID==NewAcademicYear.SchoolID&&s.IsDeleted!=true);
            if (school==null)
            {
                return NotFound("No School with this ID");
            }
            AcademicYear academicYear = mapper.Map<AcademicYear>(NewAcademicYear);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            academicYear.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                academicYear.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                academicYear.InsertedByUserId = userId;
            }

            Unit_Of_Work.academicYear_Repository.Add(academicYear);
            Unit_Of_Work.SaveChanges();
            return Ok(NewAcademicYear);
        }

        ////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
             allowedTypes: new[] { "octa", "employee" },
             allowEdit: 1,
             pages: new[] { "Academic Year", "Administrator" }
         )]
        public IActionResult Edit(AcademicYearGet newAcademicYear)
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
            if (newAcademicYear == null)
            {
                return BadRequest("AcademicYear cannot be null");
            }
            if (newAcademicYear.Name == null)
            {
                return BadRequest("the name cannot be null");
            }
            School school = Unit_Of_Work.school_Repository.First_Or_Default(s => s.ID == newAcademicYear.SchoolID && s.IsDeleted != true);
            if (school == null)
            {
                return NotFound("No School with this ID");
            }
            AcademicYear AcademicYear =Unit_Of_Work.academicYear_Repository.First_Or_Default(a=>a.ID== newAcademicYear.ID);
            if (AcademicYear == null)
            {
                return BadRequest("this academic year doesn't exist");
            }
            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Academic Years");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (AcademicYear.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("AcademicYear page doesn't exist");
                }
            }

            mapper.Map(newAcademicYear, AcademicYear);


            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            AcademicYear.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                AcademicYear.UpdatedByOctaId = userId;
                if (AcademicYear.UpdatedByUserId != null)
                {
                    AcademicYear.UpdatedByUserId = null;
                }

            }
            else if (userTypeClaim == "employee")
            {
                AcademicYear.UpdatedByUserId = userId;
                if (AcademicYear.UpdatedByOctaId != null)
                {
                    AcademicYear.UpdatedByOctaId = null;
                }
            }
            Unit_Of_Work.academicYear_Repository.Update(AcademicYear);
            Unit_Of_Work.SaveChanges();
            return Ok(newAcademicYear);

        }

        //////////////////////////////////////////////////////

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Academic Year", "Administrator" }
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
            AcademicYear academicYear = Unit_Of_Work.academicYear_Repository.Select_By_Id(id);

            if (academicYear == null || academicYear.IsDeleted == true)
            {
                return NotFound("No AcademicYear with this ID");
            }
            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Academic Years");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Delete_For_Others == false)
                    {
                        if (academicYear.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("AcademicYear page doesn't exist");
                }
            }
            academicYear.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            academicYear.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                academicYear.DeletedByOctaId = userId;
                if (academicYear.DeletedByUserId != null)
                {
                    academicYear.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                academicYear.DeletedByUserId = userId;
                if (academicYear.DeletedByOctaId != null)
                {
                    academicYear.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.academicYear_Repository.Update(academicYear);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }


}

