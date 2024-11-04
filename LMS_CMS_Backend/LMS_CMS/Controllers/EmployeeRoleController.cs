using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeRoleController : ControllerBase
    {
        UOW unitOfWork;
        IMapper mapper;

        public EmployeeRoleController(UOW unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Employee_Role> Employee_Roles = unitOfWork.employee_Role_Repository.Select_All_With_Includes(emp => emp.Employee, r => r.Role); ;
            if (Employee_Roles == null)
            {
                return NotFound();
            }

            List<Employee_Role_GetDTO> employee_RolesDTOs = mapper.Map<List<Employee_Role_GetDTO>>(Employee_Roles);

            return Ok(employee_RolesDTOs);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get_By_Id(int Id)
        {
            if (Id == 0)
            {
                return BadRequest("Employee Role ID cannot be null.");
            }

            var employee_Role = await unitOfWork.employee_Role_Repository.FindByIncludesAsync(
                empRole => empRole.Id == Id,
                query => query.Include(e => e.Employee),
                query => query.Include(e => e.Role)
            );

            if (employee_Role == null)
            {
                return NotFound();
            }
            else
            {
                Employee_Role_GetDTO employee_RoleDTO = mapper.Map<Employee_Role_GetDTO>(employee_Role);
                return Ok(employee_RoleDTO);
            }
        }

        [HttpPost]
        public ActionResult Add(Employee_Role_AddDTO employee_RoleDTO)
        {
            if (employee_RoleDTO == null)
            {
                return BadRequest("Employee Role cannot be null.");
            }

            Employee employee = unitOfWork.employee_Repository.Select_By_Id(employee_RoleDTO.Employee_ID);
            if (employee == null)
            {
                return NotFound("No Employee with this ID");
            }

            Role role = unitOfWork.role_Repository.Select_By_Id(employee_RoleDTO.Role_ID);
            if (role == null)
            {
                return NotFound("No Role with this ID");
            }

            Employee_Role employeeRole = mapper.Map<Employee_Role>(employee_RoleDTO);
            unitOfWork.employee_Role_Repository.Add(employeeRole);
            unitOfWork.SaveChanges();

            return CreatedAtAction(nameof(Get_By_Id), new { id = employeeRole.Id }, employee_RoleDTO);
        }

        [HttpPut]
        public ActionResult Edit(Employee_Role_PutDTO employee_RoleDTO)
        {
            if (employee_RoleDTO == null)
            {
                return BadRequest("Employee Role cannot be null.");
            }

            Employee employee = unitOfWork.employee_Repository.Select_By_Id(employee_RoleDTO.Employee_ID);
            if (employee == null)
            {
                return NotFound("No Employee with this ID");
            }

            Role role = unitOfWork.role_Repository.Select_By_Id(employee_RoleDTO.Role_ID);
            if (role == null)
            {
                return NotFound("No Role with this ID");
            }

            Employee_Role employeeRoleExists = unitOfWork.employee_Role_Repository.Select_By_Id(employee_RoleDTO.Id);
            if (employeeRoleExists == null)
            {
                return NotFound("No Employee Role with this ID");
            }
            mapper.Map(employee_RoleDTO, employeeRoleExists);
            unitOfWork.employee_Role_Repository.Update(employeeRoleExists);
            unitOfWork.SaveChanges();

            return Ok(employee_RoleDTO);
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            if (Id == 0)
            {
                return BadRequest("Employee Role ID cannot be null.");
            }

            Employee_Role employeeRole = unitOfWork.employee_Role_Repository.Select_By_Id(Id);
            if (employeeRole == null)
            {
                return NotFound();
            }
            else
            {
                unitOfWork.employee_Role_Repository.Delete(Id);
                unitOfWork.SaveChanges();
                return Ok("Employee Role has Successfully been deleted");
            }
        }
    }
}
