using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.DTO.Registration;
using LMS_CMS_BL.UOW;
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
    public class RegistrationFormController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public RegistrationFormController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        ///////////////////////////////////////////////////////////////////////////////////

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Registration Form", "Registration" }
        )]
        public async Task<IActionResult> GetById(long id)
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

            if (id == 0)
            {
                return BadRequest("Enter Registration Form ID");
            }

            RegistrationForm registrationForm = Unit_Of_Work.registrationForm_Repository.First_Or_Default(
                t => t.IsDeleted != true && t.ID == id);

            if (registrationForm == null)
            {
                return NotFound("No Registration Form with this ID");
            }

            List<RegistrationFormCategory> RegistrationFormCategories = await Unit_Of_Work.registrationFormCategory_Repository.Select_All_With_IncludesById
                <RegistrationFormCategory>(
                    t => t.RegistrationFormID == id && t.IsDeleted != true,
                    query => query.Include(rfc => rfc.RegistrationCategory)
                    .ThenInclude(rc => rc.CategoryFields)
                    .ThenInclude(cf => cf.FieldOptions)
                    );

            var categories = RegistrationFormCategories
                .Select(rfc => new RegistrationCategoryGetDTO
                {
                    ID = rfc.RegistrationCategory?.ID ?? 0, 
                    EnName = rfc.RegistrationCategory?.EnName ?? string.Empty,
                    ArName = rfc.RegistrationCategory?.ArName ?? string.Empty,
                    OrderInForm = rfc.RegistrationCategory?.OrderInForm ?? 0,
                    InsertedByUserId = rfc.RegistrationCategory?.InsertedByUserId,
                    Fields = Unit_Of_Work.categoryField_Repository
                        .FindBy(cf => cf.RegistrationCategoryID == rfc.RegistrationCategoryID && cf.IsDeleted != true)
                        .Select(field => new CategoryFieldGetDTO
                        {
                            ID = field.ID,
                            EnName = field.EnName ?? string.Empty, 
                            ArName = field.ArName ?? string.Empty,  
                            OrderInForm = field.OrderInForm,
                            IsMandatory = field.IsMandatory,
                            InsertedByUserId = field.InsertedByUserId,
                            FieldTypeID = field.FieldTypeID,
                            FieldTypeName = Unit_Of_Work.fieldType_Repository
                                .First_Or_Default(ft => ft.ID == field.FieldTypeID)?.Name ?? string.Empty,  
                            RegistrationCategoryID = field.RegistrationCategoryID,
                            RegistrationCategoryName = rfc.RegistrationCategory?.EnName ?? string.Empty, 
                            Options = Unit_Of_Work.fieldOption_Repository
                                .FindBy(fo => fo.CategoryFieldID == field.ID && fo.IsDeleted != true)
                                .Select(option => new FieldOptionGetDTO
                                {
                                    ID = option.ID,
                                    Name = option.Name ?? string.Empty, 
                                    CategoryFieldID = option.CategoryFieldID,
                                    CategoryFieldName = field.EnName ?? string.Empty  
                                })
                                .ToList() ?? new List<FieldOptionGetDTO>() // Default to empty list
                        })
                        .ToList() ?? new List<CategoryFieldGetDTO>() // Default to empty list
                })
                .ToList() ?? new List<RegistrationCategoryGetDTO>(); // Default to empty list

            var response = new RegistrationFormGetDTO
            {
                ID = registrationForm.ID,
                Name = registrationForm.Name,
                Categories = categories
            };

            return Ok(response);
        }
    }
}
