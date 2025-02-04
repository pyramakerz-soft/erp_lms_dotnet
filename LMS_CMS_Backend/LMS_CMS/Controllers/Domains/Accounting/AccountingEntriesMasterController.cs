using AutoMapper;
using LMS_CMS_BL.DTO.Accounting;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.AccountingModule;
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
    public class AccountingEntriesMasterController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public AccountingEntriesMasterController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        ///////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" },
           pages: new[] { "Accounting Entries", "Accounting" }
        )]
        public async Task<IActionResult> GetAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            int totalRecords = await Unit_Of_Work.accountingEntriesMaster_Repository
                .CountAsync(f => f.IsDeleted != true);

            List<AccountingEntriesMaster> AccountingEntriesMasters = await Unit_Of_Work.accountingEntriesMaster_Repository.Select_All_With_IncludesById_Pagination<AccountingEntriesMaster>(
                    t => t.IsDeleted != true,
                    query => query.Include(Master => Master.AccountingEntriesDocType)
                    )
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (AccountingEntriesMasters == null || AccountingEntriesMasters.Count == 0)
            {
                return NotFound();
            } 

            List<AccountingEntriesMasterGetDTO> DTOs = mapper.Map<List<AccountingEntriesMasterGetDTO>>(AccountingEntriesMasters);

            var paginationMetadata = new
            {
                TotalRecords = totalRecords,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
            };

            return Ok(new { Data = DTOs, Pagination = paginationMetadata });
        }

        //////////////////////////////////////////////////////////////////////////////

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Accounting Entries", "Accounting" }
        )]
        public async Task<IActionResult> GetbyIdAsync(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (id == 0 || id == null)
            {
                return BadRequest("Accounting Entries Type ID");
            }

            AccountingEntriesMaster AccountingEntriesMaster = await Unit_Of_Work.accountingEntriesMaster_Repository.FindByIncludesAsync(
                    acc => acc.IsDeleted != true && acc.ID == id,
                    query => query.Include(ac => ac.AccountingEntriesDocType)
                    );

            if (AccountingEntriesMaster == null)
            {
                return NotFound();
            }

            AccountingEntriesMasterGetDTO dto = mapper.Map<AccountingEntriesMasterGetDTO>(AccountingEntriesMaster); 

            return Ok(dto);
        }

        //////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" },
         pages: new[] { "Accounting Entries", "Accounting" }
        )]
        public IActionResult Add(AccountingEntriesMasterAddDTO newMaster)
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
                return BadRequest("Accounting Entries cannot be null");
            }

            AccountingEntriesDocType AccountingEntriesDocType = Unit_Of_Work.accountingEntriesDocType_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == newMaster.AccountingEntriesDocTypeID);
            if (AccountingEntriesDocType == null)
            {
                return BadRequest("there is no Accounting Entries Doc Type with this ID");
            }

            AccountingEntriesMaster AccountingEntriesMaster = mapper.Map<AccountingEntriesMaster>(newMaster);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            AccountingEntriesMaster.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                AccountingEntriesMaster.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                AccountingEntriesMaster.InsertedByUserId = userId;
            }

            Unit_Of_Work.accountingEntriesMaster_Repository.Add(AccountingEntriesMaster);
            Unit_Of_Work.SaveChanges();
            return Ok(newMaster);
        }

        //////////////////////////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Accounting Entries", "Accounting" }
        )]
        public IActionResult Edit(AccountingEntriesMasterPutDTO newMaster)
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

            if (newMaster == null)
            {
                return BadRequest("Accounting Entries cannot be null");
            }

            AccountingEntriesMaster AccountingEntriesMaster = Unit_Of_Work.accountingEntriesMaster_Repository.First_Or_Default(d => d.ID == newMaster.ID && d.IsDeleted != true);
            if (AccountingEntriesMaster == null)
            {
                return NotFound("There is no Accounting Entries Master with this id");
            }

            AccountingEntriesDocType AccountingEntriesDocType = Unit_Of_Work.accountingEntriesDocType_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == newMaster.AccountingEntriesDocTypeID);
            if (AccountingEntriesDocType == null)
            {
                return BadRequest("there is no Accounting Entries Doc Type with this ID");
            }
             
            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Accounting Entries");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (AccountingEntriesMaster.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Accounting Entries page doesn't exist");
                }
            }

            mapper.Map(newMaster, AccountingEntriesMaster);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            AccountingEntriesMaster.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                AccountingEntriesMaster.UpdatedByOctaId = userId;
                if (AccountingEntriesMaster.UpdatedByUserId != null)
                {
                    AccountingEntriesMaster.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                AccountingEntriesMaster.UpdatedByUserId = userId;
                if (AccountingEntriesMaster.UpdatedByOctaId != null)
                {
                    AccountingEntriesMaster.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.accountingEntriesMaster_Repository.Update(AccountingEntriesMaster);
            Unit_Of_Work.SaveChanges();
            return Ok(newMaster);
        }

        //////////////////////////////////////////////////////////////////////////////////

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Accounting Entries", "Accounting" }
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
                return BadRequest("Enter Accounting Entries ID");
            }
             
            AccountingEntriesMaster AccountingEntriesMaster = Unit_Of_Work.accountingEntriesMaster_Repository.First_Or_Default(d => d.ID == id && d.IsDeleted != true);
            if (AccountingEntriesMaster == null)
            {
                return NotFound("There is no Accounting Entries Master with this id");
            }
             
            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Accounting Entries");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Delete_For_Others == false)
                    {
                        if (AccountingEntriesMaster.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Accounting Entries page doesn't exist");
                }
            }

            AccountingEntriesMaster.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            AccountingEntriesMaster.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                AccountingEntriesMaster.DeletedByOctaId = userId;
                if (AccountingEntriesMaster.DeletedByUserId != null)
                {
                    AccountingEntriesMaster.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                AccountingEntriesMaster.DeletedByUserId = userId;
                if (AccountingEntriesMaster.DeletedByOctaId != null)
                {
                    AccountingEntriesMaster.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.accountingEntriesMaster_Repository.Update(AccountingEntriesMaster);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
