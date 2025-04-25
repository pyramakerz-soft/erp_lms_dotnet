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
    public class EvaluationTemplateGroupQuestionsController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;

        public EvaluationTemplateGroupQuestionsController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
        }
        ///////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        [Authorize_Endpoint_(
          allowedTypes: new[] { "octa", "employee" },
          pages: new[] { "" }
      )]
        public IActionResult Get()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<EvaluationTemplateGroupQuestion> questions;

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            questions = Unit_Of_Work.evaluationTemplateGroupQuestion_Repository.FindBy(t => t.IsDeleted != true);

            if (questions == null || questions.Count == 0)
            {
                return NotFound();
            }

            List<EvaluationTemplateGroupQuestionGetDTO> Dto = mapper.Map<List<EvaluationTemplateGroupQuestionGetDTO>>(questions);

            return Ok(Dto);
        }

        ///////////////////////////////////////////////////////////////////////////////////

        [HttpGet("id")]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" },
           pages: new[] { "" }
       )]
        public async Task<IActionResult> GetById(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            EvaluationTemplateGroupQuestion question;

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            question = Unit_Of_Work.evaluationTemplateGroupQuestion_Repository.First_Or_Default(
                    sem => sem.IsDeleted != true && sem.ID == id);

            if (question == null )
            {
                return NotFound();
            }

            EvaluationTemplateGroupQuestionGetDTO Dto = mapper.Map<EvaluationTemplateGroupQuestionGetDTO>(question);

            return Ok(Dto);
        }

        ///////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
          allowedTypes: new[] { "octa", "employee" },
          pages: new[] { "" }
      )]
        public async Task<IActionResult> Add(EvaluationTemplateGroupQuestionAddDTO newData)
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
            if (newData == null)
            {
                return BadRequest("Evaluation Template Group Question is empty");
            }

            EvaluationTemplateGroupQuestion question = mapper.Map<EvaluationTemplateGroupQuestion>(newData);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            question.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                question.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                question.InsertedByUserId = userId;
            }
            Unit_Of_Work.evaluationTemplateGroupQuestion_Repository.Add(question);
            Unit_Of_Work.SaveChanges();
            return Ok(newData);
        }

        ////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "" }
        )]
        public async Task<IActionResult> EditAsync(EvaluationTemplateGroupQuestionEditDTO newData)
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
                return BadRequest("Evaluation Template group question cannot be null");
            }
            if (newData.ID == null)
            {
                return BadRequest("Evaluation Template group question id can not be null");
            }

            EvaluationTemplateGroupQuestion question = Unit_Of_Work.evaluationTemplateGroupQuestion_Repository.First_Or_Default(s => s.ID == newData.ID && s.IsDeleted != true);
            if (question == null)
            {
                return BadRequest("this Evaluation Template group question not exist");
            }

            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "", roleId, userId, question);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            mapper.Map(newData, question);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            question.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                question.UpdatedByOctaId = userId;
                if (question.UpdatedByUserId != null)
                {
                    question.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                question.UpdatedByUserId = userId;
                if (question.UpdatedByOctaId != null)
                {
                    question.UpdatedByOctaId = null;
                }
            }
            Unit_Of_Work.evaluationTemplateGroupQuestion_Repository.Update(question);
            Unit_Of_Work.SaveChanges();
            return Ok(newData);
        }
        ////////////////////////////////////////////////////


        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
          allowedTypes: new[] { "octa", "employee" },
          allowDelete: 1,
          pages: new[] { "Grade" }
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
            EvaluationTemplateGroupQuestion question = Unit_Of_Work.evaluationTemplateGroupQuestion_Repository.Select_By_Id(id);

            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "", roleId, userId, question);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            if (question == null || question.IsDeleted == true)
            {
                return NotFound("No Evaluation Template group with this ID");
            }

            question.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            question.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                question.DeletedByOctaId = userId;
                if (question.DeletedByUserId != null)
                {
                    question.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                question.DeletedByUserId = userId;
                if (question.DeletedByOctaId != null)
                {
                    question.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.evaluationTemplateGroupQuestion_Repository.Update(question);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }

    }
}
