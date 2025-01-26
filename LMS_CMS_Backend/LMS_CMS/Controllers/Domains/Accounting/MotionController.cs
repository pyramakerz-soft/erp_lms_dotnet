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
    public class MotionController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public MotionController(DbContextFactoryService dbContextFactory, IMapper mapper)
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

            List<MotionType> motionTypes = Unit_Of_Work.motionType_Repository.Select_All();

            if (motionTypes == null || motionTypes.Count == 0)
            {
                return NotFound();
            }

            List<MotionTypeGetDTO1> motionTypesDTO = mapper.Map<List<MotionTypeGetDTO1>>(motionTypes);

            return Ok(motionTypesDTO);
        }
    }
}
