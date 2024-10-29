using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeRoleController : ControllerBase
    {
        UOW unitOfWork;
        public EmployeeRoleController(UOW unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Employee_Role> Employee_Roles = unitOfWork.employee_Role_Repository.Select_All();
            if (Employee_Roles == null)
            {
                return NotFound();
            }

            return Ok(Employee_Roles);
        }
    }
}
