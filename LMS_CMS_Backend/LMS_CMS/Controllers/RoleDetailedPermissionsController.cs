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
        //UOW unitOfWork;
        //public RoleDetailedPermissionsController(UOW unitOfWork)
        //{
        //    this.unitOfWork = unitOfWork;
        //}

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    List<RoleDetailedPermissionsController> RoleDetailedPermissionsController = unitOfWork.role_Detailed_Permissions_Repository.Select_All();
        //    if (RoleDetailedPermissionsController == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(RoleDetailedPermissionsController);
        //}
    }
}
