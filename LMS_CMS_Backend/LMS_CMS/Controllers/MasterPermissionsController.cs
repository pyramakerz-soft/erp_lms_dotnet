using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterPermissionsController : ControllerBase
    {
        UOW unitOfWork;
        public MasterPermissionsController(UOW unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Master_Permissions> Master_Permissions = unitOfWork.master_Permissions_Repository.Select_All();
            if (Master_Permissions == null)
            {
                return NotFound();
            }

            return Ok(Master_Permissions);
        }
    }
}
