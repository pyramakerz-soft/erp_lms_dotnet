using AutoMapper;
using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.DTO.Octa;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.BusModule;
using LMS_CMS_DAL.Models.Domains.LMS;
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
    public class SchoolTypeController : ControllerBase
    {
        private readonly UOW _Unit_Of_Work;
        private readonly DynamicDatabaseService _dynamicDatabaseService;
        private readonly DbContextFactoryService _dbContextFactory;
        private readonly GetConnectionStringService _getConnectionStringService;
        IMapper mapper;

        public SchoolTypeController(DynamicDatabaseService dynamicDatabaseService, UOW Unit_Of_Work, DbContextFactoryService dbContextFactory, IMapper mapper, GetConnectionStringService getConnectionStringService)
        {
            _Unit_Of_Work = Unit_Of_Work;
            _dynamicDatabaseService = dynamicDatabaseService;
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _getConnectionStringService = getConnectionStringService;
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

            List<LMS_CMS_DAL.Models.Octa.SchoolType> SchoolTypes = _Unit_Of_Work.schoolType_Octa_Repository.Select_All_Octa();

            return Ok(SchoolTypes);
        }
        ////////////////////////////////////////////////////

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

            LMS_CMS_DAL.Models.Octa.SchoolType SchoolTypes = _Unit_Of_Work.schoolType_Octa_Repository.Select_By_Id_Octa(Id);

            return Ok(SchoolTypes);
        }

        ////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
             allowedTypes: new[] { "octa" }
         )]
        public IActionResult Add(LMS_CMS_BL.DTO.Octa.SchoolTypeAddDTO schoolType)
        {
            var userClaims = HttpContext.User.Claims;
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            if (userTypeClaim == null || userIdClaim == null)
            {
                return Unauthorized("User Type claim not found.");
            }

            if (userTypeClaim != "octa")
            {
                return Unauthorized("Access Denied");
            }

            LMS_CMS_DAL.Models.Octa.SchoolType st = _Unit_Of_Work.schoolType_Octa_Repository.First_Or_Default_Octa(
                s => s.Name == schoolType.Name
                );

            if(st != null)
            {
                return BadRequest("Name Already Exists");
            }

            LMS_CMS_DAL.Models.Octa.SchoolType SchoolTypeDTO = mapper.Map<LMS_CMS_DAL.Models.Octa.SchoolType>(schoolType);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            SchoolTypeDTO.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            SchoolTypeDTO.InsertedByUserId = userId;
            _Unit_Of_Work.schoolType_Octa_Repository.Add_Octa(SchoolTypeDTO);
            _Unit_Of_Work.SaveOctaChanges();

            List<Domain> domains = _Unit_Of_Work.domain_Octa_Repository.Select_All_Octa();

            for (int i = 0; i < domains.Count; i++)
            {
                //var domainConStr = domains[i].ConnectionString; 
                //HttpContext.Items["ConnectionString"] = domainConStr; 
                HttpContext.Items["ConnectionString"] = _getConnectionStringService.BuildConnectionString(domains[i].Name);
                UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

                // Add The school type to the domian
                LMS_CMS_DAL.Models.Domains.LMS.SchoolType schoolTypeDomain = new LMS_CMS_DAL.Models.Domains.LMS.SchoolType();
                schoolTypeDomain.Name = schoolType.Name;
                schoolTypeDomain.ID = SchoolTypeDTO.ID;
                Unit_Of_Work.schoolType_Repository.Add(schoolTypeDomain);
                Unit_Of_Work.SaveChanges();
            }

            return Ok(schoolType);
        }

        ////////////////////////////////////////////////////
        [HttpPut]
        [Authorize_Endpoint_(
             allowedTypes: new[] { "octa" }
         )]
        public IActionResult Edit(SchoolTypePutDTO schoolType)
        {
            var userClaims = HttpContext.User.Claims;
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            if (userTypeClaim == null || userIdClaim == null)
            {
                return Unauthorized("User Type claim not found.");
            }

            if (userTypeClaim != "octa")
            {
                return Unauthorized("Access Denied");
            }

            LMS_CMS_DAL.Models.Octa.SchoolType schoolTypeExists = _Unit_Of_Work.schoolType_Octa_Repository.Select_By_Id_Octa(schoolType.ID);

            if (schoolTypeExists == null)
            {
                return NotFound("No School Type with this ID");
            }

            LMS_CMS_DAL.Models.Octa.SchoolType st = _Unit_Of_Work.schoolType_Octa_Repository.First_Or_Default_Octa(
                s => s.Name == schoolType.Name
                );

            if (st != null)
            {
                return BadRequest("Name Already Exists");
            }

            mapper.Map(schoolType, schoolTypeExists);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            schoolTypeExists.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            schoolTypeExists.UpdatedByUserId = userId;

            _Unit_Of_Work.schoolType_Octa_Repository.Update_Octa(schoolTypeExists);
            _Unit_Of_Work.SaveOctaChanges();

            List<Domain> domains = _Unit_Of_Work.domain_Octa_Repository.Select_All_Octa();

            for (int i = 0; i < domains.Count; i++)
            {
                //var domainConStr = domains[i].ConnectionString; 
                //HttpContext.Items["ConnectionString"] = domainConStr; 
                HttpContext.Items["ConnectionString"] = _getConnectionStringService.BuildConnectionString(domains[i].Name);
                UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

                // Update The school type in the domian
                LMS_CMS_DAL.Models.Domains.LMS.SchoolType schoolTypeDomainExists = Unit_Of_Work.schoolType_Repository.Select_By_Id(schoolType.ID);

                mapper.Map(schoolType, schoolTypeDomainExists);
                Unit_Of_Work.schoolType_Repository.Update(schoolTypeDomainExists);
                Unit_Of_Work.SaveChanges();
            }

            return Ok(schoolType);
        }

        ////////////////////////////////////////////////////

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

            LMS_CMS_DAL.Models.Octa.SchoolType octas = _Unit_Of_Work.schoolType_Octa_Repository.Select_By_Id_Octa(id);

            if (octas == null)
            {
                return NotFound();
            }


            if (id == 0)
            {
                return BadRequest("Enter School Type ID");
            }

            _Unit_Of_Work.schoolType_Octa_Repository.Delete_Octa(id);
            _Unit_Of_Work.SaveOctaChanges();

            List<Domain> domains = _Unit_Of_Work.domain_Octa_Repository.Select_All_Octa();

            for (int i = 0; i < domains.Count; i++)
            {
                //var domainConStr = domains[i].ConnectionString; 
                //HttpContext.Items["ConnectionString"] = domainConStr; 
                HttpContext.Items["ConnectionString"] = _getConnectionStringService.BuildConnectionString(domains[i].Name);
                UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

                // Delete The school type from the domian
                LMS_CMS_DAL.Models.Domains.LMS.SchoolType schoolTypeDomainExists = Unit_Of_Work.schoolType_Repository.Select_By_Id(id);

                if (schoolTypeDomainExists == null)
                {
                    return NotFound();
                }

                Unit_Of_Work.schoolType_Repository.Delete(id);
                Unit_Of_Work.SaveChanges();
            }
            return Ok();
        }
    }

}
