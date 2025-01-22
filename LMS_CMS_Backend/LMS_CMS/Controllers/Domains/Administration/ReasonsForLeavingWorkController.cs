using AutoMapper;
using LMS_CMS_BL.DTO.Administration;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.Administration;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Controllers.Domains.Administration
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class ReasonsForLeavingWorkController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public ReasonsForLeavingWorkController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        ///////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
       allowedTypes: new[] { "octa", "employee" },
       pages: new[] { "Reasons For Leaving Work", "Administrator" }
       )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<ReasonForLeavingWork> reasonForLeavingWorks = await Unit_Of_Work.reasonForLeavingWork_Repository.Select_All_With_IncludesById<ReasonForLeavingWork>(
                    b => b.IsDeleted != true);

            if (reasonForLeavingWorks == null || reasonForLeavingWorks.Count == 0)
            {
                return NotFound();
            }

            List<ReasonsForLeavingWorkGetDTO> Dto = mapper.Map<List<ReasonsForLeavingWorkGetDTO>>(reasonForLeavingWorks);

            return Ok(Dto);
        }

        //////////////////////////////////////////////////////////////////////////////
        [HttpGet("{id}")]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" },
         pages: new[] { "Reasons For Leaving Work", "Administrator" }
         )]
        public async Task<IActionResult> GetbyIdAsync(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            ReasonForLeavingWork reasonForLeavingWork = Unit_Of_Work.reasonForLeavingWork_Repository.First_Or_Default(d => d.ID == id && d.IsDeleted != true);

            if (reasonForLeavingWork == null)
            {
                return NotFound();
            }

            ReasonsForLeavingWorkGetDTO Dto = mapper.Map<ReasonsForLeavingWorkGetDTO>(reasonForLeavingWork);

            return Ok(Dto);
        }
        //////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" },
         pages: new[] { "Reasons For Leaving Work", "Administrator" }
       )]
        public IActionResult Add(ReasonsForLeavingWorkAddDTO newReason)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (newReason == null)
            {
                return BadRequest("Reason cannot be null");
            }
            ReasonForLeavingWork Reason = mapper.Map<ReasonForLeavingWork>(newReason);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            Reason.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                Reason.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                Reason.InsertedByUserId = userId;
            }

            Unit_Of_Work.reasonForLeavingWork_Repository.Add(Reason);
            Unit_Of_Work.SaveChanges();
            return Ok(newReason);
        }

        //////////////////////////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
        allowedTypes: new[] { "octa", "employee" },
        allowEdit: 1,
       pages: new[] { "Reasons For Leaving Work", "Administrator" }
    )]
        public IActionResult Edit(ReasonsForLeavingWorkGetDTO newReason)
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

            if (newReason == null)
            {
                return BadRequest("Reason cannot be null");
            }

            ReasonForLeavingWork Reason = Unit_Of_Work.reasonForLeavingWork_Repository.First_Or_Default(d => d.ID == newReason.ID && d.IsDeleted != true);
            if (Reason == null)
            {
                return NotFound("There is no Reason with this id");
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Reasons For Leaving Work");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (Reason.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Reasons For Leaving Work page doesn't exist");
                }
            }

            mapper.Map(newReason, Reason);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            Reason.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                Reason.UpdatedByOctaId = userId;
                if (Reason.UpdatedByUserId != null)
                {
                    Reason.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                Reason.UpdatedByUserId = userId;
                if (Reason.UpdatedByOctaId != null)
                {
                    Reason.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.reasonForLeavingWork_Repository.Update(Reason);
            Unit_Of_Work.SaveChanges();
            return Ok(newReason);
        }

        //////////////////////////////////////////////////////////////////////////////////

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
          allowedTypes: new[] { "octa", "employee" },
          allowDelete: 1,
         pages: new[] { "Reasons For Leaving Work", "Administrator" }
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
                return BadRequest("Enter Reasons For Leaving Work ID");
            }

            ReasonForLeavingWork reason = Unit_Of_Work.reasonForLeavingWork_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);


            if (reason == null)
            {
                return NotFound();
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Reasons For Leaving Work");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Delete_For_Others == false)
                    {
                        if (reason.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Reasons For Leaving Work page doesn't exist");
                }
            }

            reason.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            reason.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                reason.DeletedByOctaId = userId;
                if (reason.DeletedByUserId != null)
                {
                    reason.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                reason.DeletedByUserId = userId;
                if (reason.DeletedByOctaId != null)
                {
                    reason.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.reasonForLeavingWork_Repository.Update(reason);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
