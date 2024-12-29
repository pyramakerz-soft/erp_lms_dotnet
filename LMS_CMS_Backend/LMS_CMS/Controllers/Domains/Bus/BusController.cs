using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.DTO.Bus;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Migrations;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.BusModule;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusModel = LMS_CMS_DAL.Models.Domains.BusModule.Bus;

namespace LMS_CMS_PL.Controllers.Domains.Bus
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class BusController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public BusController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Busses", "Bus Details" }
        )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<BusModel> buses;

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            buses = await Unit_Of_Work.bus_Repository.Select_All_With_IncludesById<BusModel>(
                    bus => bus.IsDeleted != true,
                    query => query.Include(emp => emp.Driver),
                    query => query.Include(assisstant => assisstant.DriverAssistant),
                    query => query.Include(type => type.BusType),
                    query => query.Include(District => District.BusDistrict),
                    query => query.Include(StatusCode => StatusCode.BusStatus),
                    query => query.Include(company => company.BusCompany)
                    );

            if (buses == null || buses.Count == 0)
            {
                return NotFound("No buses");
            }

            List<Bus_GetDTO> busDTOs = mapper.Map<List<Bus_GetDTO>>(buses);

            return Ok(busDTOs);
        }

        [HttpGet("{Id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Busses", "Bus Details" }
        )]
        public async Task<IActionResult> GetByID(long Id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            BusModel bus;

            if (Id == 0)
            {
                return BadRequest("Enter Bus ID");
            }

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            bus = await Unit_Of_Work.bus_Repository.FindByIncludesAsync(
                   bus => bus.ID == Id && bus.IsDeleted != true,
                   query => query.Include(e => e.Driver),
                   query => query.Include(e => e.DriverAssistant),
                   query => query.Include(e => e.BusType),
                   query => query.Include(e => e.BusStatus),
                   query => query.Include(e => e.BusDistrict),
                   query => query.Include(e => e.BusCompany)
                   );

            if (bus == null)
            {
                return NotFound("No bus with this ID");
            }

            Bus_GetDTO busDTO = mapper.Map<Bus_GetDTO>(bus);

            return Ok(busDTO);
        }


        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Busses", "Bus Details" }
        )]
        public ActionResult Add(Bus_AddDTO busAddDTO)
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

            if (busAddDTO == null)
            {
                return BadRequest("Bus cannot be null");
            }

            if (busAddDTO.BusTypeID != null)
            {

                BusType busType = Unit_Of_Work.busType_Repository.First_Or_Default(b=>b.ID==busAddDTO.BusTypeID && b.IsDeleted != true);
                if (busType == null || busType.IsDeleted == true)
                {
                    return NotFound("No Bus Type with this ID");
                }
            }
            if (busAddDTO.Name == null)
            {
              return BadRequest("the bus name Cannot be null");
            }

            if (busAddDTO.BusCompanyID != null)
            {
                BusCompany busCompany = Unit_Of_Work.busCompany_Repository.First_Or_Default(b => b.ID == busAddDTO.BusCompanyID && b.IsDeleted != true);
                if (busCompany == null || busCompany.IsDeleted == true)
                {
                    return NotFound("No Bus Company with this ID");
                }
            }

            if (busAddDTO.BusDistrictID != null)
            {
                BusDistrict busDistrict = Unit_Of_Work.busDistrict_Repository.First_Or_Default(b => b.ID == busAddDTO.BusDistrictID && b.IsDeleted != true);
                if (busDistrict == null || busDistrict.IsDeleted == true)
                {
                    return NotFound("No Bus District with this ID");
                }
            }
            if (busAddDTO.BusStatusID != null)
            {

                BusStatus busStatus = Unit_Of_Work.busStatus_Repository.First_Or_Default(b => b.ID == busAddDTO.BusStatusID && b.IsDeleted != true);
                if (busStatus == null || busStatus.IsDeleted == true)
                {
                    return NotFound("No Bus Status with this ID");
                }
            }

            if (busAddDTO.DriverID != null)
            {
                Employee busDriver = Unit_Of_Work.employee_Repository.First_Or_Default(b => b.ID == busAddDTO.DriverID && b.IsDeleted != true);
                if (busDriver == null || busDriver.IsDeleted == true)
                {
                    return NotFound("No Bus Driver with this ID");
                }
            }

            if (busAddDTO.DriverAssistantID != null)
            {
                Employee busDriverAssisstant = Unit_Of_Work.employee_Repository.First_Or_Default(b => b.ID == busAddDTO.DriverAssistantID && b.IsDeleted != true);
                if (busDriverAssisstant == null || busDriverAssisstant.IsDeleted == true)
                {
                    return NotFound("No Bus Status Assisstant with this ID");
                }
            }

            BusModel bus = mapper.Map<BusModel>(busAddDTO);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            bus.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                bus.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                bus.InsertedByUserId = userId;
            }

            Unit_Of_Work.bus_Repository.Add(bus);
            Unit_Of_Work.SaveChanges();

            return CreatedAtAction(nameof(GetByID), new { Id = bus.ID }, busAddDTO);
        }


        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Busses", "Bus Details" }
        )]
        public ActionResult Edit(Bus_PutDTO busPutDTO)
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

            if (busPutDTO == null)
            {
                return BadRequest("Bus cannot be null.");
            }

            if (busPutDTO.BusTypeID != null)
            {
                BusType busType = Unit_Of_Work.busType_Repository.First_Or_Default(b=>b.ID==busPutDTO.BusTypeID&&b.IsDeleted!=true);
                if (busType == null || busType.IsDeleted == true)
                {
                    return NotFound("No Bus Type with this ID");
                }
            }

            if (busPutDTO.BusCompanyID != null)
            {
                BusCompany busCompany = Unit_Of_Work.busCompany_Repository.First_Or_Default(b => b.ID == busPutDTO.BusCompanyID && b.IsDeleted != true);
                if (busCompany == null || busCompany.IsDeleted == true)
                {
                    return NotFound("No Bus Company with this ID");
                }
            }

            if (busPutDTO.BusDistrictID != null)
            {
                BusDistrict busDistrict = Unit_Of_Work.busDistrict_Repository.First_Or_Default(b => b.ID == busPutDTO.BusDistrictID && b.IsDeleted != true);
                if (busDistrict == null || busDistrict.IsDeleted == true)
                {
                    return NotFound("No Bus District with this ID");
                }
            }
            if (busPutDTO.BusStatusID != null)
            {

                BusStatus busStatus = Unit_Of_Work.busStatus_Repository.First_Or_Default(b => b.ID == busPutDTO.BusStatusID && b.IsDeleted != true);
                if (busStatus == null || busStatus.IsDeleted == true)
                {
                    return NotFound("No Bus Status with this ID");
                }
            }

            if (busPutDTO.DriverID != null)
            {
                Employee busDriver = Unit_Of_Work.employee_Repository.First_Or_Default(b => b.ID == busPutDTO.DriverID && b.IsDeleted != true);
                if (busDriver == null || busDriver.IsDeleted == true)
                {
                    return NotFound("No Bus Driver with this ID");
                }
            }

            if (busPutDTO.DriverAssistantID != null)
            {
                Employee busDriverAssisstant = Unit_Of_Work.employee_Repository.First_Or_Default(b => b.ID == busPutDTO.DriverAssistantID && b.IsDeleted != true);
                if (busDriverAssisstant == null || busDriverAssisstant.IsDeleted == true)
                {
                    return NotFound("No Bus Status Assisstant with this ID");
                }
            }
            if (busPutDTO.Name == null)
            {
                return BadRequest("the bus name Cannot be null");
            }

            BusModel busExists = Unit_Of_Work.bus_Repository.First_Or_Default(b => b.ID == busPutDTO.ID && b.IsDeleted != true);
            if (busExists == null || busExists.IsDeleted == true)
            {
                return NotFound("No Bus with this ID");
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Bus Details");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (busExists.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Busses page doesn't exist");
                }
            }

            mapper.Map(busPutDTO, busExists);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            busExists.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                busExists.UpdatedByOctaId = userId;
                if (busExists.UpdatedByUserId != null)
                {
                    busExists.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                busExists.UpdatedByUserId = userId;
                if (busExists.UpdatedByOctaId != null)
                {
                    busExists.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.bus_Repository.Update(busExists);
            Unit_Of_Work.SaveChanges();

            return Ok(busPutDTO);
        }

        [HttpDelete("{Id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Busses", "Bus Details" }
        )]
        public IActionResult Delete(long Id)
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

            if (Id == 0)
            {
                return BadRequest("Bus ID cannot be null.");
            }

            BusModel bus = Unit_Of_Work.bus_Repository.Select_By_Id(Id);
            if (bus == null || bus.IsDeleted == true)
            {
                return NotFound("No Bus with this ID");
            }
            else
            {
                if (userTypeClaim == "employee")
                {
                    Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Bus Details");
                    if (page != null)
                    {
                        Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                        if (roleDetails != null && roleDetails.Allow_Delete_For_Others == false)
                        {
                            if (bus.InsertedByUserId != userId)
                            {
                                return Unauthorized();
                            }
                        }
                    }
                    else
                    {
                        return BadRequest("Busses page doesn't exist");
                    }
                }

                bus.IsDeleted = true;
                TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
                bus.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                if (userTypeClaim == "octa")
                {
                    bus.DeletedByOctaId = userId;
                    if (bus.DeletedByUserId != null)
                    {
                        bus.DeletedByUserId = null;
                    }
                }
                else if (userTypeClaim == "employee")
                {
                    bus.DeletedByUserId = userId;
                    if (bus.DeletedByOctaId != null)
                    {
                        bus.DeletedByOctaId = null;
                    }
                }
                Unit_Of_Work.bus_Repository.Update(bus);
                Unit_Of_Work.SaveChanges();
                return Ok(new { message = "Bus has Successfully been deleted" });
            }
        }
    }
}
