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
    public class TuitionDiscountTypesController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;

        public TuitionDiscountTypesController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
        }

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Tuition Discount Type" }
        )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<TuitionDiscountType> tuitionDiscountTypes = await Unit_Of_Work.tuitionDiscountType_Repository.Select_All_With_IncludesById<TuitionDiscountType>(
                    f => f.IsDeleted != true,
                    query => query.Include(emp => emp.AccountNumber));

            if (tuitionDiscountTypes == null || tuitionDiscountTypes.Count == 0)
            {
                return NotFound();
            }

            List<TuitionDiscountTypeGetDTO> DTOS = mapper.Map<List<TuitionDiscountTypeGetDTO>>(tuitionDiscountTypes);

            return Ok(DTOS);
        }

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Tuition Discount Type" }
        )]
        public async Task<IActionResult> GetById(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (id == 0)
            {
                return BadRequest("Enter Tuition Discount Type ID");
            }

            TuitionDiscountType tuitionDiscount = await Unit_Of_Work.tuitionDiscountType_Repository.FindByIncludesAsync(
                    t => t.IsDeleted != true && t.ID == id,
                    query => query.Include(t => t.AccountNumber));

            if (tuitionDiscount == null)
            {
                return NotFound();
            }

            TuitionDiscountTypeGetDTO DTO = mapper.Map<TuitionDiscountTypeGetDTO>(tuitionDiscount);

            return Ok(DTO);
        }

        [HttpPost]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Tuition Discount Type" }
        )]
        public IActionResult Add(TuitionDiscountTypeAddDTO NewType)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (NewType == null)
            {
                return BadRequest("type cannot be null");
            }

            if (NewType.Name == null)
            {
                return BadRequest("the name cannot be null");
            }

            AccountingTreeChart account = Unit_Of_Work.accountingTreeChart_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == NewType.AccountNumberID);

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

                if (account.LinkFileID != 12)
                {
                    return BadRequest("Wrong Link File, it should be Tuition DiscountType file link ");
                }
            }

            TuitionDiscountType tuitionDiscountType = mapper.Map<TuitionDiscountType>(NewType);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            tuitionDiscountType.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                tuitionDiscountType.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                tuitionDiscountType.InsertedByUserId = userId;
            }

            Unit_Of_Work.tuitionDiscountType_Repository.Add(tuitionDiscountType);
            Unit_Of_Work.SaveChanges();
            return Ok(NewType);
        }

        [HttpPut]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" },
           allowEdit: 1,
            pages: new[] { "Tuition Discount Type" }
       )]
        public IActionResult Edit(TuitionDiscountTypePutDTO newType)
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

            if (newType == null)
            {
                return BadRequest("Save cannot be null");
            }

            if (newType.Name == null)
            {
                return BadRequest("the name cannot be null");
            }

            TuitionDiscountType tuitionDiscountType = Unit_Of_Work.tuitionDiscountType_Repository.First_Or_Default(s => s.ID == newType.ID && s.IsDeleted != true);
            if (tuitionDiscountType == null || tuitionDiscountType.IsDeleted == true)
            {
                return NotFound("No tuition Discount Type with this ID");
            }

            AccountingTreeChart account = Unit_Of_Work.accountingTreeChart_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == newType.AccountNumberID);

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

                if (account.LinkFileID != 12)
                {
                    return BadRequest("Wrong Link File, it should be Tuition Discount Type file link ");
                }
            }

            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Tuition Discount Type", roleId, userId, tuitionDiscountType);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            mapper.Map(newType, tuitionDiscountType);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            tuitionDiscountType.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                tuitionDiscountType.UpdatedByOctaId = userId;
                if (tuitionDiscountType.UpdatedByUserId != null)
                {
                    tuitionDiscountType.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                tuitionDiscountType.UpdatedByUserId = userId;
                if (tuitionDiscountType.UpdatedByOctaId != null)
                {
                    tuitionDiscountType.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.tuitionDiscountType_Repository.Update(tuitionDiscountType);
            Unit_Of_Work.SaveChanges();
            return Ok(newType);
        }

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Tuition Discount Type" }
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
                return BadRequest("Enter Tuition Discount Type ID");
            }

            TuitionDiscountType tuitionDiscountType = Unit_Of_Work.tuitionDiscountType_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);

            if (tuitionDiscountType == null)
            {
                return NotFound();
            }
             
            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "Tuition Discount Type", roleId, userId, tuitionDiscountType);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            tuitionDiscountType.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            tuitionDiscountType.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                tuitionDiscountType.DeletedByOctaId = userId;
                if (tuitionDiscountType.DeletedByUserId != null)
                {
                    tuitionDiscountType.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                tuitionDiscountType.DeletedByUserId = userId;
                if (tuitionDiscountType.DeletedByOctaId != null)
                {
                    tuitionDiscountType.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.tuitionDiscountType_Repository.Update(tuitionDiscountType);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
