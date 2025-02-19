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
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers.Domains.Inventory
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class SalesItemController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public SalesItemController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Sales Item", "Inventory" }
        )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<SalesItem> salesItems = await Unit_Of_Work.salesItem_Repository.Select_All_With_IncludesById<SalesItem>(
                    f => f.IsDeleted != true,
                    query => query.Include(s => s.ShopItem),
                    query => query.Include(s => s.Sales)
                    );

            if (salesItems == null || salesItems.Count == 0)
            {
                return NotFound();
            }

            List<SalesItemGetDTO> DTO = mapper.Map<List<SalesItemGetDTO>>(salesItems);

            return Ok(DTO);
        }

        ////

        [HttpGet("BySaleId/{id}")]
        [Authorize_Endpoint_(
          allowedTypes: new[] { "octa", "employee" },
          pages: new[] { "Sales Item", "Inventory" }
      )]
        public async Task<IActionResult> GetBySaleIDAsync(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<SalesItem> salesItems = await Unit_Of_Work.salesItem_Repository.Select_All_With_IncludesById<SalesItem>(
                    f => f.IsDeleted != true&&f.SalesID==id,
                    query => query.Include(s => s.ShopItem),
                    query => query.Include(s => s.Sales)
                    );

            if (salesItems == null || salesItems.Count == 0)
            {
                return NotFound();
            }

            List<SalesItemGetDTO> DTO = mapper.Map<List<SalesItemGetDTO>>(salesItems);

            return Ok(DTO);
        }

        ////

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
        allowedTypes: new[] { "octa", "employee" },
        pages: new[] { "Sales Item", "Inventory" }
        )]
        public async Task<IActionResult> GetByIDAsync(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            SalesItem salesItems = await Unit_Of_Work.salesItem_Repository.FindByIncludesAsync(
                    f => f.IsDeleted != true && f.ID == id,
                    query => query.Include(s => s.ShopItem),
                    query => query.Include(s => s.Sales)
                    );

            if (salesItems == null )
            {
                return NotFound();
            }

            SalesItemGetDTO DTO = mapper.Map<SalesItemGetDTO>(salesItems);

            return Ok(DTO);
        }

        ////

        [HttpPost]
        [Authorize_Endpoint_(
        allowedTypes: new[] { "octa", "employee" },
          pages: new[] { "Sales Item", "Inventory" }
    )]
        public async Task<IActionResult> Add(SalesItemAddDTO newItem)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (newItem == null)
            {
                return BadRequest("Sales Item cannot be null");
            }

            ShopItem shopItem = Unit_Of_Work.shopItem_Repository.First_Or_Default(s => s.ID == newItem.ShopItemID && s.IsDeleted != true);
            if (shopItem == null)
            {
                return NotFound();
            }

            Sales sale = Unit_Of_Work.sales_Repository.First_Or_Default(s => s.ID == newItem.SalesID && s.IsDeleted != true);
            if (sale == null)
            {
                return NotFound();
            }

            SalesItem salesItem = mapper.Map<SalesItem>(newItem);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            salesItem.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                salesItem.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                salesItem.InsertedByUserId = userId;
            }

            Unit_Of_Work.salesItem_Repository.Add(salesItem);
            await Unit_Of_Work.SaveChangesAsync();

            return Ok(salesItem.ID);
        }

        ////

        [HttpPut]
        [Authorize_Endpoint_(
        allowedTypes: new[] { "octa", "employee" },
        allowEdit: 1,
         pages: new[] { "Sales Item", "Inventory" }
    )]
        public async Task<IActionResult> EditAsync(SalesItemGetDTO newSale)
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

            if (newSale == null)
            {
                return BadRequest("Sales Item cannot be null");
            }

            SalesItem salesItem = Unit_Of_Work.salesItem_Repository.First_Or_Default(s => s.ID == newSale.ID && s.IsDeleted != true);
            if (salesItem == null)
            {
                return NotFound("No SaleItem with this ID");
            }

            ShopItem shopItem = Unit_Of_Work.shopItem_Repository.First_Or_Default(s => s.ID == newSale.ShopItemID && s.IsDeleted != true);
            if (shopItem == null)
            {
                return NotFound();
            }

            Sales sale = Unit_Of_Work.sales_Repository.First_Or_Default(s => s.ID == newSale.SalesID && s.IsDeleted != true);
            if (sale == null)
            {
                return NotFound();
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Sales Item");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (salesItem.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Sales Item page doesn't exist");
                }
            }

            mapper.Map(newSale, salesItem);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            salesItem.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                salesItem.UpdatedByOctaId = userId;
                if (salesItem.UpdatedByUserId != null)
                {
                    salesItem.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                salesItem.UpdatedByUserId = userId;
                if (salesItem.UpdatedByOctaId != null)
                {
                    salesItem.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.salesItem_Repository.Update(salesItem);
            Unit_Of_Work.SaveChanges();
            return Ok(newSale);
        }

        ////

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" },
         allowDelete: 1,
         pages: new[] { "Sales Item", "Inventory" }
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

            SalesItem salesItem = Unit_Of_Work.salesItem_Repository.First_Or_Default(s => s.ID == id && s.IsDeleted != true);
            if (salesItem == null)
            {
                return NotFound("No SaleItem with this ID");
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Sales Item");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Delete_For_Others == false)
                    {
                        if (salesItem.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Sales Item page doesn't exist");
                }
            }

            salesItem.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            salesItem.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                salesItem.DeletedByOctaId = userId;
                if (salesItem.DeletedByUserId != null)
                {
                    salesItem.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                salesItem.DeletedByUserId = userId;
                if (salesItem.DeletedByOctaId != null)
                {
                    salesItem.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.salesItem_Repository.Update(salesItem);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
