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
            List<BusRestrict> busRestricts = Unit_Of_Work.busRestrict_Repository.Select_All();
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

        [HttpPost]

        public IActionResult add(BusRestrictAddDTO NewRestrict)
        {
            if (NewRestrict == null) { return BadRequest(); }
            BusRestrict busRestrict = mapper.Map<BusRestrict>(NewRestrict);
            Unit_Of_Work.busRestrict_Repository.Add(busRestrict);
            Unit_Of_Work.SaveChanges();
            return Ok(NewRestrict);

        }

        ////////////////////////////////////////////////////////

        [HttpPut]

        public IActionResult Edit(BusRestrictGetDTO EditBusrestrict)
        {
            if (EditBusrestrict == null) { BadRequest(); }
            BusRestrict Busrestrict = mapper.Map<BusRestrict>(EditBusrestrict);
            Unit_Of_Work.busRestrict_Repository.Update(Busrestrict);
            Unit_Of_Work.SaveChanges();
            return Ok(EditBusrestrict);
        }

        ////////////////////////////////////////////////////////

        [HttpDelete]

        public IActionResult delete(long id)
        {

            Unit_Of_Work.busRestrict_Repository.Delete(id);
            Unit_Of_Work.SaveChanges();
            return Ok();

        }
    }
}
