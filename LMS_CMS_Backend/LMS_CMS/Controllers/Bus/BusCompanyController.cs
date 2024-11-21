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
            List<BusCompany> BusCompany = Unit_Of_Work.busCompany_Repository.Select_All();
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
            if (busCompany == null) return NotFound();

            BusCompanyGetDTO CompanyDTO = mapper.Map<BusCompanyGetDTO>(busCompany);
            return Ok(CompanyDTO);
        }

        ///////////////////////////////////////////////////

        [HttpPost]

        public IActionResult add(BusCompanyAddDTO NewBusCompany)
        {

            if (NewBusCompany == null) { return BadRequest(); }
            BusCompany ExsitCompany = Unit_Of_Work.busCompany_Repository.First_Or_Default(c=>c.DomainId== NewBusCompany.DomainId && c.Name==NewBusCompany.Name );
            if (ExsitCompany!=null) { return BadRequest("this company already exist"); }
            BusCompany busCompany = mapper.Map<BusCompany>(NewBusCompany);
            Unit_Of_Work.busCompany_Repository.Add(busCompany);
            Unit_Of_Work.SaveChanges();
            return Ok(NewBusCompany);

        }

        ////////////////////////////////////////////////////////

        [HttpPut]

        public IActionResult Edit(BusCompanyGetDTO EditBusCompany)
        {
            if (EditBusCompany == null) { BadRequest(); }
            BusCompany busCompany = mapper.Map<BusCompany>(EditBusCompany);
            Unit_Of_Work.busCompany_Repository.Update(busCompany);
            Unit_Of_Work.SaveChanges();
            return Ok(EditBusCompany);
        }

        ////////////////////////////////////////////////////////

        [HttpDelete]

        public IActionResult delete(long id)
        {

            Unit_Of_Work.busCompany_Repository.Delete(id);
            Unit_Of_Work.SaveChanges();
            return Ok();

        }

    }
}
