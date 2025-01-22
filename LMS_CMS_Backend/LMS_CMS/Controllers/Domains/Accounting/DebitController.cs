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

namespace LMS_CMS_PL.Controllers.Domains.Accounting
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
<<<<<<< HEAD
=======
    [Authorize]
>>>>>>> 337321e05d7546533b90f158b397f534dcfb0ef4
    public class DebitController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public DebitController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Debit", "Accounting" }
        )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);
             
            List<Debit> Debits = await Unit_Of_Work.debit_Repository.Select_All_With_IncludesById<Debit>(
                    f => f.IsDeleted != true,
                    query => query.Include(emp => emp.AccountNumber));

            if (Debits == null || Debits.Count == 0)
            {
                return NotFound();
            }

            List<DebitGetDTO> DebitsDTO = mapper.Map<List<DebitGetDTO>>(Debits);

            return Ok(DebitsDTO);
        }

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Debit", "Accounting" }
        )]
        public async Task<IActionResult> GetById(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (id == 0)
            {
                return BadRequest("Enter Debit ID");
            }

            Debit debit = await Unit_Of_Work.debit_Repository.FindByIncludesAsync(
                    debit => debit.IsDeleted != true && debit.ID == id,
                    query => query.Include(debit => debit.AccountNumber));

            if (debit == null)
            {
                return NotFound();
            }

            DebitGetDTO debitGetDTO = mapper.Map<DebitGetDTO>(debit);

            return Ok(debitGetDTO);
        }

        [HttpPost]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" },
           pages: new[] { "Debit", "Accounting" }
       )]
        public IActionResult Add(DebitAddDTO NewDebit)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (NewDebit == null)
            {
                return BadRequest("Debit cannot be null");
            }

            if (NewDebit.Name == null)
            {
                return BadRequest("the name cannot be null");
            }

            AccountingTreeChart account = Unit_Of_Work.accountingTreeChart_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == NewDebit.AccountNumberID);

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

                if (account.LinkFileID != 3)
                {
                    return BadRequest("Wrong Link File, it should be Debit file link ");
                }
            }

            Debit debit = mapper.Map<Debit>(NewDebit);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            debit.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                debit.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                debit.InsertedByUserId = userId;
            }

            Unit_Of_Work.debit_Repository.Add(debit);
            Unit_Of_Work.SaveChanges();
            return Ok(NewDebit);
        }

        [HttpPut]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" },
           allowEdit: 1,
           pages: new[] { "Debit", "Accounting" }
       )]
        public IActionResult Edit(DebitPutDTO EditedDebit)
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

            if (EditedDebit == null)
            {
                return BadRequest("Debit cannot be null");
            }

            if (EditedDebit.Name == null)
            {
                return BadRequest("the name cannot be null");
            }

            Debit DebitExists = Unit_Of_Work.debit_Repository.First_Or_Default(s => s.ID == EditedDebit.ID && s.IsDeleted != true);
            if (DebitExists == null || DebitExists.IsDeleted == true)
            {
                return NotFound("No Debit with this ID");
            }

            AccountingTreeChart account = Unit_Of_Work.accountingTreeChart_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == EditedDebit.AccountNumberID);

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

                if (account.LinkFileID != 3)
                {
                    return BadRequest("Wrong Link File, it should be Debit file link ");
                }
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Debit");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (DebitExists.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Debit page doesn't exist");
                }
            }

            mapper.Map(EditedDebit, DebitExists);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            DebitExists.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                DebitExists.UpdatedByOctaId = userId;
                if (DebitExists.UpdatedByUserId != null)
                {
                    DebitExists.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                DebitExists.UpdatedByUserId = userId;
                if (DebitExists.UpdatedByOctaId != null)
                {
                    DebitExists.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.debit_Repository.Update(DebitExists);
            Unit_Of_Work.SaveChanges();
            return Ok(EditedDebit);
        }

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Debit", "Accounting" }
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
                return BadRequest("Enter Debit ID");
            }

            Debit debit = Unit_Of_Work.debit_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);

            if (debit == null)
            {
                return NotFound();
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Debit");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Delete_For_Others == false)
                    {
                        if (debit.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Debit page doesn't exist");
                }
            }

            debit.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            debit.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                debit.DeletedByOctaId = userId;
                if (debit.DeletedByUserId != null)
                {
                    debit.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                debit.DeletedByUserId = userId;
                if (debit.DeletedByOctaId != null)
                {
                    debit.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.debit_Repository.Update(debit);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
