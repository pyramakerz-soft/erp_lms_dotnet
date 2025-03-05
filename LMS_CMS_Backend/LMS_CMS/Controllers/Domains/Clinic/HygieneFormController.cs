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
    public class HygieneFormController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly DbContextFactoryService _dbContextFactory;

        public HygieneFormController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        #region Get
        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Hygiene Form Medical Report" }
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
            
            List<HygieneForm> hygieneForms = await Unit_Of_Work.hygieneForm_Repository.Select_All_With_IncludesById<HygieneForm>(
                    d => d.IsDeleted != true, 
                    query => query.Include(h => h.Classroom),
                    query => query.Include(h => h.School),
                    query => query.Include(h => h.Grade)
                );

            if (hygieneForms == null || hygieneForms.Count == 0)
            {
                return NotFound();
            }


            List<HygieneFormGetDTO> hygieneFormsDto = _mapper.Map<List<HygieneFormGetDTO>>(hygieneForms);

            foreach (var item in hygieneFormsDto)
            {
                List<StudentHygieneTypes> stuHyTy = await Unit_Of_Work.studentHygieneTypes_Repository.Select_All_With_IncludesById<StudentHygieneTypes>(
                    d => d.IsDeleted != true,
                    query => query.Include(h => h.HygieneForm),
                    query => query.Include(h => h.Student),
                    query => query.Include(h => h.HygieneType)
                );
                if (stuHyTy != null)
                {
                    List<StudentHygieneTypesGetDTO> stuHyTyDTO = _mapper.Map<List<StudentHygieneTypesGetDTO>>(stuHyTy);
                    item.StudentHygieneTypes = stuHyTyDTO;
                }
            } 

            return Ok(hygieneFormsDto);
        }
        #endregion

        #region GetByID
        [HttpGet("id")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Hygiene Form Medical Report" }
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

            HygieneForm hygieneForm = await Unit_Of_Work.hygieneForm_Repository.FindByIncludesAsync(
                    h => h.Id == id && h.IsDeleted != true,
                    query => query.Include(x => x.Classroom),
                    query => query.Include(x => x.School),
                    query => query.Include(x => x.Grade)
                );

            if (hygieneForm == null)
            {
                return NotFound();
            }

            HygieneFormGetDTO hygieneFormDto = _mapper.Map<HygieneFormGetDTO>(hygieneForm);

            List<StudentHygieneTypes> stuHyTy = await Unit_Of_Work.studentHygieneTypes_Repository.Select_All_With_IncludesById<StudentHygieneTypes>(
                    d => d.IsDeleted != true,
                    query => query.Include(h => h.HygieneForm),
                    query => query.Include(h => h.Student),
                    query => query.Include(h => h.HygieneType)
                );

            if (stuHyTy != null)
            {
                List<StudentHygieneTypesGetDTO> stuHyTyDTO = _mapper.Map<List<StudentHygieneTypesGetDTO>>(stuHyTy);
                hygieneFormDto.StudentHygieneTypes = stuHyTyDTO;
            }

            return Ok(hygieneFormDto);
        }
        #endregion

        #region Add Hygiene Form
        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Hygiene Form Medical Report" }
        )]
        public ActionResult Add(HygieneFormAddDTO hygieneFormDTO)
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
                return BadRequest("Hygiene Form can not be null");
            }

            HygieneForm hygieneForm = _mapper.Map<HygieneForm>(hygieneFormDTO);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            hygieneForm.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);

            if (userTypeClaim == "octa")
            {
                hygieneForm.InsertedByOctaId = userId;
            }

            else if (userTypeClaim == "employee")
            {
                hygieneForm.InsertedByUserId = userId;
            }

            Unit_Of_Work.hygieneForm_Repository.Add(hygieneForm);
            Unit_Of_Work.SaveChanges();

            StudentHygieneTypes sht = new StudentHygieneTypes
            {
                HygieneFormId = hygieneForm.Id,
                StudentId = hygieneFormDTO.StudentId,
                HygieneTypeId = hygieneFormDTO.HygieneTypeId
            };

            Unit_Of_Work.studentHygieneTypes_Repository.Add(sht);
            Unit_Of_Work.SaveChanges();

            return Ok(hygieneFormDTO);
        }
        #endregion

        #region Update Hygiene Form
        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Hygiene Form Medical Report" }
        )]
        public ActionResult Update(HygieneFormPutDTO hygieneFormDTO)
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
                return BadRequest("Hygiene Form can not be null");
            }

            HygieneForm hygieneForm = Unit_Of_Work.hygieneForm_Repository.First_Or_Default(d => d.Id == hygieneFormDTO.ID && d.IsDeleted != true);

            if (hygieneForm == null)
            {
                return NotFound();
            }

            _mapper.Map(hygieneFormDTO, hygieneForm);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");

            hygieneForm.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);

            if (userTypeClaim == "octa")
            {
                hygieneForm.UpdatedByOctaId = userId;
                if (hygieneForm.UpdatedByUserId != null)
                {
                    hygieneForm.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                hygieneForm.UpdatedByUserId = userId;
                if (hygieneForm.UpdatedByOctaId != null)
                {
                    hygieneForm.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.hygieneForm_Repository.Update(hygieneForm);
            Unit_Of_Work.SaveChanges();

            return Ok(hygieneFormDTO);
        }
        #endregion

        #region Delete Hygiene Form
        [HttpDelete]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Hygiene Form Medical Report" }
        )]
        public ActionResult Delete(long id)
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
            
            HygieneForm hygieneForm = Unit_Of_Work.hygieneForm_Repository.First_Or_Default(d => d.IsDeleted != true && d.Id == id);

            if (hygieneForm == null)
            {
                return NotFound();
            }
            
            Unit_Of_Work.hygieneForm_Repository.Delete(id);
            Unit_Of_Work.SaveChanges();
            
            return Ok("Hygiene Form deleted successfully");
        }
        #endregion
    }
}
