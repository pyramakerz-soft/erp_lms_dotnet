using AutoMapper;
using LMS_CMS_BL.DTO.Registration;
using LMS_CMS_BL.UOW;
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
    public class CategoryFieldController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public CategoryFieldController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        [HttpGet("GetByCategoryId/{TypeId}")]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" },
         pages: new[] { "Registration Form Field", "Registration" }
         )]
        public async Task<IActionResult> GetAsync(int id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<CategoryField> categorieFields = await Unit_Of_Work.categoryField_Repository.Select_All_With_IncludesById<CategoryField>(
                    b => b.IsDeleted != true &&b.RegistrationCategoryID==id,
                    query => query.Include(emp => emp.FieldType),
                    query => query.Include(emp => emp.FieldOptions)
                    );

            if (categorieFields == null || categorieFields.Count == 0)
            {
                return NotFound();
            }

            List<RegistrationCategoryGetDTO> BuildingDTO = mapper.Map<List<RegistrationCategoryGetDTO>>(categorieFields);

            return Ok(BuildingDTO);
        }

        ////////////////////////////////////////////////////////////////////////////////////
        //when mcq only add option
        [HttpPost]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" },
         pages: new[] { "Registration Form Field", "Registration" }
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
            return Ok(NewCategory);
        }
    }
}
