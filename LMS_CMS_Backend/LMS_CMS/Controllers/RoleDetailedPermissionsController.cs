using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleDetailedPermissionsController : ControllerBase
    {
        UOW unitOfWork;
        public RoleDetailedPermissionsController(UOW unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Role_Detailed_Permissions> Role_Detailed_Permissions = unitOfWork.role_Detailed_Permissions_Repository.Select_All();
            if (Role_Detailed_Permissions == null)
            {
                return NotFound();
            }

            return Ok(Role_Detailed_Permissions);
        }
    }
}
