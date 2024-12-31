using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers.Domains
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public RoleController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        ///////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Role", "Administrator" }
        )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }
            List<Role> roles = await Unit_Of_Work.role_Repository.Select_All_With_IncludesById<Role>(
                    sem => sem.IsDeleted != true);

            if (roles == null || roles.Count == 0)
            {
                return NotFound();
            }

            List<RolesGetDTO> RolesDTO = mapper.Map<List<RolesGetDTO>>(roles);

            return Ok(RolesDTO);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Role", "Administrator" }
        )]
        public async Task<IActionResult> GetAsyncByID(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }
            Role role = await Unit_Of_Work.role_Repository.FindByIncludesAsync(
                    sem => sem.IsDeleted != true && sem.ID == id);

            if (role == null)
            {
                return NotFound();
            }

            RolesGetDTO RoleDTO = mapper.Map<RolesGetDTO>(role);

            return Ok(RoleDTO);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Role", "Administrator" }
        )]
        public async Task<IActionResult> Add(RoleAddDTO NewRoles)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }
            if (NewRoles == null)
            {
                return BadRequest("Role data is null.");
            }
            if (NewRoles.Name == null)
            {
                return BadRequest("Role Name cannot be null");
            }
            if (NewRoles.Pages == null || !NewRoles.Pages.Any())
            {
                return BadRequest("Pages list cannot be null or empty.");
            }

            Role role = Unit_Of_Work.role_Repository.First_Or_Default(r => r.Name == NewRoles.Name && r.IsDeleted != true);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            if (role != null)
            {
                return BadRequest("This Role Already Exist");
            }
            role = new Role(); 
            role.Name = NewRoles.Name;
            role.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                role.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                role.InsertedByUserId = userId;
            }
            await Unit_Of_Work.role_Repository.AddAsync(role);
            await Unit_Of_Work.SaveChangesAsync();

            role = Unit_Of_Work.role_Repository.First_Or_Default(r => r.Name == NewRoles.Name);
            if (role == null) { return NotFound(); }

            foreach (var item in NewRoles.Pages)
            {
                if (item.pageId == null) { return BadRequest("Page cannot be null."); }
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(r => r.ID == item.pageId);
                if (page == null) { return NotFound("There is no page with this ID."); }
                if (page.Page_ID != null)
                {
                    Role_Detailes role_Detailes1 = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(p => p.Page_ID == page.Page_ID && p.Role_ID== role.ID);
                    if (role_Detailes1 == null)
                    {
                        role_Detailes1 = new Role_Detailes
                        {
                            Page_ID = (long)page.Page_ID,
                            Role_ID = role.ID,
                            Allow_Delete = true,
                            Allow_Delete_For_Others = true,
                            Allow_Edit = true,
                            Allow_Edit_For_Others = true,
                            InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone),
                            InsertedByOctaId = userTypeClaim == "octa" ? userId : (long?)null,
                            InsertedByUserId = userTypeClaim == "employee" ? userId : (long?)null
                        };
                        await Unit_Of_Work.role_Detailes_Repository.AddAsync(role_Detailes1);
                        await Unit_Of_Work.SaveChangesAsync();
                    }
                }
               
                Role_Detailes role_Detailes = new Role_Detailes
                {
                    Page_ID = item.pageId,
                    Role_ID = role.ID,
                    Allow_Delete = item.Allow_Delete,
                    Allow_Delete_For_Others = item.Allow_Delete_For_Others,
                    Allow_Edit = item.Allow_Edit,
                    Allow_Edit_For_Others = item.Allow_Edit_For_Others,
                    InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone),
                    InsertedByOctaId = userTypeClaim == "octa" ? userId : (long?)null,
                    InsertedByUserId = userTypeClaim == "employee" ? userId : (long?)null
                };
                await Unit_Of_Work.role_Detailes_Repository.AddAsync(role_Detailes);
                await Unit_Of_Work.SaveChangesAsync();
            }
            return Ok();
        }
        ////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Role", "Administrator" }
        )]
        public async Task<IActionResult> Edit(RolePutDTO newRole)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (newRole == null)
            {
                return BadRequest("Role cannot be null");
            }
            if (newRole.Name == null)
            {
                return BadRequest("Role Name cannot be null");
            }

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            Role role = Unit_Of_Work.role_Repository.First_Or_Default(r => r.ID == newRole.ID);
            if (role == null)
            {
                return BadRequest("Role cannot be null");
            }
            if (role.Name != newRole.Name) 
            { 
              role.Name = newRole.Name;
            role.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                role.UpdatedByOctaId = userId;
                if (role.UpdatedByUserId != null)
                {
                    role.UpdatedByUserId = null;
                }

            }
            else if (userTypeClaim == "employee")
            {
                role.UpdatedByUserId = userId;
                if (role.UpdatedByOctaId != null)
                {
                    role.UpdatedByOctaId = null;
                }
            }
                Unit_Of_Work.role_Repository.Update(role);
                Unit_Of_Work.SaveChanges();
            }

            List<Role_Detailes> role_Detailes =Unit_Of_Work.role_Detailes_Repository.FindBy(r=>r.Role_ID==newRole.ID); 
            if (role_Detailes != null)
            {
                foreach (var item in role_Detailes)
                {
                    await Unit_Of_Work.role_Detailes_Repository.DeleteAsync(item.ID);
                    await Unit_Of_Work.SaveChangesAsync();
                }
            }


            foreach (var item in newRole.Pages)
            {
                if (item.pageId == null) { return BadRequest("Page cannot be null."); }
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(r => r.ID == item.pageId);
                if (page == null) { return NotFound("There is no page with this ID."); }
                if (page.Page_ID != null)
                {
                    Role_Detailes role_Detailes1 = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(p => p.Page_ID == page.Page_ID && p.Role_ID == role.ID);
                    if (role_Detailes1 == null)
                    {
                        role_Detailes1 = new Role_Detailes
                        {
                            Page_ID = (long)page.Page_ID,
                            Role_ID = role.ID,
                            Allow_Delete = true,
                            Allow_Delete_For_Others = true,
                            Allow_Edit = true,
                            Allow_Edit_For_Others = true,
                            InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone),
                            InsertedByOctaId = userTypeClaim == "octa" ? userId : (long?)null,
                            InsertedByUserId = userTypeClaim == "employee" ? userId : (long?)null
                        };
                        await Unit_Of_Work.role_Detailes_Repository.AddAsync(role_Detailes1);
                        await Unit_Of_Work.SaveChangesAsync();
                    }
                }

                Role_Detailes role_Detailes2 = new Role_Detailes
                {
                    Page_ID = item.pageId,
                    Role_ID = role.ID,
                    Allow_Delete = item.Allow_Delete,
                    Allow_Delete_For_Others = item.Allow_Delete_For_Others,
                    Allow_Edit = item.Allow_Edit,
                    Allow_Edit_For_Others = item.Allow_Edit_For_Others,
                    InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone),
                    InsertedByOctaId = userTypeClaim == "octa" ? userId : (long?)null,
                    InsertedByUserId = userTypeClaim == "employee" ? userId : (long?)null
                };
                await Unit_Of_Work.role_Detailes_Repository.AddAsync(role_Detailes2);
                await Unit_Of_Work.SaveChangesAsync();
            }


            return Ok(newRole);
        }
        //////////////////////////////////////////////////////

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Role", "Administrator" }
        )]
        public IActionResult delete(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (id == null)
            {
                return BadRequest("id cannot be null");
            }
            Role role = Unit_Of_Work.role_Repository.Select_By_Id(id);

            if (role == null || role.IsDeleted == true)
            {
                return NotFound("No Role with this ID");
            }
            role.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            role.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                role.DeletedByOctaId = userId;
                if (role.DeletedByUserId != null)
                {
                    role.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                role.DeletedByUserId = userId;
                if (role.DeletedByOctaId != null)
                {
                    role.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.role_Repository.Update(role);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }


    }
}
