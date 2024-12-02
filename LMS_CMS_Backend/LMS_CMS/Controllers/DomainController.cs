using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.DTO.Bus;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DomainController : ControllerBase
    {
        private UOW Unit_Of_Work;
        IMapper mapper;

        public DomainController(UOW Unit_Of_Work, IMapper mapper)
        {
            this.Unit_Of_Work = Unit_Of_Work;
            this.mapper = mapper;
        }


        ///////////////////////////////////////////

        [HttpGet]
        public IActionResult Get()
        {
            List<Domain> Domains = Unit_Of_Work.domain_Repository.FindBy(t => t.IsDeleted != true);
            if (Domains == null)
            {
                return NotFound();
            }

            List<DomainGetDTO> DomainDTO = mapper.Map<List<DomainGetDTO>>(Domains);

            return Ok(DomainDTO);
        }

    }
}