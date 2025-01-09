using AutoMapper;
using LMS_CMS_BL.DTO.Registration;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.RegisterationModule;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        //[HttpGet]
        //[Authorize_Endpoint_(
        // allowedTypes: new[] { "octa", "employee" },
        // pages: new[] { "Registration Form Field", "Registration" }
        // )]
        //public async Task<IActionResult> GetAsync(int id)
        //{
        //    UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

        //    List<CategoryField> categorieFields = await Unit_Of_Work.categoryField_Repository.Select_All_With_IncludesById<CategoryField>(
        //            b => b.IsDeleted != true &&);

        //    if (categorieFields == null || categorieFields.Count == 0)
        //    {
        //        return NotFound();
        //    }

        //    List<RegistrationCategoryGetDTO> BuildingDTO = mapper.Map<List<RegistrationCategoryGetDTO>>(categories);

        //    return Ok(BuildingDTO);
        //}

    }
}
