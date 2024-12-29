using AutoMapper;
using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.BusModule;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices.JavaScript;
using LMS_CMS_BL.DTO.Bus;

namespace LMS_CMS_PL.Controllers.Domains.LMS
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class BuildingController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public BuildingController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Buildings & Floors", "Administrator" }
        )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<Building> Building = await Unit_Of_Work.building_Repository.Select_All_With_IncludesById<Building>(
                    b => b.IsDeleted != true,
                    query => query.Include(emp => emp.school)
                    );

            if (Building == null || Building.Count == 0)
            {
                return NotFound();
            }

            List<BuildingGetDTO> BuildingDTO = mapper.Map<List<BuildingGetDTO>>(Building);

            return Ok(BuildingDTO);
        }
        
        [HttpGet("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Buildings & Floors", "Administrator" }
        )]
        public async Task<IActionResult> GetById(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (id == 0)
            {
                return BadRequest("Enter Building ID");
            }

            Building Building = await Unit_Of_Work.building_Repository.FindByIncludesAsync(t => t.IsDeleted != true && t.ID == id, query => query.Include(e => e.school));


            if (Building == null)
            {
                return NotFound();
            }

            BuildingGetDTO BuildingDTO = mapper.Map<BuildingGetDTO>(Building);

            return Ok(BuildingDTO);
        }

        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Buildings & Floors", "Administrator" }
        )]
        public IActionResult Add(BuildingAddDTO NewBuilding)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (NewBuilding == null)
            {
                return BadRequest("Building cannot be null");
            }
            if (NewBuilding.Name == null)
            {
                return BadRequest("the name cannot be null");
            }
            if (NewBuilding.SchoolID != 0)
            {
                School school = Unit_Of_Work.school_Repository.First_Or_Default(s => s.ID == NewBuilding.SchoolID && s.IsDeleted != true);
                if (school == null)
                {
                    return BadRequest("No School with this ID");
                }
            }

            Building Building = mapper.Map<Building>(NewBuilding);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            Building.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                Building.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                Building.InsertedByUserId = userId;
            }

            Unit_Of_Work.building_Repository.Add(Building);
            Unit_Of_Work.SaveChanges();
            return Ok(NewBuilding);
        }

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Buildings & Floors", "Administrator" }
        )]
        public IActionResult Edit(BuildingPutDTO EditedBuilding)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
            var userRoleClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            long.TryParse(userRoleClaim, out long roleId);

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID, Type claim not found.");
            }

            if (EditedBuilding == null)
            {
                return BadRequest("Building cannot be null");
            }
            if (EditedBuilding.Name == null)
            {
                return BadRequest("the name cannot be null");
            }
            if (EditedBuilding.SchoolID != 0)
            {
                School school = Unit_Of_Work.school_Repository.First_Or_Default(s=>s.ID==EditedBuilding.SchoolID &&s.IsDeleted != true);
                if (school == null)
                {
                    return BadRequest("No School with this ID");
                }
            }

            Building BuildingExists = Unit_Of_Work.building_Repository.First_Or_Default(b=>b.ID==EditedBuilding.ID && b.IsDeleted != true);
            if (BuildingExists == null || BuildingExists.IsDeleted == true)
            {
                return NotFound("No Building with this ID");
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Buildings");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (BuildingExists.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Building page doesn't exist");
                }
            }
             
            mapper.Map(EditedBuilding, BuildingExists);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            BuildingExists.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                BuildingExists.UpdatedByOctaId = userId;
                if (BuildingExists.UpdatedByUserId != null)
                {
                    BuildingExists.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                BuildingExists.UpdatedByUserId = userId;
                if (BuildingExists.UpdatedByOctaId != null)
                {
                    BuildingExists.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.building_Repository.Update(BuildingExists);
            Unit_Of_Work.SaveChanges();
            return Ok(EditedBuilding);
        }
        
        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Buildings & Floors", "Administrator" }
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
                return BadRequest("Enter Building ID");
            }

            Building Building = Unit_Of_Work.building_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);


            if (Building == null)
            {
                return NotFound();
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Buildings");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Delete_For_Others == false)
                    {
                        if (Building.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Buildings page doesn't exist");
                }
            }

            Building.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            Building.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                Building.DeletedByOctaId = userId;
                if (Building.DeletedByUserId != null)
                {
                    Building.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                Building.DeletedByUserId = userId;
                if (Building.DeletedByOctaId != null)
                {
                    Building.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.building_Repository.Update(Building);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
