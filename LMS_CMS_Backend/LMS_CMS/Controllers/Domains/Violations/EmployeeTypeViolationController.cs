using AutoMapper;
using LMS_CMS_BL.DTO.Bus;
using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.DTO.Violation;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_DAL.Models.Domains.ViolationModule;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers.Domains.Violations
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeTypeViolationController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public EmployeeTypeViolationController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        ///////////////////////////////////////////

        [HttpGet]
        //[Authorize_Endpoint_(
        //    allowedTypes: new[] { "octa", "employee" },
        //    pages: new[] { "Violation Types", "Administrator" }
        //)]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);


            List<EmployeeTypeViolation> EmpTypeViolations = await Unit_Of_Work.employeeTypeViolation_Repository.Select_All_With_IncludesById<EmployeeTypeViolation>(
                    bus => bus.IsDeleted != true,
                    query => query.Include(emp => emp.EmployeeType),
                    query => query.Include(assisstant => assisstant.Violation)
                    );

            if (EmpTypeViolations == null || EmpTypeViolations.Count == 0)
            {
                return NotFound();
            }

            List<EmployeeTypeViolationGetDTO> EmpTypeViolationsDTO = mapper.Map<List<EmployeeTypeViolationGetDTO>>(EmpTypeViolations);

            return Ok(EmpTypeViolationsDTO);
        }

        ///////////////////////////////////////////

        [HttpGet("GetByEmployeeType/{id}")]
        public async Task<IActionResult> GetAsyncByEmployeeType(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }
            List<EmployeeTypeViolation> EmpTypeViolations = await Unit_Of_Work.employeeTypeViolation_Repository.Select_All_With_IncludesById<EmployeeTypeViolation>(
                     emp => emp.IsDeleted != true&&emp.EmployeeTypeID == id,
                     query => query.Include(emp => emp.EmployeeType),
                     query => query.Include(assisstant => assisstant.Violation)
                     );

            if (EmpTypeViolations == null || EmpTypeViolations.Count == 0)
            {
                return NotFound();
            }

            List<EmployeeTypeViolationGetDTO> EmpTypeViolationsDTO = mapper.Map<List<EmployeeTypeViolationGetDTO>>(EmpTypeViolations);

            return Ok(EmpTypeViolationsDTO);
        }

        ///////////////////////////////////////////

        [HttpGet("id")]

        public async Task<IActionResult> GetAsyncByID(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }
            EmployeeTypeViolation EmployeeTypeViolation = await Unit_Of_Work.employeeTypeViolation_Repository.FindByIncludesAsync(
                    sem => sem.IsDeleted != true && sem.ID == id,
                    query => query.Include(emp => emp.EmployeeType),
                    query => query.Include(assisstant => assisstant.Violation));

            if (EmployeeTypeViolation == null)
            {
                return NotFound();
            }

            EmployeeTypeViolationGetDTO EmpTypeViolationsDTO = mapper.Map<EmployeeTypeViolationGetDTO>(EmployeeTypeViolation);

            return Ok(EmpTypeViolationsDTO);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////

        //[HttpPost]

        //public async Task<IActionResult> Add(EmployeeTypeViolationGetDTO NewEmployeeTypeViolation)
        //{
        //    UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

        //    var userClaims = HttpContext.User.Claims;
        //    var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
        //    long.TryParse(userIdClaim, out long userId);
        //    var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

        //    if (userIdClaim == null || userTypeClaim == null)
        //    {
        //        return Unauthorized("User ID or Type claim not found.");
        //    }
        //    if (NewEmployeeTypeViolation == null)
        //    {
        //        return NotFound();
        //    }
        //    if(NewEmployeeTypeViolation.EmployeeTypeID == null)
        //    {
        //        return BadRequest("EmployeeId Can not be null");
        //    }
        //    EmployeeType empType = Unit_Of_Work.employeeType_Repository.Select_By_Id(NewEmployeeTypeViolation.EmployeeTypeID);
        //    if (empType == null) 
        //    {
        //        return NotFound("this Employee Type Is Not Exist");
        //    }
        //    if (NewEmployeeTypeViolation.ViolationsTypeName == null)
        //    {
        //        return BadRequest("ViolationsTypeName Can not be null");
        //    }
        //    Violation newViolation = new Violation();
        //    newViolation.Name = NewEmployeeTypeViolation.ViolationsTypeName;
        //    await Unit_Of_Work.violations_Repository.AddAsync(newViolation);
        //    await Unit_Of_Work.SaveChangesAsync();
        //    newViolation =Unit_Of_Work.violations_Repository.First_Or_Default(v=>v.Name == NewEmployeeTypeViolation.ViolationsTypeName);
        //    if (newViolation == null) 
        //    {
        //        return NotFound("this Violation Type Is Not Exist");
        //    }
        //    EmployeeTypeViolation EmployeeTypeViolation = mapper.Map<EmployeeTypeViolation>(NewEmployeeTypeViolation);

        //    TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
        //    EmployeeTypeViolation.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
        //    if (userTypeClaim == "octa")
        //    {
        //        EmployeeTypeViolation.InsertedByOctaId = userId;
        //    }
        //    else if (userTypeClaim == "employee")
        //    {
        //        EmployeeTypeViolation.InsertedByUserId = userId;
        //    }

        //    Unit_Of_Work.employeeTypeViolation_Repository.Add(EmployeeTypeViolation);
        //    Unit_Of_Work.SaveChanges();
        //    return Ok(NewEmployeeTypeViolation);
        //}

        ////////////////////////////////////////////////////
    }
}
