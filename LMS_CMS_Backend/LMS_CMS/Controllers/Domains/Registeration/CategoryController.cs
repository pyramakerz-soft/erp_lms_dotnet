using AutoMapper;
using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.DTO.Registration;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_DAL.Models.Domains.RegisterationModule;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers.Domains.Registeration
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;

        public CategoryController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
        }

        [HttpGet]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" },
         pages: new[] { "Registration Form Field" }
         )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<RegistrationCategory> categories = await Unit_Of_Work.registrationCategory_Repository.Select_All_With_IncludesById<RegistrationCategory>(
                    b => b.IsDeleted != true);

            if (categories == null || categories.Count == 0)
            {
                return NotFound();
            }

            List<RegistrationCategoryGetDTO> BuildingDTO = mapper.Map<List<RegistrationCategoryGetDTO>>(categories);

            return Ok(BuildingDTO);
        }

        //////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" },
         pages: new[] { "Registration Form Field" }
         )]
        public IActionResult Add(RegistrationCategoryAddDTO NewCategory)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (NewCategory == null)
            {
                return BadRequest("Building cannot be null");
            }

            RegistrationCategory category = mapper.Map<RegistrationCategory>(NewCategory);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            category.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                category.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                category.InsertedByUserId = userId;
            }

            Unit_Of_Work.registrationCategory_Repository.Add(category);
            Unit_Of_Work.SaveChanges();

            RegistrationFormCategory registrationFormCategory = new RegistrationFormCategory();
            registrationFormCategory.RegistrationCategoryID = category.ID;
            registrationFormCategory.RegistrationFormID = NewCategory.RegistrationFormId;

            registrationFormCategory.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                registrationFormCategory.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                registrationFormCategory.InsertedByUserId = userId;
            }

            Unit_Of_Work.registrationFormCategory_Repository.Add(registrationFormCategory);
            Unit_Of_Work.SaveChanges();

            return Ok(NewCategory);
        }

        //////////////////////////////////////////////////////////////////////////////////

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" },
         pages: new[] { "Registration Form Field" }
         )]
        public async Task<IActionResult> GetById(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (id == 0)
            {
                return BadRequest("Enter Category ID");
            }

            RegistrationCategory category = await Unit_Of_Work.registrationCategory_Repository.FindByIncludesAsync(t => t.IsDeleted != true && t.ID == id);


            if (category == null)
            {
                return NotFound();
            }

            RegistrationCategoryGetDTO categoryDTO = mapper.Map<RegistrationCategoryGetDTO>(category);

            return Ok(categoryDTO);
        }

        //////////////////////////////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
          allowedTypes: new[] { "octa", "employee" },
          allowEdit: 1,
         pages: new[] { "Registration Form Field" }
      )]
        public IActionResult Edit(RegistrationCategoryEditDTO NewCategory)
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

            if (NewCategory == null)
            {
                return BadRequest("Building cannot be null");
            }

            RegistrationCategory CategoryExists = Unit_Of_Work.registrationCategory_Repository.First_Or_Default(b => b.ID == NewCategory.ID && b.IsDeleted != true);
            if (CategoryExists == null )
            {
                return NotFound("No Category with this ID");
            }
             
            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Registration Form Field", roleId, userId, CategoryExists);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            mapper.Map(NewCategory, CategoryExists);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            CategoryExists.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                CategoryExists.UpdatedByOctaId = userId;
                if (CategoryExists.UpdatedByUserId != null)
                {
                    CategoryExists.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                CategoryExists.UpdatedByUserId = userId;
                if (CategoryExists.UpdatedByOctaId != null)
                {
                    CategoryExists.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.registrationCategory_Repository.Update(CategoryExists);
            Unit_Of_Work.SaveChanges();
            return Ok(CategoryExists);
        }

        //////////////////////////////////////////////////////////////////////////////////

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
          allowedTypes: new[] { "octa", "employee" },
          allowDelete: 1,
         pages: new[] { "Registration Form Field" }
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
                return BadRequest("Enter Category ID");
            }

            RegistrationCategory category = Unit_Of_Work.registrationCategory_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);


            if (category == null)
            {
                return NotFound();
            } 

            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "Registration Form Field", roleId, userId, category);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            category.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            category.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                category.DeletedByOctaId = userId;
                if (category.DeletedByUserId != null)
                {
                    category.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                category.DeletedByUserId = userId;
                if (category.DeletedByOctaId != null)
                {
                    category.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.registrationCategory_Repository.Update(category);

            List<RegistrationFormCategory> RegistrationFormCategories = Unit_Of_Work.registrationFormCategory_Repository.FindBy(
                r => r.IsDeleted != true && r.RegistrationCategoryID == id
                );

            foreach (var item in RegistrationFormCategories)
            {
                item.IsDeleted = true;
                item.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                if (userTypeClaim == "octa")
                {
                    item.DeletedByOctaId = userId;
                    if (item.DeletedByUserId != null)
                    {
                        item.DeletedByUserId = null;
                    }
                }
                else if (userTypeClaim == "employee")
                {
                    item.DeletedByUserId = userId;
                    if (item.DeletedByOctaId != null)
                    {
                        item.DeletedByOctaId = null;
                    }
                }

                Unit_Of_Work.registrationFormCategory_Repository.Update(item);
            }

            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
