using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.UOW;
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

        //[HttpGet("Employee_With_Role_Permission/{empID}")]
        //public async Task<IActionResult> Employee_With_Role_Permission(int empID)
        //{
        //    var employee = await _context.Employees
        //        .Include(e => e.Employee_Roles)
        //            .ThenInclude(er => er.Role)
        //                .ThenInclude(r => r.Role_Permissions)
        //                    .ThenInclude(rdp => rdp.Master_Detailes_Permissions)
        //                        .ThenInclude(dp => dp.Master_Permission)
        //                            .ThenInclude(d => d.Modules_Master_permissions)
        //                              .ThenInclude(p => p.Module)



        //        .FirstOrDefaultAsync(e => e.ID == empID);

        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    var result = new
        //    {
        //        Employee = new
        //        {
        //            employee.ID,
        //            employee.User_Name,
        //            employee.Email,
        //            Roles = employee.Employee_Roles.Select(er => new
        //            {
        //                er.Role.ID,
        //                er.Role.Name,
        //                DetailedPermissions = er.Role.Role_Detailed_Permissions.Select(rdp => new
        //                {
        //                    rdp.Detailed_Permissions.ID,
        //                    rdp.Detailed_Permissions.Name,
        //                    MasterPermission = new
        //                    {
        //                        rdp.Detailed_Permissions.Master_Permissions.ID,
        //                        rdp.Detailed_Permissions.Master_Permissions.Name
        //                    }
        //                })
        //            })
        //        }
        //    };

        //    return Ok(result);
        //}

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



    }
}