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

namespace LMS_CMS_PL.Controllers.Domains.Accounting
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class ReceivableDocTypeController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public ReceivableDocTypeController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        ///////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" },
           pages: new[] { "Receivable Doc Type", "Accounting" }
           )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<ReceivableDocType> ReceivableDocTypes = await Unit_Of_Work.receivableDocType_Repository.Select_All_With_IncludesById<ReceivableDocType>(
                    b => b.IsDeleted != true);

            if (ReceivableDocTypes == null || ReceivableDocTypes.Count == 0)
            {
                return NotFound();
            }

            List<ReceivableDocTypeGetDTO> DTOs = mapper.Map<List<ReceivableDocTypeGetDTO>>(ReceivableDocTypes);

            return Ok(DTOs);
        }

        //////////////////////////////////////////////////////////////////////////////
        ///
        [HttpGet("{id}")]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" },
       pages: new[] { "Receivable Doc Type", "Accounting" }
         )]
        public async Task<IActionResult> GetbyIdAsync(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if(id == 0 || id == null)
            {
                return BadRequest("Enter Doc Type ID");
            }

            ReceivableDocType ReceivableDocType = Unit_Of_Work.receivableDocType_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);


            if (ReceivableDocType == null)
            {
                return NotFound();
            }

            ReceivableDocTypeGetDTO dto = mapper.Map<ReceivableDocTypeGetDTO>(ReceivableDocType);

            return Ok(dto);
        }

        //////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" },
         pages: new[] { "Receivable Doc Type", "Accounting" }
        )]
        public IActionResult Add(ReceivableDocTypeAddDTO newDoc)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (newDoc == null)
            {
                return BadRequest("Receivable Doc Type cannot be null");
            }

            ReceivableDocType ReceivableDocType = mapper.Map<ReceivableDocType>(newDoc);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            ReceivableDocType.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                ReceivableDocType.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                ReceivableDocType.InsertedByUserId = userId;
            }

            Unit_Of_Work.receivableDocType_Repository.Add(ReceivableDocType);
            Unit_Of_Work.SaveChanges();
            return Ok(newDoc);
        }

        //////////////////////////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Receivable Doc Type", "Accounting" }
        )]
        public IActionResult Edit(ReceivableDocTypePutDTO newDoc)
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

            if (newDoc == null)
            {
                return BadRequest("Receivable Doc Type cannot be null");
            }

            ReceivableDocType ReceivableDocType = Unit_Of_Work.receivableDocType_Repository.First_Or_Default(d => d.ID == newDoc.ID && d.IsDeleted != true);
            if (ReceivableDocType == null)
            {
                return NotFound("There is no Receivable Doc Type with this id");
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Receivable Doc Type");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (ReceivableDocType.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Receivable Doc Type page doesn't exist");
                }
            }

            mapper.Map(newDoc, ReceivableDocType);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            ReceivableDocType.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                ReceivableDocType.UpdatedByOctaId = userId;
                if (ReceivableDocType.UpdatedByUserId != null)
                {
                    ReceivableDocType.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                ReceivableDocType.UpdatedByUserId = userId;
                if (ReceivableDocType.UpdatedByOctaId != null)
                {
                    ReceivableDocType.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.receivableDocType_Repository.Update(ReceivableDocType);
            Unit_Of_Work.SaveChanges();
            return Ok(newDoc);
        }

        //////////////////////////////////////////////////////////////////////////////////

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Receivable Doc Type", "Accounting" }
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
                return BadRequest("Enter Receivable Doc Type ID");
            }

            ReceivableDocType ReceivableDocType = Unit_Of_Work.receivableDocType_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);


            if (ReceivableDocType == null)
            {
                return NotFound();
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Receivable Doc Type");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Delete_For_Others == false)
                    {
                        if (ReceivableDocType.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Receivable Doc Type page doesn't exist");
                }
            }

            ReceivableDocType.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            ReceivableDocType.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                ReceivableDocType.DeletedByOctaId = userId;
                if (ReceivableDocType.DeletedByUserId != null)
                {
                    ReceivableDocType.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                ReceivableDocType.DeletedByUserId = userId;
                if (ReceivableDocType.DeletedByOctaId != null)
                {
                    ReceivableDocType.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.receivableDocType_Repository.Update(ReceivableDocType);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
