using AutoMapper;
using LMS_CMS_BL.DTO.Registration;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
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

        [HttpGet("GetByCategoryId/{id}")]
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

            List<CategoryFieldGetDTO> BuildingDTO = mapper.Map<List<CategoryFieldGetDTO>>(categorieFields);

            return Ok(BuildingDTO);
        }

        ////////////////////////////////////////////////////////////////////////////////////
        //when mcq only add option
        [HttpPost]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" },
         pages: new[] { "Registration Form Field", "Registration" }
         )]
        public async Task<IActionResult> Add(CategoryFieldAddDTO newField)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (newField == null)
            {
                return BadRequest("Building cannot be null");
            }

            if(newField.FieldTypeID == 5)
            {
                if(newField.Options.Count == 0)
                {
                    return BadRequest("options cannot be null when type is multi option");
                }
            }

            FieldType fieldType = Unit_Of_Work.fieldType_Repository.Select_By_Id(newField.FieldTypeID);
            if (fieldType == null) 
            {
                return BadRequest("there is no field type with this id");
            
            }


            RegistrationCategory registrationCategory = Unit_Of_Work.registrationCategory_Repository.Select_By_Id(newField.RegistrationCategoryID);
            if (registrationCategory == null)
            {
                return BadRequest("there is no registrationCategory with this id");

            }

            CategoryField categoryField = mapper.Map<CategoryField>(newField);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            categoryField.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                categoryField.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                categoryField.InsertedByUserId = userId;
            }

            Unit_Of_Work.categoryField_Repository.Add(categoryField);
            Unit_Of_Work.SaveChanges();

            foreach (var item in newField.Options)
            {
                if (item != "")
                {
                    FieldOption option=new FieldOption();
                    option.CategoryFieldID = categoryField.ID;
                    option.Name= item;  
                    option.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                    if (userTypeClaim == "octa")
                    {
                        option.InsertedByOctaId = userId;
                    }
                    else if (userTypeClaim == "employee")
                    {
                        option.InsertedByUserId = userId;
                    }
                    await Unit_Of_Work.fieldOption_Repository.AddAsync(option);
                    await Unit_Of_Work.SaveChangesAsync();
                }
            }
            return Ok(categoryField.ID);
        }
        /////////////////////////////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
        allowedTypes: new[] { "octa", "employee" },
         allowEdit: 1,
        pages: new[] { "Registration Form Field", "Registration" }
        )]
        public async Task<IActionResult> Edit(CategoryFieldEditDTO newField)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
            var userRoleClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            long.TryParse(userRoleClaim, out long roleId);

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (newField == null)
            {
                return BadRequest("Building cannot be null");
            }

            if (newField.FieldTypeID == 5)
            {
                if (newField.Options.Count == 0)
                {
                    return BadRequest("options cannot be null when type is multi option");
                }
            }

            CategoryField categoryField = Unit_Of_Work.categoryField_Repository.Select_By_Id(newField.ID);
            if (categoryField == null)
            {
                return BadRequest("there is no Category Field with this id");

            }

            FieldType fieldType = Unit_Of_Work.fieldType_Repository.Select_By_Id(newField.FieldTypeID);
            if (fieldType == null)
            {
                return BadRequest("there is no field type with this id");

            }


            RegistrationCategory registrationCategory = Unit_Of_Work.registrationCategory_Repository.Select_By_Id(newField.RegistrationCategoryID);
            if (registrationCategory == null)
            {
                return BadRequest("there is no registrationCategory with this id");

            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Registration Form Field");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (categoryField.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Registration Form Field page doesn't exist");
                }
            }

            mapper.Map(newField, categoryField);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            categoryField.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                categoryField.UpdatedByOctaId = userId;
                if (categoryField.UpdatedByUserId != null)
                {
                    categoryField.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                categoryField.UpdatedByUserId = userId;
                if (categoryField.UpdatedByOctaId != null)
                {
                    categoryField.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.categoryField_Repository.Update(categoryField);
            Unit_Of_Work.SaveChanges();

            List<FieldOption> Oldoptions = await Unit_Of_Work.fieldOption_Repository.Select_All_With_IncludesById<FieldOption>(
                    b => b.IsDeleted != true && b.CategoryFieldID == newField.ID);

            foreach (var i in Oldoptions)
            {
               await Unit_Of_Work.fieldOption_Repository.DeleteAsync(i.ID);
               await Unit_Of_Work.SaveChangesAsync();
            }
            foreach (var item in newField.Options)
            {
                if (item != "")
                {
                    FieldOption option = new FieldOption();
                    option.CategoryFieldID = categoryField.ID;
                    option.Name = item;
                    option.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                    if (userTypeClaim == "octa")
                    {
                        option.InsertedByOctaId = userId;
                    }
                    else if (userTypeClaim == "employee")
                    {
                        option.InsertedByUserId = userId;
                    }
                    await Unit_Of_Work.fieldOption_Repository.AddAsync(option);
                    await Unit_Of_Work.SaveChangesAsync();
                }
            }
            return Ok();
        }
    }
}
