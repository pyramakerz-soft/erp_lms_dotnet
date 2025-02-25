using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Controllers.Domains
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class PageController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public PageController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        ///////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Role" }
        )]
        public IActionResult Get()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userClaims = HttpContext.User.Claims;
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userTypeClaim == null)
            {
                return Unauthorized("User Type claim not found.");
            }

            List<LMS_CMS_DAL.Models.Domains.Page> pages = Unit_Of_Work.page_Repository.Select_All();
            List<Page_GetDTO> pagesDTO = mapper.Map<List<Page_GetDTO>>(pages);

            var hierarchicalPages = BuildHierarchy(pagesDTO);

            return Ok(hierarchicalPages);
        }

        private List<Page_GetDTO> BuildHierarchy(List<Page_GetDTO> pages)
        {
            var pageLookup = pages.ToDictionary(p => p.ID);

            var rootPages = new List<Page_GetDTO>();

            foreach (var page in pages)
            {
                if (page.Page_ID == null)
                {
                    rootPages.Add(page);
                }
                else if (pageLookup.TryGetValue(page.Page_ID.Value, out var parentPage))
                {
                    parentPage.Children.Add(page);
                }
            }
            return rootPages;
        }

    }

}
