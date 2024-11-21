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
            List<BusCategory> BusCategories = Unit_Of_Work.busCategory_Repository.Select_All();
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

        [HttpPost]

        public IActionResult add(BusCatigoryAddDTO NewCategory)
        {
            if (NewCategory == null) { return BadRequest(); }
            BusCategory ExsitCategory = Unit_Of_Work.busCategory_Repository.First_Or_Default(c => c.DomainId == NewCategory.DomainId && c.Name == NewCategory.Name);
            if (ExsitCategory != null) { return BadRequest("this Category already exist"); }
            BusCategory busCategory = mapper.Map<BusCategory>(NewCategory);
            Unit_Of_Work.busCategory_Repository.Add(busCategory);
            Unit_Of_Work.SaveChanges();
            return Ok(NewCategory);

        }

        ////////////////////////////////////////////////////////

        [HttpPut]

        public IActionResult Edit(BusCatigoryGetDTO EditBusCatigory)
        {
            if (EditBusCatigory == null) { BadRequest(); }
            BusCategory BusCatigory = mapper.Map<BusCategory>(EditBusCatigory);
            Unit_Of_Work.busCategory_Repository.Update(BusCatigory);
            Unit_Of_Work.SaveChanges();
            return Ok(EditBusCatigory);
        }

        ////////////////////////////////////////////////////////

        [HttpDelete]

        public IActionResult delete(int id)
        {

            Unit_Of_Work.busCategory_Repository.Delete(id);
            Unit_Of_Work.SaveChanges();
            return Ok();

        }
    }
}
