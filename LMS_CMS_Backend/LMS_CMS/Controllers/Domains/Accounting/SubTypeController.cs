using AutoMapper;
using LMS_CMS_BL.DTO.Accounting;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.AccountingModule;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Controllers.Domains.Accounting
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class SubTypeController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public SubTypeController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Accounting" }
        )]
        public IActionResult GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<SubType> subTypes = Unit_Of_Work.subType_Repository.Select_All();

            if (subTypes == null || subTypes.Count == 0)
            {
                return NotFound();
            }

            List<SubTypeGetDTO> subTypesDTO = mapper.Map<List<SubTypeGetDTO>>(subTypes);

            return Ok(subTypesDTO);
        }
    }
}
