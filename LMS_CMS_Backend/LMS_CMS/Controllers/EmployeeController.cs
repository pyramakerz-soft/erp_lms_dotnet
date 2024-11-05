using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Migrations;
using LMS_CMS_DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace LMS_CMS_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        UOW unitOfWork;
        IMapper mapper;
        private readonly LMS_CMS_Context _context;
        public EmployeeController(LMS_CMS_Context context, UOW unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this._context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Employee> employees = unitOfWork.employee_Repository.Select_All_With_Includes(s => s.School, d => d.School.Domain);
            if (employees == null)
            {
                return NotFound();
            }
            List<Employee_GetDTO> employeeDTOs = mapper.Map<List<Employee_GetDTO>>(employees);

            return Ok(employeeDTOs);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get_By_Id(int Id)
        {
            if (Id == 0)
            {
                return BadRequest("Employee ID cannot be null.");
            }

            var employee = await unitOfWork.employee_Repository.FindByIncludesAsync(
                emp => emp.ID == Id,
                query => query.Include(e => e.School),
                query => query.Include(e => e.School.Domain)
            );

            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                Employee_GetDTO employeeDTO = mapper.Map<Employee_GetDTO>(employee);
                return Ok(employeeDTO);
            }
        }

        [HttpGet("Employees_In_School/{Id}")]
        public IActionResult Get_By_School_Id(int Id)
        {
            if (Id == 0)
            {
                return BadRequest("School ID cannot be null.");
            }

            var employees = unitOfWork.employee_Repository.FindBy(emp => emp.School_id == Id );

            if (employees == null || employees.Count == 0)
            {
                return NotFound();
            }
            else
            {
                var employeeDTOs = mapper.Map<List<Employee_GetDTO>>(employees);

                return Ok(employeeDTOs);
            }
        }

        [HttpGet("Employee_With_Role_Permission/{empID}")]
        public async Task<IActionResult> Employee_With_Role_Permission(int empID)
        {
            var employeePermissions = await _context.Employee_With_Role_Permission_View
                .Where(ep => ep.EmployeeID == empID)
                .Select(ep => new
                {
                    ep.EmployeeID,
                    ep.User_Name,
                    ep.Email,
                    ep.RoleID,
                    ep.RoleName,
                    ep.ModuleID,
                    ep.ModuleName,
                    ep.MasterPermissionID,
                    ep.MasterPermissionName,
                    ep.DetailedPermissionID,
                    ep.DetailedPermissionName
                })
                .ToListAsync();
            if (!employeePermissions.Any())
            {
                return NotFound();
            }
            var groupedResult = employeePermissions
                .GroupBy(ep => new
                {
                    ep.EmployeeID,
                    ep.User_Name,
                    ep.Email
                })
                .Select(g => new
                {
                    Employee = new
                    {
                        g.Key.EmployeeID,
                        g.Key.User_Name,
                        g.Key.Email,
                        Roles = g.GroupBy(ep => new { ep.RoleID, ep.RoleName })
                                  .Select(r => new
                                  {
                                      r.Key.RoleID,
                                      r.Key.RoleName,
                                      Modules = r.GroupBy(ep => new { ep.ModuleID, ep.ModuleName })
                                                 .Select(m => new
                                                 {
                                                     m.Key.ModuleID,
                                                     m.Key.ModuleName,
                                                     MasterPermissions = m.GroupBy(ep => new { ep.MasterPermissionID, ep.MasterPermissionName })
                                                                          .Select(mp => new
                                                                          {
                                                                              mp.Key.MasterPermissionID,
                                                                              mp.Key.MasterPermissionName,
                                                                              DetailedPermissions = mp.Select(dp => new
                                                                              {
                                                                                  dp.DetailedPermissionID,
                                                                                  dp.DetailedPermissionName
                                                                              }).ToList()
                                                                          }).ToList()
                                                 }).ToList()
                                  }).ToList()
                    }
                }).ToList();
            return Ok(groupedResult);
        }

        [HttpPost]
        public ActionResult Add(Employee_AddDTO employeeDTO)
        {
            if (employeeDTO == null)
            {
                return BadRequest("Employee cannot be null.");
            }

            School school = unitOfWork.school_Repository.Select_By_Id(employeeDTO.School_id);
            if (school == null)
            {
                return NotFound("No School with this ID");
            }
            Employee employee = mapper.Map<Employee>(employeeDTO);
            unitOfWork.employee_Repository.Add(employee);
            unitOfWork.SaveChanges();

            return CreatedAtAction(nameof(Get_By_Id), new { Id = employee.ID }, employeeDTO);
        }

        [HttpPut]
        public ActionResult Edit(Employee_PutDTO employeeDTO)
        {
            if (employeeDTO == null)
            {
                return BadRequest("Employee cannot be null.");
            }

            Employee existingEmployee = unitOfWork.employee_Repository.Select_By_Id(employeeDTO.ID);
            if (existingEmployee == null)
            {
                return NotFound();
            }
            else
            {
                School school = unitOfWork.school_Repository.Select_By_Id(employeeDTO.School_id);
                if (school == null)
                {
                    return NotFound("No School with this ID");
                }
                mapper.Map(employeeDTO, existingEmployee);
                unitOfWork.employee_Repository.Update(existingEmployee);
                unitOfWork.SaveChanges();

                return Ok(employeeDTO);
            }
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            if (Id == 0)
            {
                return BadRequest("Employee ID cannot be null.");
            }

            Employee employee = unitOfWork.employee_Repository.Select_By_Id(Id);
            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                unitOfWork.employee_Repository.Delete(Id);
                unitOfWork.SaveChanges();
                return Ok("Employee has Successfully been deleted");
            }
        }
    }
}







