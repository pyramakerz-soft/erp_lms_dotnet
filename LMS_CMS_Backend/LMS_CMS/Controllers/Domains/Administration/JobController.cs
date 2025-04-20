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
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers.Domains.Administration
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class JobController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;

        public JobController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
        }

        ///////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
       allowedTypes: new[] { "octa", "employee" },
       pages: new[] { "Job" }
       )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<Job> jobs = await Unit_Of_Work.job_Repository.Select_All_With_IncludesById<Job>(
                    b => b.IsDeleted != true,
                    query => query.Include(emp => emp.JobCategory)
                    );

            if (jobs == null || jobs.Count == 0)
            {
                return NotFound();
            }

            List<JobGetDTO> JobsDto = mapper.Map<List<JobGetDTO>>(jobs);

            return Ok(JobsDto);
        }


        ///////////////////////////////////////////

        [HttpGet("ByJobCategory/{id}")]
        [Authorize_Endpoint_(
       allowedTypes: new[] { "octa", "employee" },
       pages: new[] { "Job" }
       )]
        public async Task<IActionResult> GetAllByJobCategoryIdAsync(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<Job> jobs = await Unit_Of_Work.job_Repository.Select_All_With_IncludesById<Job>(
                    b => b.IsDeleted != true && b.JobCategoryID==id,
                    query => query.Include(emp => emp.JobCategory)
                    );

            if (jobs == null || jobs.Count == 0)
            {
                return NotFound();
            }

            List<JobGetDTO> JobsDto = mapper.Map<List<JobGetDTO>>(jobs);

            return Ok(JobsDto);
        }

        ///////////////////////////////////////////
         
        [HttpGet("{id}")]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" },
         pages: new[] { "Job" }
         )]
        public async Task<IActionResult> GetbyIdAsync(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            Job job = await Unit_Of_Work.job_Repository.FindByIncludesAsync(
                    b => b.IsDeleted != true && b.ID == id,
                    query => query.Include(emp => emp.JobCategory)
                    );

            if (job == null)
            {
                return NotFound();
            }

            JobGetDTO JobGetDTOs = mapper.Map<JobGetDTO>(job);

            return Ok(JobGetDTOs);
        }
        //////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" },
         pages: new[] { "Job" }
       )]
        public IActionResult Add(JobAddDto newJob)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (newJob == null)
            {
                return BadRequest("Department cannot be null");
            }
            JobCategory jobCategory =Unit_Of_Work.jobCategory_Repository.First_Or_Default(j=>j.ID==newJob.JobCategoryID&&j.IsDeleted!=true);
            if (jobCategory == null) 
            {
             return NotFound("There is no Job Category with this id");
            }

            Job job = mapper.Map<Job>(newJob);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            job.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                job.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                job.InsertedByUserId = userId;
            }

            Unit_Of_Work.job_Repository.Add(job);
            Unit_Of_Work.SaveChanges();
            return Ok(newJob);
        }

        //////////////////////////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Job" }
        )]
        public IActionResult Edit(JobGetDTO newJob)
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

            if (newJob == null)
            {
                return BadRequest("Job cannot be null");
            }
            JobCategory jobCategory = Unit_Of_Work.jobCategory_Repository.First_Or_Default(j => j.ID == newJob.JobCategoryID && j.IsDeleted != true);
            if (jobCategory == null)
            {
                return NotFound("There is no Job Category with this id");
            }
            Job job = Unit_Of_Work.job_Repository.First_Or_Default(d => d.ID == newJob.ID && d.IsDeleted != true);
            if (job == null)
            {
                return NotFound("There is no Job with this id");
            }
             
            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Job", roleId, userId, job);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            mapper.Map(newJob, job);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            job.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                job.UpdatedByOctaId = userId;
                if (job.UpdatedByUserId != null)
                {
                    job.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                job.UpdatedByUserId = userId;
                if (job.UpdatedByOctaId != null)
                {
                    job.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.job_Repository.Update(job);
            Unit_Of_Work.SaveChanges();
            return Ok(newJob);
        }

        //////////////////////////////////////////////////////////////////////////////////

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
          allowedTypes: new[] { "octa", "employee" },
          allowDelete: 1,
         pages: new[] { "Job" }
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
                return BadRequest("Enter Job ID");
            }

            Job job = Unit_Of_Work.job_Repository.First_Or_Default(d => d.ID == id && d.IsDeleted != true);


            if (job == null)
            {
                return NotFound();
            }

            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "Job", roleId, userId, job);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            job.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            job.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                job.DeletedByOctaId = userId;
                if (job.DeletedByUserId != null)
                {
                    job.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                job.DeletedByUserId = userId;
                if (job.DeletedByOctaId != null)
                {
                    job.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.job_Repository.Update(job);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
