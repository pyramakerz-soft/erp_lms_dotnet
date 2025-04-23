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
    public class EvaluationEmployeeController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper; 
        private readonly CheckPageAccessService _checkPageAccessService; 

        public EvaluationEmployeeController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {

            _dbContextFactory = dbContextFactory;
            this.mapper = mapper; 
            _checkPageAccessService = checkPageAccessService; 
        }

        [HttpPost]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" }
        )]
        public IActionResult Add(EvaluationEmployeeAddDTO newEval)
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

            if (newEval == null)
            {
                return BadRequest("Employee Evaluation cannot be null");
            }
             
            //SchoolType schoolType = Unit_Of_Work.schoolType_Repository.First_Or_Default(s => s.ID == newSchool.SchoolTypeID);
            //if (schoolType == null)
            //{
            //    return BadRequest("there is no School Type with this id");
            //}
            //School school = mapper.Map<School>(newSchool);

            //TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            //school.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            //if (userTypeClaim == "octa")
            //{
            //    school.InsertedByOctaId = userId;
            //}
            //Unit_Of_Work.school_Repository.Add(school);
            //Unit_Of_Work.SaveChanges();
            //return Ok(newSchool);
            return Ok();

        }
    }
}
