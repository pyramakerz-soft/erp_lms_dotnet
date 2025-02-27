using AutoMapper;
using LMS_CMS_BL.DTO.ECommerce;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.ECommerce;
using LMS_CMS_DAL.Models.Domains.Inventory;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            long cartID = 0;

            for (int i = 0; i < carts.Count; i++)
            {
                Order order = Unit_Of_Work.order_Repository.First_Or_Default(
                    b => b.IsDeleted != true && b.CartID == carts[i].ID);

                if (order == null)
                {
                    cartID = carts[i].ID;
                }
            }

            if (cartID == 0)
            {
                return NotFound();
            }

            List<Cart_ShopItem> cart_ShopItem = await Unit_Of_Work.cart_ShopItem_Repository.Select_All_With_IncludesById<Cart_ShopItem>(
                    c => c.IsDeleted != true && c.CartID == cartID,
                    query => query.Include(store => store.Cart),
                    query => query.Include(store => store.ShopItem),
                    query => query.Include(store => store.ShopItemColor),
                    query => query.Include(store => store.ShopItemSize)
                    ); 

            List<Cart_ShopItemGetDTO> cart_ShopItemGetDTO = mapper.Map<List<Cart_ShopItemGetDTO>>(cart_ShopItem);

            string serverUrl = $"{Request.Scheme}://{Request.Host}/";

            foreach (var item in cart_ShopItemGetDTO)
            {
                if (!string.IsNullOrEmpty(item.MainImage))
                {
                    item.MainImage = $"{serverUrl}{item.MainImage.Replace("\\", "/")}";
                } 
            }

            Cart cart = await Unit_Of_Work.cart_Repository.FindByIncludesAsync(
                c => c.ID == cartID, 
                query => query.Include(c => c.PromoCode)
                );

            CartGetDTO cartGetDTO = mapper.Map<CartGetDTO>(cart);
            cartGetDTO.Cart_ShopItems = cart_ShopItemGetDTO;

            return Ok(cartGetDTO);
        }


            // AvailableInShop , Limit
    }
}