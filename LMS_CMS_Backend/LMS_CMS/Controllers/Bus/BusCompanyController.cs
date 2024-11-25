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
    public class BusCompanyController : ControllerBase
    {
        private UOW Unit_Of_Work;
        IMapper mapper;

        public BusCompanyController(UOW Unit_Of_Work, IMapper mapper)
        {
            this.Unit_Of_Work = Unit_Of_Work;
            this.mapper = mapper;
        }


        ///////////////////////////////////////////

        [HttpGet]
        public IActionResult Get()
        {
            List<BusCompany> BusCompany = Unit_Of_Work.busCompany_Repository.FindBy(t => t.IsDeleted != true);
            if (BusCompany == null)
            {
                return NotFound();
            }

            List<BusCompanyGetDTO> BusCompanyDTO = mapper.Map<List<BusCompanyGetDTO>>(BusCompany);

            return Ok(BusCompanyDTO);
        }

        ///////////////////////////////////////////////////

        [HttpGet("id")]
        public IActionResult GetById(long id)
        {
            BusCompany busCompany = Unit_Of_Work.busCompany_Repository.Select_By_Id(id);
            if (busCompany == null||busCompany.IsDeleted == true) return NotFound();

            BusCompanyGetDTO CompanyDTO = mapper.Map<BusCompanyGetDTO>(busCompany);
            return Ok(CompanyDTO);
        }
        ///////////////////////////////////////////////////

        [HttpGet("DomainId")]
        public IActionResult GetByDomainId(long id)
        {
            List<BusCompany> BusCompany = Unit_Of_Work.busCompany_Repository.FindBy(s => s.DomainId == id && s.IsDeleted != true);
            if (BusCompany == null)
            {
                return NotFound();
            }

            List<BusCompanyGetDTO> BusCompanyDTO = mapper.Map<List<BusCompanyGetDTO>>(BusCompany);

            return Ok(BusCompanyDTO);
        }
        ///////////////////////////////////////////////////

        [HttpPost]

        public IActionResult add(BusCompanyAddDTO NewBusCompany)
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
            if (NewBusCompany == null) { return BadRequest(); }
            BusCompany ExsitCompany = Unit_Of_Work.busCompany_Repository.First_Or_Default(c=>c.DomainId== NewBusCompany.DomainId && c.Name==NewBusCompany.Name );
            if (ExsitCompany!=null) { return BadRequest("this company already exist"); }
            BusCompany busCompany = mapper.Map<BusCompany>(NewBusCompany);
            busCompany.InsertedByUserId = userId;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            busCompany.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            Unit_Of_Work.busCompany_Repository.Add(busCompany);
            Unit_Of_Work.SaveChanges();
            return Ok(NewBusCompany);

        }

        ////////////////////////////////////////////////////////

        [HttpPut]

        public IActionResult Edit(BusCompanyGetDTO EditBusCompany)
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
            if (EditBusCompany == null) { BadRequest(); }
            //BusType busType = mapper.Map<BusType>(EditBusType);
            BusCompany busCompany = Unit_Of_Work.busCompany_Repository.Select_By_Id(EditBusCompany.ID);
            busCompany.Name = EditBusCompany.Name;
            busCompany.DomainId = EditBusCompany.DomainId;
            busCompany.UpdatedByUserId = userId;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            busCompany.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            Unit_Of_Work.busCompany_Repository.Update(busCompany);
            Unit_Of_Work.SaveChanges();
            return Ok(EditBusCompany);
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
            BusCompany busCompany = Unit_Of_Work.busCompany_Repository.Select_By_Id(id);
            busCompany.IsDeleted = true;
            busCompany.DeletedByUserId = userId;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            busCompany.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            Unit_Of_Work.busCompany_Repository.Update(busCompany);
            Unit_Of_Work.SaveChanges();
            return Ok();

        }

    }
}
