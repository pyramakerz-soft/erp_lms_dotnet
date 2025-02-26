using AutoMapper;
using LMS_CMS_BL.DTO.Clinic;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.ClinicModule;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Controllers.Domains.Clinic
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    public class HygieneTypeController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        private readonly IMapper _mapper;

        public HygieneTypeController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        #region Get
        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Hygiene Types" }
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

            List<HygieneType> HygieneTypes = Unit_Of_Work.hygieneType_Repository.FindBy(t => t.IsDeleted != true);

            if (HygieneTypes == null || HygieneTypes.Count == 0)
            {
                return NotFound();
            }

            List<HygieneTypeDto> HygieneTypesDto = _mapper.Map<List<HygieneTypeDto>>(HygieneTypes);

            return Ok(HygieneTypesDto);
        }
        #endregion

        #region GetByID
        [HttpGet("id")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Hygiene Types" }
        )]
        public IActionResult GetById(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (id == 0)
            {
                return BadRequest("Enter Bus Company ID");
            }

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            HygieneType hygieneType = Unit_Of_Work.hygieneType_Repository.Select_By_Id(id);

            if (hygieneType == null || hygieneType.IsDeleted == true)
            {
                return NotFound("No Hygiene Type with this ID");
            }

            HygieneTypeDto hygieneTypeDto = _mapper.Map<HygieneTypeDto>(hygieneType);

            return Ok(hygieneTypeDto);
        }
        #endregion

        #region Add Hygiene Type
        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Hygiene Types" }
        )]
        public IActionResult Add(HygieneTypeDto hygieneTypeDto)
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

            HygieneType hygieneType = _mapper.Map<HygieneType>(hygieneTypeDto);

            hygieneType.InsertedByUserId = userId;
            hygieneType.InsertedAt = DateTime.Now;

            Unit_Of_Work.hygieneType_Repository.Add(hygieneType);
            Unit_Of_Work.SaveChanges();

            return Ok("Hygiene Type added successfully");
        }
        #endregion

        #region Update Hygiene Type
        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Hygiene Types" }
        )]
        public IActionResult Update(HygieneTypeDto hygieneTypeDto)
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

            HygieneType hygieneType = Unit_Of_Work.hygieneType_Repository.Select_By_Id(userIdClaim);

            if (hygieneType == null || hygieneType.IsDeleted == true)
            {
                return NotFound("No Hygiene Type with this ID");
            }

            hygieneType.Type = hygieneTypeDto.Type;
            hygieneType.UpdatedByUserId = userId;
            hygieneType.UpdatedAt = DateTime.Now;

            Unit_Of_Work.hygieneType_Repository.Update(hygieneType);
            Unit_Of_Work.SaveChanges();

            return Ok("Hygiene Type Updated Successfully");
        }
        #endregion

        #region Delete Hygiene Type
        [HttpDelete]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Hygiene Types" }
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
                return BadRequest("Hygiene ID cannot be null.");
            }

            HygieneType hygieneType = Unit_Of_Work.hygieneType_Repository.Select_By_Id(id);

            if (hygieneType == null || hygieneType.IsDeleted == true)
            {
                return NotFound("No Hygiene Type with this ID");
            }

            hygieneType.IsDeleted = true;

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            hygieneType.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);

            if (userTypeClaim == "octa")
            {
                hygieneType.DeletedByOctaId = userId;
                if (hygieneType.DeletedByUserId != null)
                {
                    hygieneType.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                hygieneType.DeletedByUserId = userId;
                if (hygieneType.DeletedByOctaId != null)
                {
                    hygieneType.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.hygieneType_Repository.Update(hygieneType);
            Unit_Of_Work.SaveChanges();

            return Ok();
        }
        #endregion
    }
}
