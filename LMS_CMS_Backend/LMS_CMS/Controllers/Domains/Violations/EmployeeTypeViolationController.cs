﻿using AutoMapper;
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
        private readonly CheckPageAccessService _checkPageAccessService;

        public EmployeeTypeViolationController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
        }

        ///////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Violation Types" }
        )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            //List<EmployeeTypeViolation> EmpTypeViolations = Unit_Of_Work.employeeTypeViolation_Repository.Select_All();

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
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Violation Types" }
        )]
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
                     emp => emp.IsDeleted != true && emp.EmployeeTypeID == id,
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
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Violation Types" }
        )]
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

        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Violation Types" }
        )]
        public async Task<IActionResult> Add(EmployeeTypeViolationAddDTO NewEmployeeTypeViolation)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }
            if (NewEmployeeTypeViolation == null)
            {
                return NotFound();
            }
            if (NewEmployeeTypeViolation.EmployeeTypeID == null)
            {
                return BadRequest("EmployeeId Can not be null");
            }
            //EmployeeType empType = Unit_Of_Work.employeeType_Repository.First_Or_Default(e=>e.ID==NewEmployeeTypeViolation.EmployeeTypeID);
            //if (empType == null)
            //{
            //    return NotFound("this Employee Type Is Not Exist");
            //}
            if (NewEmployeeTypeViolation.ViolationName == null)
            {
                return BadRequest("ViolationsTypeName Can not be null");
            }
            Violation newViolation = Unit_Of_Work.violations_Repository.First_Or_Default(v=>v.Name==NewEmployeeTypeViolation.ViolationName);
            if(newViolation != null)
            {
                return BadRequest("this violation already exist");
            }
            newViolation = new Violation();
            newViolation.Name = NewEmployeeTypeViolation.ViolationName;
            newViolation.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                newViolation.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                newViolation.InsertedByUserId = userId;
            }
            await Unit_Of_Work.violations_Repository.AddAsync(newViolation);
            await Unit_Of_Work.SaveChangesAsync();
            Violation Violation = Unit_Of_Work.violations_Repository.First_Or_Default(v=>v.Name == NewEmployeeTypeViolation.ViolationName&&v.IsDeleted!=true);
            if (Violation == null)
            {
                return NotFound("this Violation Type Is Not Exist");
            }
            foreach (var item in NewEmployeeTypeViolation.EmployeeTypeID)
            {
            EmployeeTypeViolation EmployeeTypeViolation = new EmployeeTypeViolation();

            EmployeeTypeViolation.ViolationID = Violation.ID;
            EmployeeTypeViolation.EmployeeTypeID = item;
            EmployeeTypeViolation.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                EmployeeTypeViolation.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                EmployeeTypeViolation.InsertedByUserId = userId;
            }

            await Unit_Of_Work.employeeTypeViolation_Repository.AddAsync(EmployeeTypeViolation);
            await Unit_Of_Work.SaveChangesAsync();
                
            }
            return Ok(NewEmployeeTypeViolation);
        }

        //////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Violation Types" }
        )]
        public async Task<IActionResult> EditViolationAsync(EmployeeTypeViolationEditDTO NewViolation)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
            var userRoleClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            long.TryParse(userRoleClaim, out long roleId);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (NewViolation == null)
            {
                return BadRequest("Employee Type Violation cannot be null");
            }
            if (NewViolation.ViolationId == null)
            {
                return BadRequest("ViolationId Can not be null");
            }
            Violation violation = Unit_Of_Work.violations_Repository.First_Or_Default(v=>v.ID==NewViolation.ViolationId&&v.IsDeleted!=true);
            if (violation == null) {
                return Unauthorized("Violation not found.");

            } 

            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Violation Types", roleId, userId, violation);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            if (NewViolation.ViolationName != violation.Name) 
            {
                Violation v2 =Unit_Of_Work.violations_Repository.First_Or_Default(v=>v.Name==NewViolation.ViolationName);
                if (v2 != null)
                {
                    return BadRequest("this violation already exist");
                }
                violation.Name = NewViolation.ViolationName;
                violation.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                if (userTypeClaim == "octa")
                {
                    violation.UpdatedByOctaId = userId;
                    if (violation.UpdatedByUserId != null)
                    {
                        violation.UpdatedByUserId = null;
                    }

                }
                else if (userTypeClaim == "employee")
                {
                    violation.UpdatedByUserId = userId;
                    if (violation.UpdatedByOctaId != null)
                    {
                        violation.UpdatedByOctaId = null;
                    }
                }
                Unit_Of_Work.violations_Repository.Update(violation);
                await Unit_Of_Work.SaveChangesAsync();
            }

            //delete all empTypeViolation
            List<EmployeeTypeViolation> employeeTypeViolation = Unit_Of_Work.employeeTypeViolation_Repository.FindBy(i=>i.ViolationID==NewViolation.ViolationId);
            foreach (var item in employeeTypeViolation)
            {
                await Unit_Of_Work.employeeTypeViolation_Repository.DeleteAsync(item.ID);
                await Unit_Of_Work.SaveChangesAsync();
            }

            //Create New empTypeViolation
            foreach (var item in NewViolation.EmployeeTypeID)
            {
                EmployeeTypeViolation EmployeeTypeViolation = new EmployeeTypeViolation();

                EmployeeTypeViolation.ViolationID = NewViolation.ViolationId;
                EmployeeTypeViolation.EmployeeTypeID = item;
                EmployeeTypeViolation.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                if (userTypeClaim == "octa")
                {
                    EmployeeTypeViolation.InsertedByOctaId = userId;
                }
                else if (userTypeClaim == "employee")
                {
                    EmployeeTypeViolation.InsertedByUserId = userId;
                }

                await Unit_Of_Work.employeeTypeViolation_Repository.AddAsync(EmployeeTypeViolation);
                await Unit_Of_Work.SaveChangesAsync();

            }

            return Ok(NewViolation);
        }

        //////////////////////////////////////////////////////

        [HttpDelete]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Violation Types" }
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

            if (id == null)
            {
                return BadRequest("id cannot be null");
            }
            EmployeeTypeViolation employeeTypeViolation = Unit_Of_Work.employeeTypeViolation_Repository.Select_By_Id(id);

            if (employeeTypeViolation == null || employeeTypeViolation.IsDeleted == true)
            {
                return NotFound("No EmployeeTypeViolation with this ID");
            } 

            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "Violation Types", roleId, userId, employeeTypeViolation);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            employeeTypeViolation.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            employeeTypeViolation.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                employeeTypeViolation.DeletedByOctaId = userId;
                if (employeeTypeViolation.DeletedByUserId != null)
                {
                    employeeTypeViolation.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                employeeTypeViolation.DeletedByUserId = userId;
                if (employeeTypeViolation.DeletedByOctaId != null)
                {
                    employeeTypeViolation.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.employeeTypeViolation_Repository.Update(employeeTypeViolation);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }

    }
}
