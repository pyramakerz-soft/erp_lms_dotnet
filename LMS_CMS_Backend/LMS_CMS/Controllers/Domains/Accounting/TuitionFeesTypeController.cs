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
    [Authorize]
    public class TuitionFeesTypeController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;

        public TuitionFeesTypeController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
        }

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Tuition Fees Type" }
        )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<TuitionFeesType> TuitionFeesTypes = await Unit_Of_Work.tuitionFeesType_Repository.Select_All_With_IncludesById<TuitionFeesType>(
                    f => f.IsDeleted != true,
                    query => query.Include(emp => emp.AccountNumber));

            if (TuitionFeesTypes == null || TuitionFeesTypes.Count == 0)
            {
                return NotFound();
            }

            List<TuitionFeesTypeGetDTO> TuitionFeesTypeGetDTOs = mapper.Map<List<TuitionFeesTypeGetDTO>>(TuitionFeesTypes);

            return Ok(TuitionFeesTypeGetDTOs);
        }

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Tuition Fees Type" }
        )]
        public async Task<IActionResult> GetById(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (id == 0)
            {
                return BadRequest("Enter Tuition Fees Type ID");
            }

            TuitionFeesType tuitionFeesType = await Unit_Of_Work.tuitionFeesType_Repository.FindByIncludesAsync(
                    TuitionFeesType => TuitionFeesType.IsDeleted != true && TuitionFeesType.ID == id,
                    query => query.Include(TuitionFeesType => TuitionFeesType.AccountNumber));

            if (tuitionFeesType == null)
            {
                return NotFound();
            }

            TuitionFeesTypeGetDTO tuitionFeesTypeGetDTO = mapper.Map<TuitionFeesTypeGetDTO>(tuitionFeesType);

            return Ok(tuitionFeesTypeGetDTO);
        }

        [HttpPost]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" },
           pages: new[] { "Tuition Fees Type" }
        )]
        public IActionResult Add(TuitionFeesTypeAddDTO NewTuitionFeesType)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (NewTuitionFeesType == null)
            {
                return BadRequest("Tuition Fees Type cannot be null");
            }

            if (NewTuitionFeesType.Name == null)
            {
                return BadRequest("the name cannot be null");
            }

            AccountingTreeChart account = Unit_Of_Work.accountingTreeChart_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == NewTuitionFeesType.AccountNumberID);

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

                if (account.LinkFileID != 11)
                {
                    return BadRequest("Wrong Link File, it should be Tuition Fees Type file link");
                }
            }

            TuitionFeesType tuitionFeesType = mapper.Map<TuitionFeesType>(NewTuitionFeesType);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            tuitionFeesType.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                tuitionFeesType.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                tuitionFeesType.InsertedByUserId = userId;
            }

            Unit_Of_Work.tuitionFeesType_Repository.Add(tuitionFeesType);
            Unit_Of_Work.SaveChanges();
            return Ok(NewTuitionFeesType);
        }

        [HttpPut]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" },
           allowEdit: 1,
           pages: new[] { "Tuition Fees Type" }
       )]
        public IActionResult Edit(TuitionFeesTypePutDTO EditedTuitionFeesType)
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

            if (EditedTuitionFeesType == null)
            {
                return BadRequest("Tuition Fees Type cannot be null");
            }

            if (EditedTuitionFeesType.Name == null)
            {
                return BadRequest("the name cannot be null");
            }

            TuitionFeesType TuitionFeesTypeExists = Unit_Of_Work.tuitionFeesType_Repository.First_Or_Default(s => s.ID == EditedTuitionFeesType.ID && s.IsDeleted != true);
            if (TuitionFeesTypeExists == null || TuitionFeesTypeExists.IsDeleted == true)
            {
                return NotFound("No Tuition Fees Type with this ID");
            }

            AccountingTreeChart account = Unit_Of_Work.accountingTreeChart_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == EditedTuitionFeesType.AccountNumberID);

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

                if (account.LinkFileID != 11)
                {
                    return BadRequest("Wrong Link File, it should be Tuition Fees Type file link ");
                }
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Tuition Fees Type");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (TuitionFeesTypeExists.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Tuition Fees Type page doesn't exist");
                }
            }
            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Tuition Fees Type", roleId, userId, TuitionFeesTypeExists);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            mapper.Map(EditedTuitionFeesType, TuitionFeesTypeExists);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            TuitionFeesTypeExists.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                TuitionFeesTypeExists.UpdatedByOctaId = userId;
                if (TuitionFeesTypeExists.UpdatedByUserId != null)
                {
                    TuitionFeesTypeExists.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                TuitionFeesTypeExists.UpdatedByUserId = userId;
                if (TuitionFeesTypeExists.UpdatedByOctaId != null)
                {
                    TuitionFeesTypeExists.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.tuitionFeesType_Repository.Update(TuitionFeesTypeExists);
            Unit_Of_Work.SaveChanges();
            return Ok(EditedTuitionFeesType);
        }

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Tuition Fees Type" }
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
                return BadRequest("Enter Tuition Fees Type ID");
            }

            TuitionFeesType tuitionFeesType = Unit_Of_Work.tuitionFeesType_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);

            if (tuitionFeesType == null)
            {
                return NotFound();
            }
             
            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "Tuition Fees Type", roleId, userId, tuitionFeesType);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            tuitionFeesType.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            tuitionFeesType.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                tuitionFeesType.DeletedByOctaId = userId;
                if (tuitionFeesType.DeletedByUserId != null)
                {
                    tuitionFeesType.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                tuitionFeesType.DeletedByUserId = userId;
                if (tuitionFeesType.DeletedByOctaId != null)
                {
                    tuitionFeesType.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.tuitionFeesType_Repository.Update(tuitionFeesType);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
