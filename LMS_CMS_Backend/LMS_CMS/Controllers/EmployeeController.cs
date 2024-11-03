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
        //    var employee = await unitOfWork.employee_Repository.FindByIncludesAsync(
        //        e => e.ID == empID,
        //        query => query.Include(e => e.Employee_Roles)
        //                      .ThenInclude(er => er.Role)
        //                      .ThenInclude(r => r.Role_Detailed_Permissions)
        //                      .ThenInclude(rdp => rdp.Detailed_Permissions)
        //                      .ThenInclude(dp => dp.Master_Permissions)
        //    );

        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    var employeeDTO = mapper.Map<EmployeeDTO>(employee);
        //    return Ok(employeeDTO);
        //}

        [HttpGet("Employee_With_Role_Permission/{empID}")]
        public async Task<IActionResult> Employee_With_Role_Permission(int empID)
        {
            var employee = await _context.Employees
                .Include(e => e.Employee_Roles)
                    .ThenInclude(er => er.Role)
                        .ThenInclude(r => r.Role_Permissions)
                            .ThenInclude(rp => rp.Master_Detailes_Permissions)
                                .ThenInclude(mdp => mdp.Master_Permission)
                                    .ThenInclude(mp => mp.Modules_Master_permissions)
                                        .ThenInclude(mmp => mmp.Module)
                .Include(e => e.Employee_Roles)
                    .ThenInclude(er => er.Role)
                        .ThenInclude(r => r.Role_Permissions)
                            .ThenInclude(rp => rp.Master_Detailes_Permissions)
                                .ThenInclude(mdp => mdp.Detailed_Permission)
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
                        Permissions = er.Role.Role_Permissions.Select(rp => new
                        {
                            rp.Master_Detailes_Permissions.ID,
                            MasterPermission = new
                            {
                                rp.Master_Detailes_Permissions.Master_Permission.ID,
                                rp.Master_Detailes_Permissions.Master_Permission.Name,
                                Modules = rp.Master_Detailes_Permissions.Master_Permission.Modules_Master_permissions.Select(mmp => new
                                {
                                    mmp.Module.ID,
                                    mmp.Module.Name
                                })
                            }
                        }),
                        DetailedPermissions = er.Role.Role_Permissions.Select(rp => new
                        {
                            rp.Master_Detailes_Permissions.Detailed_Permission.ID,
                            rp.Master_Detailes_Permissions.Detailed_Permission.Name,
                            MasterPermission = new
                            {
                                rp.Master_Detailes_Permissions.Master_Permission.ID,
                                rp.Master_Detailes_Permissions.Master_Permission.Name
                            }
                        }).Distinct()
                    })
                }
            };

            return Ok(result);
        }

    }
}