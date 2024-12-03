using AutoMapper;
using LMS_CMS_BL.DTO.Bus;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.BusModule;
using LMS_CMS_PL.Attribute;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Controllers.Domains.Bus
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BusStatusController : ControllerBase
    {
        private UOW Unit_Of_Work;
        IMapper mapper;

        public BusStatusController(UOW Unit_Of_Work, IMapper mapper)
        {
            this.Unit_Of_Work = Unit_Of_Work;
            this.mapper = mapper;
        }


        ///////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "pyramakerz", "employee" },
            pages: new[] { "Busses", "Bus Status" }
        )]
        public IActionResult Get()
        {
            List<BusStatus> BusStatus;

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            BusStatus = Unit_Of_Work.busStatus_Repository.FindBy(t => t.IsDeleted != true);

            if (BusStatus == null || BusStatus.Count == 0)
            {
                return NotFound();
            }

            List<BusStatusGetDTO> BusStatusDTO = mapper.Map<List<BusStatusGetDTO>>(BusStatus);

            return Ok(BusStatusDTO);
        }

        ///////////////////////////////////////////////////

        [HttpGet("id")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "pyramakerz", "employee" },
            pages: new[] { "Busses", "Bus Status" }
        )]
        public IActionResult GetById(long id)
        {
            if (id == 0)
            {
                return BadRequest("Enter Bus Status ID");
            }

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            BusStatus busStatus = Unit_Of_Work.busStatus_Repository.Select_By_Id(id);
            if (busStatus == null || busStatus.IsDeleted == true)
            {
                return NotFound("No bus status with this ID");
            }

            BusStatusGetDTO StatusDTO = mapper.Map<BusStatusGetDTO>(busStatus);
            return Ok(StatusDTO);
        }
        ///////////////////////////////////////////////////

        //[HttpGet("DomainId")]
        //[Authorize_Endpoint_Attribute(
        //    allowedTypes: new[] { "pyramakerz", "employee" },
        //    pages: new[] { "Busses", "Bus Status" }
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

        //    List<BusStatus> BusStatus = Unit_Of_Work.busStatus_Repository.FindBy(s => s.DomainId == id && s.IsDeleted != true);
        //    if (BusStatus == null || BusStatus.Count == 0)
        //    {
        //        return NotFound("There is no bus status in this domian");
        //    }

        //    List<BusStatusGetDTO> BusStatusDTO = mapper.Map<List<BusStatusGetDTO>>(BusStatus);

        //    return Ok(BusStatusDTO);
        //}
        ///////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "pyramakerz", "employee" },
            pages: new[] { "Busses", "Bus Status" }
        )]
        public IActionResult Add(BusStatusAddDTO NewBus)
        {
            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (NewBus == null)
            {
                return BadRequest("Bus Status cannot be null");
            }

            BusStatus bustStatus = mapper.Map<BusStatus>(NewBus);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            bustStatus.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "pyramakerz")
            {
                bustStatus.InsertedByPyramakerzId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                bustStatus.InsertedByUserId = userId;
            }

            Unit_Of_Work.busStatus_Repository.Add(bustStatus);
            Unit_Of_Work.SaveChanges();
            return Ok(NewBus);

        }

        ////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "pyramakerz", "employee" },
            allowEdit: 1,
            pages: new[] { "Busses", "Bus Status" }
        )]
        public IActionResult Edit(BusStatusEditDTO EditBusStatus)
        {
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

            if (EditBusStatus == null)
            {
                BadRequest();
            }

            BusStatus busStatus = Unit_Of_Work.busStatus_Repository.Select_By_Id(EditBusStatus.ID);

            if (busStatus == null || busStatus.IsDeleted == true)
            {
                return NotFound("No Bus Status with this ID");
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Bus Status");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (busStatus.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Bus Status page doesn't exist");
                }
            }

            mapper.Map(EditBusStatus, busStatus);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            busStatus.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "pyramakerz")
            {
                busStatus.UpdatedByPyramakerzId = userId;
                if (busStatus.UpdatedByUserId != null)
                {
                    busStatus.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                busStatus.UpdatedByUserId = userId;
                if (busStatus.UpdatedByPyramakerzId != null)
                {
                    busStatus.UpdatedByPyramakerzId = null;
                }
            }

            Unit_Of_Work.busStatus_Repository.Update(busStatus);
            Unit_Of_Work.SaveChanges();
            return Ok(EditBusStatus);
        }

        ////////////////////////////////////////////////////////

        [HttpDelete]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "pyramakerz", "employee" },
            allowDelete: 1,
            pages: new[] { "Busses", "Bus Status" }
        )]
        public IActionResult Delete(long id)
        {

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
                return BadRequest("Bus Status ID cannot be null.");
            }

            BusStatus busStatus = Unit_Of_Work.busStatus_Repository.Select_By_Id(id);

            if (busStatus == null || busStatus.IsDeleted == true)
            {
                return NotFound("No Bus Status with this ID");
            }
            else
            {
                if (userTypeClaim == "employee")
                {
                    Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Bus Status");
                    if (page != null)
                    {
                        Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                        if (roleDetails != null && roleDetails.Allow_Delete_For_Others == false)
                        {
                            if (busStatus.InsertedByUserId != userId)
                            {
                                return Unauthorized();
                            }
                        }
                    }
                    else
                    {
                        return BadRequest("Bus Status page doesn't exist");
                    }
                }

                busStatus.IsDeleted = true;
                TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
                busStatus.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                if (userTypeClaim == "pyramakerz")
                {
                    busStatus.DeletedByPyramakerzId = userId;
                    if (busStatus.DeletedByUserId != null)
                    {
                        busStatus.DeletedByUserId = null;
                    }
                }
                else if (userTypeClaim == "employee")
                {
                    busStatus.DeletedByUserId = userId;
                    if (busStatus.DeletedByPyramakerzId != null)
                    {
                        busStatus.DeletedByPyramakerzId = null;
                    }
                }
                Unit_Of_Work.busStatus_Repository.Update(busStatus);
                Unit_Of_Work.SaveChanges();
                return Ok();
            }
        }

    }
}
