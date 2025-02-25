using AutoMapper;
using LMS_CMS_BL.DTO.Bus;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.BusModule;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Controllers.Domains.Bus
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class BusCompanyController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;

        public BusCompanyController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
        }

        ///////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Bus Companies" }
        )]
        public IActionResult Get()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<BusCompany> BusCompany;

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            BusCompany = Unit_Of_Work.busCompany_Repository.FindBy(t => t.IsDeleted != true);

            if (BusCompany == null || BusCompany.Count == 0)
            {
                return NotFound();
            }

            List<BusCompanyGetDTO> BusCompanyDTO = mapper.Map<List<BusCompanyGetDTO>>(BusCompany);

            return Ok(BusCompanyDTO);
        }

        ///////////////////////////////////////////////////

        [HttpGet("id")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Bus Companies" }
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

            BusCompany busCompany = Unit_Of_Work.busCompany_Repository.First_Or_Default(b=>b.ID==id&&b.IsDeleted!=true);
            if (busCompany == null || busCompany.IsDeleted == true)
            {
                return NotFound("No bus Company with this ID");
            }

            BusCompanyGetDTO CompanyDTO = mapper.Map<BusCompanyGetDTO>(busCompany);
            return Ok(CompanyDTO);
        }
        ///////////////////////////////////////////////////
        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Bus Companies" }
        )]
        public IActionResult Add(BusCompanyAddDTO NewBusCompany)
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

            if (NewBusCompany == null|| NewBusCompany.Name=="")
            {
                return BadRequest("Bus Company cannot be null");
            }
            if (NewBusCompany.Name == null)
            {
                return BadRequest("the name cannot be null");
            }

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            BusCompany busCompany = mapper.Map<BusCompany>(NewBusCompany);
            busCompany.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                busCompany.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                busCompany.InsertedByUserId = userId;
            }

            Unit_Of_Work.busCompany_Repository.Add(busCompany);
            Unit_Of_Work.SaveChanges();
            return Ok(NewBusCompany);
        }

        ////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Bus Companies" }
        )]
        public IActionResult Edit(BusCompanyEditDTO EditBusCompany)
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

            if (EditBusCompany == null)
            {
                BadRequest();
            }
            if (EditBusCompany.Name == null)
            {
                return BadRequest("the name cannot be null");
            }
            BusCompany busCompany = Unit_Of_Work.busCompany_Repository.First_Or_Default(b => b.ID == EditBusCompany.ID&&b.IsDeleted!=true);
            if (busCompany == null || busCompany.IsDeleted == true)
            {
                return NotFound("No Bus Company with this ID");
            }
             
            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Bus Companies", roleId, userId, busCompany);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            mapper.Map(EditBusCompany, busCompany);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            busCompany.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                busCompany.UpdatedByOctaId = userId;
                if (busCompany.UpdatedByUserId != null)
                {
                    busCompany.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                busCompany.UpdatedByUserId = userId;
                if (busCompany.UpdatedByOctaId != null)
                {
                    busCompany.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.busCompany_Repository.Update(busCompany);
            Unit_Of_Work.SaveChanges();
            return Ok(EditBusCompany);
        }

        ////////////////////////////////////////////////////////

        [HttpDelete]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Bus Companies" }
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
                return BadRequest("Bus Category ID cannot be null.");
            }

            BusCompany busCompany = Unit_Of_Work.busCompany_Repository.Select_By_Id(id);

            if (busCompany == null || busCompany.IsDeleted == true)
            {
                return NotFound("No Bus Company with this ID");
            }
            else
            { 
                if (userTypeClaim == "employee")
                {
                    IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "Bus Companies", roleId, userId, busCompany);
                    if (accessCheck != null)
                    {
                        return accessCheck;
                    }
                }

                busCompany.IsDeleted = true;
                TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
                busCompany.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                if (userTypeClaim == "octa")
                {
                    busCompany.DeletedByOctaId = userId;
                    if (busCompany.DeletedByUserId != null)
                    {
                        busCompany.DeletedByUserId = null;
                    }
                }
                else if (userTypeClaim == "employee")
                {
                    busCompany.DeletedByUserId = userId;
                    if (busCompany.DeletedByOctaId != null)
                    {
                        busCompany.DeletedByOctaId = null;
                    }
                }
                Unit_Of_Work.busCompany_Repository.Update(busCompany);
                Unit_Of_Work.SaveChanges();
                return Ok();
            }
        }

    }
}
