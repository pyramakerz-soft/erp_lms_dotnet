using AutoMapper;
using LMS_CMS_BL.DTO.Inventory;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.AccountingModule;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.Inventory;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LMS_CMS_PL.Controllers.Domains.Inventory
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    public class StockingController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        public InVoiceNumberCreate _InVoiceNumberCreate;
        private readonly CheckPageAccessService _checkPageAccessService;


        public StockingController(DbContextFactoryService dbContextFactory, IMapper mapper, InVoiceNumberCreate inVoiceNumberCreate, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            this._InVoiceNumberCreate = inVoiceNumberCreate;
            _checkPageAccessService = checkPageAccessService;
        }



        [HttpGet()]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Inventory" }
        )]
        public async Task<IActionResult> GetAsync( [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            // Get total record count
            int totalRecords = await Unit_Of_Work.stocking_Repository
                .CountAsync(f => f.IsDeleted != true);

            List<Stocking> Data = await Unit_Of_Work.stocking_Repository.Select_All_With_IncludesById_Pagination<Stocking>(
                    f => f.IsDeleted != true,
                    query => query.Include(store => store.Store))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (Data == null || Data.Count == 0)
            {
                return NotFound();
            }

            List<StockingGetDto> DTO = mapper.Map<List<StockingGetDto>>(Data);

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
          pages: new[] { "Inventory" }
    )]
        public async Task<IActionResult> GetById(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (id == 0)
            {
                return BadRequest("Enter Master ID");
            }

            Stocking Data = await Unit_Of_Work.stocking_Repository.FindByIncludesAsync(
                    s => s.IsDeleted != true && s.ID == id,
                    query => query.Include(store => store.Store)
                    );

            if (Data == null)
            {
                return NotFound();
            }

            StockingGetDto DTO = mapper.Map<StockingGetDto>(Data);
          
            return Ok(DTO);
        }
        /////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
          allowedTypes: new[] { "octa", "employee" },
          pages: new[] { "Inventory" }
      )]
        public async Task<IActionResult> Add(StockingAddDTO newData)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(userTypeClaim))
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (newData == null)
            {
                return BadRequest("Master cannot be null.");
            }

            //if (newData.StockingDetails.Count == 0)
            //{
            //    return BadRequest("StockingDetails IsRequired");
            //}

            /// Create
            Stocking Master = mapper.Map<Stocking>(newData);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            Master.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);

            if (userTypeClaim == "octa")
            {
                Master.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                Master.InsertedByUserId = userId;
            }

            Unit_Of_Work.stocking_Repository.Add(Master);
            await Unit_Of_Work.SaveChangesAsync();

            return Ok(Master.ID);
        }


        /////////////////////////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
             pages: new[] { "Inventory" }
        )]
        public async Task<IActionResult> EditAsync( StockingGetDto newData)
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

            Stocking data = Unit_Of_Work.stocking_Repository.First_Or_Default(s => s.ID == newData.ID && s.IsDeleted != true);
            if (data == null || data.IsDeleted == true)
            {
                return NotFound("There Is No Stocking With This Id");
            }

            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Inventory", roleId, userId, data);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            mapper.Map(newData, data);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            data.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                data.UpdatedByOctaId = userId;
                if (data.UpdatedByUserId != null)
                {
                    data.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                data.UpdatedByUserId = userId;
                if (data.UpdatedByOctaId != null)
                {
                    data.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.stocking_Repository.Update(data);
            Unit_Of_Work.SaveChanges();


            return Ok(newData);
        }

        /////////////////////////////////////////////////////////////////////////////

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
                return BadRequest("Enter Store ID");
            }

            Stocking data = Unit_Of_Work.stocking_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);

            if (data == null)
            {
                return NotFound();
            }

            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "Inventory", roleId, userId, data);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            data.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            data.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                data.DeletedByOctaId = userId;
                if (data.DeletedByUserId != null)
                {
                    data.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                data.DeletedByUserId = userId;
                if (data.DeletedByOctaId != null)
                {
                    data.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.stocking_Repository.Update(data);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
