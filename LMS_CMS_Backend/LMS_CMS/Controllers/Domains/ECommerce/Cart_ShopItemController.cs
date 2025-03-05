using AutoMapper;
using LMS_CMS_BL.DTO.ECommerce;
using LMS_CMS_BL.DTO.Inventory;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.ECommerce;
using LMS_CMS_DAL.Models.Domains.Inventory;
using LMS_CMS_DAL.Models.Domains.LMS;
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

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 

        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "student", "employee" }
        )]
        public IActionResult Add(CartShopItemAddDTO cartShopItem)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (cartShopItem == null)
            {
                return BadRequest("Cart Shop Item cannot be null");
            }

            Cart cart = new Cart(); 

            if (cartShopItem.CartID != 0 && cartShopItem.CartID != null)
            {
                cart = Unit_Of_Work.cart_Repository.First_Or_Default(c => c.ID == cartShopItem.CartID && c.IsDeleted != true);
                if (cart == null)
                {
                    Student stu = Unit_Of_Work.student_Repository.First_Or_Default(s => s.IsDeleted != true && s.ID == cartShopItem.StudentID);
                    if (stu == null) 
                    { 
                        return NotFound("No Student with this ID");
                    }

                    List<Cart> stuCarts = Unit_Of_Work.cart_Repository.FindBy(c => c.IsDeleted != true && c.StudentID == cartShopItem.StudentID);
                    if (stuCarts == null || stuCarts.Count == 0)
                    {
                        cart.StudentID = cartShopItem.StudentID;
                        cart.TotalPrice = 0;
                        Unit_Of_Work.cart_Repository.Add(cart);
                        Unit_Of_Work.SaveChanges();
                    }
                    else
                    {
                        for (int i = 0; i < stuCarts.Count; i++)
                        {
                            Order exOrder = Unit_Of_Work.order_Repository.First_Or_Default(
                                b => b.IsDeleted != true && b.CartID == stuCarts[i].ID);

                            if (exOrder == null)
                            {
                                cart = stuCarts[i];
                            }
                        }

                        if (cart.ID == 0)
                        {
                            cart.StudentID = cartShopItem.StudentID;
                            cart.TotalPrice = 0;
                            Unit_Of_Work.cart_Repository.Add(cart);
                            Unit_Of_Work.SaveChanges();
                        }
                    }
                }
            }
            else
            {
                Student stu = Unit_Of_Work.student_Repository.First_Or_Default(s => s.IsDeleted != true && s.ID == cartShopItem.StudentID);
                if (stu == null)
                {
                    return NotFound("No Student with this ID");
                }

                List<Cart> stuCarts = Unit_Of_Work.cart_Repository.FindBy(c => c.IsDeleted != true && c.StudentID == cartShopItem.StudentID);
                if (stuCarts == null || stuCarts.Count == 0)
                {
                    cart.StudentID = cartShopItem.StudentID;
                    cart.TotalPrice = 0;
                    Unit_Of_Work.cart_Repository.Add(cart);
                    Unit_Of_Work.SaveChanges();
                }
                else
                {
                    for (int i = 0; i < stuCarts.Count; i++)
                    {
                        Order exOrder = Unit_Of_Work.order_Repository.First_Or_Default(
                            b => b.IsDeleted != true && b.CartID == stuCarts[i].ID);

                        if (exOrder == null)
                        {
                            cart = stuCarts[i];
                        }
                    }

                    if (cart.ID == 0)
                    {
                        cart.StudentID = cartShopItem.StudentID;
                        cart.TotalPrice = 0;
                        Unit_Of_Work.cart_Repository.Add(cart);
                        Unit_Of_Work.SaveChanges();
                    }
                }
            }

            Order order = Unit_Of_Work.order_Repository.First_Or_Default(o => o.CartID == cartShopItem.CartID && o.IsDeleted != true);
            if (order != null)
            {
                return BadRequest("It is already an Order");
            }

            ShopItem shopItem = Unit_Of_Work.shopItem_Repository.First_Or_Default(c => c.ID == cartShopItem.ShopItemID && c.IsDeleted != true);
            if (shopItem == null)
            {
                return NotFound("No Shop Item With this ID");
            }

            if (shopItem.AvailableInShop == false)
            {
                return NotFound("Item Is Not available right now");
            }

            if (shopItem.Limit == 0)
            {
                return NotFound("Item Is Out Of Stock");
            }

            if (cartShopItem.ShopItemColorID != 0 && cartShopItem.ShopItemColorID != null)
            {
                ShopItemColor shopItemColor = Unit_Of_Work.shopItemColor_Repository.First_Or_Default(c => c.ID == cartShopItem.ShopItemColorID && c.IsDeleted != true);
                if (shopItemColor == null)
                {
                    return NotFound("No Shop Item Color With this ID");
                }
            }

            if (cartShopItem.ShopItemSizeID != 0 && cartShopItem.ShopItemSizeID != null)
            {
                ShopItemSize shopItemSize = Unit_Of_Work.shopItemSize_Repository.First_Or_Default(c => c.ID == cartShopItem.ShopItemSizeID && c.IsDeleted != true);
                if (shopItemSize == null)
                {
                    return NotFound("No Shop Item Size With this ID");
                }
            }

            if (shopItem.Limit < cartShopItem.Quantity)
            {
                return BadRequest($"There are only {shopItem.Limit} items in the store");
            }

            Student ExStu = Unit_Of_Work.student_Repository.First_Or_Default(s => s.IsDeleted != true && s.ID == cartShopItem.StudentID);
            if (ExStu.Nationality != 148)
            {
                if(shopItem.VATForForeign != null && shopItem.VATForForeign != 0)
                { 
                    cart.TotalPrice = (float)(cart.TotalPrice + (cartShopItem.Quantity * (shopItem.SalesPrice + shopItem.SalesPrice * (shopItem.VATForForeign / 100))));
                }
                else
                {
                    cart.TotalPrice = cart.TotalPrice + (cartShopItem.Quantity * shopItem.SalesPrice);
                }
            }
            else
            {
                cart.TotalPrice = cart.TotalPrice + (cartShopItem.Quantity * shopItem.SalesPrice);
            }

            Unit_Of_Work.cart_Repository.Update(cart);

            shopItem.Limit = shopItem.Limit - cartShopItem.Quantity;
            Unit_Of_Work.shopItem_Repository.Update(shopItem);

            cartShopItem.CartID = cart.ID;

            Cart_ShopItem newCartShopItem = mapper.Map<Cart_ShopItem>(cartShopItem);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            newCartShopItem.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);

            Unit_Of_Work.cart_ShopItem_Repository.Add(newCartShopItem);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 

        [HttpDelete("RemoveItemFromCart/{CartShopItemID}")]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "student", "employee" }
        )]
        public IActionResult RemoveItemFromCart(long CartShopItemID)
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

            Order order = Unit_Of_Work.order_Repository.First_Or_Default(o => o.IsDeleted != true && o.CartID == cart.ID);
            if (order != null)
            {
                return BadRequest("You can't remove it as It is already an Order");
            }

            ShopItem shopItem = Unit_Of_Work.shopItem_Repository.First_Or_Default(
                o => o.ID == cartShopItem.ShopItemID && o.IsDeleted != true);

            if (cart == null)
            {
                return NotFound("No Shop Item With this ID");
            }

            shopItem.Limit = shopItem.Limit + cartShopItem.Quantity;
            Student stu = Unit_Of_Work.student_Repository.First_Or_Default(s => s.IsDeleted != true && s.ID == cart.StudentID);
            if (stu.Nationality != 148)
            {
                if (shopItem.VATForForeign != null && shopItem.VATForForeign != 0)
                { 
                    cart.TotalPrice = (float)(cart.TotalPrice - (cartShopItem.Quantity * (shopItem.SalesPrice + shopItem.SalesPrice * (shopItem.VATForForeign / 100))));
                }
                else
                {
                    cart.TotalPrice = cart.TotalPrice - (cartShopItem.Quantity * shopItem.SalesPrice);
                }
            }
            else
            {
                cart.TotalPrice = cart.TotalPrice - (cartShopItem.Quantity * shopItem.SalesPrice);
            }

            Unit_Of_Work.cart_ShopItem_Repository.Delete(CartShopItemID);
            Unit_Of_Work.SaveChanges();

            List<Cart_ShopItem> existsCartShopItem = Unit_Of_Work.cart_ShopItem_Repository.FindBy(c => c.IsDeleted != true && c.CartID == cart.ID);
            if (existsCartShopItem == null || existsCartShopItem.Count == 0)
            {
                Unit_Of_Work.cart_Repository.Delete(cart.ID);
                Unit_Of_Work.SaveChanges();
            }

            return Ok();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 

        [HttpPut("ChangeQuantity")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "student", "employee" }
        )]
        public IActionResult ChangeQuantity(CartShopItemPutDTO cartShopItem)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (cartShopItem == null)
            {
                return BadRequest("Cart Shop Item cannot be null");
            }

            Cart_ShopItem existsCartShopItem = Unit_Of_Work.cart_ShopItem_Repository.First_Or_Default(c => c.ID == cartShopItem.ID && c.IsDeleted != true);
            if (existsCartShopItem == null)
            {
                return NotFound("No Cart with this ID");
            }

            Cart cart = Unit_Of_Work.cart_Repository.First_Or_Default(c => c.ID == cartShopItem.CartID && c.IsDeleted != true);
            if (cart == null)
            {
                return NotFound("No Cart with this ID");
            }

            Order order = Unit_Of_Work.order_Repository.First_Or_Default(o => o.IsDeleted != true && o.CartID == cartShopItem.CartID);
            if (order != null)
            {
                return BadRequest("It is already an order");
            }

            ShopItem shopItem = Unit_Of_Work.shopItem_Repository.First_Or_Default(sh => sh.IsDeleted != true && sh.ID == existsCartShopItem.ShopItemID);
            if(existsCartShopItem.Quantity < cartShopItem.Quantity)
            {
                if (shopItem.Limit < (existsCartShopItem.Quantity - cartShopItem.Quantity))
                {
                    return BadRequest($"There are only {shopItem.Limit} items in the store");
                }
            }

            if (cartShopItem.Quantity < 0)
            {
                return BadRequest("Can't Request minus");
            }

            shopItem.Limit = shopItem.Limit + (existsCartShopItem.Quantity - cartShopItem.Quantity);
            Student stu = Unit_Of_Work.student_Repository.First_Or_Default(s => s.IsDeleted != true && s.ID == cart.StudentID);
            if (stu.Nationality != 148)
            {
                if (shopItem.VATForForeign != null && shopItem.VATForForeign != 0)
                {
                    cart.TotalPrice = cart.TotalPrice + (float)(((cartShopItem.Quantity - existsCartShopItem.Quantity) * (shopItem.SalesPrice + shopItem.SalesPrice * (shopItem.VATForForeign / 100))));
                }
                else
                { 
                    cart.TotalPrice = cart.TotalPrice + ((cartShopItem.Quantity - existsCartShopItem.Quantity) * shopItem.SalesPrice);
                }
            }
            else
            { 
                cart.TotalPrice = cart.TotalPrice + ((cartShopItem.Quantity - existsCartShopItem.Quantity) * shopItem.SalesPrice);
            }
            existsCartShopItem.Quantity = cartShopItem.Quantity;

            Unit_Of_Work.shopItem_Repository.Update(shopItem);
            Unit_Of_Work.cart_Repository.Update(cart);
            Unit_Of_Work.cart_ShopItem_Repository.Update(existsCartShopItem);

            if (cartShopItem.Quantity == 0)
            {
                Unit_Of_Work.cart_ShopItem_Repository.Delete(cartShopItem.ID);
                Unit_Of_Work.SaveChanges();

                List<Cart_ShopItem> remainingCartShopItems = Unit_Of_Work.cart_ShopItem_Repository.FindBy(c => c.IsDeleted != true && c.CartID == cartShopItem.CartID);
                if (remainingCartShopItems == null || remainingCartShopItems.Count == 0)
                {
                    Unit_Of_Work.cart_Repository.Delete(cart.ID);
                    Unit_Of_Work.SaveChanges();
                }
            }

            Unit_Of_Work.SaveChanges();

            return Ok();
        }
    }
}
