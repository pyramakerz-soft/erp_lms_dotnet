using AutoMapper;
using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Controllers.Domains.LMS
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class DailyPerformanceController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;

        public DailyPerformanceController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
        }

        /////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
      allowedTypes: new[] { "octa", "employee" }
      // ,
      // pages: new[] { "" }
      )]
        public async Task<IActionResult> Add(List<DailyPerformanceAddDTO> Newtypes)
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
            if (Newtypes == null)
            {
                return BadRequest("Type is empty");
            }

            foreach (var type in Newtypes)
            {
                Student stu = Unit_Of_Work.student_Repository.First_Or_Default(s => s.ID == type.StudentID && s.IsDeleted != true);
                if (stu == null)
                {
                    return BadRequest("student id not exist");
                }

                Subject s = Unit_Of_Work.subject_Repository.First_Or_Default(s => s.ID == type.SubjectID && s.IsDeleted != true);
                if (s == null)
                {
                    return BadRequest("Subject id not exist");
                }
                DailyPerformance Type = mapper.Map<DailyPerformance>(type);

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
                Unit_Of_Work.dailyPerformance_Repository.Add(Type);

            }
            Unit_Of_Work.SaveChanges();
            return Ok(Newtypes);
        }

    }
}
