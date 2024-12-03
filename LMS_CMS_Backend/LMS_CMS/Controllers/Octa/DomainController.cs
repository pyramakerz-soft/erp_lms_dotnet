using AutoMapper;
using LMS_CMS_BL.DTO.Bus;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Octa;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers.Octa
{
    [Route("api/[controller]")]
    [ApiController]
    public class DomainController : ControllerBase
    {
        private readonly UOW _Unit_Of_Work;
        private readonly DynamicDatabaseService _dynamicDatabaseService;

        public DomainController(DynamicDatabaseService dynamicDatabaseService, UOW Unit_Of_Work)
        {
            _Unit_Of_Work = Unit_Of_Work;
            _dynamicDatabaseService = dynamicDatabaseService;
        }

        [HttpGet]
        public IActionResult Get()
        {

            List<LMS_CMS_DAL.Models.Octa.Domain> Domains = _Unit_Of_Work.domain_Octa_Repository.Select_All_Octa();

            return Ok(Domains);
        }

        [HttpPost]
        public async Task<IActionResult> AddDomain(string domainName)
        {
            if (string.IsNullOrWhiteSpace(domainName))
            {
                return BadRequest("Invalid domain name.");
            }

            var existingDomain = _Unit_Of_Work.domain_Octa_Repository.First_Or_Default_Octa(d => d.Name == domainName);
            if (existingDomain != null)
            {
                return Conflict("Domain already exists.");
            }

            // Use the service to add the domain and setup the corresponding database
            await _dynamicDatabaseService.AddDomainAndSetupDatabase(domainName);

            return Ok(new { message = "Domain and database setup successfully." });
        }
    }

}