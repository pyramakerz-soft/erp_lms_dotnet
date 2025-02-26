using AutoMapper;
using LMS_CMS_BL.DTO.Clinic;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.ClinicModule;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Controllers.Domains.Clinic
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    public class DrugController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        private readonly IMapper _mapper;

        public DrugController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        #region Get
        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Drugs" }
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

            List<Drug> drugs = Unit_Of_Work.drug_Repository.Select_All();
            
            if (drugs == null || drugs.Count == 0)
            {
                return NotFound();
            }

            List<DrugDto> drugsDto = _mapper.Map<List<DrugDto>>(drugs);

            return Ok(drugsDto);
        }
        #endregion

        #region GetByID
        [HttpGet("id")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Drugs" }
        )]
        public IActionResult GetByID(int id)
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
            Drug drug = Unit_Of_Work.drug_Repository.Select_By_Id(id);

            if (drug == null)
            {
                return NotFound("No Hygiene Type with this ID");
            }

            DrugDto drugDto = _mapper.Map<DrugDto>(drug);
            
            return Ok(drugDto);
        }
        #endregion

        #region Add Drug
        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Drugs" }
        )]
        public IActionResult Add(DrugDto drugDto)
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
                return BadRequest("Please enter the Name field");
            }

            Drug drug = _mapper.Map<Drug>(drugDto);

            drug.InsertedByUserId = userId;
            drug.InsertedAt = DateTime.Now;

            Unit_Of_Work.drug_Repository.Add(drug);
            Unit_Of_Work.SaveChanges();
            
            return Ok("Drug added successfully");
        }
        #endregion

        #region Update Drug
        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Drugs" }
        )]
        public IActionResult Update(DrugDto drugDto)
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
                return BadRequest("Please enter the Name field");
            }

            Drug drug = _mapper.Map<Drug>(drugDto);

            drug.UpdatedByUserId = userId;
            drug.UpdatedAt = DateTime.Now;
            
            Unit_Of_Work.drug_Repository.Update(drug);
            Unit_Of_Work.SaveChanges();

            return Ok("Drug updated successfully");
        }
        #endregion

        #region Delete Drug
        [HttpDelete]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Drugs" }
        )]
        public IActionResult Delete(int id)
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
            
            Drug drug = Unit_Of_Work.drug_Repository.Select_By_Id(id);
            
            if (drug == null)
            {
                return NotFound("No Drug with this ID");
            }
           
            drug.IsDeleted = true;
            drug.DeletedByUserId = userId;
            drug.DeletedAt = DateTime.Now;
            
            Unit_Of_Work.drug_Repository.Update(drug);
            Unit_Of_Work.SaveChanges();
            
            return Ok("Drug deleted successfully");
        }
        #endregion
    }
}
