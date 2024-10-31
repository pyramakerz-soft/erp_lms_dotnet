using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentController : ControllerBase
    {
        UOW unitOfWork;
        public ParentController(UOW unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Parent> parents = unitOfWork.parent_Repository.Select_All();
            if (parents == null)
            {
                return NotFound();
            }

            return Ok(parents);
        }

        [HttpGet("{parID}")]
        public IActionResult GetById(int parID)
        {
            Parent parent = unitOfWork.parent_Repository.Select_By_Id(parID);
            if (parent == null)
            {
                return NotFound();
            }

            return Ok(parent);
        }
    }
}
