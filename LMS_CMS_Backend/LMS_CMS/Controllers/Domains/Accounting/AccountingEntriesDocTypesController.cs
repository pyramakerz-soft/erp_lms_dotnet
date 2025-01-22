using AutoMapper;
using LMS_CMS_BL.DTO.Administration;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.Administration;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LMS_CMS_DAL.Models.Domains.AccountingModule;
using LMS_CMS_BL.DTO.Accounting;

namespace LMS_CMS_PL.Controllers.Domains.Accounting
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountingEntriesDocTypesController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public AccountingEntriesDocTypesController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        ///////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
       allowedTypes: new[] { "octa", "employee" },
       pages: new[] { "Accounting Entries Doc Type", "Accounting" }
       )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<AccountingEntriesDocType> accountingEntriesDocTypes = await Unit_Of_Work.accountingEntriesDocType_Repository.Select_All_With_IncludesById<AccountingEntriesDocType>(
                    b => b.IsDeleted != true);

            if (accountingEntriesDocTypes == null || accountingEntriesDocTypes.Count == 0)
            {
                return NotFound();
            }

            List<AccountingEntriesDocTypeGetDTO> DTOs = mapper.Map<List<AccountingEntriesDocTypeGetDTO>>(accountingEntriesDocTypes);

            return Ok(DTOs);
        }

        //////////////////////////////////////////////////////////////////////////////
        [HttpGet("{id}")]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" },
       pages: new[] { "Accounting Entries Doc Type", "Accounting" }
         )]
        public async Task<IActionResult> GetbyIdAsync(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            AccountingEntriesDocType accountingEntriesDocType = Unit_Of_Work.accountingEntriesDocType_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);


            if (accountingEntriesDocType == null)
            {
                return NotFound();
            }

            AccountingEntriesDocTypeGetDTO dto = mapper.Map<AccountingEntriesDocTypeGetDTO>(accountingEntriesDocType);

            return Ok(dto);
        }
        //////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" },
       pages: new[] { "Accounting Entries Doc Type", "Accounting" }
       )]
        public IActionResult Add(AccountingEntriesDocTypesAddDto newAcc)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (newAcc == null)
            {
                return BadRequest("Accounting Entries Doc Type cannot be null");
            }
            AccountingEntriesDocType accountingEntriesDocTypes = mapper.Map<AccountingEntriesDocType>(newAcc);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            accountingEntriesDocTypes.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                accountingEntriesDocTypes.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                accountingEntriesDocTypes.InsertedByUserId = userId;
            }

            Unit_Of_Work.accountingEntriesDocType_Repository.Add(accountingEntriesDocTypes);
            Unit_Of_Work.SaveChanges();
            return Ok(newAcc);
        }

        //////////////////////////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
        allowedTypes: new[] { "octa", "employee" },
        allowEdit: 1,
       pages: new[] { "Accounting Entries Doc Type", "Accounting" }
    )]
        public IActionResult Edit(AccountingEntriesDocTypeGetDTO newAcc)
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

            if (newAcc == null)
            {
                return BadRequest("Accounting Entries Doc Type cannot be null");
            }

            AccountingEntriesDocType accountingEntriesDocType = Unit_Of_Work.accountingEntriesDocType_Repository.First_Or_Default(d => d.ID == newAcc.ID && d.IsDeleted != true);
            if (accountingEntriesDocType == null)
            {
                return NotFound("There is no department with this id");
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Accounting Entries Doc Type");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (accountingEntriesDocType.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Accounting Entries Doc Type page doesn't exist");
                }
            }

            mapper.Map(newAcc, accountingEntriesDocType);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            accountingEntriesDocType.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                accountingEntriesDocType.UpdatedByOctaId = userId;
                if (accountingEntriesDocType.UpdatedByUserId != null)
                {
                    accountingEntriesDocType.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                accountingEntriesDocType.UpdatedByUserId = userId;
                if (accountingEntriesDocType.UpdatedByOctaId != null)
                {
                    accountingEntriesDocType.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.accountingEntriesDocType_Repository.Update(accountingEntriesDocType);
            Unit_Of_Work.SaveChanges();
            return Ok(newAcc);
        }

        //////////////////////////////////////////////////////////////////////////////////

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
          allowedTypes: new[] { "octa", "employee" },
          allowDelete: 1,
       pages: new[] { "Accounting Entries Doc Type", "Accounting" }
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
                return BadRequest("Enter Accounting Entries Doc Type ID");
            }

            AccountingEntriesDocType accountingEntriesDocType = Unit_Of_Work.accountingEntriesDocType_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);


            if (accountingEntriesDocType == null)
            {
                return NotFound();
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Accounting Entries Doc Type");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Delete_For_Others == false)
                    {
                        if (accountingEntriesDocType.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Accounting Entries Doc Type page doesn't exist");
                }
            }

            accountingEntriesDocType.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            accountingEntriesDocType.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                accountingEntriesDocType.DeletedByOctaId = userId;
                if (accountingEntriesDocType.DeletedByUserId != null)
                {
                    accountingEntriesDocType.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                accountingEntriesDocType.DeletedByUserId = userId;
                if (accountingEntriesDocType.DeletedByOctaId != null)
                {
                    accountingEntriesDocType.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.accountingEntriesDocType_Repository.Update(accountingEntriesDocType);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
