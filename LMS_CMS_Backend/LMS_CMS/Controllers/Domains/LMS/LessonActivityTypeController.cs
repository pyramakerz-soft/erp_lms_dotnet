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
    public class LessonActivityTypeController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;

        public LessonActivityTypeController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
        }

        ///////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
        allowedTypes: new[] { "octa", "employee" }
        //,
        //pages: new[] { "" }
    )]
        public IActionResult Get()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<LessonActivityType> types;

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            types = Unit_Of_Work.lessonActivityType_Repository.FindBy(t => t.IsDeleted != true);

            if (types == null || types.Count == 0)
            {
                return NotFound();
            }

            List<LessonActivityTypeGetDTO> Dto = mapper.Map<List<LessonActivityTypeGetDTO>>(types);

            return Ok(Dto);
        }

        ///////////////////////////////////////////////////////////////////////////////////

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" }
           //,
           //pages: new[] { "EvaluationTemplateGroupQuestion" }
       )]
        public async Task<IActionResult> GetById(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            LessonActivityType type;

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            type = Unit_Of_Work.lessonActivityType_Repository.First_Or_Default(
            sem => sem.IsDeleted != true && sem.ID == id);

            if (type == null)
            {
                return NotFound();
            }

            LessonActivityTypeGetDTO Dto = mapper.Map<LessonActivityTypeGetDTO>(type);

            return Ok(Dto);
        }

        ///////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
          allowedTypes: new[] { "octa", "employee" }
      // ,
      // pages: new[] { "" }
      )]
        public async Task<IActionResult> Add(LessonActivityTypeAddDTO newType)
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

            LessonActivityType Type = mapper.Map<LessonActivityType>(newType);

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
            Unit_Of_Work.lessonActivityType_Repository.Add(Type);
            Unit_Of_Work.SaveChanges();
            return Ok(newType);
        }

        ////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1
        //    ,
        //pages: new[] { "EvaluationTemplateGroupQuestion" }
        )]
        public async Task<IActionResult> EditAsync(LessonActivityTypeEditDto newData)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
            var userRoleClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            long.TryParse(userRoleClaim, out long roleId);

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (newData == null)
            {
                return BadRequest("Type cannot be null");
            }
            if (newData.ID == null)
            {
                return BadRequest("Type id can not be null");
            }

            LessonActivityType type = Unit_Of_Work.lessonActivityType_Repository.First_Or_Default(s => s.ID == newData.ID && s.IsDeleted != true);
            if (type == null)
            {
                return BadRequest("Type not exist");
            }

            //if (userTypeClaim == "employee")
            //{
            //    IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "EvaluationTemplateGroupQuestion", roleId, userId, question);
            //    if (accessCheck != null)
            //    {
            //        return accessCheck;
            //    }
            //}

            mapper.Map(newData, type);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            type.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                type.UpdatedByOctaId = userId;
                if (type.UpdatedByUserId != null)
                {
                    type.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                type.UpdatedByUserId = userId;
                if (type.UpdatedByOctaId != null)
                {
                    type.UpdatedByOctaId = null;
                }
            }
            Unit_Of_Work.lessonActivityType_Repository.Update(type);
            Unit_Of_Work.SaveChanges();
            return Ok(newData);
        }
        ////////////////////////////////////////////////////


        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
          allowedTypes: new[] { "octa", "employee" },
          allowDelete: 1
      //      ,
      //pages: new[] { "" }
      )]
        public IActionResult Delete(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
            var userRoleClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            long.TryParse(userRoleClaim, out long roleId);

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (id == null)
            {
                return BadRequest("id cannot be null");
            }
            LessonActivityType type = Unit_Of_Work.lessonActivityType_Repository.First_Or_Default(s => s.ID == id && s.IsDeleted != true);
            if (type == null)
            {
                return BadRequest("Type not exist");
            }
            //if (userTypeClaim == "employee")
            //{
            //    IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "EvaluationTemplateGroupQuestion", roleId, userId, question);
            //    if (accessCheck != null)
            //    {
            //        return accessCheck;
            //    }
            //}


            type.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            type.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                type.DeletedByOctaId = userId;
                if (type.DeletedByUserId != null)
                {
                    type.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                type.DeletedByUserId = userId;
                if (type.DeletedByOctaId != null)
                {
                    type.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.lessonActivityType_Repository.Update(type);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
