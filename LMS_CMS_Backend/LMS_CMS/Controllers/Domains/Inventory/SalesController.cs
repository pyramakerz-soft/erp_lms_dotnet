using AutoMapper;
using LMS_CMS_BL.DTO.Accounting;
using LMS_CMS_BL.DTO.Inventory;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.AccountingModule;
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
    public class SalesController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public SalesController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Sales", "Inventory" }
        )]
        public async Task<IActionResult> GetAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            // Get total record count
            int totalRecords = await Unit_Of_Work.sales_Repository
                .CountAsync(f => f.IsDeleted != true);

            List<Sales> sales = await Unit_Of_Work.sales_Repository.Select_All_With_IncludesById_Pagination<Sales>(
                    f => f.IsDeleted != true,
                    query => query.Include(store => store.Store),
                    query => query.Include(store => store.Student),
                    query => query.Include(store => store.Save),
                    query => query.Include(store => store.Bank))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (sales == null || sales.Count == 0)
            {
                return NotFound();
            }

            List<SalesGetDTO> DTO = mapper.Map<List<SalesGetDTO>>(sales);

            var paginationMetadata = new
            {
                TotalRecords = totalRecords,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
            };

            return Ok(new { Data = DTO, Pagination = paginationMetadata });
        }


        /////////////////////////////////////////////////////////////////////////////

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
        allowedTypes: new[] { "octa", "employee" },
          pages: new[] { "Sales", "Inventory" }
    )]
        public async Task<IActionResult> GetById(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (id == 0)
            {
                return BadRequest("Enter Sale ID");
            }

            Sales sale = await Unit_Of_Work.sales_Repository.FindByIncludesAsync(
                    s => s.IsDeleted != true && s.ID == id,
                    query => query.Include(store => store.Store),
                    query => query.Include(store => store.Student),
                    query => query.Include(store => store.Save),
                    query => query.Include(store => store.Bank)
                    );
            
            if (sale == null)
            {
                return NotFound();
            }

            SalesGetDTO DTO = mapper.Map<SalesGetDTO>(sale);

            return Ok(DTO);
        }

        /////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
        allowedTypes: new[] { "octa", "employee" },
          pages: new[] { "Sales", "Inventory" }
    )]
        public async Task<IActionResult> Add(SalesAddDTO newSale)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (newSale == null)
            {
                return BadRequest("sale cannot be null");
            }

            Store store =Unit_Of_Work.store_Repository.First_Or_Default(b=>b.ID==newSale.BankID&&b.IsDeleted!=true);
            if (store == null) 
            { 
                return NotFound();
            }

            Student student = Unit_Of_Work.student_Repository.First_Or_Default(b => b.ID == newSale.StudentID && b.IsDeleted != true);
            if (student == null)
            {
                return NotFound();
            }

            if (newSale.BankID != 0 && newSale.BankID != null)
            {
                Bank bank = Unit_Of_Work.bank_Repository.First_Or_Default(b => b.ID == newSale.BankID && b.IsDeleted != true);
                if (bank == null)
                {
                    return NotFound();
                }
            }

            if (newSale.SaveID != 0 && newSale.SaveID != null)
            {
                Save save = Unit_Of_Work.save_Repository.First_Or_Default(b => b.ID == newSale.SaveID && b.IsDeleted != true);
                if (save == null)
                {
                    return NotFound();
                }
            }

            Sales sale = mapper.Map<Sales>(newSale);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            sale.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                sale.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                sale.InsertedByUserId = userId;
            }

            Unit_Of_Work.sales_Repository.Add(sale);
            await Unit_Of_Work.SaveChangesAsync();

            return Ok(newSale);
        }

        /////////////////////////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
        allowedTypes: new[] { "octa", "employee" },
        allowEdit: 1,
         pages: new[] { "Sales", "Inventory" }
    )]
        public async Task<IActionResult> EditAsync(SalesGetDTO newSale)
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
                return BadRequest("Store cannot be null");
            }

            if (newSale.Name == null)
            {
                return BadRequest("the name cannot be null");
            }

            Store store = Unit_Of_Work.store_Repository.First_Or_Default(b => b.ID == newSale.BankID && b.IsDeleted != true);
            if (store == null)
            {
                return NotFound();
            }

            Student student = Unit_Of_Work.student_Repository.First_Or_Default(b => b.ID == newSale.StudentID && b.IsDeleted != true);
            if (student == null)
            {
                return NotFound();
            }

            if (newSale.BankID != 0 && newSale.BankID != null)
            {
                Bank bank = Unit_Of_Work.bank_Repository.First_Or_Default(b => b.ID == newSale.BankID && b.IsDeleted != true);
                if (bank == null)
                {
                    return NotFound();
                }
            }

            if (newSale.SaveID != 0 && newSale.SaveID != null)
            {
                Save save = Unit_Of_Work.save_Repository.First_Or_Default(b => b.ID == newSale.SaveID && b.IsDeleted != true);
                if (save == null)
                {
                    return NotFound();
                }
            }

            Sales sale = Unit_Of_Work.sales_Repository.First_Or_Default(s => s.ID == newSale.ID && s.IsDeleted != true);
            if (sale == null || sale.IsDeleted == true)
            {
                return NotFound("No Sale with this ID");
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Sales");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (sale.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Sales page doesn't exist");
                }
            }

            mapper.Map(newSale, sale);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            sale.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                sale.UpdatedByOctaId = userId;
                if (sale.UpdatedByUserId != null)
                {
                    sale.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                sale.UpdatedByUserId = userId;
                if (sale.UpdatedByOctaId != null)
                {
                    sale.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.sales_Repository.Update(sale);
            Unit_Of_Work.SaveChanges();

           
            return Ok(newSale);
        }

        /////////////////////////////////////////////////////////////////////////////

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
        allowedTypes: new[] { "octa", "employee" },
        allowDelete: 1,
        pages: new[] { "Sales", "Inventory" }
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

            Sales sales = Unit_Of_Work.sales_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);

            if (sales == null)
            {
                return NotFound();
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Sales");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Delete_For_Others == false)
                    {
                        if (sales.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Sales page doesn't exist");
                }
            }

            sales.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            sales.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                sales.DeletedByOctaId = userId;
                if (sales.DeletedByUserId != null)
                {
                    sales.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                sales.DeletedByUserId = userId;
                if (sales.DeletedByOctaId != null)
                {
                    sales.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.sales_Repository.Update(sales);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
