using AutoMapper;
using LMS_CMS_BL.DTO.Bus;
using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.BusModule;
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
    public class EvaluationTemplateController : ControllerBase
    {

        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;

        public EvaluationTemplateController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
        }

        ///////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" } ,
            pages: new[] { "Template" }
        )]
        public IActionResult Get()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<LMS_CMS_DAL.Models.Domains.LMS.EvaluationTemplate> templates;

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            templates = Unit_Of_Work.evaluationTemplate_Repository.FindBy(t => t.IsDeleted != true);

            if (templates == null || templates.Count == 0)
            {
                return NotFound();
            }

            List<EvaluationTemplateGetDTO> Dto = mapper.Map<List<EvaluationTemplateGetDTO>>(templates);

            return Ok(Dto);
        }

        ///////////////////////////////////////////////////////////////////////////////////

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" } ,
           pages: new[] { "Template" }
       )]
        public async Task<IActionResult> GetById(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            LMS_CMS_DAL.Models.Domains.LMS.EvaluationTemplate templates;

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            templates = await Unit_Of_Work.evaluationTemplate_Repository.FindByIncludesAsync(
                    sem => sem.IsDeleted != true && sem.ID == id,
                    query => query.Include(emp => emp.EvaluationTemplateGroups.Where(s=>s.IsDeleted!=true)));

            if (templates == null )
            {
                return NotFound();
            }

            EvaluationTemplateGetDTO Dto = mapper.Map<EvaluationTemplateGetDTO>(templates);

            return Ok(Dto);
        }

        ///////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
          allowedTypes: new[] { "octa", "employee" } ,
          pages: new[] { "Template" }
      )]
        public async Task<IActionResult> Add(EvaluationTemplateAddDTO newData)
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
                return BadRequest("Evaluation Template is empty");
            }
         
            EvaluationTemplate template = mapper.Map<EvaluationTemplate>(newData);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            template.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                template.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                template.InsertedByUserId = userId;
            }
            Unit_Of_Work.evaluationTemplate_Repository.Add(template);
            Unit_Of_Work.SaveChanges();
            return Ok(newData);
        }

        ////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1 ,
            pages: new[] { "Template" }
        )]
        public async Task<IActionResult> EditAsync(EvaluationTemplateEditDTO newData)
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
                return BadRequest("Evaluation Template cannot be null");
            }
            if (newData.ID == null)
            {
                return BadRequest("Evaluation Template id can not be null");
            }

            EvaluationTemplate template = Unit_Of_Work.evaluationTemplate_Repository.First_Or_Default(s => s.ID == newData.ID && s.IsDeleted != true);
            if (template == null)
            {
                return BadRequest("this Evaluation Template not exist");
            }

            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Template", roleId, userId, template);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            mapper.Map(newData, template);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            template.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                template.UpdatedByOctaId = userId;
                if (template.UpdatedByUserId != null)
                {
                    template.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                template.UpdatedByUserId = userId;
                if (template.UpdatedByOctaId != null)
                {
                    template.UpdatedByOctaId = null;
                }
            }
            Unit_Of_Work.evaluationTemplate_Repository.Update(template);
            Unit_Of_Work.SaveChanges();
            return Ok(newData);
        }
        ////////////////////////////////////////////////////


        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
          allowedTypes: new[] { "octa", "employee" },
          allowDelete: 1 ,
          pages: new[] { "Template" }
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
            EvaluationTemplate template = Unit_Of_Work.evaluationTemplate_Repository.Select_By_Id(id);

            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "Template", roleId, userId, template);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            if (template == null || template.IsDeleted == true)
            {
                return NotFound("No Evaluation Template with this ID");
            }

            template.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            template.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                template.DeletedByOctaId = userId;
                if (template.DeletedByUserId != null)
                {
                    template.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                template.DeletedByUserId = userId;
                if (template.DeletedByOctaId != null)
                {
                    template.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.evaluationTemplate_Repository.Update(template);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
