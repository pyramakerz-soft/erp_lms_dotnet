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

        public AcademicDegreeController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
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

            List<AcademicDegree> AcademicDegrees = Unit_Of_Work.academicDegree_Repository.Select_All();

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

            AcademicDegree academicDegree = Unit_Of_Work.academicDegree_Repository.First_Or_Default(d => d.ID == id);

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
            List<AcademicDegree> AcademicDegrees = Unit_Of_Work.academicDegree_Repository.Select_All();
            long Count = AcademicDegrees.Count();
            academicDegree.ID = Count + 1;
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

            //if (id == 0)
            //{
            //    return BadRequest("Enter Academic Degree ID");
            //}

            AcademicDegree academicDegree = Unit_Of_Work.academicDegree_Repository.First_Or_Default(t => t.ID == id);


            if (academicDegree == null)
            {
                return NotFound();
            }

            Unit_Of_Work.academicDegree_Repository.Delete(id);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
