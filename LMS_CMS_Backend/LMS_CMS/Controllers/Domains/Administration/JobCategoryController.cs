using AutoMapper;
using LMS_CMS_BL.DTO.Administration;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.Administration;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Controllers.Domains.Administration
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class JobCategoryController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;

        public JobCategoryController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
        }

        ///////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
       allowedTypes: new[] { "octa", "employee" },
       pages: new[] { "Job Category" }
       )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<JobCategory> jobCategories = await Unit_Of_Work.jobCategory_Repository.Select_All_With_IncludesById<JobCategory>(
                    b => b.IsDeleted != true);

            if (jobCategories == null || jobCategories.Count == 0)
            {
                return NotFound();
            }

            List<JobCategoryGetDto> JobCategoryDto = mapper.Map<List<JobCategoryGetDto>>(jobCategories);

            return Ok(JobCategoryDto);
        }

        //////////////////////////////////////////////////////////////////////////////
         
        [HttpGet("{id}")]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" },
        pages: new[] { "Job Category" }
         )]
        public async Task<IActionResult> GetbyIdAsync(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            JobCategory jobCategory = Unit_Of_Work.jobCategory_Repository.First_Or_Default(d => d.ID == id && d.IsDeleted != true);

            if (jobCategory == null)
            {
                return NotFound();
            }

            JobCategoryGetDto jobDTO = mapper.Map<JobCategoryGetDto>(jobCategory);

            return Ok(jobDTO);
        }
        //////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" },
       pages: new[] { "Job Category" }
       )]
        public IActionResult Add(JobCategoryAddDto newCategory)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (newCategory == null)
            {
                return BadRequest("Job Category cannot be null");
            }
            JobCategory jobCategory = mapper.Map<JobCategory>(newCategory);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            jobCategory.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                jobCategory.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                jobCategory.InsertedByUserId = userId;
            }

            Unit_Of_Work.jobCategory_Repository.Add(jobCategory);
            Unit_Of_Work.SaveChanges();
            return Ok(newCategory);
        }

        //////////////////////////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Job Category" }
        )]
        public IActionResult Edit(JobCategoryGetDto newCategory)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
            var userRoleClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            long.TryParse(userRoleClaim, out long roleId);

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID, Type claim not found.");
            }

            if (newCategory == null)
            {
                return BadRequest("Job Category cannot be null");
            }

            JobCategory category = Unit_Of_Work.jobCategory_Repository.First_Or_Default(d => d.ID == newCategory.ID && d.IsDeleted != true);
            if (category == null)
            {
                return NotFound("There is no Job Category with this id");
            }
             
            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Job Category", roleId, userId, category);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            mapper.Map(newCategory, category);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            category.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                category.UpdatedByOctaId = userId;
                if (category.UpdatedByUserId != null)
                {
                    category.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                category.UpdatedByUserId = userId;
                if (category.UpdatedByOctaId != null)
                {
                    category.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.jobCategory_Repository.Update(category);
            Unit_Of_Work.SaveChanges();
            return Ok(newCategory);
        }

        //////////////////////////////////////////////////////////////////////////////////

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Job Category" }
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

            if (id == 0)
            {
                return BadRequest("Enter Category ID");
            }

            JobCategory category = Unit_Of_Work.jobCategory_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);


            if (category == null)
            {
                return NotFound();
            }

            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "Job Category", roleId, userId, category);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            category.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            category.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                category.DeletedByOctaId = userId;
                if (category.DeletedByUserId != null)
                {
                    category.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                category.DeletedByUserId = userId;
                if (category.DeletedByOctaId != null)
                {
                    category.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.jobCategory_Repository.Update(category);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
