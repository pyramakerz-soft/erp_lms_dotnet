using AutoMapper;
using LMS_CMS_BL.DTO.Bus;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.BusModule;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Controllers.Domains.Bus
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class BusDistrictController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;

        IMapper mapper;

        public BusDistrictController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }


        ///////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Busses", "Bus Districts" }
        )]
        public IActionResult Get()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<BusDistrict> busDistricts;

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            busDistricts = Unit_Of_Work.busDistrict_Repository.FindBy(t => t.IsDeleted != true);

            if (busDistricts == null || busDistricts.Count == 0)
            {
                return NotFound();
            }

            List<BusDistrictGetDTO> busDistrictsDTO = mapper.Map<List<BusDistrictGetDTO>>(busDistricts);

            return Ok(busDistrictsDTO);
        }

        ///////////////////////////////////////////////////

        [HttpGet("id")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Busses", "Bus Districts" }
        )]
        public IActionResult GetById(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (id == 0)
            {
                return BadRequest("Enter Bus District ID");
            }

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            BusDistrict busDistrict = Unit_Of_Work.busDistrict_Repository.Select_By_Id(id);

            if (busDistrict == null || busDistrict.IsDeleted == true)
            {
                return NotFound("No bus District with this ID");
            }

            BusDistrictGetDTO busDistrictDto = mapper.Map<BusDistrictGetDTO>(busDistrict);
            return Ok(busDistrictDto);
        }
        ///////////////////////////////////////////////////

        //[HttpGet("DomainId")]
        //[Authorize_Endpoint_Attribute(
        //    allowedTypes: new[] { "pyramakerz", "employee" },
        //    pages: new[] { "Busses", "Bus Districts" }
        //)]
        //public IActionResult GetByDomainId(long id)
        //{
        //    var userClaims = HttpContext.User.Claims;
        //    var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
        //    long.TryParse(userIdClaim, out long userId);
        //    var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

        //    if (userIdClaim == null || userTypeClaim == null)
        //    {
        //        return Unauthorized("User ID or Type claim not found.");
        //    }

        //    if (userTypeClaim == "employee")
        //    {
        //        Employee employee = Unit_Of_Work.employee_Repository.Select_By_Id(userId);
        //        long employeeDomain = employee.Domain_ID;

        //        if (id != employeeDomain)
        //        {
        //            return Unauthorized();
        //        }
        //    }

        //    Domain domain = Unit_Of_Work.domain_Repository.Select_By_Id(id);
        //    if (domain == null)
        //    {
        //        return NotFound("No Domain with this Id");
        //    }

        //    List<BusDistrict> BusDistrict = Unit_Of_Work.busDistrict_Repository.FindBy(s => s.DomainId == id && s.IsDeleted != true);
        //    if (BusDistrict == null || BusDistrict.Count == 0)
        //    {
        //        return NotFound("There are no bus Districts in this domian");
        //    }

        //    List<BusDistrictGetDTO> BusDistrictDTO = mapper.Map<List<BusDistrictGetDTO>>(BusDistrict);

        //    return Ok(BusDistrictDTO);
        //}

        ///////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Busses", "Bus Districts" }
        )]
        public IActionResult Add(BusDistrictAddDTO NewDistrict)
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

            if (NewDistrict == null)
            {
                return BadRequest("Bus District cannot be null");
            }

            BusDistrict busDistrict = mapper.Map<BusDistrict>(NewDistrict);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            busDistrict.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                busDistrict.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                busDistrict.InsertedByUserId = userId;
            }

            Unit_Of_Work.busDistrict_Repository.Add(busDistrict);
            Unit_Of_Work.SaveChanges();
            return Ok(NewDistrict);

        }

        ////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Busses", "Bus Districts" }
        )]
        public IActionResult Edit(BusDistrictEditDTO EditBusDistrict)
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

            if (EditBusDistrict == null)
            {
                BadRequest();
            }

            BusDistrict busDistrict = Unit_Of_Work.busDistrict_Repository.Select_By_Id(EditBusDistrict.ID);
            if (busDistrict == null || busDistrict.IsDeleted == true)
            {
                return NotFound("No Bus District with this ID");
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Bus Districts");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (busDistrict.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Bus Districts page doesn't exist");
                }
            }

            mapper.Map(EditBusDistrict, busDistrict);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            busDistrict.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                busDistrict.UpdatedByOctaId = userId;
                if (busDistrict.UpdatedByUserId != null)
                {
                    busDistrict.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                busDistrict.UpdatedByUserId = userId;
                if (busDistrict.UpdatedByOctaId != null)
                {
                    busDistrict.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.busDistrict_Repository.Update(busDistrict);
            Unit_Of_Work.SaveChanges();
            return Ok(EditBusDistrict);
        }

        ////////////////////////////////////////////////////////

        [HttpDelete]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Busses", "Bus Districts" }
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
                return BadRequest("Bus District ID cannot be null.");
            }

            BusDistrict busDistrict = Unit_Of_Work.busDistrict_Repository.Select_By_Id(id);
            if (busDistrict == null || busDistrict.IsDeleted == true)
            {
                return NotFound("No Bus District with this ID");
            }
            else
            {
                if (userTypeClaim == "employee")
                {
                    Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Bus Districts"); 
                    if (page != null)
                    {
                        Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                        if (roleDetails != null && roleDetails.Allow_Delete_For_Others == false)
                        {
                            if (busDistrict.InsertedByUserId != userId)
                            {
                                return Unauthorized();
                            }
                        }
                    }
                    else
                    {
                        return BadRequest("Bus Districts page doesn't exist");
                    }
                }

                busDistrict.IsDeleted = true;
                TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
                busDistrict.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                if (userTypeClaim == "octa")
                {
                    busDistrict.DeletedByOctaId = userId;
                    if (busDistrict.DeletedByUserId != null)
                    {
                        busDistrict.DeletedByUserId = null;
                    }
                }
                else if (userTypeClaim == "employee")
                {
                    busDistrict.DeletedByUserId = userId;
                    if (busDistrict.DeletedByOctaId != null)
                    {
                        busDistrict.DeletedByOctaId = null;
                    }
                }

                Unit_Of_Work.busDistrict_Repository.Update(busDistrict);
                Unit_Of_Work.SaveChanges();
                return Ok();
            }
        }
    }
}
