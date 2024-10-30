using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        UOW unitOfWork;
        public RoleController(UOW unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Role> roles = unitOfWork.role_Repository.Select_All();
            if (roles == null)
            {
                return NotFound();
            }

            return Ok(roles);
        }
    }
}
