using AutoMapper;
using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.DTO.Octa;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_DAL.Models.Octa;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers.Octa
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OctaController : ControllerBase
    {
        private readonly UOW _Unit_Of_Work;
        private readonly DynamicDatabaseService _dynamicDatabaseService;
        IMapper mapper;

        public OctaController(DynamicDatabaseService dynamicDatabaseService, UOW Unit_Of_Work, IMapper mapper)
        {
            _Unit_Of_Work = Unit_Of_Work;
            _dynamicDatabaseService = dynamicDatabaseService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize_Endpoint_(
             allowedTypes: new[] { "octa" }
         )]
        public IActionResult Get()
        {
            List<LMS_CMS_DAL.Models.Octa.Octa> octa = _Unit_Of_Work.octa_Repository.FindBy_Octa(
                    b => b.IsDeleted != true);

            if (octa == null || octa.Count == 0)
            {
                return NotFound();
            }

            List<OctaGetDTO> octaDTO = mapper.Map<List<OctaGetDTO>>(octa);

            return Ok(octaDTO);
        }


        [HttpGet("{Id}")]
        [Authorize_Endpoint_(
             allowedTypes: new[] { "octa" }
         )]
        public IActionResult GetByID(long Id)
        {
            LMS_CMS_DAL.Models.Octa.Octa octa = _Unit_Of_Work.octa_Repository.First_Or_Default_Octa(
                    b => b.ID == Id && b.IsDeleted != true);

            if (octa == null)
            {
                return NotFound();
            }

            OctaGetDTO octaDTO = mapper.Map<OctaGetDTO>(octa);

            return Ok(octaDTO);
        }

        [HttpPost]
        [Authorize_Endpoint_(
             allowedTypes: new[] { "octa" }
         )]
        public IActionResult Add(OctaAddDTO newAcc)
        {
            var userClaims = HttpContext.User.Claims;
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (newAcc == null)
            {
                return BadRequest("Octa cannot be null");
            }

            LMS_CMS_DAL.Models.Octa.Octa octa = mapper.Map<LMS_CMS_DAL.Models.Octa.Octa>(newAcc);
            octa.InsertedByUserId = userId;
            octa.InsertedAt= TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            _Unit_Of_Work.octa_Repository.Add_Octa(octa);
            _Unit_Of_Work.SaveOctaChanges();
            return Ok(newAcc);
        }

        [HttpPut]
        [Authorize_Endpoint_(
             allowedTypes: new[] { "octa" }
         )]
        public IActionResult Edit(OctaPutDTO editedAcc)
        {
            var userClaims = HttpContext.User.Claims;
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (editedAcc == null)
            {
                return BadRequest("Octa cannot be null");
            }

            LMS_CMS_DAL.Models.Octa.Octa octaExists = _Unit_Of_Work.octa_Repository.First_Or_Default_Octa(b => b.ID == editedAcc.ID && b.IsDeleted != true);
            if (octaExists == null || octaExists.IsDeleted == true)
            {
                return NotFound("No Octa with this ID");
            }

            mapper.Map(editedAcc, octaExists);
            octaExists.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            octaExists.UpdatedByUserId = userId;
            _Unit_Of_Work.octa_Repository.Update_Octa(octaExists);
            _Unit_Of_Work.SaveOctaChanges();
            return Ok(editedAcc);
        }

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
             allowedTypes: new[] { "octa" }
         )]
        public IActionResult Delete(long id)
        {
            var userClaims = HttpContext.User.Claims;
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");

            if (userTypeClaim == null)
            {
                return Unauthorized("User Type claim not found.");
            }

            LMS_CMS_DAL.Models.Octa.Octa octaExists = _Unit_Of_Work.octa_Repository.First_Or_Default_Octa(b => b.ID == id && b.IsDeleted != true);
            if (octaExists == null || octaExists.IsDeleted == true)
            {
                return NotFound("No Octa with this ID");
            }

            octaExists.DeletedByUserId = userId;
            octaExists.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            octaExists.IsDeleted= true;
            _Unit_Of_Work.octa_Repository.Update_Octa(octaExists);
            _Unit_Of_Work.SaveOctaChanges();
            return Ok();
        }

    }
}
