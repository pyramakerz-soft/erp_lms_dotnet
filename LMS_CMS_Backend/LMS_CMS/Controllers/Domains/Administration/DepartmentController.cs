using AutoMapper;
using LMS_CMS_BL.DTO.Administration;
using LMS_CMS_BL.DTO.Bus;
using LMS_CMS_BL.DTO.Registration;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.Administration;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_DAL.Models.Domains.RegisterationModule;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers.Domains.Administration
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public DepartmentController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        ///////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
       allowedTypes: new[] { "octa", "employee" },
       pages: new[] { "Department", "Administrator" }
       )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<Department> departments = await Unit_Of_Work.department_Repository.Select_All_With_IncludesById<Department>(
                    b => b.IsDeleted != true);

            if (departments == null || departments.Count == 0)
            {
                return NotFound();
            }

            List<DepartmentGetDTO> DeptsDTO = mapper.Map<List<DepartmentGetDTO>>(departments);

            return Ok(DeptsDTO);
        }

        //////////////////////////////////////////////////////////////////////////////
        [HttpGet("{id}")]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" },
         pages: new[] { "Department", "Administrator" }
         )]
        public async Task<IActionResult> GetbyIdAsync(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

           Department department = Unit_Of_Work.department_Repository.First_Or_Default(d=>d.ID == id&&d.IsDeleted!=true);

            if (department == null )
            {
                return NotFound();
            }

            DepartmentGetDTO DeptsDTO = mapper.Map<DepartmentGetDTO>(department);

            return Ok(DeptsDTO);
        }
        //////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" },
         pages: new[] { "Department", "Administrator" }
       )]
        public IActionResult Add(DepartmentAddDto newDept)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (newDept == null)
            {
                return BadRequest("Department cannot be null");
            }
            Department department = mapper.Map<Department>(newDept);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            department.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                department.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                department.InsertedByUserId = userId;
            }

            Unit_Of_Work.department_Repository.Add(department);
            Unit_Of_Work.SaveChanges();
            return Ok(newDept);
        }

        //////////////////////////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
        allowedTypes: new[] { "octa", "employee" },
        allowEdit: 1,
       pages: new[] { "Department", "Administrator" }
    )]
        public IActionResult Edit(DepartmentGetDTO newDept)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
            var userRoleClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            long.TryParse(userRoleClaim, out long roleId);

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID, Type claim not found.");
            }

            if (newDept == null)
            {
                return BadRequest("Department cannot be null");
            }

            Department department = Unit_Of_Work.department_Repository.First_Or_Default(d=>d.ID==newDept.ID&&d.IsDeleted!=true);
            if (department==null)
            {
                return NotFound("There is no department with this id");
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Department");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (department.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Department page doesn't exist");
                }
            }

            mapper.Map(newDept, department);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            department.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                department.UpdatedByOctaId = userId;
                if (department.UpdatedByUserId != null)
                {
                    department.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                department.UpdatedByUserId = userId;
                if (department.UpdatedByOctaId != null)
                {
                    department.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.department_Repository.Update(department);
            Unit_Of_Work.SaveChanges();
            return Ok(newDept);
        }

        //////////////////////////////////////////////////////////////////////////////////

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
          allowedTypes: new[] { "octa", "employee" },
          allowDelete: 1,
         pages: new[] { "Department", "Administrator" }
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
                return BadRequest("Enter Category ID");
            }

            Department department = Unit_Of_Work.department_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);


            if (department == null)
            {
                return NotFound();
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Department");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Delete_For_Others == false)
                    {
                        if (department.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Admission Test page doesn't exist");
                }
            }

            department.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            department.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                department.DeletedByOctaId = userId;
                if (department.DeletedByUserId != null)
                {
                    department.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                department.DeletedByUserId = userId;
                if (department.DeletedByOctaId != null)
                {
                    department.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.department_Repository.Update(department);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
