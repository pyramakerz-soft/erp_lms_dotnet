using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.DTO.Bus;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Migrations;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.BusModule;
using LMS_CMS_DAL.Models.Domains.RegisterationModule;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices.JavaScript;
using System.Security.Claims;

namespace LMS_CMS_PL.Controllers.Domains.Bus
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class BusTypeController : ControllerBase
    {

        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;

        public BusTypeController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
        }


        ///////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Bus Types" }
        )]
        public IActionResult Get()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<BusType> BusType;

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            BusType = Unit_Of_Work.busType_Repository.FindBy(t => t.IsDeleted != true);

            if (BusType == null || BusType.Count == 0)
            {
                return NotFound();
            }

            List<BusTypeGetDTO> BusTypeDTO = mapper.Map<List<BusTypeGetDTO>>(BusType);

            return Ok(BusTypeDTO);
        }

        ///////////////////////////////////////////////////

        [HttpGet("id")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Bus Types" }
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

            BusType busType = Unit_Of_Work.busType_Repository.Select_By_Id(id);
            if (busType == null || busType.IsDeleted == true)
            {
                return NotFound("No bus Type with this ID");
            }

            BusTypeGetDTO typeDTO = mapper.Map<BusTypeGetDTO>(busType);
            return Ok(typeDTO);
        }

        ///////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Bus Types" }
        )]
        public IActionResult Add(BusTypeAddDTO NewBus)
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

            if (NewBus == null)
            {
                return BadRequest("Bus Type cannot be null");
            }
            if (NewBus.Name == null)
            {
                return BadRequest("the name cannot be null");
            }
            BusType bustType = mapper.Map<BusType>(NewBus);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            bustType.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                bustType.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                bustType.InsertedByUserId = userId;
            }

            Unit_Of_Work.busType_Repository.Add(bustType);
            Unit_Of_Work.SaveChanges();
            return Ok(NewBus);

        }

        ////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Bus Types" }
        )]
        public IActionResult Edit(BusTypeEditDTO EditBusType)
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

            if (EditBusType == null)
            {
                BadRequest();
            }
            if (EditBusType.Name == null)
            {
                return BadRequest("the name cannot be null");
            }
            BusType busType = Unit_Of_Work.busType_Repository.Select_By_Id(EditBusType.ID);

            if (busType == null || busType.IsDeleted == true)
            {
                return NotFound("No Bus Type with this ID");
            }
             
            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Bus Types", roleId, userId, busType);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            mapper.Map(EditBusType, busType);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            busType.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                busType.UpdatedByOctaId = userId;
                if (busType.UpdatedByUserId != null)
                {
                    busType.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                busType.UpdatedByUserId = userId;
                if (busType.UpdatedByOctaId != null)
                {
                    busType.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.busType_Repository.Update(busType);
            Unit_Of_Work.SaveChanges();
            return Ok(EditBusType);
        }

        ////////////////////////////////////////////////////////

        [HttpDelete]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Bus Types" }
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

            BusType busType = Unit_Of_Work.busType_Repository.Select_By_Id(id);
            if (busType == null || busType.IsDeleted == true)
            {
                return NotFound("No Bus Type with this ID");
            }
            else
            { 
                if (userTypeClaim == "employee")
                {
                    IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "Bus Types", roleId, userId, busType);
                    if (accessCheck != null)
                    {
                        return accessCheck;
                    }
                }

                busType.IsDeleted = true;
                TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
                busType.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                if (userTypeClaim == "octa")
                {
                    busType.DeletedByOctaId = userId;
                    if (busType.DeletedByUserId != null)
                    {
                        busType.DeletedByUserId = null;
                    }
                }
                else if (userTypeClaim == "employee")
                {
                    busType.DeletedByUserId = userId;
                    if (busType.DeletedByOctaId != null)
                    {
                        busType.DeletedByOctaId = null;
                    }
                }

                Unit_Of_Work.busType_Repository.Update(busType);
                Unit_Of_Work.SaveChanges();
                return Ok();
            }
        }

    }
}
