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
    public class FieldOptionController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public FieldOptionController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        //[HttpGet]
        //[Authorize_Endpoint_(
        // allowedTypes: new[] { "octa", "employee" },
        // pages: new[] { "Registration Form Field", "Registration" }
        // )]
        //public async Task<IActionResult> GetAsync()
        //{
        //    UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

        //    List<> categorieFields = await Unit_Of_Work.categoryField_Repository.Select_All_With_IncludesById<CategoryField>(
        //            b => b.IsDeleted != true && b.RegistrationCategoryID == id,
        //            query => query.Include(emp => emp.FieldType),
        //            query => query.Include(emp => emp.FieldOptions)
        //            );

        //    if (categorieFields == null || categorieFields.Count == 0)
        //    {
        //        return NotFound();
        //    }

        //    List<CategoryFieldGetDTO> BuildingDTO = mapper.Map<List<CategoryFieldGetDTO>>(categorieFields);

        //    return Ok(BuildingDTO);
        //}

        ////////////////////////////////////////////////////////////////////////////////////
    }
}
