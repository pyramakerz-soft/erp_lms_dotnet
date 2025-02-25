using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.DTO.Accounting;
using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.AccountingModule;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_DAL.Models.Octa;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers.Domains
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeStudentController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;

        public EmployeeStudentController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {

            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService; 
        }

        //////

        [HttpGet("ByEmployeeId/{Id}")]
        public async Task<IActionResult> GetAsync(long Id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<EmployeeStudent> EmpStudents = await Unit_Of_Work.employeeStudent_Repository.Select_All_With_IncludesById<EmployeeStudent>(
                query => query.IsDeleted != true&&query.EmployeeID==Id,
                 query => query.Include(stu => stu.Student),
                 query => query.Include(stu => stu.employee));

            if (EmpStudents == null || EmpStudents.Count == 0)
            {
                return NotFound("No Student found");
            }

            List<EmployeeStudentGetDTO> StudentDTO = mapper.Map<List<EmployeeStudentGetDTO>>(EmpStudents);
            
            return Ok(StudentDTO);
        }

        //////

        [HttpPost]
        [Authorize_Endpoint_(
          allowedTypes: new[] { "octa", "employee" },
          pages: new[] { "Add Children" }
       )]
        public IActionResult Add(EmployeeStudentAddDTO newChild)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (newChild == null)
            {
                return BadRequest("Child cannot be null");
            }

            Student student=Unit_Of_Work.student_Repository.First_Or_Default(s=>s.ID==newChild.StudentID&&s.IsDeleted!=true);
            if(student == null)
            {
                return NotFound();
            }

            Employee employee = Unit_Of_Work.employee_Repository.First_Or_Default(s => s.ID == newChild.EmployeeID && s.IsDeleted != true);
            if (employee == null)
            {
                return NotFound();
            }

            EmployeeStudent employeeStudent = mapper.Map<EmployeeStudent>(newChild);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            employeeStudent.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                employeeStudent.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                employeeStudent.InsertedByUserId = userId;
            }

            Unit_Of_Work.employeeStudent_Repository.Add(employeeStudent);
            Unit_Of_Work.SaveChanges();
            return Ok(newChild);
        }

        //////

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" },
           allowDelete: 1,
          pages: new[] { "Add Children" }
       )]
        public IActionResult Delete(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
            var userRoleClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            long.TryParse(userRoleClaim, out long roleId);

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (id == 0)
            {
                return BadRequest("Enter Save ID");
            }

            EmployeeStudent employeeStudent = Unit_Of_Work.employeeStudent_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);

            if (employeeStudent == null)
            {
                return NotFound();
            }
             
            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "Add Children", roleId, userId, employeeStudent);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            employeeStudent.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            employeeStudent.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                employeeStudent.DeletedByOctaId = userId;
                if (employeeStudent.DeletedByUserId != null)
                {
                    employeeStudent.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                employeeStudent.DeletedByUserId = userId;
                if (employeeStudent.DeletedByOctaId != null)
                {
                    employeeStudent.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.employeeStudent_Repository.Update(employeeStudent);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
