using AutoMapper;
using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Controllers.Domains
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]

        public class AcademicYearController : ControllerBase
        {
            private readonly DbContextFactoryService _dbContextFactory;
            IMapper mapper;

            public AcademicYearController(DbContextFactoryService dbContextFactory, IMapper mapper)
            {
                _dbContextFactory = dbContextFactory;
                this.mapper = mapper;
            }

            [HttpGet]
            public IActionResult Get()
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

                List<AcademicYear> academicYear = Unit_Of_Work.academicYear_Repository.Select_All();

                if (academicYear == null || academicYear.Count == 0)
                {
                    return NotFound();
                }

                List<AcademicYearGet> semesterDTOs = mapper.Map<List<AcademicYearGet>>(academicYear);

                return Ok(semesterDTOs);
            }

        }
    

}

