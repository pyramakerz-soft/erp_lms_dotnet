using AutoMapper;
using LMS_CMS_BL.DTO.Inventory;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.AccountingModule;
using LMS_CMS_DAL.Models.Domains.Inventory;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers.Domains.Inventory
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class StoresController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public StoresController (DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Stores", "Inventory" }
        )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<Store> stores = await Unit_Of_Work.store_Repository.Select_All_With_IncludesById<Store>(
                    f => f.IsDeleted != true,
                    query => query.Include(store => store.StoreCategories).ThenInclude(s=>s.InventoryCategories));

            if (stores == null || stores.Count == 0)
            {
                return NotFound();
            }

            List<LMS_CMS_BL.DTO.Inventory.InventoryStoreGetDTO> DTO = mapper.Map<List<LMS_CMS_BL.DTO.Inventory.InventoryStoreGetDTO>>(stores);

            return Ok(DTO);
        }

        ////

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
          allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Stores", "Inventory" }
      )]
        public async Task<IActionResult> GetById(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (id == 0)
            {
                return BadRequest("Enter Store ID");
            }

            Store store = await Unit_Of_Work.store_Repository.FindByIncludesAsync(
                    income => income.IsDeleted != true && income.ID == id,
                    query => query.Include(store => store.StoreCategories).ThenInclude(s => s.InventoryCategories));

            if (store == null)
            {
                return NotFound();
            }

            InventoryStoreGetDTO DTO = mapper.Map<InventoryStoreGetDTO>(store);

            return Ok(DTO);
        }

        ////

        [HttpPost]
        [Authorize_Endpoint_(
          allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Stores", "Inventory" }
      )]
        public async Task<IActionResult> Add(InventoryStoreAddDTO newStore)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (newStore == null)
            {
                return BadRequest("Store cannot be null");
            }

            if (newStore.Name == null)
            {
                return BadRequest("the name cannot be null");
            }

         
            Store store = mapper.Map<Store>(newStore);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            store.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                store.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                store.InsertedByUserId = userId;
            }

            Unit_Of_Work.store_Repository.Add(store);
            await Unit_Of_Work.SaveChangesAsync();

            foreach (var item in newStore.categoriesIds)
            {
                InventoryCategories category = Unit_Of_Work.inventoryCategories_Repository.First_Or_Default(c => c.ID == item && c.IsDeleted != true);
                if(category != null)
                {
                    StoreCategories storeCategory = new StoreCategories();
                    storeCategory.StoreID = store.ID;
                    storeCategory.InventoryCategoriesID=item;
                    Unit_Of_Work.storeCategories_Repository.Add(storeCategory);
                    await Unit_Of_Work.SaveChangesAsync();
                }
            }

            return Ok(newStore);
        }

        ////

        [HttpPut]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" },
           allowEdit: 1,
            pages: new[] { "Stores", "Inventory" }
       )]
       public async Task<IActionResult> EditAsync(StoreCategoriesEditDTO newStore)
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

            if (newStore == null)
            {
                return BadRequest("Store cannot be null");
            }

            if (newStore.Name == null)
            {
                return BadRequest("the name cannot be null");
            }

            Store store = Unit_Of_Work.store_Repository.First_Or_Default(s => s.ID == newStore.ID && s.IsDeleted != true);
            if (store == null || store.IsDeleted == true)
            {
                return NotFound("No Store with this ID");
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Stores");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (store.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("store page doesn't exist");
                }
            }

            mapper.Map(newStore, store);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            store.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                store.UpdatedByOctaId = userId;
                if (store.UpdatedByUserId != null)
                {
                    store.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                store.UpdatedByUserId = userId;
                if (store.UpdatedByOctaId != null)
                {
                    store.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.store_Repository.Update(store);
            Unit_Of_Work.SaveChanges();

            List<StoreCategories> storeCategories = Unit_Of_Work.storeCategories_Repository.FindBy(
                    f => f.IsDeleted != true&&f.StoreID==newStore.ID);

            foreach (var item in storeCategories)
            {
                Unit_Of_Work.storeCategories_Repository.Delete(item.ID);
                await Unit_Of_Work.SaveChangesAsync();
            }

            foreach (var item in newStore.categoriesIds)
            {
                InventoryCategories category = Unit_Of_Work.inventoryCategories_Repository.First_Or_Default(c => c.ID == item && c.IsDeleted != true);
                if (category != null)
                {
                    StoreCategories storeCategory = new StoreCategories();
                    storeCategory.StoreID = store.ID;
                    storeCategory.InventoryCategoriesID = item;
                    Unit_Of_Work.storeCategories_Repository.Add(storeCategory);
                    await Unit_Of_Work.SaveChangesAsync();
                }
            }

            return Ok(newStore);
       }

        ////

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" },
         allowDelete: 1,
         pages: new[] { "Stores", "Inventory" }
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
                return BadRequest("Enter Store ID");
            }

            Store store = Unit_Of_Work.store_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);

            if (store == null)
            {
                return NotFound();
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Stores");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Delete_For_Others == false)
                    {
                        if (store.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Stores page doesn't exist");
                }
            }

            store.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            store.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                store.DeletedByOctaId = userId;
                if (store.DeletedByUserId != null)
                {
                    store.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                store.DeletedByUserId = userId;
                if (store.DeletedByOctaId != null)
                {
                    store.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.store_Repository.Update(store);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }


    }
}
