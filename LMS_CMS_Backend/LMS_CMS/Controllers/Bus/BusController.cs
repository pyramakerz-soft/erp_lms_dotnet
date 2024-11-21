using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.DTO.Bus;
using LMS_CMS_BL.UOW;
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
        public IActionResult Get()
        {
            List<BusModel> buses = Unit_Of_Work.bus_Repository.Select_All_With_Includes(
                emp => emp.Driver,
                assisstant => assisstant.DriverAssistant,
                type => type.BusType,
                restrict => restrict.BusRestrict,
                StatusCode => StatusCode.BusStatus,
                company => company.BusCompany
                );
            if (buses == null)
            {
                return NotFound();
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
                bus => bus.ID == Id,
                query => query.Include(e => e.Driver),
                query => query.Include(e => e.DriverAssistant),
                query => query.Include(e => e.BusType),
                query => query.Include(e => e.BusStatus),
                query => query.Include(e => e.BusRestrict),
                query => query.Include(e => e.BusCompany)
                );

            if (bus == null)
            {
                return NotFound();
            }

            Bus_GetDTO busDTO = mapper.Map<Bus_GetDTO>(bus);

            return Ok(busDTO);
        }

        [HttpPost]
        public ActionResult Add(Bus_AddDTO busAddDTO)
        {
            if (busAddDTO == null)
            {
                return BadRequest("Bus cannot be null.");
            }

            if(busAddDTO.BusTypeID != null)
            {

                BusType busType = Unit_Of_Work.busType_Repository.Select_By_Id(busAddDTO.BusTypeID);
                if (busType == null)
                {
                    return NotFound("No Bus Type with this ID");
                }
            }

            if (busAddDTO.BusCompanyID != null)
            {
                BusCompany busCompany = Unit_Of_Work.busCompany_Repository.Select_By_Id(busAddDTO.BusCompanyID);
                if (busCompany == null)
                {
                    return NotFound("No Bus Company with this ID");
                }
            }

            if (busAddDTO.BusRestrictID != null)
            {
                BusRestrict busRestrict = Unit_Of_Work.busRestrict_Repository.Select_By_Id(busAddDTO.BusRestrictID);
                if (busRestrict == null)
                {
                    return NotFound("No Bus Restrict with this ID");
                }
            }
            if (busAddDTO.BusStatusID != null)
            {

                BusStatus busStatus = Unit_Of_Work.busStatus_Repository.Select_By_Id(busAddDTO.BusStatusID);
                if (busStatus == null)
                {
                    return NotFound("No Bus Status with this ID");
                }
            }

            if (busAddDTO.DriverID != null)
            {
                Employee busDriver = Unit_Of_Work.employee_Repository.Select_By_Id(busAddDTO.DriverID);
                if (busDriver == null)
                {
                    return NotFound("No Bus Driver with this ID");
                }
            }

            if (busAddDTO.DriverAssistantID != null)
            {
                Employee busDriverAssisstant = Unit_Of_Work.employee_Repository.Select_By_Id(busAddDTO.DriverAssistantID);
                if (busDriverAssisstant == null)
                {
                    return NotFound("No Bus Status Assisstant with this ID");
                }
            }

            BusModel bus = mapper.Map<BusModel>(busAddDTO);
            Unit_Of_Work.bus_Repository.Add(bus);
            Unit_Of_Work.SaveChanges();

            return CreatedAtAction(nameof(GetByID), new { Id = bus.ID }, busAddDTO);
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
                return NotFound();
            }
            else
            {
                Unit_Of_Work.bus_Repository.Delete(Id);
                Unit_Of_Work.SaveChanges();
                return Ok("Bus has Successfully been deleted");
            }
        }
    }
}
