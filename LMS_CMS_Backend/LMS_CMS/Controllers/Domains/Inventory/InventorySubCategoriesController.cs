using AutoMapper;
using LMS_CMS_BL.DTO.Inventory;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.Inventory;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers.Domains.Inventory
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class InventorySubCategoriesController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public InventorySubCategoriesController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        ///////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" },
           pages: new[] { "Inventory Sub Categories", "Inventory" }
        )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<InventorySubCategories> InventorySubCategories = await Unit_Of_Work.inventorySubCategories_Repository.Select_All_With_IncludesById<InventorySubCategories>(
                    b => b.IsDeleted != true,
                    query => query.Include(sub => sub.InventoryCategories)
                    );

            if (InventorySubCategories == null || InventorySubCategories.Count == 0)
            {
                return NotFound();
            }

            List<InventorySubCategoriesGetDTO> inventorySubCategoriesGetDTO = mapper.Map<List<InventorySubCategoriesGetDTO>>(InventorySubCategories);

            return Ok(inventorySubCategoriesGetDTO);
        }

        //////////////////////////////////////////////////////////////////////////////

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Inventory Sub Categories", "Inventory" }
         )]
        public async Task<IActionResult> GetbyIdAsync(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            InventorySubCategories InventorySubCategories = await Unit_Of_Work.inventorySubCategories_Repository.FindByIncludesAsync(
                d => d.ID == id && d.IsDeleted != true,
                query => query.Include(sub => sub.InventoryCategories)
                );

            if (InventorySubCategories == null)
            {
                return NotFound();
            }

            InventorySubCategoriesGetDTO InventorySubCategoriesDTO = mapper.Map<InventorySubCategoriesGetDTO>(InventorySubCategories);

            return Ok(InventorySubCategoriesDTO);
        }

        //////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
           pages: new[] { "Inventory Sub Categories", "Inventory" }
        )]
        public IActionResult Add(InventorySubCategoriesAddDTO newCategory)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (newCategory == null)
            {
                return BadRequest("Inventory Sub Categories cannot be null");
            }

            InventoryCategories invCat = Unit_Of_Work.inventoryCategories_Repository.First_Or_Default(
                d => d.ID == newCategory.InventoryCategoriesID && d.IsDeleted != true
                );
            if(invCat == null)
            {
                return NotFound("No Inventory Categories With this ID");
            }

            InventorySubCategories InventorySubCategories = mapper.Map<InventorySubCategories>(newCategory);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            InventorySubCategories.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                InventorySubCategories.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                InventorySubCategories.InsertedByUserId = userId;
            }

            Unit_Of_Work.inventorySubCategories_Repository.Add(InventorySubCategories);
            Unit_Of_Work.SaveChanges();
            return Ok(newCategory);
        }

        //////////////////////////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Inventory Sub Categories", "Inventory" }
        )]
        public IActionResult Edit(InventorySubCategoriesPutDTO newCategory)
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

            if (newCategory == null)
            {
                return BadRequest("Inventory Sub Categories cannot be null");
            }

            InventorySubCategories category = Unit_Of_Work.inventorySubCategories_Repository.First_Or_Default(d => d.ID == newCategory.ID && d.IsDeleted != true);
            if (category == null)
            {
                return NotFound("There is no Inventory Sub Categories with this id");
            }

            InventoryCategories invCat = Unit_Of_Work.inventoryCategories_Repository.First_Or_Default(
               d => d.ID == newCategory.InventoryCategoriesID && d.IsDeleted != true
               );
            if (invCat == null)
            {
                return NotFound("No Inventory Categories With this ID");
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Inventory Sub Categories");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (category.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Inventory Sub Categories page doesn't exist");
                }
            }

            mapper.Map(newCategory, category);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            category.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                category.UpdatedByOctaId = userId;
                if (category.UpdatedByUserId != null)
                {
                    category.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                category.UpdatedByUserId = userId;
                if (category.UpdatedByOctaId != null)
                {
                    category.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.inventorySubCategories_Repository.Update(category);
            Unit_Of_Work.SaveChanges();
            return Ok(newCategory);
        }

        //////////////////////////////////////////////////////////////////////////////////

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Inventory Sub Categories", "Inventory" }
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
                return BadRequest("Enter Sub Category ID");
            }

            InventorySubCategories category = Unit_Of_Work.inventorySubCategories_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);

            if (category == null)
            {
                return NotFound();
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Inventory Sub Categories");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Delete_For_Others == false)
                    {
                        if (category.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Inventory Sub Categories page doesn't exist");
                }
            }

            category.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            category.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                category.DeletedByOctaId = userId;
                if (category.DeletedByUserId != null)
                {
                    category.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                category.DeletedByUserId = userId;
                if (category.DeletedByOctaId != null)
                {
                    category.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.inventorySubCategories_Repository.Update(category);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
