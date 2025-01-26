using AutoMapper;
using LMS_CMS_BL.DTO.Octa;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.AccountingModule;
using LMS_CMS_DAL.Models.Octa;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Controllers.Octa
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CountryController : ControllerBase
    {
        private readonly UOW _Unit_Of_Work;
        private readonly DynamicDatabaseService _dynamicDatabaseService;
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public CountryController(DynamicDatabaseService dynamicDatabaseService, UOW Unit_Of_Work, DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _Unit_Of_Work = Unit_Of_Work;
            _dynamicDatabaseService = dynamicDatabaseService;
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {

            List<Country> countries = _Unit_Of_Work.country_Repository.Select_All_Octa();
           List< CountriesGetDTO> DTOS = mapper.Map<List<CountriesGetDTO>>(countries);

            return Ok(DTOS);
        }
    }
}
