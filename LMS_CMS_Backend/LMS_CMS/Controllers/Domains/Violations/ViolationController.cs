﻿using AutoMapper;
using LMS_CMS_BL.DTO;
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
using System.Diagnostics;

namespace LMS_CMS_PL.Controllers.Domains.Violations
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class ViolationController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;

        public ViolationController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
        }
        ///////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Violation Types" }
        )]
        public async Task<IActionResult> GetAsync()
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

            List<Violation> violations = await Unit_Of_Work.violations_Repository.Select_All_With_IncludesById<Violation>(
                sem => sem.IsDeleted != true);

            if (violations == null || violations.Count == 0)
            {
                return NotFound();
            }

            List<ViolationGetDTO> violationDTOs = mapper.Map<List<ViolationGetDTO>>(violations);

            foreach (var item in violationDTOs)
            {
                List<EmployeeTypeViolation> employeeTypeViolations = await Unit_Of_Work.employeeTypeViolation_Repository
                    .Select_All_With_IncludesById<EmployeeTypeViolation>(e => e.ViolationID == item.ID,
                    query => query.Include(emp => emp.EmployeeType));

                item.employeeTypes = mapper.Map<List<EmployeeTypeGetDTO>>(employeeTypeViolations);
            }

            return Ok(violationDTOs);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("{id}")]
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

            Violation violation = await Unit_Of_Work.violations_Repository.FindByIncludesAsync(
                sem => sem.IsDeleted != true && sem.ID==id);

            if (violation == null )
            {
                return NotFound();
            }

            ViolationGetDTO violationDTO = mapper.Map<ViolationGetDTO>(violation);


                List<EmployeeTypeViolation> employeeTypeViolations = await Unit_Of_Work.employeeTypeViolation_Repository
                    .Select_All_With_IncludesById<EmployeeTypeViolation>(e => e.ViolationID == violationDTO.ID,
                    query => query.Include(emp => emp.EmployeeType));

            violationDTO.employeeTypes = mapper.Map<List<EmployeeTypeGetDTO>>(employeeTypeViolations);
            return Ok(violationDTO);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////

        //[HttpPost]
        //[Authorize_Endpoint_(
        //    allowedTypes: new[] { "octa", "employee" },
        //    pages: new[] { "Violation Types", "Administrator" }
        //)]
        //public async Task<IActionResult> Add(ViolationAddDTO Newviolation)
        //{
        //    UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);
        //
        //    var userClaims = HttpContext.User.Claims;
        //    var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
        //    long.TryParse(userIdClaim, out long userId);
        //    var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
        //
        //    if (userIdClaim == null || userTypeClaim == null)
        //    {
        //        return Unauthorized("User ID or Type claim not found.");
        //    }
        //    if (Newviolation == null)
        //    {
        //        return NotFound();
        //    }
        //    if (Newviolation.Name == null)
        //    {
        //        return BadRequest("the name cannot be null");
        //    }
        //    Violation violation = mapper.Map<Violation>(Newviolation);
        //
         //   TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
         //   violation.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
         //   if (userTypeClaim == "octa")
         //   {
         //       violation.InsertedByOctaId = userId;
         //   }
         //   else if (userTypeClaim == "employee")
         //   {
         //       violation.InsertedByUserId = userId;
         //   }
         //
         //   Unit_Of_Work.violations_Repository.Add(violation);
         //   Unit_Of_Work.SaveChanges();
         //   return Ok(Newviolation);
         //}

        ////////////////////////////////////////////////////

        //[HttpPut]
        //[Authorize_Endpoint_(
          //  allowedTypes: new[] { "octa", "employee" },
           // allowEdit: 1,
           // pages: new[] { "Violation Types", "Administrator" }
        //)]
        //public IActionResult Edit(ViolationGetDTO Newviolation)
        //{
        //    UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);
        //
        //    var userClaims = HttpContext.User.Claims;
        //    var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
        //    long.TryParse(userIdClaim, out long userId);
        //    var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
        //    var userRoleClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
        //    long.TryParse(userRoleClaim, out long roleId);
        //
        //    if (userIdClaim == null || userTypeClaim == null)
        //    {
        //        return Unauthorized("User ID or Type claim not found.");
        //    }
        //
        //    if (Newviolation == null)
        //    {
        //        return BadRequest("Violation cannot be null");
        //    }
        //    if (Newviolation.Name == null)
        //    {
        //        return BadRequest("the name cannot be null");
        //    }
        //    Violation violation=Unit_Of_Work.violations_Repository.First_Or_Default(v=>v.ID==Newviolation.ID);
        //    if (violation == null)
        //    { 
        //      return NotFound();
        //    }
        //    if (userTypeClaim == "employee")
        //    {
        //        Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Violations");
        //        if (page != null)
        //        {
        //            Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
        //            if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
        //            {
        //                if (violation.InsertedByUserId != userId)
        //                {
        //                    return Unauthorized();
        //                }
        //            }
        //        }
        //        else
        //        {
        //            return BadRequest("Violations page doesn't exist");
        //        }
        //    }
        //    ////mapper.Map<Violation>(Newviolation);
        //    mapper.Map(Newviolation, violation);
        //    TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
        //    violation.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
        //    if (userTypeClaim == "octa")
        //    {
        //        violation.UpdatedByOctaId = userId;
        //        if (violation.UpdatedByUserId != null)
        //        {
        //            violation.UpdatedByUserId = null;
        //        }
        //
        //    }
        //    else if (userTypeClaim == "employee")
        //    {
        //        violation.UpdatedByUserId = userId;
        //        if (violation.UpdatedByOctaId != null)
        //        {
        //            violation.UpdatedByOctaId = null;
        //        }
        //    }
        //    Unit_Of_Work.violations_Repository.Update(violation);
        //    Unit_Of_Work.SaveChanges();
        //    return Ok(Newviolation);
        //
        //}
        
        //////////////////////////////////////////////////////

        [HttpDelete("{id}")]
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
            Violation violation = Unit_Of_Work.violations_Repository.Select_By_Id(id);

            if (violation == null || violation.IsDeleted == true)
            {
                return NotFound("No Violation with this ID");
            } 

            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "Violation Types", roleId, userId, violation);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            violation.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            violation.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                violation.DeletedByOctaId = userId;
                if (violation.DeletedByUserId != null)
                {
                    violation.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                violation.DeletedByUserId = userId;
                if (violation.DeletedByOctaId != null)
                {
                    violation.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.violations_Repository.Update(violation);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
