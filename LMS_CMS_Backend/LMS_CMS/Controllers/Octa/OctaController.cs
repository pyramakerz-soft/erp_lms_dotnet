using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Octa;
using LMS_CMS_PL.Attribute;
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
        [Authorize_Endpoint_(
             allowedTypes: new[] { "octa" }
         )]
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
        [Authorize_Endpoint_(
             allowedTypes: new[] { "octa" }
         )]
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

        [HttpPost]
        [Authorize_Endpoint_(
             allowedTypes: new[] { "octa" }
         )]
        public IActionResult Add(LMS_CMS_DAL.Models.Octa.Octa newAcc)
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

             _Unit_Of_Work.octa_Repository.Add_Octa(newAcc);
            _Unit_Of_Work.SaveOctaChanges();
            return Ok(newAcc);
        }

        [HttpPut]
        [Authorize_Endpoint_(
             allowedTypes: new[] { "octa" }
         )]
        public IActionResult Edit(LMS_CMS_DAL.Models.Octa.Octa newAcc)
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

            _Unit_Of_Work.octa_Repository.Update_Octa(newAcc);
            _Unit_Of_Work.SaveOctaChanges();
            return Ok(newAcc);
        }

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
             allowedTypes: new[] { "octa" }
         )]
        public IActionResult Delete(long id)
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

            LMS_CMS_DAL.Models.Octa.Octa octas = _Unit_Of_Work.octa_Repository.Select_By_Id_Octa(id);
            _Unit_Of_Work.octa_Repository.Delete_Octa(id);
            _Unit_Of_Work.SaveOctaChanges();
            return Ok();
        }

    }
}
