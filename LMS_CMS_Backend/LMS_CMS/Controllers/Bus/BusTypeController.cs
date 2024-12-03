using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.DTO.Bus;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Migrations;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.BusModule;
using LMS_CMS_PL.Attribute;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices.JavaScript;
using System.Security.Claims;

namespace LMS_CMS_PL.Controllers.Bus
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BusTypeController : ControllerBase
    {

        private UOW Unit_Of_Work;
        IMapper mapper;

        public BusTypeController(UOW Unit_Of_Work, IMapper mapper)
        {
            this.Unit_Of_Work = Unit_Of_Work;
            this.mapper = mapper;
        }


        ///////////////////////////////////////////
        
        [HttpGet]
        [Authorize_Endpoint_Attribute(
            allowedTypes: new[] { "pyramakerz", "employee" },
            pages: new[] { "Busses", "Bus Types" }
        )]
        public IActionResult Get()
        {
            List<BusType> BusType;

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            BusType = Unit_Of_Work.busType_Repository.FindBy(t => t.IsDeleted != true);

            if (BusType == null || BusType.Count == 0)
            {
                return NotFound();
            }

            List<BusTypeGetDTO> BusTypeDTO = mapper.Map<List<BusTypeGetDTO>>(BusType);

            return Ok(BusTypeDTO);
        }

        ///////////////////////////////////////////////////

        [HttpGet("id")]
        [Authorize_Endpoint_Attribute(
            allowedTypes: new[] { "pyramakerz", "employee" },
            pages: new[] { "Busses", "Bus Types" }
        )]
        public IActionResult GetById(long id)
        {
            if (id == 0)
            {
                return BadRequest("Enter Bus Company ID");
            }

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            BusType busType = Unit_Of_Work.busType_Repository.Select_By_Id(id);
            if (busType == null || busType.IsDeleted == true)
            {
                return NotFound("No bus Type with this ID");
            }

            BusTypeGetDTO typeDTO = mapper.Map<BusTypeGetDTO>(busType);
            return Ok(typeDTO);
        }

        ///////////////////////////////////////////////////

        //[HttpGet("DomainId")]
        //[Authorize_Endpoint_Attribute(
        //    allowedTypes: new[] { "pyramakerz", "employee" },
        //    pages: new[] { "Busses", "Bus Types" }
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

        //    List<BusType> BusType = Unit_Of_Work.busType_Repository.FindBy(s => s.IsDeleted != true);
        //    if (BusType == null || BusType.Count == 0)
        //    {
        //        return NotFound();
        //    }

        //    List<BusTypeGetDTO> BusTypeDTO = mapper.Map<List<BusTypeGetDTO>>(BusType);

        //    return Ok(BusTypeDTO);
        //}

        ///////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_Attribute(
            allowedTypes: new[] { "pyramakerz", "employee" },
            pages: new[] { "Busses", "Bus Types" }
        )]
        public IActionResult Add(BusTypeAddDTO NewBus)
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
                return BadRequest("Bus Type cannot be null");
            }

            BusType bustType = mapper.Map<BusType>(NewBus);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            bustType.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "pyramakerz")
            {
                bustType.InsertedByPyramakerzId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                bustType.InsertedByUserId= userId;
            }

            Unit_Of_Work.busType_Repository.Add(bustType);
            Unit_Of_Work.SaveChanges();
            return Ok(NewBus);

        }

        ////////////////////////////////////////////////////////
        
        [HttpPut]
        [Authorize_Endpoint_Attribute(
            allowedTypes: new[] { "pyramakerz", "employee" },
            allowEdit: 1,
            pages: new[] { "Busses", "Bus Types" }
        )]
        public IActionResult Edit(BusTypeEditDTO EditBusType)
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

            if (EditBusType == null)
            {
                BadRequest();
            }

            BusType busType = Unit_Of_Work.busType_Repository.Select_By_Id(EditBusType.ID);

            if (busType == null || busType.IsDeleted == true)
            {
                return NotFound("No Bus Type with this ID");
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Bus Types");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (busType.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Bus Types page doesn't exist");
                }
            }

            mapper.Map(EditBusType, busType);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            busType.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "pyramakerz")
            {
                busType.UpdatedByPyramakerzId = userId;
                if(busType.UpdatedByUserId != null) 
                {
                    busType.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                busType.UpdatedByUserId = userId;
                if (busType.UpdatedByPyramakerzId != null)
                {
                    busType.UpdatedByPyramakerzId = null;
                }
            }

            Unit_Of_Work.busType_Repository.Update(busType);
            Unit_Of_Work.SaveChanges();
            return Ok(EditBusType);
        }

        ////////////////////////////////////////////////////////

        [HttpDelete]
        [Authorize_Endpoint_Attribute(
            allowedTypes: new[] { "pyramakerz", "employee" },
            allowDelete: 1,
            pages: new[] { "Busses", "Bus Types" }
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
                return BadRequest("Bus Category ID cannot be null.");
            }

            BusType busType = Unit_Of_Work.busType_Repository.Select_By_Id(id);
            if (busType == null || busType.IsDeleted == true)
            {
                return NotFound("No Bus Type with this ID");
            }
            else
            {
                if (userTypeClaim == "employee")
                {
                    Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Bus Types");
                    if (page != null)
                    {
                        Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                        if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                        {
                            if (busType.InsertedByUserId != userId)
                            {
                                return Unauthorized();
                            }
                        }
                    }
                    else
                    {
                        return BadRequest("Bus Types page doesn't exist");
                    }
                }

                busType.IsDeleted = true;
                TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
                busType.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                if (userTypeClaim == "pyramakerz")
                {
                    busType.DeletedByPyramakerzId = userId;
                    if (busType.DeletedByUserId != null)
                    {
                        busType.DeletedByUserId = null;
                    }
                }
                else if (userTypeClaim == "employee")
                {
                    busType.DeletedByUserId = userId;
                    if (busType.DeletedByPyramakerzId != null)
                    {
                        busType.DeletedByPyramakerzId = null;
                    }
                }

                Unit_Of_Work.busType_Repository.Update(busType);
                Unit_Of_Work.SaveChanges();
                return Ok();
            }
        }

    }
}
