using AutoMapper;
using LMS_CMS_BL.DTO.Clinic;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.ClinicModule;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers.Domains.Clinic
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    public class FollowUpController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        private readonly IMapper _mapper;

        public FollowUpController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        #region Get
        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Follow Up" }
        )]
        public IActionResult Get()
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

            List<FollowUp> followUps = Unit_Of_Work.followUp_Repository.FindBy(d => d.IsDeleted != true);

            if (followUps == null || followUps.Count == 0)
            {
                return NotFound();
            }

            List<FollowUpGetDTO> followUpDto = _mapper.Map<List<FollowUpGetDTO>>(followUps);

            return Ok(followUpDto);
        }
        #endregion

        #region GetByID
        [HttpGet("id")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Follow Up" }
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

            FollowUp followUp = await Unit_Of_Work.followUp_Repository
                .FindByIncludesAsync(d => d.Id == id && d.IsDeleted != true, query => query.Include(f => f.FollowUpDrugs));

            if (followUp == null)
            {
                return NotFound();
            }

            FollowUpGetDTO followUpDto = _mapper.Map<FollowUpGetDTO>(followUp);

            return Ok(followUpDto);
        }
        #endregion

        #region Add Follow Up
        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Follow Up" }
        )]
        public IActionResult Add(FollowUpAddDTO followUpDto)
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

            FollowUp followUp = _mapper.Map<FollowUp>(followUpDto);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            followUp.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);

            if (userTypeClaim == "octa")
            {
                followUp.InsertedByOctaId = userId;
            }

            else if (userTypeClaim == "employee")
            {
                followUp.InsertedByUserId = userId;
            }

            Unit_Of_Work.followUp_Repository.Add(followUp);
            Unit_Of_Work.SaveChanges();

            foreach (var followUpDrug in followUpDto.FollowUpDrugs)
            {
                FollowUpDrug fud = _mapper.Map<FollowUpDrug>(followUpDrug);
                
                fud.FollowUpId = followUp.Id;

                Unit_Of_Work.followUpDrug_Repository.Add(fud);
            }
            
            Unit_Of_Work.SaveChanges();

            return Ok(followUpDto);
        }
        #endregion

        #region Update Follow Up
        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Follow Up" }
        )]
        public IActionResult Update(FollowUpPutDTO followUpDto)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            FollowUp followUp = Unit_Of_Work.followUp_Repository.First_Or_Default(d => d.Id == followUpDto.ID && d.IsDeleted != true);

            if (followUp == null)
            {
                return NotFound();
            }

            _mapper.Map(followUpDto, followUp);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");

            followUp.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);

            if (userTypeClaim == "octa")
            {
                followUp.UpdatedByOctaId = userId;
                if (followUp.UpdatedByUserId != null)
                {
                    followUp.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                followUp.UpdatedByUserId = userId;
                if (followUp.UpdatedByOctaId != null)
                {
                    followUp.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.followUp_Repository.Update(followUp);
            Unit_Of_Work.SaveChanges();

            foreach (var followUpDrug in followUpDto.FollowUpDrugs)
            {
                FollowUpDrug fud = _mapper.Map<FollowUpDrug>(followUpDrug);

                Unit_Of_Work.followUpDrug_Repository.Update(fud);
            }

            Unit_Of_Work.SaveChanges();

            return Ok(followUpDto);
        }
        #endregion

        #region Delete Follow Up
        [HttpDelete]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Follow Up" }
        )]
        public IActionResult Delete(long id)
        {
            if (id == 0)
            {
                return BadRequest("Follow Up ID cannot be null.");
            }

            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            long.TryParse(userIdClaim, out long userId);

            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            FollowUp followUp = Unit_Of_Work.followUp_Repository.First_Or_Default(d => d.IsDeleted != true && d.Id == id);

            if (followUp == null)
            {
                return NotFound();
            }

            followUp.IsDeleted = true;

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            followUp.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);

            if (userTypeClaim == "octa")
            {
                followUp.DeletedByOctaId = userId;

                if (followUp.DeletedByUserId != null)
                {
                    followUp.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                followUp.DeletedByUserId = userId;
                if (followUp.DeletedByOctaId != null)
                {
                    followUp.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.followUp_Repository.Update(followUp);
            Unit_Of_Work.SaveChanges();

            return Ok("Follow Up deleted successfully");
        }
        #endregion
    }
}
