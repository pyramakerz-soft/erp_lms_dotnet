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
    public class BusStatusController : ControllerBase
    {
        private UOW Unit_Of_Work;
        IMapper mapper;

        public BusStatusController(UOW Unit_Of_Work, IMapper mapper)
        {
            this.Unit_Of_Work = Unit_Of_Work;
            this.mapper = mapper;
        }


        ///////////////////////////////////////////

        [HttpGet]
        public IActionResult Get()
        {
            List<BusStatus> BusStatus = Unit_Of_Work.busStatus_Repository.Select_All();
            if (BusStatus == null)
            {
                return NotFound();
            }

            List<BusStatusGetDTO> BusStatusDTO = mapper.Map<List<BusStatusGetDTO>>(BusStatus);

            return Ok(BusStatusDTO);
        }

        ///////////////////////////////////////////////////

        [HttpGet("id")]
        public IActionResult GetById(long id)
        {
            BusStatus busStatus = Unit_Of_Work.busStatus_Repository.Select_By_Id(id);
            if (busStatus == null) return NotFound();

            BusStatusGetDTO StatusDTO = mapper.Map<BusStatusGetDTO>(busStatus);
            return Ok(StatusDTO);
        }
        ///////////////////////////////////////////////////

        [HttpGet("DomainId")]
        public IActionResult GetByDomainId(long id)
        {
            List<BusStatus> BusStatus = Unit_Of_Work.busStatus_Repository.FindBy(s => s.DomainId == id);
            if (BusStatus == null)
            {
                return NotFound();
            }

            List<BusStatusGetDTO> BusStatusDTO = mapper.Map<List<BusStatusGetDTO>>(BusStatus);

            return Ok(BusStatusDTO);
        }
        ///////////////////////////////////////////////////

        [HttpPost]

        public IActionResult add(BusStatusAddDTO NewBus)
        {
            if (NewBus == null) { return BadRequest(); }
            BusStatus ExsitStatus = Unit_Of_Work.busStatus_Repository.First_Or_Default(c => c.DomainId == NewBus.DomainId && c.Name == NewBus.Name);
            if (ExsitStatus != null) { return BadRequest("this Status already exist"); }
            BusStatus bustStatus = mapper.Map<BusStatus>(NewBus);
            Unit_Of_Work.busStatus_Repository.Add(bustStatus);
            Unit_Of_Work.SaveChanges();
            return Ok(NewBus);

        }

        ////////////////////////////////////////////////////////

        [HttpPut]

        public IActionResult Edit(BusStatusGetDTO EditBusStatus)
        {
            if (EditBusStatus == null) { BadRequest(); }
            BusStatus busStatus = mapper.Map<BusStatus>(EditBusStatus);
            Unit_Of_Work.busStatus_Repository.Update(busStatus);
            Unit_Of_Work.SaveChanges();
            return Ok(EditBusStatus);
        }

        ////////////////////////////////////////////////////////

        [HttpDelete]

        public IActionResult delete(long id)
        {

            Unit_Of_Work.busStatus_Repository.Delete(id);
            Unit_Of_Work.SaveChanges();
            return Ok();

        }

    }
}
