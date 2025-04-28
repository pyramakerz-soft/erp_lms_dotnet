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
    public class BookCorrectionController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;

        public BookCorrectionController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
        }
        ///////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        [Authorize_Endpoint_(
          allowedTypes: new[] { "octa", "employee" }
          //pages: new[] { "" }
      )]
        public IActionResult Get()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<EvaluationBookCorrection> Books;

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            Books = Unit_Of_Work.evaluationBookCorrection_Repository.FindBy(t => t.IsDeleted != true);

            if (Books == null || Books.Count == 0)
            {
                return NotFound();
            }

            List<EvaluationBookCorrectionGetDTO> Dto = mapper.Map<List<EvaluationBookCorrectionGetDTO>>(Books);

            return Ok(Dto);
        }

        ///////////////////////////////////////////////////////////////////////////////////

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" }
           //pages: new[] { "" }
       )]
        public async Task<IActionResult> GetById(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            EvaluationBookCorrection book;

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            book = Unit_Of_Work.evaluationBookCorrection_Repository.First_Or_Default(
                    sem => sem.IsDeleted != true && sem.ID == id);

            if (book == null)
            {
                return NotFound();
            }

            EvaluationBookCorrectionGetDTO Dto = mapper.Map<EvaluationBookCorrectionGetDTO>(book);

            return Ok(Dto);
        }

        ///////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
          allowedTypes: new[] { "octa", "employee" }
          //pages: new[] { "" }
      )]
        public async Task<IActionResult> Add(EvaluationBookCorrectionAddDTO newData)
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
                return BadRequest("Evaluation Book Correction is empty");
            }

            EvaluationBookCorrection book = mapper.Map<EvaluationBookCorrection>(newData);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            book.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                book.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                book.InsertedByUserId = userId;
            }
            Unit_Of_Work.evaluationBookCorrection_Repository.Add(book);
            Unit_Of_Work.SaveChanges();
            return Ok(newData);
        }

        ////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1
            //pages: new[] { "" }
        )]
        public async Task<IActionResult> EditAsync(EvaluationBookCorrectionEditDTO newData)
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

            EvaluationBookCorrection book = Unit_Of_Work.evaluationBookCorrection_Repository.First_Or_Default(s => s.ID == newData.ID && s.IsDeleted != true);
            if (book == null)
            {
                return BadRequest("this Evaluation Template group question not exist");
            }

            //if (userTypeClaim == "employee")
            //{
            //    IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "", roleId, userId, book);
            //    if (accessCheck != null)
            //    {
            //        return accessCheck;
            //    }
            //}

            mapper.Map(newData, book);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            book.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                book.UpdatedByOctaId = userId;
                if (book.UpdatedByUserId != null)
                {
                    book.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                book.UpdatedByUserId = userId;
                if (book.UpdatedByOctaId != null)
                {
                    book.UpdatedByOctaId = null;
                }
            }
            Unit_Of_Work.evaluationBookCorrection_Repository.Update(book);
            Unit_Of_Work.SaveChanges();
            return Ok(newData);
        }
        ////////////////////////////////////////////////////


        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
          allowedTypes: new[] { "octa", "employee" },
          allowDelete: 1
          //pages: new[] { "Grade" }
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
            EvaluationBookCorrection book = Unit_Of_Work.evaluationBookCorrection_Repository.Select_By_Id(id);

            //if (userTypeClaim == "employee")
            //{
            //    IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "", roleId, userId, book);
            //    if (accessCheck != null)
            //    {
            //        return accessCheck;
            //    }
            //}

            if (book == null || book.IsDeleted == true)
            {
                return NotFound("No Evaluation Template group with this ID");
            }

            book.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            book.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                book.DeletedByOctaId = userId;
                if (book.DeletedByUserId != null)
                {
                    book.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                book.DeletedByUserId = userId;
                if (book.DeletedByOctaId != null)
                {
                    book.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.evaluationBookCorrection_Repository.Update(book);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }

    }
}
