using AutoMapper;
using LMS_CMS_BL.DTO.Accounting;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.AccountingModule;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace LMS_CMS_PL.Controllers.Domains.Accounting
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class OutcomeController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;

        public OutcomeController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
        }

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Outcome" }
        )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<Outcome> Outcomes = await Unit_Of_Work.outcome_Repository.Select_All_With_IncludesById<Outcome>(
                    f => f.IsDeleted != true,
                    query => query.Include(emp => emp.AccountNumber));

            if (Outcomes == null || Outcomes.Count == 0)
            {
                return NotFound();
            }

            List<OutcomeGetDTO> OutcomeGetDTOs = mapper.Map<List<OutcomeGetDTO>>(Outcomes);

            return Ok(OutcomeGetDTOs);
        }

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Outcome" }
        )]
        public async Task<IActionResult> GetById(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (id == 0)
            {
                return BadRequest("Enter Outcome ID");
            }

            Outcome outcome = await Unit_Of_Work.outcome_Repository.FindByIncludesAsync(
                    outcome => outcome.IsDeleted != true && outcome.ID == id,
                    outcome => outcome.Include(outcome => outcome.AccountNumber));

            if (outcome == null)
            {
                return NotFound();
            }

            OutcomeGetDTO OutcomeGetDTO = mapper.Map<OutcomeGetDTO>(outcome);

            return Ok(OutcomeGetDTO);
        }

        [HttpPost]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" },
           pages: new[] { "Outcome" }
       )]
        public IActionResult Add(OutcomeAddDTO NewOutcome)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (NewOutcome == null)
            {
                return BadRequest("Outcome cannot be null");
            }

            if (NewOutcome.Name == null)
            {
                return BadRequest("the name cannot be null");
            }

            AccountingTreeChart account = Unit_Of_Work.accountingTreeChart_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == NewOutcome.AccountNumberID);

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

                if (account.LinkFileID != 8)
                {
                    return BadRequest("Wrong Link File, it should be Outcome file link ");
                }
            }

            Outcome outcome = mapper.Map<Outcome>(NewOutcome);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            outcome.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                outcome.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                outcome.InsertedByUserId = userId;
            }

            Unit_Of_Work.outcome_Repository.Add(outcome);
            Unit_Of_Work.SaveChanges();
            return Ok(NewOutcome);
        }

        [HttpPut]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" },
           allowEdit: 1,
           pages: new[] { "Outcome" }
       )]
        public IActionResult Edit(OutcomePutDTO EditedOutcome)
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

            if (EditedOutcome == null)
            {
                return BadRequest("Outcome cannot be null");
            }

            if (EditedOutcome.Name == null)
            {
                return BadRequest("the name cannot be null");
            }

            Outcome OutcomeExists = Unit_Of_Work.outcome_Repository.First_Or_Default(s => s.ID == EditedOutcome.ID && s.IsDeleted != true);
            if (OutcomeExists == null || OutcomeExists.IsDeleted == true)
            {
                return NotFound("No Outcome with this ID");
            }

            AccountingTreeChart account = Unit_Of_Work.accountingTreeChart_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == EditedOutcome.AccountNumberID);

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

                if (account.LinkFileID != 8)
                {
                    return BadRequest("Wrong Link File, it should be Outcome file link ");
                }
            }
             
            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Outcome", roleId, userId, OutcomeExists);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            mapper.Map(EditedOutcome, OutcomeExists);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            OutcomeExists.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                OutcomeExists.UpdatedByOctaId = userId;
                if (OutcomeExists.UpdatedByUserId != null)
                {
                    OutcomeExists.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                OutcomeExists.UpdatedByUserId = userId;
                if (OutcomeExists.UpdatedByOctaId != null)
                {
                    OutcomeExists.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.outcome_Repository.Update(OutcomeExists);
            Unit_Of_Work.SaveChanges();
            return Ok(EditedOutcome);
        }

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Outcome" }
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
                return BadRequest("Enter outcome ID");
            }

            Outcome outcome = Unit_Of_Work.outcome_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);

            if (outcome == null)
            {
                return NotFound();
            }

            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "Outcome", roleId, userId, outcome);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            outcome.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            outcome.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                outcome.DeletedByOctaId = userId;
                if (outcome.DeletedByUserId != null)
                {
                    outcome.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                outcome.DeletedByUserId = userId;
                if (outcome.DeletedByOctaId != null)
                {
                    outcome.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.outcome_Repository.Update(outcome);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
