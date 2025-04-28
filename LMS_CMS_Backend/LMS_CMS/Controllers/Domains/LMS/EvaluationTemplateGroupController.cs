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
    public class EvaluationTemplateGroupController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;

        public EvaluationTemplateGroupController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
        }

        ///////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" } ,
            pages: new[] { "EvaluationTemplateGroup" }
        )]
        public IActionResult Get()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<EvaluationTemplateGroup> groups;

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            groups = Unit_Of_Work.evaluationTemplateGroup_Repository.FindBy(t => t.IsDeleted != true);

            if (groups == null || groups.Count == 0)
            {
                return NotFound();
            }

            List<EvaluationTemplateGroupDTO> Dto = mapper.Map<List<EvaluationTemplateGroupDTO>>(groups);

            for (int i = 0; i < groups.Count; i++)
            {
                List<EvaluationTemplateGroupQuestion> evaluationTemplateGroupQuestions = Unit_Of_Work.evaluationTemplateGroupQuestion_Repository.FindBy(t => t.IsDeleted != true && t.EvaluationTemplateGroupID == groups[i].ID);   
                List<EvaluationTemplateGroupQuestionGetDTO> evaluationTemplateGroupQuestionGetDTOs = mapper.Map<List<EvaluationTemplateGroupQuestionGetDTO>>(evaluationTemplateGroupQuestions);
                Dto[i].EvaluationTemplateGroupQuestions = evaluationTemplateGroupQuestionGetDTOs;
            }

            return Ok(Dto);
        }
        
        ///////////////////////////////////////////////////////////////////////////////////

        [HttpGet("GetByTemplateID/{templateId}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" } ,
            pages: new[] { "EvaluationTemplateGroup" }
        )]
        public IActionResult GetByTemplateID(long templateId)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<EvaluationTemplateGroup> groups;

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            groups = Unit_Of_Work.evaluationTemplateGroup_Repository.FindBy(t => t.IsDeleted != true && t.EvaluationTemplateID == templateId);

            if (groups == null || groups.Count == 0)
            {
                return NotFound();
            }

            List<EvaluationTemplateGroupDTO> Dto = mapper.Map<List<EvaluationTemplateGroupDTO>>(groups);

            for (int i = 0; i < groups.Count; i++)
            {
                List<EvaluationTemplateGroupQuestion> evaluationTemplateGroupQuestions = Unit_Of_Work.evaluationTemplateGroupQuestion_Repository.FindBy(t => t.IsDeleted != true && t.EvaluationTemplateGroupID == groups[i].ID);   
                List<EvaluationTemplateGroupQuestionGetDTO> evaluationTemplateGroupQuestionGetDTOs = mapper.Map<List<EvaluationTemplateGroupQuestionGetDTO>>(evaluationTemplateGroupQuestions);
                Dto[i].EvaluationTemplateGroupQuestions = evaluationTemplateGroupQuestionGetDTOs;
            }

            return Ok(Dto);
        }

        ///////////////////////////////////////////////////////////////////////////////////

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" } ,
           pages: new[] { "EvaluationTemplateGroup" }
       )]
        public async Task<IActionResult> GetById(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            EvaluationTemplateGroup group;

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            group = await Unit_Of_Work.evaluationTemplateGroup_Repository.FindByIncludesAsync(
                    sem => sem.IsDeleted != true && sem.ID == id,
                    query => query.Include(emp => emp.EvaluationTemplateGroupQuestions.Where(s => s.IsDeleted != true)));

            if (group == null )
            {
                return NotFound();
            }

            EvaluationTemplateGroupDTO Dto = mapper.Map<EvaluationTemplateGroupDTO>(group);

            return Ok(Dto);
        }

        ///////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
          allowedTypes: new[] { "octa", "employee" } ,
          pages: new[] { "EvaluationTemplateGroup" }
      )]
        public async Task<IActionResult> Add(EvaluationTemplateGroupAddDTO newData)
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
                return BadRequest("Evaluation Template Group is empty");
            }

            EvaluationTemplateGroup group = mapper.Map<EvaluationTemplateGroup>(newData);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            group.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                group.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                group.InsertedByUserId = userId;
            }
            Unit_Of_Work.evaluationTemplateGroup_Repository.Add(group);
            Unit_Of_Work.SaveChanges();
            return Ok(newData);
        }

        ////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1 ,
        pages: new[] { "EvaluationTemplateGroup" }
        )]
        public async Task<IActionResult> EditAsync(EvaluationTemplateGroupEditDTO newData)
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
                return BadRequest("Evaluation Template group cannot be null");
            }
            if (newData.ID == null)
            {
                return BadRequest("Evaluation Template group id can not be null");
            }

            EvaluationTemplateGroup group = Unit_Of_Work.evaluationTemplateGroup_Repository.First_Or_Default(s => s.ID == newData.ID && s.IsDeleted != true);
            if (group == null)
            {
                return BadRequest("this Evaluation Template group not exist");
            }

            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "EvaluationTemplateGroup", roleId, userId, group);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            mapper.Map(newData, group);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            group.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                group.UpdatedByOctaId = userId;
                if (group.UpdatedByUserId != null)
                {
                    group.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                group.UpdatedByUserId = userId;
                if (group.UpdatedByOctaId != null)
                {
                    group.UpdatedByOctaId = null;
                }
            }
            Unit_Of_Work.evaluationTemplateGroup_Repository.Update(group);
            Unit_Of_Work.SaveChanges();
            return Ok(newData);
        }
        ////////////////////////////////////////////////////


        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
          allowedTypes: new[] { "octa", "employee" },
          allowDelete: 1 ,
          pages: new[] { "EvaluationTemplateGroup" }
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
            EvaluationTemplateGroup group = Unit_Of_Work.evaluationTemplateGroup_Repository.Select_By_Id(id);

            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "EvaluationTemplateGroup", roleId, userId, group);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            if (group == null || group.IsDeleted == true)
            {
                return NotFound("No Evaluation Template group with this ID");
            }

            group.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            group.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                group.DeletedByOctaId = userId;
                if (group.DeletedByUserId != null)
                {
                    group.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                group.DeletedByUserId = userId;
                if (group.DeletedByOctaId != null)
                {
                    group.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.evaluationTemplateGroup_Repository.Update(group);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}

