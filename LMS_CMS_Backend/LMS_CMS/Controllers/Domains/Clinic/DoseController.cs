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
    public class DoseController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        private readonly IMapper _mapper;

        public DoseController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        #region Get
        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Doses" }
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

            List<Dose> dose = Unit_Of_Work.dose_Repository.FindBy(d => d.IsDeleted != true);

            if (dose == null || dose.Count == 0)
            {
                return NotFound();
            }

            List<DoseGetDTO> DoseDto = _mapper.Map<List<DoseGetDTO>>(dose);

            return Ok(DoseDto);
        }
        #endregion

        #region GetByID
        [HttpGet("id")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Doses" }
        )]
        public IActionResult GetByID(long id)
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

            Dose dose = Unit_Of_Work.dose_Repository.First_Or_Default(d => d.Id == id && d.IsDeleted != true);

            if (dose == null)
            {
                return NotFound();
            }

            DoseGetDTO doseDto = _mapper.Map<DoseGetDTO>(dose);

            return Ok(doseDto);
        }
        #endregion

        #region Add Dose
        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Doses" }
        )]
        public IActionResult Add(DoseAddDTO doseDto)
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
                return BadRequest("Please enter the Name field.");
            }

            Dose dose = _mapper.Map<Dose>(doseDto);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            dose.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);

            if (userTypeClaim == "octa")
            {
                dose.InsertedByOctaId = userId;
            }

            else if (userTypeClaim == "employee")
            {
                dose.InsertedByUserId = userId;
            }

            Unit_Of_Work.dose_Repository.Add(dose);
            Unit_Of_Work.SaveChanges();

            return Ok(doseDto);
        }
        #endregion

        #region Update Dose
        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Doses" }
        )]
        public IActionResult Update(DosePutDTO doseDto)
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

            Dose dose = Unit_Of_Work.dose_Repository.First_Or_Default(d => d.Id == doseDto.ID && d.IsDeleted != true);

            if (dose == null || dose.IsDeleted == true)
            {
                return NotFound();
            }

            _mapper.Map(doseDto, dose);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");

            dose.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);

            if (userTypeClaim == "octa")
            {
                dose.UpdatedByOctaId = userId;
                if (dose.UpdatedByUserId != null)
                {
                    dose.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                dose.UpdatedByUserId = userId;
                if (dose.UpdatedByOctaId != null)
                {
                    dose.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.dose_Repository.Update(dose);
            Unit_Of_Work.SaveChanges();

            return Ok(doseDto);
        }
        #endregion

        #region Delete Dose
        [HttpDelete]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Doses" }
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

            if (id == 0)
            {
                return BadRequest("Dose ID cannot be null.");
            }

            Dose dose = Unit_Of_Work.dose_Repository.First_Or_Default(d => d.IsDeleted != true && d.Id == id);

            if (dose == null)
            {
                return NotFound();
            }

            dose.IsDeleted = true;

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            dose.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);

            if (userTypeClaim == "octa")
            {
                dose.DeletedByOctaId = userId;

                if (dose.DeletedByUserId != null)
                {
                    dose.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                dose.DeletedByUserId = userId;
                if (dose.DeletedByOctaId != null)
                {
                    dose.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.dose_Repository.Update(dose);
            Unit_Of_Work.SaveChanges();

            return Ok("Dose deleted successfully");
        }
        #endregion
    }
}
