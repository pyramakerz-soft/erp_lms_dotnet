using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.DTO.Bus;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Migrations;
using LMS_CMS_DAL.Models;
using LMS_CMS_DAL.Models.BusModule;
using LMS_CMS_PL.Attribute;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusModel = LMS_CMS_DAL.Models.BusModule.Bus;

namespace LMS_CMS_PL.Controllers.Bus
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BusController : ControllerBase
    {
        private UOW Unit_Of_Work;
        IMapper mapper;

        public BusController(UOW Unit_Of_Work, IMapper mapper)
        {
            this.Unit_Of_Work = Unit_Of_Work;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize_Endpoint_Attribute(
            allowedTypes: new[] { "pyramakerz", "employee" },
            pages: new[] { "Busses", "Bus Details" }
        )]
        public async Task<IActionResult> GetAsync()
        {
            List<BusModel> buses;

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (userTypeClaim == "employee")
            {
                Employee employee = Unit_Of_Work.employee_Repository.Select_By_Id(userId);
                long employeeDomain = employee.Domain_ID;

                buses = await Unit_Of_Work.bus_Repository.Select_All_With_IncludesById<BusModel>(
                    bus => bus.IsDeleted != true && bus.DomainID == employeeDomain,
                    query => query.Include(emp => emp.Driver),
                    query => query.Include(assisstant => assisstant.DriverAssistant),
                    query => query.Include(type => type.BusType),
                    query => query.Include(restrict => restrict.BusRestrict),
                    query => query.Include(StatusCode => StatusCode.BusStatus),
                    query => query.Include(company => company.BusCompany)
                    );
            }
            else
            {
                buses = await Unit_Of_Work.bus_Repository.Select_All_With_IncludesById<BusModel>(
                    bus => bus.IsDeleted != true,
                    query => query.Include(emp => emp.Driver),
                    query => query.Include(assisstant => assisstant.DriverAssistant),
                    query => query.Include(type => type.BusType),
                    query => query.Include(restrict => restrict.BusRestrict),
                    query => query.Include(StatusCode => StatusCode.BusStatus),
                    query => query.Include(company => company.BusCompany)
                    );
            }

            if (buses == null || buses.Count == 0)
            {
                return NotFound("No buses");
            }

            List<Bus_GetDTO> busDTOs = mapper.Map<List<Bus_GetDTO>>(buses);

            return Ok(busDTOs);
        }

        [HttpGet("{Id}")]
        [Authorize_Endpoint_Attribute(
            allowedTypes: new[] { "pyramakerz", "employee"},
            pages: new[] { "Busses", "Bus Details" }
        )]
        public async Task<IActionResult> GetByID(long Id)
        {
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
                   query => query.Include(e => e.BusRestrict),
                   query => query.Include(e => e.BusCompany)
                   );

            if (bus == null)
            {
                return NotFound("No bus with this ID");
            }
            else
            {
                if (userTypeClaim == "employee")
                {
                    Employee employee = Unit_Of_Work.employee_Repository.Select_By_Id(userId);
                    long employeeDomain = employee.Domain_ID;
                    if (bus.DomainID != employeeDomain)
                    {
                        return Unauthorized();
                    }
                }
            }

            Bus_GetDTO busDTO = mapper.Map<Bus_GetDTO>(bus);

            return Ok(busDTO);
        }

        [HttpGet("GetByDomainID/{Id}")]
        [Authorize_Endpoint_Attribute(
            allowedTypes: new[] { "pyramakerz", "employee" },
            pages: new[] { "Busses", "Bus Details" }
        )]
        public async Task<IActionResult> GetByDomainIDAsync(long Id)
        {
            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (userTypeClaim == "employee")
            {
                Employee employee = Unit_Of_Work.employee_Repository.Select_By_Id(userId);
                long employeeDomain = employee.Domain_ID;

                if (Id != employeeDomain)
                {
                    return Unauthorized();
                }
            }

            Domain domain = Unit_Of_Work.domain_Repository.Select_By_Id(Id);
            if (domain == null)
            {
                return NotFound("No Domain with this Id");
            }

            List<BusModel> buses = await Unit_Of_Work.bus_Repository.Select_All_With_IncludesById<BusModel>(
                bus => bus.DomainID == Id && bus.IsDeleted != true,
                query => query.Include(e => e.Driver),
                query => query.Include(e => e.DriverAssistant),
                query => query.Include(e => e.BusType),
                query => query.Include(e => e.BusStatus),
                query => query.Include(e => e.BusRestrict),
                query => query.Include(e => e.BusCompany)
            );

            if (buses == null || buses.Count == 0)
            {
                return NotFound("There are no buses in this domian");
            }

            List<Bus_GetDTO> busDTOs = mapper.Map<List<Bus_GetDTO>>(buses);

            return Ok(busDTOs);
        }

        [HttpPost]
        [Authorize_Endpoint_Attribute(
            allowedTypes: new[] { "pyramakerz", "employee" },
            pages: new[] { "Busses", "Bus Details" }
        )]
        public ActionResult Add(Bus_AddDTO busAddDTO)
        {
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

            if (userTypeClaim == "employee")
            {
                Employee employee = Unit_Of_Work.employee_Repository.Select_By_Id(userId);
                long employeeDomain = employee.Domain_ID;

                if (busAddDTO.DomainID != employeeDomain)
                {
                    return Unauthorized();
                }
            }

            Domain domain = Unit_Of_Work.domain_Repository.Select_By_Id(busAddDTO.DomainID);
            if (domain == null||domain.IsDeleted==true)
            {
                return NotFound("No Domain with this Id");
            }

            if (busAddDTO.BusTypeID != null)
            {

                BusType busType = Unit_Of_Work.busType_Repository.Select_By_Id(busAddDTO.BusTypeID);
                if (busType == null||busType.IsDeleted == true)
                {
                    return NotFound("No Bus Type with this ID");
                }
            }

            if (busAddDTO.BusCompanyID != null)
            {
                BusCompany busCompany = Unit_Of_Work.busCompany_Repository.Select_By_Id(busAddDTO.BusCompanyID);
                if (busCompany == null || busCompany.IsDeleted == true)
                {
                    return NotFound("No Bus Company with this ID");
                }
            }

            if (busAddDTO.BusRestrictID != null)
            {
                BusRestrict busRestrict = Unit_Of_Work.busRestrict_Repository.Select_By_Id(busAddDTO.BusRestrictID);
                if (busRestrict == null || busRestrict.IsDeleted == true)
                {
                    return NotFound("No Bus Restrict with this ID");
                }
            }
            if (busAddDTO.BusStatusID != null)
            {

                BusStatus busStatus = Unit_Of_Work.busStatus_Repository.Select_By_Id(busAddDTO.BusStatusID);
                if (busStatus == null||busStatus.IsDeleted == true)
                {
                    return NotFound("No Bus Status with this ID");
                }
            }

            if (busAddDTO.DriverID != null)
            {
                Employee busDriver = Unit_Of_Work.employee_Repository.Select_By_Id(busAddDTO.DriverID);
                if (busDriver == null || busDriver.IsDeleted == true)
                {
                    return NotFound("No Bus Driver with this ID");
                }
            }

            if (busAddDTO.DriverAssistantID != null)
            {
                Employee busDriverAssisstant = Unit_Of_Work.employee_Repository.Select_By_Id(busAddDTO.DriverAssistantID);
                if (busDriverAssisstant == null||busDriverAssisstant.IsDeleted == true)
                {
                    return NotFound("No Bus Status Assisstant with this ID");
                }
            }

            BusModel bus = mapper.Map<BusModel>(busAddDTO);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            bus.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            bus.InsertedByUserId = userId;
            if(userTypeClaim == "pyramakerz")
            {
                bus.InsertedByUserRole = "pyramakerz";
            }
            else if (userTypeClaim == "employee") 
            {
                bus.InsertedByUserRole = "employee";
            }

            Unit_Of_Work.bus_Repository.Add(bus);
            Unit_Of_Work.SaveChanges();

            return CreatedAtAction(nameof(GetByID), new { Id = bus.ID }, busAddDTO);
        }


        [HttpPut]
        [Authorize_Endpoint_Attribute(
            allowedTypes: new[] { "pyramakerz", "employee" },
            allowEdit: 1,
            pages: new[] { "Busses", "Bus Details" }
        )]
        public ActionResult Edit(Bus_PutDTO busPutDTO)
        {
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

            Domain domain = Unit_Of_Work.domain_Repository.Select_By_Id(busPutDTO.DomainID);
            if (domain == null || domain.IsDeleted == true)
            {
                return NotFound("No Domain with this Id");
            }

            if (busPutDTO.BusTypeID != null)
            {

                BusType busType = Unit_Of_Work.busType_Repository.Select_By_Id(busPutDTO.BusTypeID);
                if (busType == null ||busType.IsDeleted == true)
                {
                    return NotFound("No Bus Type with this ID");
                }
            }

            if (busPutDTO.BusCompanyID != null)
            {
                BusCompany busCompany = Unit_Of_Work.busCompany_Repository.Select_By_Id(busPutDTO.BusCompanyID);
                if (busCompany == null || busCompany.IsDeleted == true)
                {
                    return NotFound("No Bus Company with this ID");
                }
            }

            if (busPutDTO.BusRestrictID != null)
            {
                BusRestrict busRestrict = Unit_Of_Work.busRestrict_Repository.Select_By_Id(busPutDTO.BusRestrictID);
                if (busRestrict == null||busRestrict.IsDeleted == true)
                {
                    return NotFound("No Bus Restrict with this ID");
                }
            }
            if (busPutDTO.BusStatusID != null)
            {

                BusStatus busStatus = Unit_Of_Work.busStatus_Repository.Select_By_Id(busPutDTO.BusStatusID);
                if (busStatus == null || busStatus.IsDeleted == true)
                {
                    return NotFound("No Bus Status with this ID");
                }
            }

            if (busPutDTO.DriverID != null)
            {
                Employee busDriver = Unit_Of_Work.employee_Repository.Select_By_Id(busPutDTO.DriverID);
                if (busDriver == null||busDriver.IsDeleted == true)
                {
                    return NotFound("No Bus Driver with this ID");
                }
            }

            if (busPutDTO.DriverAssistantID != null)
            {
                Employee busDriverAssisstant = Unit_Of_Work.employee_Repository.Select_By_Id(busPutDTO.DriverAssistantID);
                if (busDriverAssisstant == null || busDriverAssisstant.IsDeleted == true)
                {
                    return NotFound("No Bus Status Assisstant with this ID");
                }
            }


            BusModel busExists = Unit_Of_Work.bus_Repository.Select_By_Id(busPutDTO.ID);
            if (busExists == null || busExists.IsDeleted == true)
            {
                return NotFound("No Bus with this ID");
            }


            if (userTypeClaim == "employee")
            {
                Employee employee = Unit_Of_Work.employee_Repository.Select_By_Id(userId);
                long employeeDomain = employee.Domain_ID;

                if (busPutDTO.DomainID != employeeDomain || busExists.DomainID != employeeDomain)
                {
                    return Unauthorized();
                }
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Bus Details");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if(busExists.InsertedByUserId != userId)
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
            busExists.UpdatedByUserId = userId;
            if (userTypeClaim == "pyramakerz")
            {
                busExists.UpdatedByUserRole = "pyramakerz";
            }
            else if (userTypeClaim == "employee")
            {
                busExists.UpdatedByUserRole = "employee";
            }
            Unit_Of_Work.bus_Repository.Update(busExists);
            Unit_Of_Work.SaveChanges();

            return Ok(busPutDTO);
        }

        [HttpDelete("{Id}")]
        [Authorize_Endpoint_Attribute(
            allowedTypes: new[] { "pyramakerz", "employee" },
            allowDelete: 1,
            pages: new[] { "Busses", "Bus Details" }
        )]
        public IActionResult Delete(long Id)
        {
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
                    Employee employee = Unit_Of_Work.employee_Repository.Select_By_Id(userId);
                    long employeeDomain = employee.Domain_ID;

                    if (bus.DomainID != employeeDomain)
                    {
                        return Unauthorized();
                    }

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
                bus.DeletedByUserId = userId;
                TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
                bus.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                if (userTypeClaim == "pyramakerz")
                {
                    bus.DeletedByUserRole = "pyramakerz";
                }
                else if (userTypeClaim == "employee")
                {
                    bus.DeletedByUserRole = "employee";
                }
                Unit_Of_Work.bus_Repository.Update(bus);
                Unit_Of_Work.SaveChanges();
                return Ok("Bus has Successfully been deleted");
            }
        }
    }
}
