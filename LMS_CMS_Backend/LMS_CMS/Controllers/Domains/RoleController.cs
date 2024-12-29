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
                return NotFound();
            }

            /// Create Role
            Role role = new Role();
            role.Name= NewRoles.Name;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
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

            /// Create Role Drtails
            role= Unit_Of_Work.role_Repository.First_Or_Default(r=>r.Name==NewRoles.Name);
            if(role == null) { return NotFound(); }
            if(NewRoles.pageId == null) { return BadRequest("page can not be null"); }
            Page page = Unit_Of_Work.page_Repository.First_Or_Default(r => r.ID == NewRoles.pageId);
            if (page == null) { return NotFound("there is no page with this id");}

            //this page is child 
            if (page.Page_ID != null) 
            {
                //check if the parent of this page exist in role details table
                Role_Detailes role_Detailes1 =Unit_Of_Work.role_Detailes_Repository.First_Or_Default(p=>p.Page_ID == page.Page_ID);
                if (role_Detailes1 == null)
                {
                    role_Detailes1 = new Role_Detailes();
                    role_Detailes1.Page_ID = (long)page.Page_ID;
                    role_Detailes1.Role_ID = role.ID;
                    role_Detailes1.Allow_Delete = true;
                    role_Detailes1.Allow_Delete_For_Others = true;
                    role_Detailes1.Allow_Edit = true;
                    role_Detailes1.Allow_Edit_For_Others = true;
                    role_Detailes1.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                    if (userTypeClaim == "octa")
                    {
                        role_Detailes1.InsertedByOctaId = userId;
                    }
                    else if (userTypeClaim == "employee")
                    {
                        role_Detailes1.InsertedByUserId = userId;
                    }

                    await Unit_Of_Work.role_Detailes_Repository.AddAsync(role_Detailes1);
                    await Unit_Of_Work.SaveChangesAsync();
                }
            }

            Role_Detailes role_Detailes = new Role_Detailes();
            role_Detailes.Page_ID=NewRoles.pageId;
            role_Detailes.Role_ID = role.ID;
            role_Detailes.Allow_Delete = NewRoles.Allow_Delete;
            role_Detailes.Allow_Delete_For_Others = NewRoles.Allow_Delete_For_Others;
            role_Detailes.Allow_Edit = NewRoles.Allow_Edit;
            role_Detailes.Allow_Edit_For_Others = NewRoles.Allow_Edit_For_Others;
            role_Detailes.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                role_Detailes.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                role_Detailes.InsertedByUserId = userId;
            }

            await Unit_Of_Work.role_Detailes_Repository.AddAsync(role_Detailes);
            await Unit_Of_Work.SaveChangesAsync();
            return Ok(NewRoles);
        }
        ////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Role", "Administrator" }
        )]
        public IActionResult Edit(RolesGetDTO newRole)
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

            Role role = mapper.Map<Role>(newRole);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
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
