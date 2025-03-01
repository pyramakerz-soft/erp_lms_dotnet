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

namespace LMS_CMS_PL.Controllers.Domains.ECommerce
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class Cart_ShopItemController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public Cart_ShopItemController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        [HttpGet("RemoveItemFromCart/{CartShopItemID}")]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "student" }
        )]
        public async Task<IActionResult> RemoveItemFromCart(long CartShopItemID)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            Cart_ShopItem cartShopItem = Unit_Of_Work.cart_ShopItem_Repository.First_Or_Default(
                o => o.ID == CartShopItemID && o.IsDeleted != true
                );

            if (cartShopItem == null)
            {
                return NotFound("No Cart Shop Item With this ID");
            }

            Cart cart = Unit_Of_Work.cart_Repository.First_Or_Default(
                o => o.ID == cartShopItem.CartID && o.IsDeleted != true);

            if (cart == null)
            {
                return NotFound("No Cart With this ID");
            }

             




            CartGetDTO cartGetDTO = mapper.Map<CartGetDTO>(cart);
            //cartGetDTO.Cart_ShopItems = cart_ShopItemGetDTO;

            return Ok(cartGetDTO);
        }
    }
}
