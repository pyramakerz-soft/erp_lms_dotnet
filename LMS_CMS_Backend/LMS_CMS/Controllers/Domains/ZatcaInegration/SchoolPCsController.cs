using AutoMapper;
using LMS_CMS_BL.DTO.Zatca;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.Zatca;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers.Domains.ZatcaInegration
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class SchoolPCsController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        private readonly IMapper _mapper;

        public SchoolPCsController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        #region Get
        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "SchoolPCs" }
        )]
        public async Task<IActionResult> Get()
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

            List<SchoolPCs> pcs = await Unit_Of_Work.schoolPCs_Repository.Select_All_With_IncludesById<SchoolPCs>(
                d => d.IsDeleted != true,
                query => query.Include(s => s.School)
            );

            if (pcs == null || pcs.Count == 0)
            {
                return NotFound();
            }

            if (pcs == null || pcs.Count == 0)
            {
                return NotFound();
            }

            List<SchoolPCsGetDTO> schoolPCsDto = _mapper.Map<List<SchoolPCsGetDTO>>(pcs);

            return Ok(schoolPCsDto);
        }
        #endregion

        #region GetByID
        [HttpGet("id")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "SchoolPCs" }
        )]
        public async Task<IActionResult> GetByID(long id)
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
            SchoolPCs pc = await Unit_Of_Work.schoolPCs_Repository.FindByIncludesAsync(
                d => d.ID == id && d.IsDeleted != true,
                query => query.Include(s => s.School)
                );

            if (pc == null)
            {
                return NotFound();
            }

            SchoolPCsGetDTO drugDto = _mapper.Map<SchoolPCsGetDTO>(pc);

            return Ok(drugDto);
        }
        #endregion

        #region Add PC
        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "SchoolPCs" }
        )]
        public IActionResult Add(SchoolPCsAddDTO schoolPCsDto)
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

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            SchoolPCs pc = _mapper.Map<SchoolPCs>(schoolPCsDto);
            pc.SerialNumber = Guid.NewGuid().ToString();

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            pc.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);

            if (userTypeClaim == "octa")
            {
                pc.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                pc.InsertedByUserId = userId;
            }

            Unit_Of_Work.schoolPCs_Repository.Add(pc);
            Unit_Of_Work.SaveChanges();

            return Ok(schoolPCsDto);
        }
        #endregion

        #region Update PC
        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "SchoolPCs" }
        )]
        public IActionResult Update(SchoolPCsPutDTO SchoolPCsDto)
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

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            SchoolPCs pc = Unit_Of_Work.schoolPCs_Repository.First_Or_Default(d => d.ID == SchoolPCsDto.ID && d.IsDeleted != true);

            if (pc == null)
            {
                return NotFound();
            }

            _mapper.Map(SchoolPCsDto, pc);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");

            pc.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);

            if (userTypeClaim == "octa")
            {
                pc.UpdatedByOctaId = userId;
                if (pc.UpdatedByUserId != null)
                {
                    pc.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                pc.UpdatedByUserId = userId;
                if (pc.UpdatedByOctaId != null)
                {
                    pc.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.schoolPCs_Repository.Update(pc);
            Unit_Of_Work.SaveChanges();

            return Ok(SchoolPCsDto);
        }
        #endregion

        #region Delete PC
        [HttpDelete]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "SchoolPCs" }
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

            SchoolPCs pc = Unit_Of_Work.schoolPCs_Repository.First_Or_Default(d => d.IsDeleted != true && d.ID == id);

            if (pc == null)
            {
                return NotFound();
            }

            pc.IsDeleted = true;

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            pc.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);

            if (userTypeClaim == "octa")
            {
                pc.DeletedByOctaId = userId;

                if (pc.DeletedByUserId != null)
                {
                    pc.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                pc.DeletedByUserId = userId;
                if (pc.DeletedByOctaId != null)
                {
                    pc.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.schoolPCs_Repository.Update(pc);
            Unit_Of_Work.SaveChanges();

            return Ok("PC deleted successfully");
        }
        #endregion
    }
}
