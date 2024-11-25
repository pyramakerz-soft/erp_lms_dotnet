using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.DTO.Bus;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Migrations;
using LMS_CMS_DAL.Models.BusModule;
using LMS_CMS_PL.Attribute;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices.JavaScript;
using System.Security.Claims;

namespace LMS_CMS_PL.Controllers.Bus
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusTypeController : ControllerBase
    {

        private UOW Unit_Of_Work;
        IMapper mapper;

        public BusTypeController(UOW Unit_Of_Work, IMapper mapper)
        {
            this.Unit_Of_Work = Unit_Of_Work;
            this.mapper = mapper;
        }


        ///////////////////////////////////////////
        
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            List<BusType> BusType = Unit_Of_Work.busType_Repository.FindBy(t=>t.IsDeleted!=true);
            if (BusType == null)
            {
                return NotFound();
            }

            List<BusTypeGetDTO> BusTypeDTO = mapper.Map<List<BusTypeGetDTO>>(BusType);

            return Ok(BusTypeDTO);
        }

        ///////////////////////////////////////////////////

        [HttpGet("id")]
        [Authorize]
        public IActionResult GetById(long id)
        {
            BusType busType = Unit_Of_Work.busType_Repository.Select_By_Id(id);
            if (busType == null) return NotFound();

            BusTypeGetDTO typeDTO = mapper.Map<BusTypeGetDTO>(busType);
            return Ok(typeDTO);
        }

        ///////////////////////////////////////////////////

        [HttpGet("DomainId")]
        [Authorize]
        public IActionResult GetByDomainId(long id)
        {
            List<BusType> BusType = Unit_Of_Work.busType_Repository.FindBy(s => s.DomainId == id && s.IsDeleted != true);
            if (BusType == null)
            {
                return NotFound();
            }

            List<BusTypeGetDTO> BusTypeDTO = mapper.Map<List<BusTypeGetDTO>>(BusType);

            return Ok(BusTypeDTO);
        }

        ///////////////////////////////////////////////////

        [HttpPost]
        [Authorize]
        public IActionResult add(BusTypeAddDTO NewBus)
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

            if (NewBus == null) { return BadRequest(); }
            BusType ExsitType = Unit_Of_Work.busType_Repository.First_Or_Default(c => c.DomainId == NewBus.DomainId && c.Name == NewBus.Name);
            if (ExsitType != null) { return BadRequest("this Type already exist"); }
            BusType bustType = mapper.Map<BusType>(NewBus);
            bustType.InsertedByUserId = userId;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            bustType.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            Unit_Of_Work.busType_Repository.Add(bustType);
            Unit_Of_Work.SaveChanges();
            return Ok(NewBus);

        }

        ////////////////////////////////////////////////////////
        
        [HttpPut]
        [Authorize]

        public IActionResult Edit(BusTypeEditDTO EditBusType)
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
            if (EditBusType == null) { BadRequest(); }
            //BusType busType = mapper.Map<BusType>(EditBusType);
            BusType busType = Unit_Of_Work.busType_Repository.Select_By_Id(EditBusType.ID);
            busType.Name = EditBusType.Name;
            busType.DomainId = EditBusType.DomainId;
            busType.UpdatedByUserId = userId;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            busType.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            Unit_Of_Work.busType_Repository.Update(busType);
            Unit_Of_Work.SaveChanges();
            return Ok(EditBusType);
        }

        ////////////////////////////////////////////////////////

        [HttpDelete]
        [Authorize]

        public IActionResult delete(long id)
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
            BusType busType = Unit_Of_Work.busType_Repository.Select_By_Id(id);
            busType.IsDeleted = true;
            busType.DeletedByUserId = userId;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            busType.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            Unit_Of_Work.busType_Repository.Update(busType);
            Unit_Of_Work.SaveChanges();
            return Ok();

        }

    }
}
