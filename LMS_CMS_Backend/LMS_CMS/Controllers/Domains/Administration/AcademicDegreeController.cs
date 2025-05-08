using AutoMapper;
using LMS_CMS_BL.DTO.Administration;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.Administration;
using LMS_CMS_DAL.Models.Domains.BusModule;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Controllers.Domains.Administration
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class AcademicDegreeController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;

        public AcademicDegreeController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
        }

        ///////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
       allowedTypes: new[] { "octa", "employee" },
       pages: new[] { "Academic Degree" }
       )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<AcademicDegree> AcademicDegrees = Unit_Of_Work.academicDegree_Repository.FindBy(d => d.IsDeleted != true);

            if (AcademicDegrees == null || AcademicDegrees.Count == 0)
            {
                return NotFound();
            }

            List<AcademicDegreeGetDTO> AcademicDegreesDTO = mapper.Map<List<AcademicDegreeGetDTO>>(AcademicDegrees);

            return Ok(AcademicDegreesDTO);
        }
        //////////////////////////////////////////////////////////////////////////////
        [HttpGet("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Academic Degree" }
        )]
        public async Task<IActionResult> GetbyIdAsync(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            AcademicDegree academicDegree = Unit_Of_Work.academicDegree_Repository.First_Or_Default(d => d.ID == id && d.IsDeleted != true);

            if (academicDegree == null)
            {
                return NotFound();
            }

            AcademicDegreeGetDTO AcademicDegreesDTO = mapper.Map<AcademicDegreeGetDTO>(academicDegree);

            return Ok(AcademicDegreesDTO);
        }
        //////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Academic Degree" }
        )]
        public IActionResult Add(AcademicDegreeAddDTO newAcademicDegree)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (newAcademicDegree == null)
            {
                return BadRequest("Academic Degree cannot be null");
            }
            AcademicDegree academicDegree = mapper.Map<AcademicDegree>(newAcademicDegree);

            long newId = Unit_Of_Work.academicDegree_Repository
               .Select_All()
               .Select(a => (long?)a.ID) // Use nullable to handle empty case
               .Max() ?? 0;

            List<AcademicDegree> AcademicDegrees = Unit_Of_Work.academicDegree_Repository.Select_All();
            academicDegree.ID = newId + 1;

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            academicDegree.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                academicDegree.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                academicDegree.InsertedByUserId = userId;
            }  

            Unit_Of_Work.academicDegree_Repository.Add(academicDegree);
            Unit_Of_Work.SaveChanges();
            return Ok(newAcademicDegree);
        }

        //////////////////////////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Academic Degree" }
        )]
        public IActionResult Edit(AcademicDegreeGetDTO newAcademicDegree)
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

            if (newAcademicDegree == null)
            {
                return BadRequest("Academic Degree cannot be null");
            }

            AcademicDegree academicDegree = Unit_Of_Work.academicDegree_Repository.First_Or_Default(d => d.ID == newAcademicDegree.ID);
            if (academicDegree == null)
            {
                return NotFound("There is no Academic Degree with this id");
            }
              
            mapper.Map(newAcademicDegree, academicDegree);

            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Academic Degree", roleId, userId, academicDegree);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }
             
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            academicDegree.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                academicDegree.UpdatedByOctaId = userId;
                if (academicDegree.UpdatedByUserId != null)
                {
                    academicDegree.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                academicDegree.UpdatedByUserId = userId;
                if (academicDegree.UpdatedByOctaId != null)
                {
                    academicDegree.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.academicDegree_Repository.Update(academicDegree);
            Unit_Of_Work.SaveChanges();
            return Ok(newAcademicDegree);
        }

        //////////////////////////////////////////////////////////////////////////////////

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
              allowedTypes: new[] { "octa", "employee" },
              allowDelete: 1,
              pages: new[] { "Academic Degree" }
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
             
            AcademicDegree academicDegree = Unit_Of_Work.academicDegree_Repository.First_Or_Default(t => t.ID == id);
             
            if (academicDegree == null)
            {
                return NotFound();
            }

            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "Academic Degree", roleId, userId, academicDegree);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            academicDegree.IsDeleted = true;

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            academicDegree.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                academicDegree.DeletedByOctaId = userId;
                if (academicDegree.DeletedByUserId != null)
                {
                    academicDegree.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                academicDegree.DeletedByUserId = userId;
                if (academicDegree.DeletedByOctaId != null)
                {
                    academicDegree.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.academicDegree_Repository.Update(academicDegree);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
