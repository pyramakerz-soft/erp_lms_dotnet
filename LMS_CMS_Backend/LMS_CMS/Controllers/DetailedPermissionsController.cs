using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailedPermissionsController : ControllerBase
    {
        UOW unitOfWork;
        public DetailedPermissionsController(UOW unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Detailed_Permissions> Detailed_Permissions = unitOfWork.detailed_Permissions_Repository.Select_All();
            if (Detailed_Permissions == null)
            {
                return NotFound();
            }

            return Ok(Detailed_Permissions);
        }
    }
}
