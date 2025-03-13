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
    public class BankController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;

        public BankController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
        }

        /////////

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Bank" ,"Inventory" }
        )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<Bank> banks = await Unit_Of_Work.bank_Repository.Select_All_With_IncludesById<Bank>(
                    f => f.IsDeleted != true,
                    query => query.Include(b => b.AccountNumber));

            if (banks == null || banks.Count == 0)
            {
                return NotFound();
            }

            List<BankGetDTO> DTOs = mapper.Map<List<BankGetDTO>>(banks);

            return Ok(DTOs);
        }

        /////////

        [HttpPost]
        [Authorize_Endpoint_(
        allowedTypes: new[] { "octa", "employee" },
        pages: new[] { "Bank" }
         )]
        public IActionResult Add(BankAddDto newBank)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (newBank == null)
            {
                return BadRequest("Bank cannot be null");
            }

            if (newBank.Name == null)
            {
                return BadRequest("the name cannot be null");
            }

            AccountingTreeChart account = Unit_Of_Work.accountingTreeChart_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == newBank.AccountNumberID);

            if (account == null)
            {
                return NotFound("No Account chart with this Id");
            }
            else
            {
                if (account.SubTypeID == 1)
                {
                    return BadRequest("You can't use main account, only sub account");
                }

                if (account.LinkFileID != 6)
                {
                    return BadRequest("Wrong Link File, it should be Bank file link ");
                }
            }

            Bank bank = mapper.Map<Bank>(newBank);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            bank.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                bank.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                bank.InsertedByUserId = userId;
            }

            Unit_Of_Work.bank_Repository.Add(bank);
            Unit_Of_Work.SaveChanges();
            return Ok(newBank);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Bank" }
        )]
        public async Task<IActionResult> GetById(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (id == 0)
            {
                return BadRequest("Enter Bank ID");
            }

            Bank Bank = await Unit_Of_Work.bank_Repository.FindByIncludesAsync(
                    credit => credit.IsDeleted != true && credit.ID == id,
                    query => query.Include(credit => credit.AccountNumber));

            if (Bank == null)
            {
                return NotFound();
            }

            BankGetDTO BankGetDTO = mapper.Map<BankGetDTO>(Bank);

            return Ok(BankGetDTO);
        }

        /////////

        [HttpPut]
        [Authorize_Endpoint_(
        allowedTypes: new[] { "octa", "employee" },
        allowEdit: 1,
        pages: new[] { "Bank" }
     )]
        public IActionResult Edit(BankGetDTO newBank)
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

            if (newBank == null)
            {
                return BadRequest("Bank cannot be null");
            }

            if (newBank.Name == null)
            {
                return BadRequest("the name cannot be null");
            }

            Bank bank = Unit_Of_Work.bank_Repository.First_Or_Default(s => s.ID == newBank.ID && s.IsDeleted != true);
            if (bank == null || bank.IsDeleted == true)
            {
                return NotFound("No Bank with this ID");
            }

            AccountingTreeChart account = Unit_Of_Work.accountingTreeChart_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == newBank.AccountNumberID);

            if (account == null)
            {
                return NotFound("No Account chart with this Id");
            }
            else
            {
                if (account.SubTypeID == 1)
                {
                    return BadRequest("You can't use main account, only sub account");
                }

                if (account.LinkFileID != 6)
                {
                    return BadRequest("Wrong Link File, it should be Bank file link ");
                }
            }
             
            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Bank", roleId, userId, bank);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            mapper.Map(newBank, bank);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            bank.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                bank.UpdatedByOctaId = userId;
                if (bank.UpdatedByUserId != null)
                {
                    bank.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                bank.UpdatedByUserId = userId;
                if (bank.UpdatedByOctaId != null)
                {
                    bank.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.bank_Repository.Update(bank);
            Unit_Of_Work.SaveChanges();
            return Ok(newBank);
        }

        ////////

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
          allowedTypes: new[] { "octa", "employee" },
          allowDelete: 1,
        pages: new[] { "Bank" }
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
                return BadRequest("Enter Bank ID");
            }

            Bank bank = Unit_Of_Work.bank_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);

            if (bank == null)
            {
                return NotFound();
            }
             
            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "Bank", roleId, userId, bank);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            bank.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            bank.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                bank.DeletedByOctaId = userId;
                if (bank.DeletedByUserId != null)
                {
                    bank.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                bank.DeletedByUserId = userId;
                if (bank.DeletedByOctaId != null)
                {
                    bank.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.bank_Repository.Update(bank);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
