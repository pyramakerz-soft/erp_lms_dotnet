using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.UOW;
using LMS_CMS_PL.Attribute;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Controllers.Bus
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusController : ControllerBase
    {
        private UOW Unit_Of_Work;
        IMapper mapper;

        public BusController(UOW Unit_Of_Work, IMapper mapper)
        {
            this.Unit_Of_Work = Unit_Of_Work;
            this.mapper = mapper;
        }

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    List<Bus> pages = Unit_Of_Work.busre.Select_All();
        //    if (pages == null)
        //    {
        //        return NotFound();
        //    }

        //    List<Page_AddDTO> pageDTOs = mapper.Map<List<Page_AddDTO>>(pages);

        //    return Ok(pageDTOs);
        //}
    }
}
