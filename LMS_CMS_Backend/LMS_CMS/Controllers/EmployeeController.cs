using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LMS_CMS_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        UOW unitOfWork;
        private readonly LMS_CMS_Context _context;
        IMapper mapper;


        public EmployeeController(LMS_CMS_Context context, UOW unitOfWork , IMapper mapper)
        {
            _context = context;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
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

        [HttpGet("{empID}")]
        public IActionResult GetById(int empID)
        {
            Employee Employee = unitOfWork.employee_Repository.Select_By_Id(empID);
            if (Employee == null)
            {
                return NotFound();
            }

            return Ok(Employee);
        }

        [HttpGet("Employee_With_Role_Permission/{empID}")]
        public async Task<IActionResult> Employee_With_Role_Permission(int empID)
        {
            var employee = await unitOfWork.employee_Repository.FindByIncludesAsync(
                e => e.ID == empID,
                query => query.Include(e => e.Employee_Roles)
                              .ThenInclude(er => er.Role)
                              .ThenInclude(r => r.Role_Detailed_Permissions)
                              .ThenInclude(rdp => rdp.Detailed_Permissions)
                              .ThenInclude(dp => dp.Master_Permissions)
            );

            if (employee == null)
            {
                return NotFound();
            }

            var employeeDTO = mapper.Map<EmployeeDTO>(employee);
            return Ok(employeeDTO);
        }

    }
}