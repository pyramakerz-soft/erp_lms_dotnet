using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentController : ControllerBase
    {
        private UOW Unit_Of_Work;
        IMapper mapper;

        public ParentController(UOW Unit_Of_Work, IMapper mapper)
        {
            this.Unit_Of_Work = Unit_Of_Work;
            this.mapper = mapper;
        }


        [HttpGet("{Id}")]
        public IActionResult GetByID(long Id)
        {
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
