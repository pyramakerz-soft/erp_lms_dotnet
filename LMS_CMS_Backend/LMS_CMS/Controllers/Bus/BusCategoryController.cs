using AutoMapper;
using LMS_CMS_BL.DTO.Bus;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Migrations;
using LMS_CMS_DAL.Models;
using LMS_CMS_DAL.Models.BusModule;
using LMS_CMS_PL.Attribute;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers.Bus
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BusCategoryController : ControllerBase
    {
        private UOW Unit_Of_Work;
        IMapper mapper;

        public BusCategoryController(UOW Unit_Of_Work, IMapper mapper)
        {
            this.Unit_Of_Work = Unit_Of_Work;
            this.mapper = mapper;
        }


        ///////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_Attribute(
            allowedTypes: new[] { "pyramakerz", "employee" },
            pages: new[] { "Busses", "Bus Categories" }
        )]
        public IActionResult Get()
        {
            List<BusCategory> BusCategories;

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (userTypeClaim == "employee")
            {
                Employee employee = Unit_Of_Work.employee_Repository.Select_By_Id(userId);
                long employeeDomain = employee.Domain_ID;

                BusCategories = Unit_Of_Work.busCategory_Repository.FindBy(t => t.IsDeleted != true && t.DomainId == employeeDomain);
            }
            else
            {
                BusCategories = Unit_Of_Work.busCategory_Repository.FindBy(t => t.IsDeleted != true);
            }

            if (BusCategories == null || BusCategories.Count == 0)
            {
                return NotFound();
            }

            List<BusCatigoryGetDTO> BusCatigoryDTO = mapper.Map<List<BusCatigoryGetDTO>>(BusCategories);

            return Ok(BusCatigoryDTO);
        }

        ///////////////////////////////////////////////////

        [HttpGet("id")]
        [Authorize_Endpoint_Attribute(
            allowedTypes: new[] { "pyramakerz", "employee" },
            pages: new[] { "Busses", "Bus Categories" }
        )]
        public IActionResult GetById(long id)
        {
            if (id == 0)
            {
                return BadRequest("Enter Bus Category ID");
            }

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            BusCategory busCategory = Unit_Of_Work.busCategory_Repository.Select_By_Id(id);
            if (busCategory == null || busCategory.IsDeleted == true)
            {
                return NotFound("No bus category with this ID");
            }
            else
            {
                if (userTypeClaim == "employee")
                {
                    Employee employee = Unit_Of_Work.employee_Repository.Select_By_Id(userId);
                    long employeeDomain = employee.Domain_ID;
                    if (busCategory.DomainId != employeeDomain)
                    {
                        return Unauthorized();
                    }
                }
            }

            BusCatigoryGetDTO busCategoryDto = mapper.Map<BusCatigoryGetDTO>(busCategory);
            return Ok(busCategoryDto);
        }
        ///////////////////////////////////////////////////

        [HttpGet("DomainId")]
        [Authorize_Endpoint_Attribute(
            allowedTypes: new[] { "pyramakerz", "employee" },
            pages: new[] { "Busses", "Bus Categories" }
        )]
        public IActionResult GetByDomainId(long id)
        {
            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (userTypeClaim == "employee")
            {
                Employee employee = Unit_Of_Work.employee_Repository.Select_By_Id(userId);
                long employeeDomain = employee.Domain_ID;

                if (id != employeeDomain)
                {
                    return Unauthorized();
                }
            }

            Domain domain = Unit_Of_Work.domain_Repository.Select_By_Id(id);
            if (domain == null)
            {
                return NotFound("No Domain with this Id");
            }

            List<BusCategory> BusCategory = Unit_Of_Work.busCategory_Repository.FindBy(s => s.DomainId == id && s.IsDeleted != true);
            if (BusCategory == null || BusCategory.Count == 0)
            {
                return NotFound("There are no buse categories in this domian");
            }

            List<BusCatigoryGetDTO> BusCategoryDTO = mapper.Map<List<BusCatigoryGetDTO>>(BusCategory);

            return Ok(BusCategoryDTO);
        }
        ///////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_Attribute(
            allowedTypes: new[] { "pyramakerz", "employee" },
            pages: new[] { "Busses", "Bus Categories" }
        )]
        public IActionResult Add(BusCatigoryAddDTO NewCategory)
        {
            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (NewCategory == null) 
            { 
                return BadRequest("Bus Category cannot be null"); 
            }

            if (userTypeClaim == "employee")
            {
                Employee employee = Unit_Of_Work.employee_Repository.Select_By_Id(userId);
                long employeeDomain = employee.Domain_ID;

                if (NewCategory.DomainId != employeeDomain)
                {
                    return Unauthorized();
                }
            }

            Domain domain = Unit_Of_Work.domain_Repository.Select_By_Id(NewCategory.DomainId);
            if (domain == null || domain.IsDeleted == true)
            {
                return NotFound("No Domain with this Id");
            }

            BusCategory busCategory = mapper.Map<BusCategory>(NewCategory);

            busCategory.InsertedByUserId = userId;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            busCategory.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "pyramakerz")
            {
                busCategory.InsertedByUserRole = "pyramakerz";
            }
            else if (userTypeClaim == "employee")
            {
                busCategory.InsertedByUserRole = "employee";
            }

            Unit_Of_Work.busCategory_Repository.Add(busCategory);
            Unit_Of_Work.SaveChanges();
            return Ok(NewCategory);
        }

        ///////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_Attribute(
            allowedTypes: new[] { "pyramakerz", "employee" },
            allowEdit: 1,
            pages: new[] { "Busses", "Bus Categories" }
        )]
        public IActionResult Edit(BusCategoryEditDTO EditBusCatigory)
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
             
            if (EditBusCatigory == null) 
            { 
                BadRequest(); 
            }


            Domain domain = Unit_Of_Work.domain_Repository.Select_By_Id(EditBusCatigory.DomainId);
            if (domain == null || domain.IsDeleted == true)
            {
                return NotFound("No Domain with this Id");
            }

            BusCategory busCatigory = Unit_Of_Work.busCategory_Repository.Select_By_Id(EditBusCatigory.ID);
            if (busCatigory == null || busCatigory.IsDeleted == true)
            {
                return NotFound("No Bus Category with this ID");
            }


            if (userTypeClaim == "employee")
            {
                Employee employee = Unit_Of_Work.employee_Repository.Select_By_Id(userId);
                long employeeDomain = employee.Domain_ID;

                if (EditBusCatigory.DomainId != employeeDomain || busCatigory.DomainId != employeeDomain)
                {
                    return Unauthorized();
                }
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Bus Categories");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (busCatigory.InsertedByUserId != userId)
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

            mapper.Map(EditBusCatigory, busCatigory);
            busCatigory.UpdatedByUserId = userId;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            busCatigory.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "pyramakerz")
            {
                busCatigory.UpdatedByUserRole = "pyramakerz";
            }
            else if (userTypeClaim == "employee")
            {
                busCatigory.UpdatedByUserRole = "employee";
            }

            Unit_Of_Work.busCategory_Repository.Update(busCatigory);
            Unit_Of_Work.SaveChanges();
            return Ok(EditBusCatigory);
        }

        ///////////////////////////////////////////////////

        [HttpDelete]
        [Authorize_Endpoint_Attribute(
            allowedTypes: new[] { "pyramakerz", "employee" },
            allowDelete: 1,
            pages: new[] { "Busses", "Bus Categories" }
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

            BusCategory busCategory = Unit_Of_Work.busCategory_Repository.Select_By_Id(id);
            if (busCategory == null || busCategory.IsDeleted == true)
            {
                return NotFound("No Bus Category with this ID");
            }
            else
            {
                if (userTypeClaim == "employee")
                {
                    Employee employee = Unit_Of_Work.employee_Repository.Select_By_Id(userId);
                    long employeeDomain = employee.Domain_ID;

                    if (busCategory.DomainId != employeeDomain)
                    {
                        return Unauthorized();
                    }

                    Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Bus Categories");
                    if (page != null)
                    {
                        Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                        if (roleDetails != null && roleDetails.Allow_Delete_For_Others == false)
                        {
                            if (busCategory.InsertedByUserId != userId)
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

                busCategory.IsDeleted = true;
                busCategory.DeletedByUserId = userId;
                TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
                busCategory.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                if (userTypeClaim == "pyramakerz")
                {
                    busCategory.DeletedByUserRole = "pyramakerz";
                }
                else if (userTypeClaim == "employee")
                {
                    busCategory.DeletedByUserRole = "employee";
                }

                Unit_Of_Work.busCategory_Repository.Update(busCategory);
                Unit_Of_Work.SaveChanges();
                return Ok();
            }
        }
    }
}
