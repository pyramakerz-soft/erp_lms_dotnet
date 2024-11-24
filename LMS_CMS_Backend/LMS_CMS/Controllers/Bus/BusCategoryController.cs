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
    public class BusCategoryController : ControllerBase
    {
        private UOW Unit_Of_Work;
        IMapper mapper;

        public BusCategoryController(UOW Unit_Of_Work, IMapper mapper)
        {
            this.Unit_Of_Work = Unit_Of_Work;
            this.mapper = mapper;
        }


        ///////////////////////////////////////////

        [HttpGet]
        public IActionResult Get()
        {
            List<BusCategory> BusCategories = Unit_Of_Work.busCategory_Repository.FindBy(t => t.IsDeleted != true);
            if (BusCategories == null)
            {
                return NotFound();
            }

            List<BusCatigoryGetDTO> BusCatigoryDTO = mapper.Map<List<BusCatigoryGetDTO>>(BusCategories);

            return Ok(BusCatigoryDTO);
        }

        ///////////////////////////////////////////////////

        [HttpGet("id")]
        public IActionResult GetById(long id)
        {
            BusCategory busCategory = Unit_Of_Work.busCategory_Repository.Select_By_Id(id);
            if (busCategory == null) return NotFound();

            BusCatigoryGetDTO busCategoryDto = mapper.Map<BusCatigoryGetDTO>(busCategory);
            return Ok(busCategoryDto);
        }
        ///////////////////////////////////////////////////

        [HttpGet("DomainId")]
        public IActionResult GetByDomainId(long id)
        {
            List<BusCategory> BusCategory = Unit_Of_Work.busCategory_Repository.FindBy(s => s.DomainId == id);
            if (BusCategory == null)
            {
                return NotFound();
            }

            List<BusCatigoryGetDTO> BusCategoryDTO = mapper.Map<List<BusCatigoryGetDTO>>(BusCategory);

            return Ok(BusCategoryDTO);
        }
        ///////////////////////////////////////////////////

        [HttpPost]

        public IActionResult add(BusCatigoryAddDTO NewCategory)
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
            if (NewCategory == null) { return BadRequest(); }
            BusCategory ExsitCategory = Unit_Of_Work.busCategory_Repository.First_Or_Default(c => c.DomainId == NewCategory.DomainId && c.Name == NewCategory.Name);
            if (ExsitCategory != null) { return BadRequest("this Category already exist"); }
            BusCategory busCategory = mapper.Map<BusCategory>(NewCategory);
            busCategory.InsertedByUserId = userId;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            busCategory.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            Unit_Of_Work.busCategory_Repository.Add(busCategory);
            Unit_Of_Work.SaveChanges();
            return Ok(NewCategory);

        }

        ////////////////////////////////////////////////////////

        [HttpPut]

        public IActionResult Edit(BusCatigoryGetDTO EditBusCatigory)
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
            if (EditBusCatigory == null) { BadRequest(); }
            //BusType busType = mapper.Map<BusType>(EditBusType);
            BusCategory busCatigory = Unit_Of_Work.busCategory_Repository.Select_By_Id(EditBusCatigory.ID);
            busCatigory.Name = EditBusCatigory.Name;
            busCatigory.DomainId = EditBusCatigory.DomainId;
            busCatigory.UpdatedByUserId = userId;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            busCatigory.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            Unit_Of_Work.busCategory_Repository.Update(busCatigory);
            Unit_Of_Work.SaveChanges();
            return Ok(EditBusCatigory);
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
            BusCategory busCategory = Unit_Of_Work.busCategory_Repository.Select_By_Id(id);
            busCategory.IsDeleted = true;
            busCategory.DeletedByUserId = userId;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            busCategory.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            Unit_Of_Work.busCategory_Repository.Update(busCategory);
            Unit_Of_Work.SaveChanges();
            return Ok();

        }
    }
}
