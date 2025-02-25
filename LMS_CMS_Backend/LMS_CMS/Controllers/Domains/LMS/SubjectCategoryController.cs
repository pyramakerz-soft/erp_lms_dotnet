using AutoMapper;
using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
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
    public class SubjectCategoryController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;

        public SubjectCategoryController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
        }

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Subject Categories"}
        )]
        public IActionResult GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<SubjectCategory> SubjectCategorys = Unit_Of_Work.subjectCategory_Repository.FindBy(
                    f => f.IsDeleted != true);

            if (SubjectCategorys == null || SubjectCategorys.Count == 0)
            {
                return NotFound();
            }

            List<SubjectCategoryGetDTO> SubjectCategorysDTO = mapper.Map<List<SubjectCategoryGetDTO>>(SubjectCategorys);

            return Ok(SubjectCategorysDTO);
        }

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Subject Categories" }
        )]
        public IActionResult GetById(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (id == 0)
            {
                return BadRequest("Enter Subject Category ID");
            }

            SubjectCategory subjectCategory = Unit_Of_Work.subjectCategory_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);


            if (subjectCategory == null)
            {
                return NotFound();
            }

            SubjectCategoryGetDTO subjectCategoryGetDTO = mapper.Map<SubjectCategoryGetDTO>(subjectCategory);

            return Ok(subjectCategoryGetDTO);
        }

        [HttpPost]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" },
           pages: new[] { "Subject Categories" }
       )]
        public IActionResult Add(SubjectCategoryAddDTO NewSubCat)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (NewSubCat == null)
            {
                return BadRequest("Subject Category cannot be null");
            }
            if (NewSubCat.Name == null)
            {
                return BadRequest("the name cannot be null");
            }
            SubjectCategory subjectCat = mapper.Map<SubjectCategory>(NewSubCat);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            subjectCat.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                subjectCat.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                subjectCat.InsertedByUserId = userId;
            }

            Unit_Of_Work.subjectCategory_Repository.Add(subjectCat);
            Unit_Of_Work.SaveChanges();
            return Ok(NewSubCat);
        }

        [HttpPut]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" },
           allowEdit: 1,
           pages: new[] { "Subject Categories" }
       )]
        public IActionResult Edit(SubjectCategoryPutDTO EditedSubjectCategory)
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

            if (EditedSubjectCategory == null)
            {
                return BadRequest("Subject Category cannot be null");
            }

            if (EditedSubjectCategory.Name == null)
            {
                return BadRequest("the name cannot be null");
            }
            SubjectCategory SubjectCategoryExists = Unit_Of_Work.subjectCategory_Repository.First_Or_Default(s=>s.ID==EditedSubjectCategory.ID&&s.IsDeleted!=true);
            if (SubjectCategoryExists == null || SubjectCategoryExists.IsDeleted == true)
            {
                return NotFound("No Subject Category with this ID");
            } 

            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Subject Categories", roleId, userId, SubjectCategoryExists);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            mapper.Map(EditedSubjectCategory, SubjectCategoryExists);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            SubjectCategoryExists.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                SubjectCategoryExists.UpdatedByOctaId = userId;
                if (SubjectCategoryExists.UpdatedByUserId != null)
                {
                    SubjectCategoryExists.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                SubjectCategoryExists.UpdatedByUserId = userId;
                if (SubjectCategoryExists.UpdatedByOctaId != null)
                {
                    SubjectCategoryExists.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.subjectCategory_Repository.Update(SubjectCategoryExists);
            Unit_Of_Work.SaveChanges();
            return Ok(EditedSubjectCategory);
        }

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Subject Categories" }
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
                return BadRequest("Enter Subject Category ID");
            }

            SubjectCategory subjectCategory = Unit_Of_Work.subjectCategory_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);


            if (subjectCategory == null)
            {
                return NotFound();
            } 

            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "Subject Categories", roleId, userId, subjectCategory);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            subjectCategory.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            subjectCategory.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                subjectCategory.DeletedByOctaId = userId;
                if (subjectCategory.DeletedByUserId != null)
                {
                    subjectCategory.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                subjectCategory.DeletedByUserId = userId;
                if (subjectCategory.DeletedByOctaId != null)
                {
                    subjectCategory.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.subjectCategory_Repository.Update(subjectCategory);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
