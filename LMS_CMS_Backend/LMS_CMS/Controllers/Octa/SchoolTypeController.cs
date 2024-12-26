using AutoMapper;
using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.DTO.Octa;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.LMS;
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
    public class SchoolTypeController : ControllerBase
    {
        private readonly UOW _Unit_Of_Work;
        private readonly DynamicDatabaseService _dynamicDatabaseService;
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public SchoolTypeController(DynamicDatabaseService dynamicDatabaseService, UOW Unit_Of_Work, DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _Unit_Of_Work = Unit_Of_Work;
            _dynamicDatabaseService = dynamicDatabaseService;
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
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

            List<LMS_CMS_DAL.Models.Octa.SchoolType> SchoolTypes = _Unit_Of_Work.schoolType_Octa_Repository.Select_All_Octa();

            return Ok(SchoolTypes);
        }
        ////////////////////////////////////////////////////

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

            LMS_CMS_DAL.Models.Octa.SchoolType SchoolTypes = _Unit_Of_Work.schoolType_Octa_Repository.Select_By_Id_Octa(Id);

            return Ok(SchoolTypes);
        }

        ////////////////////////////////////////////////////

        [HttpPost]
        public IActionResult Add(LMS_CMS_BL.DTO.Octa.SchoolTypeAddDTO schoolType)
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

            LMS_CMS_DAL.Models.Octa.SchoolType SchoolTypeDTO = mapper.Map<LMS_CMS_DAL.Models.Octa.SchoolType>(schoolType);

            _Unit_Of_Work.schoolType_Octa_Repository.Add_Octa(SchoolTypeDTO);
            _Unit_Of_Work.SaveOctaChanges();

            List<Domain> domains = _Unit_Of_Work.domain_Octa_Repository.Select_All_Octa();

            for (int i = 0; i < domains.Count; i++)
            {
                var domainConStr = domains[i].ConnectionString;
                // Make the DB Connection
                HttpContext.Items["ConnectionString"] = domainConStr;
                UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

                // Add The school type to the domian
                LMS_CMS_DAL.Models.Domains.LMS.SchoolType schoolTypeDomain = new LMS_CMS_DAL.Models.Domains.LMS.SchoolType();
                schoolTypeDomain.Name = schoolType.Name;
                //schoolTypeDomain.ID = schoolType.ID;
                Unit_Of_Work.schoolType_Repository.Add(schoolTypeDomain);
                Unit_Of_Work.SaveChanges();
            }

            return Ok(schoolType);
        }

        ////////////////////////////////////////////////////
        [HttpPut]
        public IActionResult Edit(LMS_CMS_DAL.Models.Octa.SchoolType schoolType)
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

            _Unit_Of_Work.schoolType_Octa_Repository.Update_Octa(schoolType);
            _Unit_Of_Work.SaveOctaChanges();
            return Ok(schoolType);
        }

        ////////////////////////////////////////////////////

        [HttpDelete("{id}")]
        public IActionResult delete(long id)
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

            LMS_CMS_DAL.Models.Octa.SchoolType octas = _Unit_Of_Work.schoolType_Octa_Repository.Select_By_Id_Octa(id);
            _Unit_Of_Work.schoolType_Octa_Repository.Delete_Octa(id);
            _Unit_Of_Work.SaveOctaChanges();
            return Ok();
        }
    }

}
