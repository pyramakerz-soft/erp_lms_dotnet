using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Octa;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Controllers.Octa
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OctaController : ControllerBase
    {
        private readonly UOW _Unit_Of_Work;
        private readonly DynamicDatabaseService _dynamicDatabaseService;

        public OctaController(DynamicDatabaseService dynamicDatabaseService, UOW Unit_Of_Work)
        {
            _Unit_Of_Work = Unit_Of_Work;
            _dynamicDatabaseService = dynamicDatabaseService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var userClaims = HttpContext.User.Claims;
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
            if (userTypeClaim == null)
            {
                return Unauthorized("User Type claim not found.");
            }

            if (userTypeClaim != "octa")
            {
                return Unauthorized("Access Denied");
            }

            List<LMS_CMS_DAL.Models.Octa.Octa> octas = _Unit_Of_Work.octa_Repository.Select_All_Octa();

            return Ok(octas);
        }


        [HttpGet("{Id}")]
        public IActionResult GetByID(long Id)
        {
            var userClaims = HttpContext.User.Claims;
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
            if (userTypeClaim == null)
            {
                return Unauthorized("User Type claim not found.");
            }

            if (userTypeClaim != "octa")
            {
                return Unauthorized("Access Denied");
            }

            LMS_CMS_DAL.Models.Octa.Octa octas = _Unit_Of_Work.octa_Repository.Select_By_Id_Octa(Id);

            return Ok(octas);
        }

    }
}
