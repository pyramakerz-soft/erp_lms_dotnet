using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models;
using LMS_CMS_PL.Attribute;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PagesController : ControllerBase
    {
        private UOW Unit_Of_Work;
        IMapper mapper;

        public PagesController(UOW Unit_Of_Work, IMapper mapper)
        {
            this.Unit_Of_Work = Unit_Of_Work;
            this.mapper = mapper;
        }

        [HttpGet]
        //[Authorize_Endpoint_Attribute(
        //    allowedTypes: new[] { "employee", "parent" },
        //    allowDelete:1,
        //    pages: new[] { "Settings" }
        //)]
        public IActionResult Get()
        {
            List<Page> pages = Unit_Of_Work.page_Repository.Select_All();
            if (pages == null)
            {
                return NotFound();
            }

            List<Page_AddDTO> pageDTOs = mapper.Map<List<Page_AddDTO>>(pages);

            return Ok(pageDTOs);
        }

        [HttpGet("Get_With_Group_By")]
        public IActionResult Get_With_Group_By()
        {
            List<Page> pages = Unit_Of_Work.page_Repository.Select_All();
            if (pages == null)
            {
                return NotFound();
            }

            List<Page_GetDTO> pageDTOs = mapper.Map<List<Page_GetDTO>>(pages);

            var pageLookup = pageDTOs.ToDictionary(p => p.ID);

            // Build the hierarchy
            foreach (var page in pageDTOs)
            {
                if (page.Page_ID.HasValue && pageLookup.ContainsKey(page.Page_ID.Value))
                {
                    // Add as a child to its parent
                    pageLookup[page.Page_ID.Value].Children.Add(page);
                }
            }

            // Return only top-level pages (those without a PageID)
            var topLevelPages = pageDTOs.Where(p => !p.Page_ID.HasValue).ToList();

            return Ok(topLevelPages);
        }
    }
}
