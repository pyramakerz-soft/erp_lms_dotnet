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
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LMS_CMS_PL.Controllers.Domains.LMS
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class MedalController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;
        private readonly FileImageValidationService _fileImageValidationService;

        public MedalController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService, FileImageValidationService fileImageValidationService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
            _fileImageValidationService = fileImageValidationService;
        }

        [HttpGet]
        [Authorize_Endpoint_(
          allowedTypes: new[] { "octa", "employee" }
          //,
          //pages: new[] { "" }
      )]
        public IActionResult GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<Medal> medals = Unit_Of_Work.medal_Repository.FindBy(
                    f => f.IsDeleted != true);

            if (medals == null || medals.Count == 0)
            {
                return NotFound();
            }

            List<MedalGetDTO> DTO = mapper.Map<List<MedalGetDTO>>(medals);

            return Ok(DTO);
        }

        ////////////////////////////////////////////////////////////////////////

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
        allowedTypes: new[] { "octa", "employee" }
    //,
    //pages: new[] { "" }
    )]
        public IActionResult GetById(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            Medal medals = Unit_Of_Work.medal_Repository.First_Or_Default(
                    f => f.IsDeleted != true&& f.ID==id);

            if (medals == null )
            {
                return NotFound();
            }

            MedalGetDTO DTO = mapper.Map<MedalGetDTO>(medals);

            return Ok(DTO);
        }

        ////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" }
         //,
         //pages: new[] { "" }
     )]
        public async Task<IActionResult> Add([FromForm] MedalAddDto newMedal)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (newMedal == null)
            {
                return BadRequest("Medal cannot be null");
            }
            if (newMedal.EnglishName == null)
            {
                return BadRequest("the name cannot be null");
            }
            if (newMedal.ArabicName == null)
            {
                return BadRequest("the name cannot be null");
            }

            if (newMedal.ImageForm != null)
            {
                string returnFileInput = _fileImageValidationService.ValidateImageFile(newMedal.ImageForm);
                if (returnFileInput != null)
                {
                    return BadRequest(returnFileInput);
                }
            }


            Medal medal = mapper.Map<Medal>(newMedal);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            medal.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                medal.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                medal.InsertedByUserId = userId;
            }
            medal.ImageLink = "1";
            Unit_Of_Work.medal_Repository.Add(medal);
            Unit_Of_Work.SaveChanges();


            var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/Medal");
            var medalFolder = Path.Combine(baseFolder, medal.ID.ToString());
            if (!Directory.Exists(medalFolder))
            {
                Directory.CreateDirectory(medalFolder);
            }

            if (newMedal.ImageForm != null && newMedal.ImageForm.Length > 0)
            {
                var fileName = Path.GetFileName(newMedal.ImageForm.FileName);
                var filePath = Path.Combine(medalFolder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await newMedal.ImageForm.CopyToAsync(stream);
                }
                //medal.ImageLink = Path.Combine("Uploads", "Medal", medal.ID.ToString(), fileName);
                medal.ImageLink = $"{Request.Scheme}://{Request.Host}/Uploads/Medal/{medal.ID.ToString()}/{fileName}";

            }

            Unit_Of_Work.medal_Repository.Update(medal);
            Unit_Of_Work.SaveChanges();
            return Ok(newMedal);
        }

        ////////////////////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" },
         allowEdit: 1
         //   ,
         //pages: new[] { "" }
     )]
        public async Task<IActionResult> Edit([FromForm] MedalEditDTO newModal)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
            var userRoleClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            long.TryParse(userRoleClaim, out long roleId);

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (newModal == null)
            {
                return BadRequest("Medal cannot be null");
            }


            if (newModal.ImageForm != null)
            {
                string returnFileInput = _fileImageValidationService.ValidateImageFile(newModal.ImageForm);
                if (returnFileInput != null)
                {
                    return BadRequest(returnFileInput);
                }
            }

            Medal medal = Unit_Of_Work.medal_Repository.Select_By_Id(newModal.ID);

            string imageLinkExists = newModal.ImageLink;
            if (medal == null || medal.IsDeleted == true)
            {
                return NotFound("No Medal with this ID");
            }

            //if (userTypeClaim == "employee")
            //{
            //    IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Subject", roleId, userId, medal);
            //    if (accessCheck != null)
            //    {
            //        return accessCheck;
            //    }
            //}
            mapper.Map(newModal, medal);

            if (newModal.ImageForm != null)
            {
                var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/Medal");
                var oldMedalFolder = Path.Combine(baseFolder, newModal.ID.ToString());
                var medalFolder = Path.Combine(baseFolder, newModal.ID.ToString());


                if (System.IO.File.Exists(oldMedalFolder))
                {
                    System.IO.File.Delete(oldMedalFolder); // Delete the old file
                }

                if (Directory.Exists(oldMedalFolder))
                {
                    Directory.Delete(oldMedalFolder, true);
                }

                if (!Directory.Exists(medalFolder))
                {
                    Directory.CreateDirectory(medalFolder);
                }

                var fileName = Path.GetFileName(newModal.ImageForm.FileName);
                var filePath = Path.Combine(medalFolder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await newModal.ImageForm.CopyToAsync(stream);
                }
                medal.ImageLink = $"{Request.Scheme}://{Request.Host}/Uploads/Medal/{medal.ID.ToString()}/{fileName}";

            }


            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            medal.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                medal.UpdatedByOctaId = userId;
                if (medal.UpdatedByUserId != null)
                {
                    medal.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                medal.UpdatedByUserId = userId;
                if (medal.UpdatedByOctaId != null)
                {
                    medal.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.medal_Repository.Update(medal);
            Unit_Of_Work.SaveChanges();
            return Ok(newModal);
        }

        ////////////////////////////////////////////////////////////////////////

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
          allowedTypes: new[] { "octa", "employee" },
          allowDelete: 1
          //  ,
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

            if (id == 0)
            {
                return BadRequest("Enter medal ID");
            }

            Medal medal = Unit_Of_Work.medal_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);


            if (medal == null)
            {
                return NotFound();
            }

            //if (userTypeClaim == "employee")
            //{
            //    IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "Subject", roleId, userId, subject);
            //    if (accessCheck != null)
            //    {
            //        return accessCheck;
            //    }
            //}

            medal.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            medal.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                medal.DeletedByOctaId = userId;
                if (medal.DeletedByUserId != null)
                {
                    medal.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                medal.DeletedByUserId = userId;
                if (medal.DeletedByOctaId != null)
                {
                    medal.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.medal_Repository.Update(medal);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
