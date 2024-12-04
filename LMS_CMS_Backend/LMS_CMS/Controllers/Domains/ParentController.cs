using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Controllers.Domains
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ParentController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public ParentController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }


        [HttpGet("{Id}")]
        public IActionResult GetByID(long Id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            Parent parent = Unit_Of_Work.parent_Repository.Select_By_Id(Id);

            if (parent == null || parent.IsDeleted == true)
            {
                return NotFound("No employee found");
            }

            ParentGetDTO employeeDTO = mapper.Map<ParentGetDTO>(parent);

            return Ok(employeeDTO);
        }
    }
}
