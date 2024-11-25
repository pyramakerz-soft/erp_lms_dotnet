using AutoMapper;
using LMS_CMS_BL.DTO.Bus;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.BusModule;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Controllers.Bus
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusRestrictController : ControllerBase
    {
        private UOW Unit_Of_Work;
        IMapper mapper;

        public BusRestrictController(UOW Unit_Of_Work, IMapper mapper)
        {
            this.Unit_Of_Work = Unit_Of_Work;
            this.mapper = mapper;
        }


        ///////////////////////////////////////////

        [HttpGet]
        public IActionResult Get()
        {
            List<BusRestrict> busRestricts = Unit_Of_Work.busRestrict_Repository.FindBy(t => t.IsDeleted != true);
            if (busRestricts == null)
            {
                return NotFound();
            }

            List<BusRestrictGetDTO> busRestrictsDTO = mapper.Map<List<BusRestrictGetDTO>>(busRestricts);

            return Ok(busRestrictsDTO);
        }

        ///////////////////////////////////////////////////

        [HttpGet("id")]
        public IActionResult GetById(long id)
        {
            BusRestrict busRestrict = Unit_Of_Work.busRestrict_Repository.Select_By_Id(id);
            if (busRestrict == null) return NotFound();

            BusRestrictGetDTO busRestrictDto = mapper.Map<BusRestrictGetDTO>(busRestrict);
            return Ok(busRestrictDto);
        }
        ///////////////////////////////////////////////////

        [HttpGet("DomainId")]
        public IActionResult GetByDomainId(long id)
        {
            List<BusRestrict> BusRestrict = Unit_Of_Work.busRestrict_Repository.FindBy(s => s.DomainId == id && s.IsDeleted != true);
            if (BusRestrict == null)
            {
                return NotFound();
            }

            List<BusRestrictGetDTO> BusRestrictDTO = mapper.Map<List<BusRestrictGetDTO>>(BusRestrict);

            return Ok(BusRestrictDTO);
        }

        ///////////////////////////////////////////////////

        [HttpPost]

        public IActionResult add(BusRestrictAddDTO NewRestrict)
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
            if (NewRestrict == null) { return BadRequest(); }
            BusRestrict ExsitRestrict = Unit_Of_Work.busRestrict_Repository.First_Or_Default(c => c.DomainId == NewRestrict.DomainId && c.Name == NewRestrict.Name);
            if (ExsitRestrict != null) { return BadRequest("this Restrict already exist"); }
            BusRestrict busRestrict = mapper.Map<BusRestrict>(NewRestrict);
            busRestrict.InsertedByUserId = userId;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            busRestrict.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            Unit_Of_Work.busRestrict_Repository.Add(busRestrict);
            Unit_Of_Work.SaveChanges();
            return Ok(NewRestrict);

        }

        ////////////////////////////////////////////////////////

        [HttpPut]

        public IActionResult Edit(BusRestrictGetDTO EditBusrestrict)
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
            if (EditBusrestrict == null) { BadRequest(); }
            //BusType busType = mapper.Map<BusType>(EditBusType);
            BusRestrict busRestrict = Unit_Of_Work.busRestrict_Repository.Select_By_Id(EditBusrestrict.ID);
            busRestrict.Name = EditBusrestrict.Name;
            busRestrict.DomainId = EditBusrestrict.DomainId;
            busRestrict.UpdatedByUserId = userId;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            busRestrict.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            Unit_Of_Work.busRestrict_Repository.Update(busRestrict);
            Unit_Of_Work.SaveChanges();
            return Ok(EditBusrestrict);
        }

        ////////////////////////////////////////////////////////

        [HttpDelete]

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
            BusRestrict busRestrict = Unit_Of_Work.busRestrict_Repository.Select_By_Id(id);
            busRestrict.IsDeleted = true;
            busRestrict.DeletedByUserId = userId;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            busRestrict.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            Unit_Of_Work.busRestrict_Repository.Update(busRestrict);
            Unit_Of_Work.SaveChanges();
            return Ok();

        }
    }
}
