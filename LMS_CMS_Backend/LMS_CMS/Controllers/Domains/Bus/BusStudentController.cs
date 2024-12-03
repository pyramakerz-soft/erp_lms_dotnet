using AutoMapper;
using LMS_CMS_BL.DTO.Bus;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Migrations;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.BusModule;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices.JavaScript;
using BusModel = LMS_CMS_DAL.Models.Domains.BusModule.Bus;

namespace LMS_CMS_PL.Controllers.Domains.Bus
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class BusStudentController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;

        IMapper mapper;

        public BusStudentController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        [HttpGet("GetByBusId/{busId}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "pyramakerz", "employee" },
            pages: new[] { "Busses" }
        )]
        public async Task<IActionResult> GetByBusID(long busId)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<BusStudent> busStudents;

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            BusModel bus = Unit_Of_Work.bus_Repository.Select_By_Id(busId);
            if (bus == null)
            {
                return NotFound("No Bus with this Id");
            }

            busStudents = await Unit_Of_Work.busStudent_Repository.Select_All_With_IncludesById<BusStudent>(
                    bus => bus.BusID == busId && bus.IsDeleted != true,
                    query => query.Include(bus => bus.Bus),
                    query => query.Include(stu => stu.Student),
                    query => query.Include(busCat => busCat.BusCategory),
                    query => query.Include(sem => sem.Semester)
                    );

            if (busStudents == null || busStudents.Count == 0)
            {
                return NotFound();
            }

            List<BusStudentGetDTO> busStudentDTOs = mapper.Map<List<BusStudentGetDTO>>(busStudents);

            return Ok(busStudentDTOs);
        }

        [HttpGet("{Id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "pyramakerz", "employee" },
            pages: new[] { "Busses" }
        )]
        public async Task<IActionResult> GetByID(long Id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (Id == 0)
            {
                return BadRequest("Enter Bus Student ID");
            }

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            var busStudent = await Unit_Of_Work.busStudent_Repository.FindByIncludesAsync(
                busStu => busStu.ID == Id && busStu.IsDeleted != true,
                query => query.Include(e => e.Bus),
                query => query.Include(e => e.Student),
                query => query.Include(e => e.BusCategory),
                query => query.Include(e => e.Semester)
            );

            if (busStudent == null || busStudent.IsDeleted == true)
            {
                return NotFound("No bus Student with this ID");
            }

            BusStudentGetDTO busStudentDTO = mapper.Map<BusStudentGetDTO>(busStudent);

            return Ok(busStudentDTO);
        }

        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "pyramakerz", "employee" },
            pages: new[] { "Busses" }
        )]
        public ActionResult Add(BusStudent_AddDTO busStudentAddDTO)
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

            if (busStudentAddDTO == null)
            {
                return BadRequest("Bus Student cannot be null.");
            }

            BusModel bus = Unit_Of_Work.bus_Repository.Select_By_Id(busStudentAddDTO.BusID);
            if (bus == null || bus.IsDeleted == true)
            {
                return NotFound("No Bus with this ID");
            }

            Student student = Unit_Of_Work.student_Repository.Select_By_Id(busStudentAddDTO.StudentID);
            if (student == null || student.IsDeleted == true)
            {
                return NotFound("No Student with this ID");
            }

            if (busStudentAddDTO.SemseterID != null)
            {
                Semester semester = Unit_Of_Work.semester_Repository.Select_By_Id(busStudentAddDTO.SemseterID);
                if (semester == null || semester.IsDeleted == true)
                {
                    return NotFound("No Semester with this ID");
                }
            }

            if (busStudentAddDTO.BusCategoryID != null)
            {
                BusCategory busCategory = Unit_Of_Work.busCategory_Repository.Select_By_Id(busStudentAddDTO.BusCategoryID);
                if (busCategory == null || busCategory.IsDeleted == true)
                {
                    return NotFound("No Bus Category with this ID");
                }
            }

            BusStudent busStudent = mapper.Map<BusStudent>(busStudentAddDTO);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            busStudent.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "pyramakerz")
            {
                busStudent.InsertedByPyramakerzId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                busStudent.InsertedByUserId = userId;
            }

            Unit_Of_Work.busStudent_Repository.Add(busStudent);
            Unit_Of_Work.SaveChanges();

            return CreatedAtAction(nameof(GetByID), new { Id = bus.ID }, busStudentAddDTO);
        }

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "pyramakerz", "employee" },
            allowEdit: 1,
            pages: new[] { "Busses" }
        )]
        public ActionResult Edit(BusStudent_PutDTO busStudentPutDTO)
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

            if (busStudentPutDTO == null)
            {
                return BadRequest("Bus Student cannot be null.");
            }

            BusModel bus = Unit_Of_Work.bus_Repository.Select_By_Id(busStudentPutDTO.BusID);
            if (bus == null || bus.IsDeleted == true)
            {
                return NotFound("No Bus with this ID");
            }

            Student student = Unit_Of_Work.student_Repository.Select_By_Id(busStudentPutDTO.StudentID);
            if (student == null || student.IsDeleted == true)
            {
                return NotFound("No Student with this ID");
            }

            if (busStudentPutDTO.SemseterID != null)
            {
                Semester semester = Unit_Of_Work.semester_Repository.Select_By_Id(busStudentPutDTO.SemseterID);
                if (semester == null || semester.IsDeleted == true)
                {
                    return NotFound("No Semester with this ID");
                }
            }

            if (busStudentPutDTO.BusCategoryID != null)
            {
                BusCategory busCategory = Unit_Of_Work.busCategory_Repository.Select_By_Id(busStudentPutDTO.BusCategoryID);
                if (busCategory == null || busCategory.IsDeleted == true)
                {
                    return NotFound("No Bus Category with this ID");
                }
            }

            BusStudent busStudentExists = Unit_Of_Work.busStudent_Repository.Select_By_Id(busStudentPutDTO.ID);
            if (busStudentExists == null || busStudentExists.IsDeleted == true)
            {
                return NotFound("No Bus Student with this ID");
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Bus Status");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (busStudentExists.InsertedByUserId != userId)
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

            mapper.Map(busStudentPutDTO, busStudentExists);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            busStudentExists.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "pyramakerz")
            {
                busStudentExists.UpdatedByPyramakerzId = userId;
                if (busStudentExists.UpdatedByUserId != null)
                {
                    busStudentExists.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                busStudentExists.UpdatedByUserId = userId;
                if (busStudentExists.UpdatedByPyramakerzId != null)
                {
                    busStudentExists.UpdatedByPyramakerzId = null;
                }
            }
            Unit_Of_Work.busStudent_Repository.Update(busStudentExists);
            Unit_Of_Work.SaveChanges();

            return Ok(busStudentPutDTO);
        }

        [HttpDelete("{Id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "pyramakerz", "employee" },
            allowDelete: 1,
            pages: new[] { "Busses" }
        )]
        public IActionResult Delete(long Id)
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

            if (Id == 0)
            {
                return BadRequest("Bus Student ID cannot be null.");
            }

            BusStudent busStudent = Unit_Of_Work.busStudent_Repository.Select_By_Id(Id);
            if (busStudent == null)
            {
                return NotFound();
            }
            else
            {
                if (userTypeClaim == "employee")
                {
                    Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Busses");
                    if (page != null)
                    {
                        Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                        if (roleDetails != null && roleDetails.Allow_Delete_For_Others == false)
                        {
                            if (busStudent.InsertedByUserId != userId)
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

                busStudent.IsDeleted = true;
                TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
                busStudent.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                if (userTypeClaim == "pyramakerz")
                {
                    busStudent.DeletedByPyramakerzId = userId;
                    if (busStudent.DeletedByUserId != null)
                    {
                        busStudent.DeletedByUserId = null;
                    }
                }
                else if (userTypeClaim == "employee")
                {
                    busStudent.DeletedByUserId = userId;
                    if (busStudent.DeletedByPyramakerzId != null)
                    {
                        busStudent.DeletedByPyramakerzId = null;
                    }
                }
                Unit_Of_Work.busStudent_Repository.Update(busStudent);
                Unit_Of_Work.SaveChanges();
                return Ok("Bus Student has Successfully been deleted");
            }
        }
    }
}
