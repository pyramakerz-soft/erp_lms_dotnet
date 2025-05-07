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
    public class StudentPerformanceController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;

        public StudentPerformanceController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
        }


        /////////////////////////////////////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
     allowedTypes: new[] { "octa", "employee" }
         //,
         //pages: new[] { "" }
         )]
        public async Task<IActionResult> Get()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<StudentPerformance> types;

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            types =await Unit_Of_Work.studentPerformance_Repository.Select_All_With_IncludesById<StudentPerformance>(
                    b => b.IsDeleted != true,
                    query => query.Include(emp => emp.PerformanceType)
                    );

            if (types == null || types.Count == 0)
            {
                return NotFound();
            }

            List<StudentPerformanceGetDTO> Dto = mapper.Map<List<StudentPerformanceGetDTO>>(types);

            return Ok(Dto);
        }

        /////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" }
         // ,
         // pages: new[] { "" }
         )]
        public async Task<IActionResult> Add(List<StudentPerformanceAddDTO> newType)
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
            if (newType == null)
            {
                return BadRequest("Type is empty");
            }
            foreach (var type in newType)
            {

                //Student stu = Unit_Of_Work.student_Repository.First_Or_Default(s => s.ID == type.StudentID && s.IsDeleted != true);
                //if (stu == null)
                //{
                //    return BadRequest("student id not exist");
                //}

                PerformanceType p = Unit_Of_Work.performanceType_Repository.First_Or_Default(s => s.ID == type.PerformanceTypeID && s.IsDeleted != true);
                if (p == null)
                {
                    return BadRequest("PerformanceType id not exist");
                }

                //Subject s = Unit_Of_Work.subject_Repository.First_Or_Default(s => s.ID == type.SubjectID && s.IsDeleted != true);
                //if (s == null)
                //{
                //    return BadRequest("Subject id not exist");
                //}
                StudentPerformance Type = mapper.Map<StudentPerformance>(type);

                TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
                Type.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                if (userTypeClaim == "octa")
                {
                    Type.InsertedByOctaId = userId;
                }
                else if (userTypeClaim == "employee")
                {
                    Type.InsertedByUserId = userId;
                }
                Unit_Of_Work.studentPerformance_Repository.Add(Type);

            }
            Unit_Of_Work.SaveChanges();
            return Ok(newType);
        }

    }
}
