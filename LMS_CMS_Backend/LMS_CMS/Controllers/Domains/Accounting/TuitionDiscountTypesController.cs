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

        public TuitionDiscountTypesController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Tuition Discount Type", "Accounting" }
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
            pages: new[] { "Tuition Discount Type", "Accounting" }
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
           pages: new[] { "Save", "Accounting" }
        )]
        public IActionResult Add(TuitionDiscountTypeAddDTO N)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (NewSave == null)
            {
                return BadRequest("Save cannot be null");
            }

            if (NewSave.Name == null)
            {
                return BadRequest("the name cannot be null");
            }

            AccountingTreeChart account = Unit_Of_Work.accountingTreeChart_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == NewSave.AccountNumberID);

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
                    return BadRequest("Wrong Link File, it should be Save file link ");
                }
            }

            Save save = mapper.Map<Save>(NewSave);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            save.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                save.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                save.InsertedByUserId = userId;
            }

            Unit_Of_Work.tuitionDiscountType_Repository.Add(save);
            Unit_Of_Work.SaveChanges();
            return Ok(NewSave);
        }

        [HttpPut]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" },
           allowEdit: 1,
           pages: new[] { "Save", "Accounting" }
       )]
        public IActionResult Edit(SavePutDTO EditedSave)
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

            if (EditedSave == null)
            {
                return BadRequest("Save cannot be null");
            }

            if (EditedSave.Name == null)
            {
                return BadRequest("the name cannot be null");
            }

            Save SaveExists = Unit_Of_Work.tuitionDiscountType_Repository.First_Or_Default(s => s.ID == EditedSave.ID && s.IsDeleted != true);
            if (SaveExists == null || SaveExists.IsDeleted == true)
            {
                return NotFound("No Save with this ID");
            }

            AccountingTreeChart account = Unit_Of_Work.accountingTreeChart_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == EditedSave.AccountNumberID);

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

                if (account.LinkFileID != 5)
                {
                    return BadRequest("Wrong Link File, it should be Save file link ");
                }
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Save");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (SaveExists.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Save page doesn't exist");
                }
            }

            mapper.Map(EditedSave, SaveExists);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            SaveExists.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                SaveExists.UpdatedByOctaId = userId;
                if (SaveExists.UpdatedByUserId != null)
                {
                    SaveExists.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                SaveExists.UpdatedByUserId = userId;
                if (SaveExists.UpdatedByOctaId != null)
                {
                    SaveExists.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.tuitionDiscountType_Repository.Update(SaveExists);
            Unit_Of_Work.SaveChanges();
            return Ok(EditedSave);
        }

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Save", "Accounting" }
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
                return BadRequest("Enter Save ID");
            }

            Save save = Unit_Of_Work.tuitionDiscountType_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);

            if (save == null)
            {
                return NotFound();
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Save");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Delete_For_Others == false)
                    {
                        if (save.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Save page doesn't exist");
                }
            }

            save.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            save.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                save.DeletedByOctaId = userId;
                if (save.DeletedByUserId != null)
                {
                    save.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                save.DeletedByUserId = userId;
                if (save.DeletedByOctaId != null)
                {
                    save.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.tuitionDiscountType_Repository.Update(save);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
