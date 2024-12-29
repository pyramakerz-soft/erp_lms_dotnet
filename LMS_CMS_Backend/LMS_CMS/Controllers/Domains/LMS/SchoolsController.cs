using AutoMapper;
using LMS_CMS_BL.DTO.Bus;
using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.UOW;
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

        public SchoolsController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }


        ///////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa"}
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

            return Ok(schoolDTO);
        }
        ///////////////////////////////////////////

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa" }
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
            allowedTypes: new[] { "octa" }
        )]
        public IActionResult Edit(SchoolEditDTO newSchool)
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
            School school = Unit_Of_Work.school_Repository.First_Or_Default(s => s.ID == newSchool.ID);
            if (school == null)
            {
                return BadRequest("there is no School with this id");
            }
            mapper.Map(newSchool, school);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
           school.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
           if (userTypeClaim == "octa")
           {
               school.InsertedByOctaId = userId;
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
        public IActionResult delete(long id)
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
