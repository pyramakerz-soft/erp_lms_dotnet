using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.DTO.Bus;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.BusModule;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers.Domains
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;

        IMapper mapper;

        public SchoolController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
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

            List<School> schools = await Unit_Of_Work.school_Repository.Select_All_With_IncludesById<School>(
                query => query.IsDeleted != true,
                query => query.Include(school => school.Grades).ThenInclude(grade => grade.Classes)
            );

            if (schools == null || schools.Count == 0)
            {
                return NotFound("No schools found.");
            }

            var result = schools.Select(school => new School_GetDTO
            {
                ID = school.ID,
                Name = school.Name,
                Grades = school.Grades.Select(grade => new Grade_GetDTO
                {
                    ID = grade.ID,
                    Name = grade.Name,
                    Classes = grade.Classes.Select(classEntity => new Class_GetDTO
                    {
                        ID = classEntity.ID,
                        Name = classEntity.Name
                    }).ToList()
                }).ToList()
            }).ToList();

            return Ok(result);
        }

    }
}
