using AutoMapper;
using LMS_CMS_BL.DTO.Bus;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.BusModule;
using LMS_CMS_PL.Attribute;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Controllers.Bus
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BusCompanyController : ControllerBase
    {
        private UOW Unit_Of_Work;
        IMapper mapper;

        public BusCompanyController(UOW Unit_Of_Work, IMapper mapper)
        {
            this.Unit_Of_Work = Unit_Of_Work;
            this.mapper = mapper;
        }


        ///////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_Attribute(
            allowedTypes: new[] { "pyramakerz", "employee" },
            pages: new[] { "Busses", "Bus Companies" }
        )]
        public IActionResult Get()
        {
            List<BusCompany> BusCompany;

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            BusCompany = Unit_Of_Work.busCompany_Repository.FindBy(t => t.IsDeleted != true);

            if (BusCompany == null || BusCompany.Count == 0)
            {
                return NotFound();
            }

            List<BusCompanyGetDTO> BusCompanyDTO = mapper.Map<List<BusCompanyGetDTO>>(BusCompany);

            return Ok(BusCompanyDTO);
        }

        ///////////////////////////////////////////////////

        [HttpGet("id")]
        [Authorize_Endpoint_Attribute(
            allowedTypes: new[] { "pyramakerz", "employee" },
            pages: new[] { "Busses", "Bus Companies" }
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

            BusCompany busCompany = Unit_Of_Work.busCompany_Repository.Select_By_Id(id);
            if (busCompany == null || busCompany.IsDeleted == true)
            {
                return NotFound("No bus Company with this ID");
            }

            BusCompanyGetDTO CompanyDTO = mapper.Map<BusCompanyGetDTO>(busCompany);
            return Ok(CompanyDTO);
        }
        ///////////////////////////////////////////////////

        //[HttpGet("DomainId")]
        //[Authorize_Endpoint_Attribute(
        //    allowedTypes: new[] { "pyramakerz", "employee" },
        //    pages: new[] { "Busses", "Bus Companies" }
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

        //    List<BusCompany> BusCompany = Unit_Of_Work.busCompany_Repository.FindBy(s => s.DomainId == id && s.IsDeleted != true);
        //    if (BusCompany == null || BusCompany.Count == 0)
        //    {
        //        return NotFound("There are no bus Companies in this domian");
        //    }

        //    List<BusCompanyGetDTO> BusCompanyDTO = mapper.Map<List<BusCompanyGetDTO>>(BusCompany);

        //    return Ok(BusCompanyDTO);
        //}
        ///////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_Attribute(
            allowedTypes: new[] { "pyramakerz", "employee" },
            pages: new[] { "Busses", "Bus Companies" }
        )]
        public IActionResult Add(BusCompanyAddDTO NewBusCompany)
        {
            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (NewBusCompany == null)
            {
                return BadRequest("Bus Company cannot be null");
            }

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            BusCompany busCompany = mapper.Map<BusCompany>(NewBusCompany);
            busCompany.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "pyramakerz")
            {
                busCompany.InsertedByPyramakerzId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                busCompany.InsertedByUserId = userId;
            }

            Unit_Of_Work.busCompany_Repository.Add(busCompany);
            Unit_Of_Work.SaveChanges();
            return Ok(NewBusCompany);
        }

        ////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_Attribute(
            allowedTypes: new[] { "pyramakerz", "employee" },
            allowEdit: 1,
            pages: new[] { "Busses", "Bus Companies" }
        )]
        public IActionResult Edit(BusCompanyEditDTO EditBusCompany)
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

            if (EditBusCompany == null)
            {
                BadRequest();
            }

            BusCompany busCompany = Unit_Of_Work.busCompany_Repository.Select_By_Id(EditBusCompany.ID);
            if (busCompany == null || busCompany.IsDeleted == true)
            {
                return NotFound("No Bus Company with this ID");
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Bus Companies");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (busCompany.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Bus Categories page doesn't exist");
                }
            }
            mapper.Map(EditBusCompany, busCompany);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            busCompany.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "pyramakerz")
            {
                busCompany.UpdatedByPyramakerzId = userId;
                if(busCompany.UpdatedByUserId != null)
                {
                    busCompany.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                busCompany.UpdatedByUserId = userId;
                if (busCompany.UpdatedByPyramakerzId != null)
                {
                    busCompany.UpdatedByPyramakerzId = null;
                }
            }

            Unit_Of_Work.busCompany_Repository.Update(busCompany);
            Unit_Of_Work.SaveChanges();
            return Ok(EditBusCompany);
        }

        ////////////////////////////////////////////////////////

        [HttpDelete]
        [Authorize_Endpoint_Attribute(
            allowedTypes: new[] { "pyramakerz", "employee" },
            allowDelete: 1,
            pages: new[] { "Busses", "Bus Companies" }
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
             
            BusCompany busCompany = Unit_Of_Work.busCompany_Repository.Select_By_Id(id);

            if (busCompany == null || busCompany.IsDeleted == true)
            {
                return NotFound("No Bus Company with this ID");
            }
            else
            {
                if (userTypeClaim == "employee")
                {
                    Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Bus Categories");
                    if (page != null)
                    {
                        Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                        if (roleDetails != null && roleDetails.Allow_Delete_For_Others == false)
                        {
                            if (busCompany.InsertedByUserId != userId)
                            {
                                return Unauthorized();
                            }
                        }
                    }
                    else
                    {
                        return BadRequest("Bus Categories page doesn't exist");
                    }
                }

                busCompany.IsDeleted = true;
                TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
                busCompany.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                if (userTypeClaim == "pyramakerz")
                {
                    busCompany.DeletedByPyramakerzId = userId;
                    if(busCompany.DeletedByUserId != null)
                    {
                        busCompany.DeletedByUserId = null;
                    }
                }
                else if (userTypeClaim == "employee")
                {
                    busCompany.DeletedByUserId = userId;
                    if (busCompany.DeletedByPyramakerzId != null)
                    {
                        busCompany.DeletedByPyramakerzId = null;
                    }
                }
                Unit_Of_Work.busCompany_Repository.Update(busCompany);
                Unit_Of_Work.SaveChanges();
                return Ok();
            }
        }

    }
}
