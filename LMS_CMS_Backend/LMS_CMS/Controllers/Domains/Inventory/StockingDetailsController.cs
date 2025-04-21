using AutoMapper;
using LMS_CMS_BL.DTO.Inventory;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.Inventory;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using MailKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LMS_CMS_PL.Controllers.Domains.Inventory
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class StockingDetailsController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;
        private readonly CalculateCurrentStock _calculateCurrentStock;

        public StockingDetailsController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService , CalculateCurrentStock calculateCurrentStock)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
            _calculateCurrentStock = calculateCurrentStock;
        }

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Inventory" }
        )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<StockingDetails> Items = await Unit_Of_Work.stockingDetails_Repository.Select_All_With_IncludesById<StockingDetails>(
                    f => f.IsDeleted != true,
                    query => query.Include(s => s.ShopItem),
                    query => query.Include(s => s.Stocking)
                    );

            if (Items == null || Items.Count == 0)
            {
                return NotFound();
            }

            List<StockingDetailsGetDto> DTO = mapper.Map<List<StockingDetailsGetDto>>(Items);

            return Ok(DTO);
        }

        ////

        [HttpGet("ByStockingId/{id}")]
        [Authorize_Endpoint_(
          allowedTypes: new[] { "octa", "employee" },
          pages: new[] { "Inventory" }
      )]
        public async Task<IActionResult> GetBySaleIDAsync(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<StockingDetails> stockingDetails = await Unit_Of_Work.stockingDetails_Repository.Select_All_With_IncludesById<StockingDetails>(
                    f => f.IsDeleted != true && f.StockingId == id,
                    query => query.Include(s => s.ShopItem),
                    query => query.Include(s => s.Stocking)
                    );

            if (stockingDetails == null || stockingDetails.Count == 0)
            {
                return NotFound();
            }

            List<StockingDetailsGetDto> DTO = mapper.Map<List<StockingDetailsGetDto>>(stockingDetails);

            return Ok(DTO);
        }

        ////

        [HttpPost]
        [Authorize_Endpoint_(
              allowedTypes: new[] { "octa", "employee" },
                pages: new[] { "Inventory" }
          )]
        public async Task<IActionResult> Add(List<StockingDetailsAddDto> newItems)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(userTypeClaim))
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            foreach (var newItem in newItems)
            {
                if (newItem == null)
                {
                    return BadRequest("Stocking Details cannot be null");
                }

                ShopItem shopItem = Unit_Of_Work.shopItem_Repository.First_Or_Default(s => s.ID == newItem.ShopItemID && s.IsDeleted != true);
                if (shopItem == null)
                {
                    return NotFound();
                }

                Stocking stocking = Unit_Of_Work.stocking_Repository.First_Or_Default(s => s.ID == newItem.StockingId && s.IsDeleted != true);
                if (stocking == null)
                {
                    return NotFound();
                }

                StockingDetails stockingDetails = mapper.Map<StockingDetails>(newItem);

                TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
                stockingDetails.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                if (userTypeClaim == "octa")
                {
                    stockingDetails.InsertedByOctaId = userId;
                }
                else if (userTypeClaim == "employee")
                {
                    stockingDetails.InsertedByUserId = userId;
                }

                Unit_Of_Work.stockingDetails_Repository.Add(stockingDetails);
            }

            await Unit_Of_Work.SaveChangesAsync();
            return Ok();
        }

        ////

        [HttpPut]
        [Authorize_Endpoint_(
        allowedTypes: new[] { "octa", "employee" },
        allowEdit: 1,
         pages: new[] { "Inventory" }
    )]
        public async Task<IActionResult> EditAsync(List<StockingDetailsGetDto> newItems)
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

            foreach (var newItem in newItems)
            {
                
            if (newItem == null)
            {
                return BadRequest("stockingDetails Item cannot be null");
            }

            StockingDetails stockingDetails = Unit_Of_Work.stockingDetails_Repository.First_Or_Default(s => s.ID == newItem.ID && s.IsDeleted != true);
            if (stockingDetails == null)
            {
                return NotFound("No stockingDetails with this ID");
            }

            ShopItem shopItem = Unit_Of_Work.shopItem_Repository.First_Or_Default(s => s.ID == newItem.ShopItemID && s.IsDeleted != true);
            if (shopItem == null)
            {
                return NotFound();
            }

            Stocking stocking = Unit_Of_Work.stocking_Repository.First_Or_Default(s => s.ID == newItem.StockingId && s.IsDeleted != true);
            if (stocking == null)
            {
                return NotFound();
            }

            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Inventory", roleId, userId, stockingDetails);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            mapper.Map(newItem, stockingDetails);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            stockingDetails.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                stockingDetails.UpdatedByOctaId = userId;
                if (stockingDetails.UpdatedByUserId != null)
                {
                    stockingDetails.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                stockingDetails.UpdatedByUserId = userId;
                if (stockingDetails.UpdatedByOctaId != null)
                {
                    stockingDetails.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.stockingDetails_Repository.Update(stockingDetails);

            }
           await Unit_Of_Work.SaveChangesAsync();
            return Ok();

        }

        ////

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" },
         allowDelete: 1,
         pages: new[] { "Inventory" }
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
                return BadRequest("Enter Sales Item ID");
            }

            StockingDetails stockingDetails = Unit_Of_Work.stockingDetails_Repository.First_Or_Default(s => s.ID == id && s.IsDeleted != true);
            if (stockingDetails == null)
            {
                return NotFound("No Stocking Details with this ID");
            }


            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "Inventory", roleId, userId, stockingDetails);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            stockingDetails.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            stockingDetails.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                stockingDetails.DeletedByOctaId = userId;
                if (stockingDetails.DeletedByUserId != null)
                {
                    stockingDetails.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                stockingDetails.DeletedByUserId = userId;
                if (stockingDetails.DeletedByOctaId != null)
                {
                    stockingDetails.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.stockingDetails_Repository.Update(stockingDetails);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }

        ////

        [HttpGet("{StoreId}/{ShopItemID}/{date}")]
        [Authorize_Endpoint_(
                 allowedTypes: new[] { "octa", "employee" },
                 pages: new[] { "Inventory" }
             )]
        public async Task<IActionResult> GetCurrentStockByIDAsync(long StoreId , long ShopItemID , string date) 
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);
            LMS_CMS_Context db = Unit_Of_Work.inventoryMaster_Repository.Database();
            long FirstCurrentStock1 = await _calculateCurrentStock.GetCurrentStock(db, StoreId , ShopItemID , date);
            long SecondCurrentStock1 = await _calculateCurrentStock.GetCurrentStockInTransformedStore(db, StoreId, ShopItemID, date);

            return Ok(FirstCurrentStock1+ SecondCurrentStock1);
        }

        ////

        [HttpGet("CurrentStockByCategoryId/{StoreId}/{CategoryId}/{date}")]
        [Authorize_Endpoint_(
                 allowedTypes: new[] { "octa", "employee" },
                 pages: new[] { "Inventory" }
             )]
        public async Task<IActionResult> GetCurrentStockByCategoryId(long StoreId, long CategoryId, string date)
        {

            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);
            LMS_CMS_Context db = Unit_Of_Work.inventoryMaster_Repository.Database();

            List<InventorySubCategories> inventorySubCategories = await Unit_Of_Work.inventorySubCategories_Repository
                .Select_All_With_IncludesById<InventorySubCategories>(
                    f => f.InventoryCategoriesID == CategoryId && f.IsDeleted != true
                );

            List<long> subCategoryIds = inventorySubCategories.Select(s => s.ID).ToList();

            List<ShopItem> shopItems = await Unit_Of_Work.shopItem_Repository
                .Select_All_With_IncludesById<ShopItem>(
                    f => f.IsDeleted != true && subCategoryIds.Contains(f.InventorySubCategoriesID) );

            List<ShopItemGetDTO> shopItemGetDTO = mapper.Map<List<ShopItemGetDTO>>(shopItems);

            foreach (var item in shopItemGetDTO)
            {
                long FirstCurrentStock1 = await _calculateCurrentStock.GetCurrentStock(db, StoreId, item.ID, date);
                long SecondCurrentStock1 = await _calculateCurrentStock.GetCurrentStockInTransformedStore(db, StoreId, item.ID, date);
                item.CurrentStock = FirstCurrentStock1 + SecondCurrentStock1;
            }

            return Ok(shopItemGetDTO);
        }

        ////

        [HttpGet("CurrentStockBySubCategoryId/{StoreId}/{SubCategoryId}/{date}")]
        [Authorize_Endpoint_(
                 allowedTypes: new[] { "octa", "employee" },
                 pages: new[] { "Inventory" }
             )]
        public async Task<IActionResult> GetCurrentStockBySubCategoryId(long StoreId, long SubCategoryId, string date)
        {

            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);
            LMS_CMS_Context db = Unit_Of_Work.inventoryMaster_Repository.Database();

            //List<InventorySubCategories> inventorySubCategories = await Unit_Of_Work.inventorySubCategories_Repository
            //    .Select_All_With_IncludesById<InventorySubCategories>(
            //        f => f. == CategoryId && f.IsDeleted != true
            //    );

            //List<long> subCategoryIds = inventorySubCategories.Select(s => s.ID).ToList();

            List<ShopItem> shopItems = await Unit_Of_Work.shopItem_Repository
                .Select_All_With_IncludesById<ShopItem>(
                    f => f.IsDeleted != true && f.InventorySubCategoriesID==SubCategoryId);

            List<ShopItemGetDTO> shopItemGetDTO = mapper.Map<List<ShopItemGetDTO>>(shopItems);

            foreach (var item in shopItemGetDTO)
            {
                long FirstCurrentStock1 = await _calculateCurrentStock.GetCurrentStock(db, StoreId, item.ID, date);
                long SecondCurrentStock1 = await _calculateCurrentStock.GetCurrentStockInTransformedStore(db, StoreId, item.ID, date);
                item.CurrentStock = FirstCurrentStock1 + SecondCurrentStock1;
            }

            return Ok(shopItemGetDTO);
        }
    }
}
