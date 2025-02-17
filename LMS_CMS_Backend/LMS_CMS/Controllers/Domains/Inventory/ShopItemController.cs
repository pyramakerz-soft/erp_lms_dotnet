using AutoMapper;
using LMS_CMS_BL.DTO;
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
    public class ShopItemController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly FileImageValidationService _fileImageValidationService;

        public ShopItemController(DbContextFactoryService dbContextFactory, IMapper mapper, FileImageValidationService fileImageValidationService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _fileImageValidationService = fileImageValidationService;
        }

        ///////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" },
           pages: new[] { "Shop", "Inventory" }
        )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<ShopItem> shopItem = await Unit_Of_Work.shopItem_Repository.Select_All_With_IncludesById<ShopItem>(
                    b => b.IsDeleted != true,
                    query => query.Include(sub => sub.InventorySubCategories),
                    query => query.Include(sub => sub.School),
                    query => query.Include(sub => sub.Grade),
                    query => query.Include(sub => sub.Gender),
                    query => query.Include(sub => sub.ShopItemColor),
                    query => query.Include(sub => sub.ShopItemSize)
                    );

            if (shopItem == null || shopItem.Count == 0)
            {
                return NotFound();
            }

            List<ShopItemGetDTO> shopItemGetDTO = mapper.Map<List<ShopItemGetDTO>>(shopItem);
            string serverUrl = $"{Request.Scheme}://{Request.Host}/";
            foreach (var item in shopItemGetDTO)
            {
                if (!string.IsNullOrEmpty(item.MainImage))
                {
                    item.MainImage = $"{serverUrl}{item.MainImage.Replace("\\", "/")}";
                }
                if (!string.IsNullOrEmpty(item.OtherImage))
                {
                    item.OtherImage = $"{serverUrl}{item.OtherImage.Replace("\\", "/")}";
                }

                List<ShopItemColor> shopItemColors = Unit_Of_Work.shopItemColor_Repository.FindBy(s => s.ShopItemID == item.ID && s.IsDeleted != true);
                List<ShopItemColorGetDTO> shopItemColorGetDTO = mapper.Map<List<ShopItemColorGetDTO>>(shopItemColors);
                if (shopItemColorGetDTO != null)
                    item.shopItemColors = shopItemColorGetDTO;
                else
                    item.shopItemColors = new List<ShopItemColorGetDTO>();

                List<ShopItemSize> shopItemSizes = Unit_Of_Work.shopItemSize_Repository.FindBy(s => s.ShopItemID == item.ID && s.IsDeleted != true);
                List<ShopItemSizeGetDTO> shopItemSizeGetDTO = mapper.Map<List<ShopItemSizeGetDTO>>(shopItemSizes);
                if (shopItemSizeGetDTO != null)
                    item.shopItemSizes = shopItemSizeGetDTO;
                else
                    item.shopItemSizes = new List<ShopItemSizeGetDTO>();
            }

            return Ok(shopItemGetDTO);
        }

        //////////////////////////////////////////////////////////////////////////////

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Shop Item", "Inventory" }
         )]
        public async Task<IActionResult> GetbyIdAsync(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            ShopItem shopItem = await Unit_Of_Work.shopItem_Repository.FindByIncludesAsync(
                d => d.ID == id && d.IsDeleted != true,
                query => query.Include(sub => sub.InventorySubCategories),
                query => query.Include(sub => sub.School),
                query => query.Include(sub => sub.Grade),
                query => query.Include(sub => sub.Gender),
                query => query.Include(sub => sub.ShopItemColor),
                query => query.Include(sub => sub.ShopItemSize)
                );

            if (shopItem == null)
            {
                return NotFound();
            }

            ShopItemGetDTO shopItemDTO = mapper.Map<ShopItemGetDTO>(shopItem);
            string serverUrl = $"{Request.Scheme}://{Request.Host}/";

            if (!string.IsNullOrEmpty(shopItemDTO.MainImage))
            {
                shopItemDTO.MainImage = $"{serverUrl}{shopItemDTO.MainImage.Replace("\\", "/")}";
            }
            if (!string.IsNullOrEmpty(shopItemDTO.OtherImage))
            {
                shopItemDTO.OtherImage = $"{serverUrl}{shopItemDTO.OtherImage.Replace("\\", "/")}";
            }

            List<ShopItemColor> shopItemColors = Unit_Of_Work.shopItemColor_Repository.FindBy(s => s.ShopItemID == shopItemDTO.ID && s.IsDeleted != true);
            List<ShopItemColorGetDTO> shopItemColorGetDTO = mapper.Map<List<ShopItemColorGetDTO>>(shopItemColors);
            if (shopItemColorGetDTO != null)
                shopItemDTO.shopItemColors = shopItemColorGetDTO;
            else
                shopItemDTO.shopItemColors = new List<ShopItemColorGetDTO>();

            List<ShopItemSize> shopItemSizes = Unit_Of_Work.shopItemSize_Repository.FindBy(s => s.ShopItemID == shopItemDTO.ID && s.IsDeleted != true);
            List<ShopItemSizeGetDTO> shopItemSizeGetDTO = mapper.Map<List<ShopItemSizeGetDTO>>(shopItemSizes);
            if (shopItemSizeGetDTO != null)
                shopItemDTO.shopItemSizes = shopItemSizeGetDTO;
            else
                shopItemDTO.shopItemSizes = new List<ShopItemSizeGetDTO>();

            return Ok(shopItemDTO);
        }

        //////////////////////////////////////////////////////////////////////////////////

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Shop", "Inventory" }
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

            ShopItem shopItem = Unit_Of_Work.shopItem_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);

            if (shopItem == null)
            {
                return NotFound();
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Shop");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Delete_For_Others == false)
                    {
                        if (shopItem.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Shop page doesn't exist");
                }
            }

            shopItem.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            shopItem.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                shopItem.DeletedByOctaId = userId;
                if (shopItem.DeletedByUserId != null)
                {
                    shopItem.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                shopItem.DeletedByUserId = userId;
                if (shopItem.DeletedByOctaId != null)
                {
                    shopItem.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.shopItem_Repository.Update(shopItem);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
