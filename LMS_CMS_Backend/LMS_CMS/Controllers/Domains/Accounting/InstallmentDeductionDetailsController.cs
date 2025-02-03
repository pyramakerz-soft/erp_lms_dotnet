using AutoMapper;
using LMS_CMS_BL.DTO.Accounting;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.AccountingModule;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_PL.Attribute;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LMS_CMS_PL.Services;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers.Domains.Accounting
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class InstallmentDeductionDetailsController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public InstallmentDeductionDetailsController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" },
           pages: new[] { "Installment Deduction Details", "Accounting" }
       )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<InstallmentDeductionDetails> details = await Unit_Of_Work.installmentDeductionDetails_Repository.Select_All_With_IncludesById<InstallmentDeductionDetails>(
                    f => f.IsDeleted != true,
                    query => query.Include(Income => Income.TuitionFeesType));

            if (details == null || details.Count == 0)
            {
                return NotFound();
            }

            List<InstallmentDeductionDetailsGetDTO> DTO = mapper.Map<List<InstallmentDeductionDetailsGetDTO>>(details);

            return Ok(DTO);
        }

        ///////

        [HttpGet("GetByMaster/{id}")]
        [Authorize_Endpoint_(
             allowedTypes: new[] { "octa", "employee" },
             pages: new[] { "Installment Deduction Details", "Accounting" }
         )]
        public async Task<IActionResult> GetByMasterAsync(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<InstallmentDeductionDetails> details = await Unit_Of_Work.installmentDeductionDetails_Repository.Select_All_With_IncludesById<InstallmentDeductionDetails>(
                    f => f.IsDeleted != true && f.InstallmentDeductionMasterID== id,
                    query => query.Include(Income => Income.TuitionFeesType));

            if (details == null || details.Count == 0)
            {
                return NotFound();
            }

            List<InstallmentDeductionDetailsGetDTO> DTO = mapper.Map<List<InstallmentDeductionDetailsGetDTO>>(details);

            return Ok(DTO);
        }

        ///////

        [HttpPost]
        [Authorize_Endpoint_(
          allowedTypes: new[] { "octa", "employee" },
           pages: new[] { "Installment Deduction Details", "Accounting" }
         )]
        public IActionResult Add(InstallmentDeductionDetailsAddDTO NewDetails)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (NewDetails == null)
            {
                return BadRequest("Details cannot be null");
            }

            InstallmentDeductionMaster installmentDeductionMaster = Unit_Of_Work.installmentDeductionMaster_Repository.First_Or_Default(m => m.ID == NewDetails.InstallmentDeductionMasterID && m.IsDeleted != true);
            if (installmentDeductionMaster == null)
            {
                return  NotFound();
            }

            TuitionFeesType tuitionFeesType = Unit_Of_Work.tuitionFeesType_Repository.First_Or_Default(m => m.ID == NewDetails.FeeTypeID && m.IsDeleted != true);
            if (tuitionFeesType == null)
            {
                return NotFound();
            }

            InstallmentDeductionDetails installmentDeductionDetails = mapper.Map<InstallmentDeductionDetails>(NewDetails);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            installmentDeductionDetails.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                installmentDeductionDetails.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                installmentDeductionDetails.InsertedByUserId = userId;
            }

            Unit_Of_Work.installmentDeductionDetails_Repository.Add(installmentDeductionDetails);
            Unit_Of_Work.SaveChanges();
            return Ok(NewDetails);
        }

        ///////

        [HttpPut]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" },
           allowEdit: 1,
           pages: new[] { "Installment Deduction Details", "Accounting" }
       )]
        public IActionResult Edit(InstallmentDeductionDetailsGetDTO NewDetails)
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

            InstallmentDeductionDetails detail = Unit_Of_Work.installmentDeductionDetails_Repository.First_Or_Default(s => s.ID == NewDetails.ID && s.IsDeleted != true);
            if (detail == null || detail.IsDeleted == true)
            {
                return NotFound("No detail with this ID");
            }

            InstallmentDeductionMaster installmentDeductionMaster = Unit_Of_Work.installmentDeductionMaster_Repository.First_Or_Default(m => m.ID == NewDetails.InstallmentDeductionMasterID && m.IsDeleted != true);
            if (installmentDeductionMaster == null)
            {
                return NotFound();
            }

            TuitionFeesType tuitionFeesType = Unit_Of_Work.tuitionFeesType_Repository.First_Or_Default(m => m.ID == NewDetails.FeeTypeID && m.IsDeleted != true);
            if (tuitionFeesType == null)
            {
                return NotFound();
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Installment Deduction Details");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (detail.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("details page doesn't exist");
                }
            }

            mapper.Map(NewDetails, detail);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            detail.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                detail.UpdatedByOctaId = userId;
                if (detail.UpdatedByUserId != null)
                {
                    detail.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                detail.UpdatedByUserId = userId;
                if (detail.UpdatedByOctaId != null)
                {
                    detail.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.installmentDeductionDetails_Repository.Update(detail);
            Unit_Of_Work.SaveChanges();
            return Ok(NewDetails);
        }

        ///////

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
           pages: new[] { "Installment Deduction Details", "Accounting" }
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
                return BadRequest("Enter Installment Deduction Details ID");
            }

            InstallmentDeductionDetails detail = Unit_Of_Work.installmentDeductionDetails_Repository.First_Or_Default(s => s.ID == id && s.IsDeleted != true);
            if (detail == null || detail.IsDeleted == true)
            {
                return NotFound("No detail with this ID");
            }


            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Installment Deduction Details");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Delete_For_Others == false)
                    {
                        if (detail.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Installment Deduction Details page doesn't exist");
                }
            }

            detail.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            detail.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                detail.DeletedByOctaId = userId;
                if (detail.DeletedByUserId != null)
                {
                    detail.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                detail.DeletedByUserId = userId;
                if (detail.DeletedByOctaId != null)
                {
                    detail.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.installmentDeductionDetails_Repository.Update(detail);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
