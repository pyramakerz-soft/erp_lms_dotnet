using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.DTO.Bus;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Migrations;
using LMS_CMS_DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private UOW Unit_Of_Work;
        IMapper mapper;

        public EmployeeController(UOW Unit_Of_Work, IMapper mapper)
        {
            this.Unit_Of_Work = Unit_Of_Work;
            this.mapper = mapper;
        }

        [HttpGet("GetByTypeIdDomainId/{TypeId}/{DomainId}")]
        public IActionResult GetByTypeIDAsync(long TypeId, long DomainId)
        {
            EmployeeType type = Unit_Of_Work.employeeType_Repository.Select_By_Id(TypeId);
            if (type == null || type.IsDeleted==true)
            {
                return NotFound("No Type with this Id");
            }

            Domain domain = Unit_Of_Work.domain_Repository.Select_By_Id(DomainId);
            if (domain == null || domain.IsDeleted==true)
            {
                return NotFound("No Domain with this Id");
            }

            List<Employee> employees = Unit_Of_Work.employee_Repository.FindBy(
                emp => emp.EmployeeTypeID == TypeId && emp.Domain_ID == DomainId && emp.IsDeleted!=true
            );

            if (employees == null || employees.Count == 0)
            {
                return NotFound("There is no employees with this type");
            }

            List<Employee_GetDTO> employeeDTOs = mapper.Map<List<Employee_GetDTO>>(employees);

            return Ok(employeeDTOs);
        }

        [HttpGet("{empId}")]
        public IActionResult GetByID(long empId)
        {
            Console.WriteLine("================================================================================================",empId);
            Employee employee = Unit_Of_Work.employee_Repository.Select_By_Id(empId);
            Console.WriteLine("================================================================================================", employee);

            if (employee == null || employee.IsDeleted == true)
            {
                return NotFound("No employee found");
            }

            Employee_GetDTO employeeDTO = mapper.Map<Employee_GetDTO>(employee);

            return Ok(employeeDTO);
        }
    }
}
