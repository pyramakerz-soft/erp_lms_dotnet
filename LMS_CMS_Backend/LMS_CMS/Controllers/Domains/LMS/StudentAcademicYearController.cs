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

    public class StudentAcademicYearController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public StudentAcademicYearController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }
        //////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee", "parent" },
            pages: new[] { "Academic Years", "Administrator" }
        )]
        public async Task<IActionResult> GetAsync()
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

            List<StudentAcademicYear> academicYear = await Unit_Of_Work.studentAcademicYear_Repository.Select_All_With_IncludesById<StudentAcademicYear>(
                    sem => sem.IsDeleted != true,
                     query => query.Include(emp => emp.Student),
                     query => query.Include(emp => emp.Grade).ThenInclude(g=>g.Section),
                      query => query.Include(emp => emp.Classroom),
                    query => query.Include(emp => emp.School));

            if (academicYear == null || academicYear.Count == 0)
            {
                return NotFound();
            }

            List<StudentAcademicYearGetDTO> AcademicYearDTOs = mapper.Map<List<StudentAcademicYearGetDTO>>(academicYear);

            return Ok(AcademicYearDTOs);
        }


    }
}
