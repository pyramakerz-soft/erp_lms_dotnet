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
    public class DiagnosisController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        private readonly IMapper _mapper;

        public DiagnosisController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        #region Get
        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Diagnosis" }
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
            
            List<Diagnosis> diagnosis = Unit_Of_Work.diagnosis_Repository.FindBy(t => t.IsDeleted != true);

            if (diagnosis == null || diagnosis.Count == 0)
            {
                return NotFound();
            }
            
            List<DiagnosisGetDTO> DiagnosisDto = _mapper.Map<List<DiagnosisGetDTO>>(diagnosis);
            
            return Ok(DiagnosisDto);
        }
        #endregion

        #region GetByID
        [HttpGet("id")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Diagnosis" }
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
            
            Diagnosis diagnosis = Unit_Of_Work.diagnosis_Repository.First_Or_Default(d => d.Id == id && d.IsDeleted != true);

            if (diagnosis == null)
            {
                return NotFound();
            }
            
            DiagnosisGetDTO diagnosisDto = _mapper.Map<DiagnosisGetDTO>(diagnosis);

            return Ok(diagnosisDto);
        }
        #endregion

        #region Add Diagnosis
        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Diagnosis" }
        )]
        public IActionResult Add(DiagnosisAddDTO diagnosisDto)
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
            
            Diagnosis diagnosis = _mapper.Map<Diagnosis>(diagnosisDto);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            diagnosis.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);

            if (userTypeClaim == "octa")
            {
                diagnosis.InsertedByOctaId = userId;
            }

            else if (userTypeClaim == "employee")
            {
                diagnosis.InsertedByUserId = userId;
            }

            Unit_Of_Work.diagnosis_Repository.Add(diagnosis);
            Unit_Of_Work.SaveChanges();

            return Ok(diagnosisDto);
        }
        #endregion

        #region Update Diagnosis
        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Diagnosis" }
        )]
        public IActionResult Update(DiagnosisPutDTO diagnosisDto)
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

            Diagnosis diagnosis = Unit_Of_Work.diagnosis_Repository.First_Or_Default(d => d.Id == diagnosisDto.ID && d.IsDeleted != true);

            if (diagnosis == null || diagnosis.IsDeleted == true)
            {
                return NotFound();
            }

            _mapper.Map(diagnosisDto, diagnosis);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");

            diagnosis.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);

            if (userTypeClaim == "octa")
            {
                diagnosis.UpdatedByOctaId = userId;
                if (diagnosis.UpdatedByUserId != null)
                {
                    diagnosis.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                diagnosis.UpdatedByUserId = userId;
                if (diagnosis.UpdatedByOctaId != null)
                {
                    diagnosis.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.diagnosis_Repository.Update(diagnosis);
            Unit_Of_Work.SaveChanges();

            return Ok(diagnosisDto);
        }
        #endregion

        #region Delete Diagnosis
        [HttpDelete]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Diagnosis" }
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
                return BadRequest("Diagnosis ID cannot be null.");
            }

            Diagnosis diagnosis = Unit_Of_Work.diagnosis_Repository.First_Or_Default(d => d.IsDeleted != true && d.Id == id);

            if (diagnosis == null)
            {
                return NotFound();
            }

            diagnosis.IsDeleted = true;

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            diagnosis.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);

            if (userTypeClaim == "octa")
            {
                diagnosis.DeletedByOctaId = userId;

                if (diagnosis.DeletedByUserId != null)
                {
                    diagnosis.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                diagnosis.DeletedByUserId = userId;
                if (diagnosis.DeletedByOctaId != null)
                {
                    diagnosis.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.diagnosis_Repository.Update(diagnosis);
            Unit_Of_Work.SaveChanges();

            return Ok("Diagnosis deleted successfully");
        }
        #endregion
    }
}
