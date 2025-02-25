using AutoMapper;
using LMS_CMS_BL.DTO.Accounting;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.AccountingModule;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers.Domains.Accounting
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class InstallmentDeductionMasterController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;

        public InstallmentDeductionMasterController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
        }


        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Installment Deduction" }
        )]
        public async Task<IActionResult> GetAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            // Get total record count
            int totalRecords = await Unit_Of_Work.installmentDeductionMaster_Repository
                .CountAsync(f => f.IsDeleted != true);

            // Apply pagination
            List<InstallmentDeductionMaster> installmentDeductionMasters = await Unit_Of_Work.installmentDeductionMaster_Repository
                .Select_All_With_IncludesById_Pagination<InstallmentDeductionMaster>(
                    f => f.IsDeleted != true,
                    query => query.Include(Income => Income.Student),
                    query => query.Include(Income => Income.Employee))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (installmentDeductionMasters == null || installmentDeductionMasters.Count == 0)
            {
                return NotFound();
            }

            List<InstallmentDeductionMasterGetDTO> DTOs = mapper.Map<List<InstallmentDeductionMasterGetDTO>>(installmentDeductionMasters);

            // Pagination metadata
            var paginationMetadata = new
            {
                TotalRecords = totalRecords,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
            };

            return Ok(new { Data = DTOs, Pagination = paginationMetadata });
        }


        //////

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" },
           pages: new[] { "Installment Deduction" }
       )]
        public async Task<IActionResult> GetById(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (id == 0)
            {
                return BadRequest("Enter Installment Deduction ID");
            }

            InstallmentDeductionMaster installmentDeductionMaster = await Unit_Of_Work.installmentDeductionMaster_Repository.FindByIncludesAsync(
                    income => income.IsDeleted != true && income.ID == id,
                     query => query.Include(Income => Income.Student),
                    query => query.Include(Income => Income.Employee));

            if (installmentDeductionMaster == null)
            {
                return NotFound();
            }

            InstallmentDeductionMasterGetDTO Dto = mapper.Map<InstallmentDeductionMasterGetDTO>(installmentDeductionMaster);

            return Ok(Dto);
        }

        ///////
        
        [HttpPost]
        [Authorize_Endpoint_(
          allowedTypes: new[] { "octa", "employee" },
           pages: new[] { "Installment Deduction" }
         )]
        public IActionResult Add(InstallmentDeductionMasterAddDTO newMaster)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (newMaster == null)
            {
                return BadRequest("Income cannot be null");
            }

            Employee employee = Unit_Of_Work.employee_Repository.First_Or_Default(e => e.ID == newMaster.EmployeeID && e.IsDeleted != true);
            if (employee == null) 
            {
                return NotFound();
            }


            Student student = Unit_Of_Work.student_Repository.First_Or_Default(e => e.ID == newMaster.StudentID && e.IsDeleted != true);
            if (student == null)
            {
                return NotFound();
            }

            InstallmentDeductionMaster installmentDeductionMaster = mapper.Map<InstallmentDeductionMaster>(newMaster);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            installmentDeductionMaster.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                installmentDeductionMaster.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                installmentDeductionMaster.InsertedByUserId = userId;
            }

            Unit_Of_Work.installmentDeductionMaster_Repository.Add(installmentDeductionMaster);
            Unit_Of_Work.SaveChanges();
            return Ok(installmentDeductionMaster.ID);
        }

        ///////

        [HttpPut]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" },
           allowEdit: 1,
           pages: new[] { "Installment Deduction" }
       )]
        public IActionResult Edit(InstallmentDeductionMasterGetDTO newMaster)
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

            InstallmentDeductionMaster master = Unit_Of_Work.installmentDeductionMaster_Repository.First_Or_Default(s => s.ID == newMaster.ID && s.IsDeleted != true);
            if (master == null || master.IsDeleted == true)
            {
                return NotFound("No master with this ID");
            }

            Employee employee = Unit_Of_Work.employee_Repository.First_Or_Default(e => e.ID == newMaster.EmployeeID && e.IsDeleted != true);
            if (employee == null)
            {
                return NotFound();
            }

            Student student = Unit_Of_Work.student_Repository.First_Or_Default(e => e.ID == newMaster.StudentID && e.IsDeleted != true);
            if (student == null)
            {
                return NotFound();
            }
             
            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Installment Deduction", roleId, userId, master);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            mapper.Map(newMaster, master);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            master.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                master.UpdatedByOctaId = userId;
                if (master.UpdatedByUserId != null)
                {
                    master.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                master.UpdatedByUserId = userId;
                if (master.UpdatedByOctaId != null)
                {
                    master.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.installmentDeductionMaster_Repository.Update(master);
            Unit_Of_Work.SaveChanges();
            return Ok(newMaster);
        }

        ///////

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
           pages: new[] { "Installment Deduction" }
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
                return BadRequest("Enter Installment Deduction ID");
            }

            InstallmentDeductionMaster master = Unit_Of_Work.installmentDeductionMaster_Repository.First_Or_Default(s => s.ID == id && s.IsDeleted != true);
            if (master == null || master.IsDeleted == true)
            {
                return NotFound("No master with this ID");
            }


            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "Installment Deduction", roleId, userId, master);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            master.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            master.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                master.DeletedByOctaId = userId;
                if (master.DeletedByUserId != null)
                {
                    master.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                master.DeletedByUserId = userId;
                if (master.DeletedByOctaId != null)
                {
                    master.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.installmentDeductionMaster_Repository.Update(master);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
