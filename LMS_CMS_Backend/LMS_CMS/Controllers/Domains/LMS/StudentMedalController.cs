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

    public class StudentMedalController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;

        public StudentMedalController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;  
            _checkPageAccessService = checkPageAccessService;
        }

        ///////////////////////////////////////////////////////////////////////////////////

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
        allowedTypes: new[] { "octa", "employee" }
        //,
        //pages: new[] { "" }
    )]
        public async Task<IActionResult> GetByStudentId(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<StudentMedal> types;

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            types =await Unit_Of_Work.studentMedal_Repository.Select_All_With_IncludesById<StudentMedal>(
                    sem => sem.IsDeleted != true && sem.StudentID==id,
                     query => query.Include(emp => emp.Student),
                    query => query.Include(emp => emp.Medal));

            if (types == null || types.Count == 0)
            {
                return NotFound();
            }

            List<StudentMedalGetDTO> Dto = mapper.Map<List<StudentMedalGetDTO>>(types);

            return Ok(Dto);
        }

        ///////////////////////////////////////////////////////////////////////////////////


        [HttpPost]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" }
         // ,
         // pages: new[] { "" }
         )]
        public async Task<IActionResult> Add(StudentMedalAddDTO type)
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

            Student stu = Unit_Of_Work.student_Repository.First_Or_Default(s => s.ID == type.StudentID && s.IsDeleted != true);
            if (stu == null)
            {
                return BadRequest("student id not exist");
            }

            Medal s = Unit_Of_Work.medal_Repository.First_Or_Default(s => s.ID == type.MedalID && s.IsDeleted != true);
            if (s == null)
            {
                return BadRequest("medal id not exist");
            }
            StudentMedal Type = mapper.Map<StudentMedal>(type);

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
            Unit_Of_Work.studentMedal_Repository.Add(Type);

            Unit_Of_Work.SaveChanges();
            return Ok(type);
        }

    }
}
