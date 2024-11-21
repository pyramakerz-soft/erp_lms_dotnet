using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.DTO.Bus;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.BusModule;
using LMS_CMS_PL.Attribute;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Get()
        {
            List<BusType> BusType = Unit_Of_Work.busType_Repository.Select_All();
            if (BusType == null)
            {
                return NotFound();
            }

            List<BusTypeGetDTO> BusTypeDTO = mapper.Map<List<BusTypeGetDTO>>(BusType);

            return Ok(BusTypeDTO);
        }

        ///////////////////////////////////////////////////

        [HttpGet("id")]
        public IActionResult GetById(long id)
        {
            BusType busType = Unit_Of_Work.busType_Repository.Select_By_Id(id);
            if (busType == null) return NotFound();

            BusTypeGetDTO typeDTO = mapper.Map<BusTypeGetDTO>(busType);
            return Ok(typeDTO);
        }

        ///////////////////////////////////////////////////

        [HttpGet("DomainId")]
        public IActionResult GetByDomainId(long id)
        {
            List<BusType> BusType = Unit_Of_Work.busType_Repository.FindBy(s=>s.DomainId==id);
            if (BusType == null)
            {
                return NotFound();
            }

            List<BusTypeGetDTO> BusTypeDTO = mapper.Map<List<BusTypeGetDTO>>(BusType);

            return Ok(BusTypeDTO);
        }

        ///////////////////////////////////////////////////

        [HttpPost]

        public IActionResult add(BusTypeAddDTO NewBus)
        {
            if (NewBus == null) { return BadRequest(); }
            BusType ExsitType = Unit_Of_Work.busType_Repository.First_Or_Default(c => c.DomainId == NewBus.DomainId && c.Name == NewBus.Name);
            if (ExsitType != null) { return BadRequest("this Type already exist"); }
            BusType bustType = mapper.Map<BusType>(NewBus);
            Unit_Of_Work.busType_Repository.Add(bustType);
            Unit_Of_Work.SaveChanges();
            return Ok(NewBus);

        }

        ////////////////////////////////////////////////////////
        
        [HttpPut]

        public IActionResult Edit(BusTypeGetDTO EditBusType)
        {
            if (EditBusType == null) { BadRequest(); }
            BusType busType = mapper.Map<BusType>(EditBusType);
            Unit_Of_Work.busType_Repository.Update(busType);
            Unit_Of_Work.SaveChanges();
            return Ok(EditBusType);
        }

        ////////////////////////////////////////////////////////

        [HttpDelete]

        public IActionResult delete(long id)
        {

            Unit_Of_Work.busType_Repository.Delete(id);
            Unit_Of_Work.SaveChanges();
            return Ok();

        }

    }
}
