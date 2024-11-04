using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Migrations;
using LMS_CMS_DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DomainController : ControllerBase
    {

        UOW unitOfWork;
        private readonly IMapper _mapper;
        public DomainController(UOW unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //----------Get Domains ----------//

        [HttpGet]
        public IActionResult Get()
        {
            List<Domain> domains = unitOfWork.domain_Repository.Select_All_With_Includes(d => d.Schools);

            if (domains == null || !domains.Any())
            {
                return NotFound();
            }
            List<DomainDTO> domainDtos = _mapper.Map<List<DomainDTO>>(domains);

            return Ok(domainDtos);
        }

        //----------Get Domain by id----------//
        [HttpGet("{domainId}")]
        public async Task<IActionResult> GetById(int domainId)
        {
            Domain domain = await unitOfWork.domain_Repository.FindByIncludesAsync(
                e => e.ID == domainId,
                query => query.Include(e => e.Schools)); 

            if (domain == null)
            {
                return NotFound();
            }

            DomainDTO domainDto = _mapper.Map<DomainDTO>(domain);


            return Ok(domainDto);
        }
        //----------add Domain by id----------//
        [HttpPost]
        public IActionResult addDomain(DomainAddDTO newDomain)
        {
            if (newDomain == null) { return BadRequest(); }
            Domain domain = _mapper.Map<Domain>(newDomain);
            unitOfWork.domain_Repository.Add(domain);
            unitOfWork.SaveChanges();
            return Ok(newDomain);
        }

        ////----------Update Domain----------//

        [HttpPut]

        public IActionResult EditBreed(DomainUpdateDTO newDomian)
        {
            if (newDomian == null) { BadRequest(); }
            Domain domain = _mapper.Map<Domain>(newDomian);
            unitOfWork.domain_Repository.Update(domain);
            unitOfWork.SaveChanges();
            return Ok(newDomian);
        }

        ////----------Delete Domain----------//

        [HttpDelete]

        public IActionResult deletDomain(int id)
        {
            unitOfWork.domain_Repository.Delete(id);
            unitOfWork.SaveChanges();
            return Ok();

        }

      }
}
