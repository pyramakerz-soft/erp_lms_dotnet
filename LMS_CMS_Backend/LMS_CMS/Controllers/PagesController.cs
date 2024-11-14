using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagesController : ControllerBase
    {
        private UOW Unit_Of_Work;
        private readonly IConfiguration _configuration;
        IMapper mapper;


        public PagesController(UOW Unit_Of_Work, IConfiguration configuration, IMapper mapper)
        {
            this.Unit_Of_Work = Unit_Of_Work;
            _configuration = configuration;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Page> pages = Unit_Of_Work.page_Repository.Select_All();
            if (pages == null)
            {
                return NotFound();
            }

            PageDTO BreedDTO = mapper.Map<PageDTO>(pages);  

            return Ok(BreedDTO);
        }

        [HttpPost]

        public IActionResult addPage(PageDTO newPage)
        {
            if (newPage == null) { return BadRequest(); }
            Page page = mapper.Map<Page>(newPage);
            Unit_Of_Work.page_Repository.Add(page);
            Unit_Of_Work.SaveChanges();
            return Ok(newPage);

        }
    }
}
