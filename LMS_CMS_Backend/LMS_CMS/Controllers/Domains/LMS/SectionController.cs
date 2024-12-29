using AutoMapper;
using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.LMS;
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
    public class SectionController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public SectionController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        ///////////////////////////////////////////////////////////////////////////////////

        [HttpGet]

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
            List<Section> Sections = await Unit_Of_Work.section_Repository.Select_All_With_IncludesById<Section>(
                    sem => sem.IsDeleted != true,
                    query => query.Include(emp => emp.school));

            if (Sections == null || Sections.Count == 0)
            {
                return NotFound();
            }

            List<SectionGetDTO> SectionDTO = mapper.Map<List<SectionGetDTO>>(Sections);

            return Ok(SectionDTO);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("id")]

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
            Section Sections = await Unit_Of_Work.section_Repository.FindByIncludesAsync(
                    sem => sem.IsDeleted != true && sem.ID == id,
                    query => query.Include(emp => emp.school));

            if (Sections == null)
            {
                return NotFound();
            }

            SectionGetDTO SectionDTO = mapper.Map<SectionGetDTO>(Sections);

            return Ok(SectionDTO);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        public async Task<IActionResult> Add(SectionAddDTO NewSection)
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
            if (NewSection == null)
            {
                return NotFound();
            }
            School school = Unit_Of_Work.school_Repository.First_Or_Default(s=>s.ID==NewSection.SchoolID&&s.IsDeleted!=true);
            if (school == null)
            {
              return NotFound("there is no school with this id");
            }

            Section section = mapper.Map<Section>(NewSection);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            section.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                section.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                section.InsertedByUserId = userId;
            }

            Unit_Of_Work.section_Repository.Add(section);
            Unit_Of_Work.SaveChanges();
            return Ok(NewSection);
        }
        ////////////////////////////////////////////////////

        [HttpPut]
        public IActionResult Edit(SectionGetDTO newSection)
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

            if (newSection == null)
            {
                return BadRequest("Section cannot be null");
            }
            School school = Unit_Of_Work.school_Repository.First_Or_Default(s => s.ID == newSection.SchoolID && s.IsDeleted != true);
            if (school == null)
            {
                return NotFound("there is no school with this id");
            }
            Section section = Unit_Of_Work.section_Repository.First_Or_Default(s => s.ID == newSection.ID && s.IsDeleted != true);
            if (section == null)
            {
                return NotFound("there is no section with this id");
            }
            mapper.Map(newSection, section);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            section.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                section.UpdatedByOctaId = userId;
                if (section.UpdatedByUserId != null)
                {
                    section.UpdatedByUserId = null;
                }

            }
            else if (userTypeClaim == "employee")
            {
                section.UpdatedByUserId = userId;
                if (section.UpdatedByOctaId != null)
                {
                    section.UpdatedByOctaId = null;
                }
            }
            Unit_Of_Work.section_Repository.Update(section);
            Unit_Of_Work.SaveChanges();
            return Ok(newSection);

        }

        //////////////////////////////////////////////////////

        [HttpDelete]
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
            Section section = Unit_Of_Work.section_Repository.Select_By_Id(id);

            if (section == null || section.IsDeleted == true)
            {
                return NotFound("No Section with this ID");
            }
            section.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            section.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                section.DeletedByOctaId = userId;
                if (section.DeletedByUserId != null)
                {
                    section.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                section.DeletedByUserId = userId;
                if (section.DeletedByOctaId != null)
                {
                    section.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.section_Repository.Update(section);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
