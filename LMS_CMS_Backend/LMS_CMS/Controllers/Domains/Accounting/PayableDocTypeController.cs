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

namespace LMS_CMS_PL.Controllers.Domains.Accounting
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class PayableDocTypeController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public PayableDocTypeController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        ///////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" },
           pages: new[] { "Payable Doc Type", "Accounting" }
           )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<PayableDocType> PayableDocType = await Unit_Of_Work.payableDocType_Repository.Select_All_With_IncludesById<PayableDocType>(
                    b => b.IsDeleted != true);

            if (PayableDocType == null || PayableDocType.Count == 0)
            {
                return NotFound();
            }

            List<PayableDocTypeGetDTO> DTOs = mapper.Map<List<PayableDocTypeGetDTO>>(PayableDocType);

            return Ok(DTOs);
        }

        //////////////////////////////////////////////////////////////////////////////
         
        [HttpGet("{id}")]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" },
       pages: new[] { "Payable Doc Type", "Accounting" }
         )]
        public async Task<IActionResult> GetbyIdAsync(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (id == 0 || id == null)
            {
                return BadRequest("Enter Doc Type ID");
            }

            PayableDocType PayableDocType = Unit_Of_Work.payableDocType_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);


            if (PayableDocType == null)
            {
                return NotFound();
            }

            PayableDocTypeGetDTO dto = mapper.Map<PayableDocTypeGetDTO>(PayableDocType);

            return Ok(dto);
        }

        //////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" },
         pages: new[] { "Payable Doc Type", "Accounting" }
        )]
        public IActionResult Add(PayableDocTypeAddDTO newDoc)
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
                return BadRequest("Payable Doc Type cannot be null");
            }

            PayableDocType PayableDocType = mapper.Map<PayableDocType>(newDoc);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            PayableDocType.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                PayableDocType.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                PayableDocType.InsertedByUserId = userId;
            }

            Unit_Of_Work.payableDocType_Repository.Add(PayableDocType);
            Unit_Of_Work.SaveChanges();
            return Ok(newDoc);
        }

        //////////////////////////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Payable Doc Type", "Accounting" }
        )]
        public IActionResult Edit(PayableDocTypePutDTO newDoc)
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
                return BadRequest("Payable Doc Type cannot be null");
            }

            PayableDocType PayableDocType = Unit_Of_Work.payableDocType_Repository.First_Or_Default(d => d.ID == newDoc.ID && d.IsDeleted != true);
            if (PayableDocType == null)
            {
                return NotFound("There is no Payable Doc Type with this id");
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Payable Doc Type");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (PayableDocType.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Payable Doc Type page doesn't exist");
                }
            }

            mapper.Map(newDoc, PayableDocType);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            PayableDocType.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                PayableDocType.UpdatedByOctaId = userId;
                if (PayableDocType.UpdatedByUserId != null)
                {
                    PayableDocType.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                PayableDocType.UpdatedByUserId = userId;
                if (PayableDocType.UpdatedByOctaId != null)
                {
                    PayableDocType.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.payableDocType_Repository.Update(PayableDocType);
            Unit_Of_Work.SaveChanges();
            return Ok(newDoc);
        }

        //////////////////////////////////////////////////////////////////////////////////

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Payable Doc Type", "Accounting" }
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
                return BadRequest("Enter Payable Doc Type ID");
            }

            PayableDocType PayableDocType = Unit_Of_Work.payableDocType_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);


            if (PayableDocType == null)
            {
                return NotFound();
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Payable Doc Type");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Delete_For_Others == false)
                    {
                        if (PayableDocType.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Payable Doc Type page doesn't exist");
                }
            }

            PayableDocType.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            PayableDocType.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                PayableDocType.DeletedByOctaId = userId;
                if (PayableDocType.DeletedByUserId != null)
                {
                    PayableDocType.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                PayableDocType.DeletedByUserId = userId;
                if (PayableDocType.DeletedByOctaId != null)
                {
                    PayableDocType.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.payableDocType_Repository.Update(PayableDocType);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
