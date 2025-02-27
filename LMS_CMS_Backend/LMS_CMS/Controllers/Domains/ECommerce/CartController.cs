using AutoMapper;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.ECommerce;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Controllers.Domains.ECommerce
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;

        public CartController(DbContextFactoryService dbContextFactory, IMapper mapper, FileImageValidationService fileImageValidationService, GenerateBarCodeEan13 generateBarCodeEan13, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
        }

        [HttpGet("ByStudentId/{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee", "student" }
         )]
        public async Task<IActionResult> GetByStudentId(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<Cart> carts = Unit_Of_Work.cart_Repository.FindBy(
                    b => b.IsDeleted != true && b.StudentID == id);

            if (carts == null || carts.Count == 0)
            {
                return NotFound();
            }

            Cart cart = null;

            for (int i = 0; i < carts.Count; i++)
            {
                Order order = Unit_Of_Work.order_Repository.First_Or_Default(
                    b => b.IsDeleted != true && b.CartID == carts[i].ID);

                if (order == null)
                {
                    cart = carts[i];
                }
            }

            //if(cart != null)
            //{

            //}

            //List<ShopItemGetDTO> shopItemGetDTO = mapper.Map<List<ShopItemGetDTO>>(shopItem);
            //string serverUrl = $"{Request.Scheme}://{Request.Host}/";
            //foreach (var item in shopItemGetDTO)
            //{
            //    if (!string.IsNullOrEmpty(item.MainImage))
            //    {
            //        item.MainImage = $"{serverUrl}{item.MainImage.Replace("\\", "/")}";
            //    }
            //    if (!string.IsNullOrEmpty(item.OtherImage))
            //    {
            //        item.OtherImage = $"{serverUrl}{item.OtherImage.Replace("\\", "/")}";
            //    }

            //    List<ShopItemColor> shopItemColors = await Unit_Of_Work.shopItemColor_Repository.Select_All_With_IncludesById<ShopItemColor>(s => s.ShopItemID == item.ID && s.IsDeleted != true);
            //    if (shopItemColors.Count != 0)
            //    {
            //        List<ShopItemColorGetDTO> shopItemColorGetDTO = mapper.Map<List<ShopItemColorGetDTO>>(shopItemColors);
            //        item.shopItemColors = shopItemColorGetDTO;
            //    }
            //    else
            //        item.shopItemColors = new List<ShopItemColorGetDTO>();

            //    List<ShopItemSize> shopItemSizes = await Unit_Of_Work.shopItemSize_Repository.Select_All_With_IncludesById<ShopItemSize>(s => s.ShopItemID == item.ID && s.IsDeleted != true);
            //    if (shopItemSizes.Count != 0)
            //    {
            //        List<ShopItemSizeGetDTO> shopItemSizeGetDTO = mapper.Map<List<ShopItemSizeGetDTO>>(shopItemSizes);
            //        item.shopItemSizes = shopItemSizeGetDTO;
            //    }
            //    else
            //        item.shopItemSizes = new List<ShopItemSizeGetDTO>();
            //}

            return Ok();
        }
    }
}