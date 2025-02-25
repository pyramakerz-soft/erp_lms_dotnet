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
    public class BusStatusController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;

        public BusStatusController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
        }


        ///////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Bus Status" }
        )]
        public IActionResult Get()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<BusStatus> BusStatus;

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            BusStatus = Unit_Of_Work.busStatus_Repository.FindBy(t => t.IsDeleted != true);

            if (BusStatus == null || BusStatus.Count == 0)
            {
                return NotFound();
            }

            List<BusStatusGetDTO> BusStatusDTO = mapper.Map<List<BusStatusGetDTO>>(BusStatus);

            return Ok(BusStatusDTO);
        }

        ///////////////////////////////////////////////////

        [HttpGet("id")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Bus Status" }
        )]
        public IActionResult GetById(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (id == 0)
            {
                return BadRequest("Enter Bus Status ID");
            }

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            BusStatus busStatus = Unit_Of_Work.busStatus_Repository.First_Or_Default(b=>b.ID==id&&b.IsDeleted!=true);
            if (busStatus == null || busStatus.IsDeleted == true)
            {
                return NotFound("No bus status with this ID");
            }

            BusStatusGetDTO StatusDTO = mapper.Map<BusStatusGetDTO>(busStatus);
            return Ok(StatusDTO);
        }
        ///////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Bus Status" }
        )]
        public IActionResult Add(BusStatusAddDTO NewBus)
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
                return BadRequest("Bus Status cannot be null");
            }
            if (NewBus.Name == null)
            {
                return BadRequest("the name cannot be null");
            }
            BusStatus bustStatus = mapper.Map<BusStatus>(NewBus);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            bustStatus.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                bustStatus.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                bustStatus.InsertedByUserId = userId;
            }

            Unit_Of_Work.busStatus_Repository.Add(bustStatus);
            Unit_Of_Work.SaveChanges();
            return Ok(NewBus);

        }

        ////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Bus Status" }
        )]
        public IActionResult Edit(BusStatusEditDTO EditBusStatus)
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

            if (EditBusStatus == null)
            {
                BadRequest();
            }
            if (EditBusStatus.Name == null)
            {
                return BadRequest("the name cannot be null");
            }
            BusStatus busStatus = Unit_Of_Work.busStatus_Repository.Select_By_Id(EditBusStatus.ID);

            if (busStatus == null || busStatus.IsDeleted == true)
            {
                return NotFound("No Bus Status with this ID");
            }
             
            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Bus Status", roleId, userId, busStatus);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            mapper.Map(EditBusStatus, busStatus);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            busStatus.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                busStatus.UpdatedByOctaId = userId;
                if (busStatus.UpdatedByUserId != null)
                {
                    busStatus.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                busStatus.UpdatedByUserId = userId;
                if (busStatus.UpdatedByOctaId != null)
                {
                    busStatus.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.busStatus_Repository.Update(busStatus);
            Unit_Of_Work.SaveChanges();
            return Ok(EditBusStatus);
        }

        ////////////////////////////////////////////////////////

        [HttpDelete]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Bus Status" }
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
                return BadRequest("Bus Status ID cannot be null.");
            }

            BusStatus busStatus = Unit_Of_Work.busStatus_Repository.Select_By_Id(id);

            if (busStatus == null || busStatus.IsDeleted == true)
            {
                return NotFound("No Bus Status with this ID");
            }
            else
            { 
                if (userTypeClaim == "employee")
                {
                    IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "Bus Status", roleId, userId, busStatus);
                    if (accessCheck != null)
                    {
                        return accessCheck;
                    }
                }

                busStatus.IsDeleted = true;
                TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
                busStatus.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                if (userTypeClaim == "octa")
                {
                    busStatus.DeletedByOctaId = userId;
                    if (busStatus.DeletedByUserId != null)
                    {
                        busStatus.DeletedByUserId = null;
                    }
                }
                else if (userTypeClaim == "employee")
                {
                    busStatus.DeletedByUserId = userId;
                    if (busStatus.DeletedByOctaId != null)
                    {
                        busStatus.DeletedByOctaId = null;
                    }
                }
                Unit_Of_Work.busStatus_Repository.Update(busStatus);
                Unit_Of_Work.SaveChanges();
                return Ok();
            }
        }

    }
}
