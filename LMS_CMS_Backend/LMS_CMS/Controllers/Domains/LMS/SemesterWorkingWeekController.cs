using AutoMapper;
using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers.Domains.LMS
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class SemesterWorkingWeekController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;

        public SemesterWorkingWeekController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
        }

        [HttpGet("GetBySemesterID/{id}")]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" }
       )]
        public async Task<IActionResult> GetAsync(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            Lesson lesson = Unit_Of_Work.lesson_Repository.First_Or_Default(d => d.IsDeleted != true && d.ID == id);
            if (lesson == null)
            {
                return NotFound("No Lesson With this ID");
            }

            List<SemesterWorkingWeek> semesterWorkingWeeks = await Unit_Of_Work.semesterWorkingWeek_Repository.Select_All_With_IncludesById<SemesterWorkingWeek>(
                    f => f.IsDeleted != true && f.SemesterID == id,
                    query => query.Include(emp => emp.Semester)
                    );

            if (semesterWorkingWeeks == null || semesterWorkingWeeks.Count == 0)
            {
                return NotFound();
            }

            List<SemesterWorkingWeekGetDTO> semesterWorkingWeeksDTO = mapper.Map<List<SemesterWorkingWeekGetDTO>>(semesterWorkingWeeks);
             
            return Ok(semesterWorkingWeeksDTO);
        }
    }
}
