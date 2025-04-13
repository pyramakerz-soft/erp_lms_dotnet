using AutoMapper;
using LMS_CMS_BL.DTO.Bus;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Migrations;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.BusModule;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers.Domains.Bus
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class BusCategoryController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;

        public BusCategoryController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
        }
        ///////////////////////////////////////////
        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Bus Categories" , "Employee Create" , "Employee Edit" }
        )]
        public IActionResult Get()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);
            List<BusCategory> BusCategories;
            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }
            BusCategories = Unit_Of_Work.busCategory_Repository.FindBy(t => t.IsDeleted != true);
            if (BusCategories == null || BusCategories.Count == 0)
            {
                return NotFound();
            }
            List<BusCatigoryGetDTO> BusCatigoryDTO = mapper.Map<List<BusCatigoryGetDTO>>(BusCategories);
            return Ok(BusCatigoryDTO);
        }

        ///////////////////////////////////////////////////

        [HttpGet("id")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Bus Categories" }
        )]
        public IActionResult GetById(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (id == 0)
            {
                return BadRequest("Enter Bus Category ID");
            }

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            BusCategory busCategory = Unit_Of_Work.busCategory_Repository.First_Or_Default(b=>b.ID==id&&b.IsDeleted!=true);
            if (busCategory == null || busCategory.IsDeleted == true)
            {
                return NotFound("No bus category with this ID");
            }

            BusCatigoryGetDTO busCategoryDto = mapper.Map<BusCatigoryGetDTO>(busCategory);
            return Ok(busCategoryDto);
        }
        ///////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Bus Categories" }
        )]
        public IActionResult Add(BusCatigoryAddDTO NewCategory)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (NewCategory == null)
            {
                return BadRequest("Bus Category cannot be null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Returns validation errors
            }
            if(NewCategory.Name == null)
            {
                return BadRequest("the name cannot be null");
            }
            BusCategory busCategory = mapper.Map<BusCategory>(NewCategory);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            busCategory.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                busCategory.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                busCategory.InsertedByUserId = userId;
            }

            Unit_Of_Work.busCategory_Repository.Add(busCategory);
            Unit_Of_Work.SaveChanges();
            return Ok(NewCategory);
        }

        ///////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Bus Categories" }
        )]
        public IActionResult Edit(BusCategoryEditDTO EditBusCatigory)
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

            if (EditBusCatigory == null)
            {
                BadRequest();
            }
            if (EditBusCatigory.Name == null)
            {
                return BadRequest("the name cannot be null");
            }
            BusCategory busCatigory = Unit_Of_Work.busCategory_Repository.First_Or_Default(b => b.ID == EditBusCatigory.ID);
            if (busCatigory == null || busCatigory.IsDeleted == true)
            {
                return NotFound("No Bus Category with this ID");
            }
             
            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Bus Categories", roleId, userId, busCatigory);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            mapper.Map(EditBusCatigory, busCatigory);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            busCatigory.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                busCatigory.UpdatedByOctaId = userId;
                if (busCatigory.UpdatedByUserId != null)
                {
                    busCatigory.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                busCatigory.UpdatedByUserId = userId;
                if (busCatigory.UpdatedByOctaId != null)
                {
                    busCatigory.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.busCategory_Repository.Update(busCatigory);
            Unit_Of_Work.SaveChanges();
            return Ok(EditBusCatigory);
        }

        ///////////////////////////////////////////////////

        [HttpDelete]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Bus Categories" }
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
                return BadRequest("Bus Category ID cannot be null.");
            }

            BusCategory busCategory = Unit_Of_Work.busCategory_Repository.Select_By_Id(id);
            if (busCategory == null || busCategory.IsDeleted == true)
            {
                return NotFound("No Bus Category with this ID");
            }
            else
            { 
                if (userTypeClaim == "employee")
                {
                    IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "Bus Categories", roleId, userId, busCategory);
                    if (accessCheck != null)
                    {
                        return accessCheck;
                    }
                }

                busCategory.IsDeleted = true;
                TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
                busCategory.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                if (userTypeClaim == "octa")
                {
                    busCategory.DeletedByOctaId = userId;
                    if (busCategory.DeletedByUserId != null)
                    {
                        busCategory.DeletedByUserId = null;
                    }
                }
                else if (userTypeClaim == "employee")
                {
                    busCategory.DeletedByUserId = userId;
                    if (busCategory.DeletedByOctaId != null)
                    {
                        busCategory.DeletedByOctaId = null;
                    }
                }

                Unit_Of_Work.busCategory_Repository.Update(busCategory);
                Unit_Of_Work.SaveChanges();
                return Ok();
            }
        }
    }
}
