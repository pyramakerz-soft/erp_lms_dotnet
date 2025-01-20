using AutoMapper;
using LMS_CMS_BL.DTO.Registration;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.RegisterationModule;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers.Domains.Registeration
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class RegisterationFormSubmittionController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public RegisterationFormSubmittionController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        ///////////////////////////////////////////////////////////////////////////////////

        [HttpGet("GetByRegistrationParentID/{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Registration Confirmation", "Registration" }
        )]
        public async Task<IActionResult> GetByRegistrationParentID(long id)
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

            List<RegisterationFormSubmittion> registerationFormSubmittions = await Unit_Of_Work.registerationFormSubmittion_Repository.Select_All_With_IncludesById<RegisterationFormSubmittion>(
                    r => r.IsDeleted != true && r.RegisterationFormParentID == id,
                    query => query.Include(emp => emp.RegisterationFormParent),
                    query => query.Include(emp => emp.CategoryField).ThenInclude(f => f.RegistrationCategory),
                    query => query.Include(emp => emp.FieldOption));

            if (registerationFormSubmittions == null || registerationFormSubmittions.Count == 0)
            {
                return NotFound();
            }

            string serverUrl = $"{Request.Scheme}://{Request.Host}/";
            foreach (var r in registerationFormSubmittions)
            {
                if (!string.IsNullOrEmpty(r.TextAnswer) && r.CategoryField.FieldTypeID == 6)
                {
                    r.TextAnswer = $"{serverUrl}{r.TextAnswer.Replace("\\", "/")}";
                }
            }
            List<RegisterationFormSubmittionGetDTO> registerationFormSubmittionDTO = mapper.Map<List<RegisterationFormSubmittionGetDTO>>(registerationFormSubmittions);


            return Ok(registerationFormSubmittionDTO);
        }
    }
}
