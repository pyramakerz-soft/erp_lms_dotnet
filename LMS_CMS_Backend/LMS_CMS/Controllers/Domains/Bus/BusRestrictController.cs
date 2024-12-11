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
    public class BusRestrictController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;

        IMapper mapper;

        public BusRestrictController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }


        ///////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Busses", "Bus Restricts" }
        )]
        public IActionResult Get()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<BusRestrict> busRestricts;

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            busRestricts = Unit_Of_Work.busRestrict_Repository.FindBy(t => t.IsDeleted != true);

            if (busRestricts == null || busRestricts.Count == 0)
            {
                return NotFound();
            }

            List<BusRestrictGetDTO> busRestrictsDTO = mapper.Map<List<BusRestrictGetDTO>>(busRestricts);

            return Ok(busRestrictsDTO);
        }

        ///////////////////////////////////////////////////

        [HttpGet("id")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Busses", "Bus Restricts" }
        )]
        public IActionResult GetById(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (id == 0)
            {
                return BadRequest("Enter Bus Restrict ID");
            }

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            BusRestrict busRestrict = Unit_Of_Work.busRestrict_Repository.Select_By_Id(id);

            if (busRestrict == null || busRestrict.IsDeleted == true)
            {
                return NotFound("No bus restrict with this ID");
            }

            BusRestrictGetDTO busRestrictDto = mapper.Map<BusRestrictGetDTO>(busRestrict);
            return Ok(busRestrictDto);
        }
        ///////////////////////////////////////////////////

        //[HttpGet("DomainId")]
        //[Authorize_Endpoint_Attribute(
        //    allowedTypes: new[] { "pyramakerz", "employee" },
        //    pages: new[] { "Busses", "Bus Restricts" }
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

        //    List<BusRestrict> BusRestrict = Unit_Of_Work.busRestrict_Repository.FindBy(s => s.DomainId == id && s.IsDeleted != true);
        //    if (BusRestrict == null || BusRestrict.Count == 0)
        //    {
        //        return NotFound("There are no bus Restricts in this domian");
        //    }

        //    List<BusRestrictGetDTO> BusRestrictDTO = mapper.Map<List<BusRestrictGetDTO>>(BusRestrict);

        //    return Ok(BusRestrictDTO);
        //}

        ///////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Busses", "Bus Restricts" }
        )]
        public IActionResult Add(BusRestrictAddDTO NewRestrict)
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

            if (NewRestrict == null)
            {
                return BadRequest("Bus Restrict cannot be null");
            }

            BusRestrict busRestrict = mapper.Map<BusRestrict>(NewRestrict);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            busRestrict.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                busRestrict.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                busRestrict.InsertedByUserId = userId;
            }

            Unit_Of_Work.busRestrict_Repository.Add(busRestrict);
            Unit_Of_Work.SaveChanges();
            return Ok(NewRestrict);

        }

        ////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Busses", "Bus Restricts" }
        )]
        public IActionResult Edit(BusRestrictEditDTO EditBusrestrict)
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

            if (EditBusrestrict == null)
            {
                BadRequest();
            }

            BusRestrict busRestrict = Unit_Of_Work.busRestrict_Repository.Select_By_Id(EditBusrestrict.ID);
            if (busRestrict == null || busRestrict.IsDeleted == true)
            {
                return NotFound("No Bus Restrict with this ID");
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Bus Restrict");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (busRestrict.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Bus Restricts page doesn't exist");
                }
            }

            mapper.Map(EditBusrestrict, busRestrict);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            busRestrict.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                busRestrict.UpdatedByOctaId = userId;
                if (busRestrict.UpdatedByUserId != null)
                {
                    busRestrict.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                busRestrict.UpdatedByUserId = userId;
                if (busRestrict.UpdatedByOctaId != null)
                {
                    busRestrict.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.busRestrict_Repository.Update(busRestrict);
            Unit_Of_Work.SaveChanges();
            return Ok(EditBusrestrict);
        }

        ////////////////////////////////////////////////////////

        [HttpDelete]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Busses", "Bus Restricts" }
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
                return BadRequest("Bus Restrict ID cannot be null.");
            }

            BusRestrict busRestrict = Unit_Of_Work.busRestrict_Repository.Select_By_Id(id);
            if (busRestrict == null || busRestrict.IsDeleted == true)
            {
                return NotFound("No Bus Restrict with this ID");
            }
            else
            {
                if (userTypeClaim == "employee")
                {
                    Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Bus Restrict"); 
                    if (page != null)
                    {
                        Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                        if (roleDetails != null && roleDetails.Allow_Delete_For_Others == false)
                        {
                            if (busRestrict.InsertedByUserId != userId)
                            {
                                return Unauthorized();
                            }
                        }
                    }
                    else
                    {
                        return BadRequest("Bus Restricts page doesn't exist");
                    }
                }

                busRestrict.IsDeleted = true;
                TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
                busRestrict.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                if (userTypeClaim == "octa")
                {
                    busRestrict.DeletedByOctaId = userId;
                    if (busRestrict.DeletedByUserId != null)
                    {
                        busRestrict.DeletedByUserId = null;
                    }
                }
                else if (userTypeClaim == "employee")
                {
                    busRestrict.DeletedByUserId = userId;
                    if (busRestrict.DeletedByOctaId != null)
                    {
                        busRestrict.DeletedByOctaId = null;
                    }
                }

                Unit_Of_Work.busRestrict_Repository.Update(busRestrict);
                Unit_Of_Work.SaveChanges();
                return Ok();
            }
        }
    }
}
