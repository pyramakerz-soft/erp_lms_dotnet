using AutoMapper;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        UOW unitOfWork;
        public EmployeeController(UOW unitOfWork )
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Employee> Employees = unitOfWork.employee_Repository.Select_All();
            if (Employees == null)
            {
                return NotFound();
            }

            return Ok(Employees);
        }


    }
}
