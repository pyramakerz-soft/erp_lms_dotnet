using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.DTO.Bus;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Migrations;
using LMS_CMS_DAL.Models;
using LMS_CMS_DAL.Models.BusModule;
using LMS_CMS_PL.Attribute;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusModel = LMS_CMS_DAL.Models.BusModule.Bus;

namespace LMS_CMS_PL.Controllers.Bus
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<IActionResult> GetAsync()
        {
            List<BusModel> buses = await Unit_Of_Work.bus_Repository.Select_All_With_IncludesById<BusModel>(
                bus => bus.IsDeleted!=true, // Filtering here
                query => query.Include(emp => emp.Driver),
                query => query.Include(assisstant => assisstant.DriverAssistant),
                query => query.Include(type => type.BusType),
                query => query.Include(restrict => restrict.BusRestrict),
                query => query.Include(StatusCode => StatusCode.BusStatus),
                query => query.Include(company => company.BusCompany)
                );
            if (buses == null)
            {
                return NotFound("No buses");
            }

            List<Bus_GetDTO> busDTOs = mapper.Map<List<Bus_GetDTO>>(buses);

            return Ok(busDTOs);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByID(long Id)
        {
            if (Id == 0)
            {
                return BadRequest("Enter Bus ID");
            }

            var bus = await Unit_Of_Work.bus_Repository.FindByIncludesAsync(
                bus => bus.ID == Id&&bus.IsDeleted!=true,
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

            Bus_GetDTO busDTO = mapper.Map<Bus_GetDTO>(bus);

            return Ok(busDTO);
        }

        [HttpGet("GetByDomainID/{Id}")]
        public async Task<IActionResult> GetByDomainIDAsync(long Id)
        {
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
                return NotFound("There is no buses in this domian");
            }

            List<Bus_GetDTO> busDTOs = mapper.Map<List<Bus_GetDTO>>(buses);

            return Ok(busDTOs);
        }

        [HttpPost]
        public ActionResult Add(Bus_AddDTO busAddDTO)
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id");
            if (userIdClaim == null )
            {
                return Unauthorized(new { message = "User ID not found in token." });
            }
            int userId;
            if (!int.TryParse(userIdClaim.Value, out userId))
            {
                return BadRequest(new { message = "Invalid User ID in token." });
            }
            if (busAddDTO == null)
            {
                return BadRequest("Bus cannot be null.");
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
            Unit_Of_Work.bus_Repository.Add(bus);
            Unit_Of_Work.SaveChanges();

            return CreatedAtAction(nameof(GetByID), new { Id = bus.ID }, busAddDTO);
        }


        [HttpPut]
        public ActionResult Edit(Bus_PutDTO busPutDTO)
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id");
            if (userIdClaim == null)
            {
                return Unauthorized(new { message = "User ID not found in token." });
            }
            int userId;
            if (!int.TryParse(userIdClaim.Value, out userId))
            {
                return BadRequest(new { message = "Invalid User ID in token." });
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

            mapper.Map(busPutDTO, busExists);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            busExists.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            busExists.UpdatedByUserId = userId;
            Unit_Of_Work.bus_Repository.Update(busExists);
            Unit_Of_Work.SaveChanges();

            return Ok(busPutDTO);
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(long Id)
        {
            if (Id == 0)
            {
                return BadRequest("Bus ID cannot be null.");
            }

            BusModel bus = Unit_Of_Work.bus_Repository.Select_By_Id(Id);
            if (bus == null)
            {
                return NotFound("No Bus with this ID");
            }
            else
            {
                var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id" );
                if (userIdClaim == null)
                {
                    return BadRequest(new { message = "User ID not found in token." });
                }
                int userId;
                if (!int.TryParse(userIdClaim.Value, out userId))
                {
                    return BadRequest(new { message = "Invalid User ID in token." });
                }
                bus.IsDeleted = true;
                bus.DeletedByUserId = userId;
                TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
                bus.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                Unit_Of_Work.bus_Repository.Update(bus);
                Unit_Of_Work.SaveChanges();
                return Ok("Bus has Successfully been deleted");
            }
        }
    }
}
