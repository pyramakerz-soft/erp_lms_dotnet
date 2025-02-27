using AutoMapper;
using LMS_CMS_BL.DTO.Clinic;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.ClinicModule;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Controllers.Domains.Clinic
{
    [Route("api/[controller]")]
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
            pages: new[] { "HygieneForm" }
        )]
        public ActionResult Get()
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
            
            List<HygieneForm> hygieneForms = Unit_Of_Work.hygieneForm_Repository.Select_All();
            
            if (hygieneForms == null || hygieneForms.Count == 0)
            {
                return NotFound();
            }
            
            List<HygieneFormGetDTO> hygieneFormsDto = _mapper.Map<List<HygieneFormGetDTO>>(hygieneForms);
            
            return Ok(hygieneFormsDto);
        }
        #endregion

        #region GetByID
        [HttpGet("id")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "HygieneForm" }
        )]
        public ActionResult GetByID(long id)
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

            HygieneForm hygieneForm = Unit_Of_Work.hygieneForm_Repository.Select_By_Id(id);

            if (hygieneForm == null)
            {
                return NotFound("No Hygiene Type with this ID");
            }

            HygieneFormGetDTO hygieneFormDto = _mapper.Map<HygieneFormGetDTO>(hygieneForm);

            return Ok(hygieneFormDto);
        }
        #endregion

        #region Add Hygiene Form
        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "HygieneForm" }
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
            
            Unit_Of_Work.hygieneForm_Repository.Add(hygieneForm);
            Unit_Of_Work.SaveChanges();
            
            return Ok(hygieneFormDTO);
        }
        #endregion

        #region Update Hygiene Form
        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "HygieneForm" }
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

            HygieneForm hygieneForm = _mapper.Map<HygieneForm>(hygieneFormDTO);

            hygieneForm.UpdatedByUserId = userId;
            hygieneForm.UpdatedAt = DateTime.Now;

            Unit_Of_Work.hygieneForm_Repository.Update(hygieneForm);
            Unit_Of_Work.SaveChanges();

            return Ok(hygieneFormDTO);
        }
        #endregion

        #region Delete Hygiene Form
        [HttpDelete]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "HygieneForm" }
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
            
            HygieneForm hygieneForm = Unit_Of_Work.hygieneForm_Repository.Select_By_Id(id);
            
            if (hygieneForm == null)
            {
                return NotFound("No Hygiene Form with this ID");
            }
            
            Unit_Of_Work.hygieneForm_Repository.Delete(id);
            Unit_Of_Work.SaveChanges();
            
            return Ok("Hygiene Form deleted successfully");
        }
        #endregion
    }
}
