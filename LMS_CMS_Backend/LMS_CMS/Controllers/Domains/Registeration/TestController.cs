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
    public class TestController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public TestController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" },
         pages: new[] { "Admission Test", "Registration" }
         )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<Test> tests = await Unit_Of_Work.test_Repository.Select_All_With_IncludesById<Test>(
                    b => b.IsDeleted != true,
                    query => query.Include(emp => emp.academicYear),
                    query => query.Include(emp => emp.subject),
                    query => query.Include(emp => emp.Grade),
                    query => query.Include(emp => emp.academicYear.School)
                    );

            if (tests == null || tests.Count == 0)
            {
                return NotFound();
            }

            List<TestGetDTO> testDTO = mapper.Map<List<TestGetDTO>>(tests);

            return Ok(testDTO);
        }

    }
}
