using AutoMapper;
using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers.Domains.LMS
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class FloorController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public FloorController(DbContextFactoryService dbContextFactory, IMapper mapper)
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

            List<Floor> floors = await Unit_Of_Work.floor_Repository.Select_All_With_IncludesById<Floor>(
                    f => f.IsDeleted != true,
                    query => query.Include(emp => emp.building),
                    query => query.Include(emp => emp.floorMonitor)
                    );

            if (floors == null || floors.Count == 0)
            {
                return NotFound();
            }

            List<FloorGetDTO> floorsDTO = mapper.Map<List<FloorGetDTO>>(floors);

            return Ok(floorsDTO);
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
                return BadRequest("Enter Floor ID");
            }

            Floor floor = await Unit_Of_Work.floor_Repository.FindByIncludesAsync(
                t => t.IsDeleted != true && t.ID == id, 
                query => query.Include(e => e.building),
                query => query.Include(emp => emp.floorMonitor)
                );


            if (floor == null)
            {
                return NotFound();
            }

            FloorGetDTO floorDTO = mapper.Map<FloorGetDTO>(floor);

            return Ok(floorDTO);
        }

        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Buildings & Floors", "Administrator" }
        )]
        public IActionResult Add(FloorAddDTO NewFloor)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (NewFloor == null)
            {
                return BadRequest("Floor cannot be null");
            }

            if (NewFloor.buildingID != 0)
            {
                Building building = Unit_Of_Work.building_Repository.First_Or_Default(b=>b.ID==NewFloor.buildingID&&b.IsDeleted!=true);
                if (building == null)
                {
                    return BadRequest("No Building with this ID");
                }
            }
            else
            {
                return BadRequest("Building id cannot be null");
            }
            if (NewFloor.FloorMonitorID != 0)
            {
                Employee employee = Unit_Of_Work.employee_Repository.First_Or_Default(b => b.ID == NewFloor.FloorMonitorID && b.IsDeleted != true);
                if (employee == null)
                {
                    return BadRequest("No FloorMonitor with this ID");
                }
            }

            Floor Floor = mapper.Map<Floor>(NewFloor);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            Floor.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                Floor.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                Floor.InsertedByUserId = userId;
            }

            Unit_Of_Work.floor_Repository.Add(Floor);
            Unit_Of_Work.SaveChanges();
            return Ok(NewFloor);
        }

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Buildings & Floors", "Administrator" }
        )]
        public IActionResult Edit(FloorPutDTO EditedFloor)
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

            if (EditedFloor == null)
            {
                return BadRequest("Floor cannot be null");
            }

            if (EditedFloor.buildingID != 0)
            {
                Building building = Unit_Of_Work.building_Repository.First_Or_Default(b => b.ID == EditedFloor.buildingID && b.IsDeleted != true);
                if (building == null)
                {
                    return BadRequest("No Building with this ID");
                }
            }
            else
            {
                return BadRequest("Building id cannot be null");
            }
            if (EditedFloor.FloorMonitorID != 0)
            {
                Employee employee = Unit_Of_Work.employee_Repository.First_Or_Default(b => b.ID == EditedFloor.FloorMonitorID && b.IsDeleted != true);
                if (employee == null)
                {
                    return BadRequest("No FloorMonitor with this ID");
                }
            }

            Floor FloorExists = Unit_Of_Work.floor_Repository.Select_By_Id(EditedFloor.ID);
            if (FloorExists == null || FloorExists.IsDeleted == true)
            {
                return NotFound("No Floor with this ID");
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Floors");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (FloorExists.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Floors page doesn't exist");
                }
            }

            mapper.Map(EditedFloor, FloorExists);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            FloorExists.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                FloorExists.UpdatedByOctaId = userId;
                if (FloorExists.UpdatedByUserId != null)
                {
                    FloorExists.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                FloorExists.UpdatedByUserId = userId;
                if (FloorExists.UpdatedByOctaId != null)
                {
                    FloorExists.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.floor_Repository.Update(FloorExists);
            Unit_Of_Work.SaveChanges();
            return Ok(EditedFloor);
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
                return BadRequest("Enter Floor ID");
            }

            Floor floor = Unit_Of_Work.floor_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);


            if (floor == null)
            {
                return NotFound();
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Floors");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Delete_For_Others == false)
                    {
                        if (floor.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Floors page doesn't exist");
                }
            }

            floor.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            floor.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                floor.DeletedByOctaId = userId;
                if (floor.DeletedByUserId != null)
                {
                    floor.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                floor.DeletedByUserId = userId;
                if (floor.DeletedByOctaId != null)
                {
                    floor.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.floor_Repository.Update(floor);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
