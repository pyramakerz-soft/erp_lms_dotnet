using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.DTO.Inventory;
using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.Inventory;
using LMS_CMS_DAL.Models.Domains.LMS;
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

        //////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
           pages: new[] { "Shop", "Shop Item", "Inventory" }
        )]
        public async Task<IActionResult> Add([FromForm] ShopItemAddDTO newShopItem)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (newShopItem == null)
            {
                return BadRequest("Shop Item cannot be null");
            }

            InventorySubCategories invSubCat = Unit_Of_Work.inventorySubCategories_Repository.First_Or_Default(
                d => d.ID == newShopItem.InventorySubCategoriesID && d.IsDeleted != true
                );
            if (invSubCat == null)
            {
                return NotFound("No Inventory Sub Categories With this ID");
            }
            
            School school = Unit_Of_Work.school_Repository.First_Or_Default(
                d => d.ID == newShopItem.SchoolID && d.IsDeleted != true
                );
            if (school == null)
            {
                return NotFound("No School With this ID");
            }
            
            Grade grade = Unit_Of_Work.grade_Repository.First_Or_Default(
                d => d.ID == newShopItem.GradeID && d.IsDeleted != true
                );
            if (grade == null)
            {
                return NotFound("No Grade With this ID");
            }
            
            Gender gender = Unit_Of_Work.gender_Repository.First_Or_Default(
                d => d.ID == newShopItem.GenderID 
                );
            if (gender == null)
            {
                return NotFound("No Gender With this ID");
            }

            if (newShopItem.MainImageFile != null)
            {
                string returnFileInput = _fileImageValidationService.ValidateImageFile(newShopItem.MainImageFile);
                if (returnFileInput != null)
                {
                    return BadRequest(returnFileInput);
                }
            }
            if (newShopItem.OtherImageFile != null)
            {
                string returnFileInput = _fileImageValidationService.ValidateImageFile(newShopItem.OtherImageFile);
                if (returnFileInput != null)
                {
                    return BadRequest(returnFileInput);
                }
            } 

            ShopItem ShopItem = mapper.Map<ShopItem>(newShopItem);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            ShopItem.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                ShopItem.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                ShopItem.InsertedByUserId = userId;
            }
             
            Unit_Of_Work.shopItem_Repository.Add(ShopItem);
            Unit_Of_Work.SaveChanges();

            var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/ShopItems");
            var shopItemFolder = Path.Combine(baseFolder, newShopItem.EnName + "_" + ShopItem.ID);
            var shopItemMainImageFolder = Path.Combine(shopItemFolder, "MainImage");
            var shopItemOtherImageFolder = Path.Combine(shopItemFolder, "OtherImage");
            if (!Directory.Exists(shopItemMainImageFolder))
            {
                Directory.CreateDirectory(shopItemMainImageFolder);
            }
            if (!Directory.Exists(shopItemOtherImageFolder))
            {
                Directory.CreateDirectory(shopItemOtherImageFolder);
            }

            if (newShopItem.MainImageFile != null)
            {
                if (newShopItem.MainImageFile.Length > 0)
                {
                    var filePath = Path.Combine(shopItemMainImageFolder, newShopItem.MainImageFile.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await newShopItem.MainImageFile.CopyToAsync(stream);
                    }
                }
            }

            if (newShopItem.OtherImageFile != null)
            {
                if (newShopItem.OtherImageFile.Length > 0)
                {
                    var filePath = Path.Combine(shopItemOtherImageFolder, newShopItem.OtherImageFile.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await newShopItem.OtherImageFile.CopyToAsync(stream);
                    }
                }
            }

            if (newShopItem.MainImageFile != null)
                ShopItem.MainImage = Path.Combine("Uploads", "ShopItems", newShopItem.EnName + "_" + ShopItem.ID, "MainImage", newShopItem.MainImageFile.FileName);
            if (newShopItem.OtherImageFile != null)
                ShopItem.OtherImage = Path.Combine("Uploads", "ShopItems", newShopItem.EnName + "_" + ShopItem.ID, "OtherImage", newShopItem.OtherImageFile.FileName);

            Unit_Of_Work.shopItem_Repository.Update(ShopItem);

            if(newShopItem.ShopItemColors.Length != 0 && newShopItem.ShopItemColors != null)
            {
                foreach (var item in newShopItem.ShopItemColors)
                {
                    ShopItemColor shopItemColor = new ShopItemColor();
                    shopItemColor.Name = item;
                    shopItemColor.ShopItemID = ShopItem.ID;

                    shopItemColor.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                    if (userTypeClaim == "octa")
                    {
                        shopItemColor.InsertedByOctaId = userId;
                    }
                    else if (userTypeClaim == "employee")
                    {
                        shopItemColor.InsertedByUserId = userId;
                    }

                    Unit_Of_Work.shopItemColor_Repository.Add(shopItemColor);
                }
            }

            if(newShopItem.ShopItemSizes.Length != 0 && newShopItem.ShopItemSizes != null)
            {
                foreach (var item in newShopItem.ShopItemSizes)
                {
                    ShopItemSize shopItemSize = new ShopItemSize();
                    shopItemSize.Name = item;
                    shopItemSize.ShopItemID = ShopItem.ID;

                    shopItemSize.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                    if (userTypeClaim == "octa")
                    {
                        shopItemSize.InsertedByOctaId = userId;
                    }
                    else if (userTypeClaim == "employee")
                    {
                        shopItemSize.InsertedByUserId = userId;
                    }

                    Unit_Of_Work.shopItemSize_Repository.Add(shopItemSize);
                }
            }
            Unit_Of_Work.SaveChanges();
            return Ok(newShopItem);
        }

        //////////////////////////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Shop", "Shop Item", "Inventory" }
        )]
        public async Task<IActionResult> Edit([FromForm] ShopItemPutDTO newShopItem)
        {
            return Ok();
        }

        //////////////////////////////////////////////////////////////////////////////

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
