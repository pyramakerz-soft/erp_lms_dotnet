using AutoMapper;
using LMS_CMS_BL.DTO.ECommerce;
using LMS_CMS_BL.DTO.Inventory;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.ECommerce;
using LMS_CMS_DAL.Models.Domains.LMS;
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
    public class OrderController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;

        public OrderController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
        }
        
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "student" }
         )]
        public async Task<IActionResult> GetById(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);
             
            if (id == null || id == 0)
            {
                return NotFound("Id can't be null");
            }

            Order order = Unit_Of_Work.order_Repository.First_Or_Default(
                    b => b.IsDeleted != true && b.ID == id);

            if (order == null)
            {
                return NotFound();
            }

            OrderGetDTO orderDTO = mapper.Map<OrderGetDTO>(order);

            string serverUrl = $"{Request.Scheme}://{Request.Host}/";
             
            Cart_ShopItem cartShopitem = await Unit_Of_Work.cart_ShopItem_Repository.FindByIncludesAsync(
                c => c.IsDeleted != true && c.CartID == orderDTO.CartID,
                query => query.Include(c => c.ShopItem)
                );

            orderDTO.MainImage = $"{serverUrl}{cartShopitem.ShopItem.MainImage.Replace("\\", "/")}"; 
            
            return Ok(orderDTO);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 

        [HttpGet("ByStudentId/{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "student" }
         )]
        public async Task<IActionResult> GetByStudentId(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            Student stu = Unit_Of_Work.student_Repository.First_Or_Default(
                stu => stu.ID == id && stu.IsDeleted != true
                );

            if (stu == null)
            {
                return NotFound("No Student With this ID");
            }

            List<Order> orders = await Unit_Of_Work.order_Repository.Select_All_With_IncludesById<Order>(
                    b => b.IsDeleted != true && b.StudentID == id,
                    query => query.Include(order => order.OrderState));

            if (orders == null || orders.Count == 0)
            {
                return NotFound();
            }
             
            List<OrderGetDTO> orderDTO = mapper.Map<List<OrderGetDTO>>(orders);

            string serverUrl = $"{Request.Scheme}://{Request.Host}/";

            for (int i = 0; i < orderDTO.Count; i++)
            {
                Cart_ShopItem cartShopitem = await Unit_Of_Work.cart_ShopItem_Repository.FindByIncludesAsync(
                    c => c.IsDeleted != true && c.CartID == orderDTO[i].CartID,
                    query => query.Include(c => c.ShopItem)
                    );

                orderDTO[i].MainImage = $"{serverUrl}{cartShopitem.ShopItem.MainImage.Replace("\\", "/")}"; 
            }
             
            return Ok(orderDTO);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 

        [HttpDelete("CancelOrder/{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "student" }
        )]
        public IActionResult CancelOrder(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            Order order = Unit_Of_Work.order_Repository.First_Or_Default(
                o => o.ID == id && o.IsDeleted != true
                );

            if (order == null)
            {
                return NotFound("No Order With this ID");
            }

            order.OrderStateID = 3;

            Unit_Of_Work.order_Repository.Update(order);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 

        [HttpGet("ConfirmCart/{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "student" }
        )]
        public async Task<IActionResult> ConfirmCart(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            Cart cart = Unit_Of_Work.cart_Repository.First_Or_Default(
                o => o.ID == id && o.IsDeleted != true);

            if (cart == null)
            {
                return NotFound("No Cart With this ID");
            }

            Order order = Unit_Of_Work.order_Repository.First_Or_Default(
                    b => b.IsDeleted != true && b.CartID == id);

            if(order != null)
            {
                return BadRequest("This is already an Order");
            }

            Order newOrder = new Order(); 
            newOrder.TotalPrice = cart.TotalPrice;
            newOrder.CartID = cart.ID;
            newOrder.StudentID = cart.StudentID;
            newOrder.OrderStateID = 1;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            newOrder.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);

            Unit_Of_Work.order_Repository.Add(newOrder);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
