using AutoMapper;
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

        public EmployeeController(LMS_CMS_Context context, UOW unitOfWork)
        {
            _context = context;
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

        [HttpGet("Employee_With_Role_Permission/{empID}")]
        public async Task<IActionResult> Employee_With_Role_Permission(int empID)
        {
            var employee = await _context.Employees
                .Include(e => e.Employee_Roles)
                    .ThenInclude(er => er.Role)
                        .ThenInclude(r => r.Role_Detailed_Permissions)
                            .ThenInclude(rdp => rdp.Detailed_Permissions)
                                .ThenInclude(dp => dp.Master_Permissions)
                .FirstOrDefaultAsync(e => e.ID == empID);

            if (employee == null)
            {
                return NotFound();
            }

            var result = new
            {
                Employee = new
                {
                    employee.ID,
                    employee.User_Name,
                    employee.Email,
                    Roles = employee.Employee_Roles.Select(er => new
                    {
                        er.Role.ID,
                        er.Role.Name,
                        DetailedPermissions = er.Role.Role_Detailed_Permissions.Select(rdp => new
                        {
                            rdp.Detailed_Permissions.ID,
                            rdp.Detailed_Permissions.Name,
                            MasterPermission = new
                            {
                                rdp.Detailed_Permissions.Master_Permissions.ID,
                                rdp.Detailed_Permissions.Master_Permissions.Name
                            }
                        })
                    })
                }
            };

            return Ok(result);
        }
    }
}