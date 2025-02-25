using AutoMapper;
using LMS_CMS_BL.DTO.Accounting;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.AccountingModule;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LMS_CMS_PL.Controllers.Domains.Accounting
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class IncomeController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;

        public IncomeController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
        }

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Income" }
        )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<Income> Incomes = await Unit_Of_Work.income_Repository.Select_All_With_IncludesById<Income>(
                    f => f.IsDeleted != true,
                    query => query.Include(Income => Income.AccountNumber));

            if (Incomes == null || Incomes.Count == 0)
            {
                return NotFound();
            }

            List<IncomeGetDTO> IncomeGetDTOs = mapper.Map<List<IncomeGetDTO>>(Incomes);

            return Ok(IncomeGetDTOs);
        }

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Income" }
        )]
        public async Task<IActionResult> GetById(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (id == 0)
            {
                return BadRequest("Enter Income ID");
            }

            Income income = await Unit_Of_Work.income_Repository.FindByIncludesAsync(
                    income => income.IsDeleted != true && income.ID == id,
                    query => query.Include(income => income.AccountNumber));

            if (income == null)
            {
                return NotFound();
            }

            IncomeGetDTO IncomeGetDTO = mapper.Map<IncomeGetDTO>(income);

            return Ok(IncomeGetDTO);
        }

        [HttpPost]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" },
           pages: new[] { "Income" }
       )]
        public IActionResult Add(IncomeAddDTO NewIncome)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (NewIncome == null)
            {
                return BadRequest("Income cannot be null");
            }

            if (NewIncome.Name == null)
            {
                return BadRequest("the name cannot be null");
            }

            AccountingTreeChart account = Unit_Of_Work.accountingTreeChart_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == NewIncome.AccountNumberID);

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

                if (account.LinkFileID != 7)
                {
                    return BadRequest("Wrong Link File, it should be Income file link ");
                }
            }

            Income income = mapper.Map<Income>(NewIncome);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            income.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                income.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                income.InsertedByUserId = userId;
            }

            Unit_Of_Work.income_Repository.Add(income);
            Unit_Of_Work.SaveChanges();
            return Ok(NewIncome);
        }

        [HttpPut]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" },
           allowEdit: 1,
           pages: new[] { "Income" }
       )]
        public IActionResult Edit(IncomePutDTO EditedIncome)
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

            if (EditedIncome == null)
            {
                return BadRequest("Income cannot be null");
            }

            if (EditedIncome.Name == null)
            {
                return BadRequest("the name cannot be null");
            }

            Income IncomeExists = Unit_Of_Work.income_Repository.First_Or_Default(s => s.ID == EditedIncome.ID && s.IsDeleted != true);
            if (IncomeExists == null || IncomeExists.IsDeleted == true)
            {
                return NotFound("No Income with this ID");
            }

            AccountingTreeChart account = Unit_Of_Work.accountingTreeChart_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == EditedIncome.AccountNumberID);

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

                if (account.LinkFileID != 7)
                {
                    return BadRequest("Wrong Link File, it should be Income file link ");
                }
            }
             
            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Income", roleId, userId, IncomeExists);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            mapper.Map(EditedIncome, IncomeExists);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            IncomeExists.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                IncomeExists.UpdatedByOctaId = userId;
                if (IncomeExists.UpdatedByUserId != null)
                {
                    IncomeExists.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                IncomeExists.UpdatedByUserId = userId;
                if (IncomeExists.UpdatedByOctaId != null)
                {
                    IncomeExists.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.income_Repository.Update(IncomeExists);
            Unit_Of_Work.SaveChanges();
            return Ok(EditedIncome);
        }

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Income" }
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
                return BadRequest("Enter Income ID");
            }

            Income income = Unit_Of_Work.income_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);

            if (income == null)
            {
                return NotFound();
            }
             
            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "Income", roleId, userId, income);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            income.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            income.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                income.DeletedByOctaId = userId;
                if (income.DeletedByUserId != null)
                {
                    income.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                income.DeletedByUserId = userId;
                if (income.DeletedByOctaId != null)
                {
                    income.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.income_Repository.Update(income);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
