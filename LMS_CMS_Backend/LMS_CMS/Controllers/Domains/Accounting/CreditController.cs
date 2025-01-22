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
    public class CreditController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public CreditController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" },
           pages: new[] { "Credit", "Accounting" }
       )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<Credit> Credits = await Unit_Of_Work.credit_Repository.Select_All_With_IncludesById<Credit>(
                    f => f.IsDeleted != true,
                    query => query.Include(credit => credit.AccountNumber));

            if (Credits == null || Credits.Count == 0)
            {
                return NotFound();
            }

            List<CreditGetDTO> CreditsDTO = mapper.Map<List<CreditGetDTO>>(Credits);

            return Ok(CreditsDTO);
        }

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Credit", "Accounting" }
        )]
        public async Task<IActionResult> GetById(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (id == 0)
            {
                return BadRequest("Enter Credit ID");
            }

            Credit credit = await Unit_Of_Work.credit_Repository.FindByIncludesAsync(
                    credit => credit.IsDeleted != true && credit.ID == id,
                    query => query.Include(credit => credit.AccountNumber));

            if (credit == null)
            {
                return NotFound();
            }

            CreditGetDTO creditGetDTO = mapper.Map<CreditGetDTO>(credit);

            return Ok(creditGetDTO);
        }

        [HttpPost]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" },
           pages: new[] { "Credit", "Accounting" }
       )]
        public IActionResult Add(CreditAddDTO NewCredit)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (NewCredit == null)
            {
                return BadRequest("Credit cannot be null");
            }

            if (NewCredit.Name == null)
            {
                return BadRequest("the name cannot be null");
            }

            AccountingTreeChart account = Unit_Of_Work.accountingTreeChart_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == NewCredit.AccountNumberID);

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

                if (account.LinkFileID != 4)
                {
                    return BadRequest("Wrong Link File, it should be Credit file link ");
                }
            }

            Credit credit = mapper.Map<Credit>(NewCredit);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            credit.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                credit.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                credit.InsertedByUserId = userId;
            }

            Unit_Of_Work.credit_Repository.Add(credit);
            Unit_Of_Work.SaveChanges();
            return Ok(NewCredit);
        }

        [HttpPut]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" },
           allowEdit: 1,
           pages: new[] { "Credit", "Accounting" }
       )]
        public IActionResult Edit(CreditPutDTO EditedCredit)
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

            if (EditedCredit == null)
            {
                return BadRequest("Credit cannot be null");
            }

            if (EditedCredit.Name == null)
            {
                return BadRequest("the name cannot be null");
            }

            Credit CreditExists = Unit_Of_Work.credit_Repository.First_Or_Default(s => s.ID == EditedCredit.ID && s.IsDeleted != true);
            if (CreditExists == null || CreditExists.IsDeleted == true)
            {
                return NotFound("No Credit with this ID");
            }

            AccountingTreeChart account = Unit_Of_Work.accountingTreeChart_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == EditedCredit.AccountNumberID);

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

                if (account.LinkFileID != 4)
                {
                    return BadRequest("Wrong Link File, it should be Credit file link ");
                }
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Credit");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (CreditExists.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Credit page doesn't exist");
                }
            }

            mapper.Map(EditedCredit, CreditExists);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            CreditExists.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                CreditExists.UpdatedByOctaId = userId;
                if (CreditExists.UpdatedByUserId != null)
                {
                    CreditExists.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                CreditExists.UpdatedByUserId = userId;
                if (CreditExists.UpdatedByOctaId != null)
                {
                    CreditExists.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.credit_Repository.Update(CreditExists);
            Unit_Of_Work.SaveChanges();
            return Ok(EditedCredit);
        }

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Credit", "Accounting" }
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
                return BadRequest("Enter Credit ID");
            }

            Credit credit = Unit_Of_Work.credit_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);

            if (credit == null)
            {
                return NotFound();
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Credit");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Delete_For_Others == false)
                    {
                        if (credit.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Credit page doesn't exist");
                }
            }

            credit.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            credit.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                credit.DeletedByOctaId = userId;
                if (credit.DeletedByUserId != null)
                {
                    credit.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                credit.DeletedByUserId = userId;
                if (credit.DeletedByOctaId != null)
                {
                    credit.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.credit_Repository.Update(credit);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
