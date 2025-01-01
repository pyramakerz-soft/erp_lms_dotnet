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
    public class ClassroomController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public ClassroomController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Classroom", "Administrator" }
        )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<Classroom> classrooms = await Unit_Of_Work.classroom_Repository.Select_All_With_IncludesById<Classroom>(
                    f => f.IsDeleted != true,
                    query => query.Include(emp => emp.Grade),
                    query => query.Include(emp => emp.AcademicYear),
                    query => query.Include(emp => emp.Floor)
                    );

            if (classrooms == null || classrooms.Count == 0)
            {
                return NotFound();
            }

            List<ClassroomGetDTO> classroomsDTO = mapper.Map<List<ClassroomGetDTO>>(classrooms);

            return Ok(classroomsDTO);
        }

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Classroom", "Administrator" }
        )]
        public async Task<IActionResult> GetById(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (id == 0)
            {
                return BadRequest("Enter Classroom ID");
            }

            Classroom classroom = await Unit_Of_Work.classroom_Repository.FindByIncludesAsync(
                t => t.IsDeleted != true && t.ID == id,
                query => query.Include(e => e.Floor),
                query => query.Include(emp => emp.AcademicYear),
                query => query.Include(emp => emp.Grade)
                );


            if (classroom == null)
            {
                return NotFound();
            }

            ClassroomGetDTO classroomDTO = mapper.Map<ClassroomGetDTO>(classroom);

            return Ok(classroomDTO);
        }

        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Classroom", "Administrator" }
        )]
        public IActionResult Add(ClassroomAddDTO NewClassroom)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (NewClassroom == null)
            {
                return BadRequest("Classroom cannot be null");
            }
            if (NewClassroom.Name == null)
            {
                return BadRequest("the name cannot be null");
            }
            if (NewClassroom.GradeID != 0)
            {
                Grade grade = Unit_Of_Work.grade_Repository.First_Or_Default(g=>g.ID==NewClassroom.GradeID&&g.IsDeleted!=true);
                if (grade == null)
                {
                    return BadRequest("No Grade with this ID");
                }
            }

            if (NewClassroom.FloorID != 0)
            {
                Floor Floor = Unit_Of_Work.floor_Repository.First_Or_Default(g => g.ID == NewClassroom.FloorID && g.IsDeleted != true);
                if (Floor == null)
                {
                    return BadRequest("No Floor with this ID");
                }
            }

            if (NewClassroom.AcademicYearID != 0)
            {
                AcademicYear AcademicYear = Unit_Of_Work.academicYear_Repository.First_Or_Default(g => g.ID == NewClassroom.AcademicYearID && g.IsDeleted != true);
                if (AcademicYear == null)
                {
                    return BadRequest("No Academic Year with this ID");
                }
            }

            Classroom Classroom = mapper.Map<Classroom>(NewClassroom);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            Classroom.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                Classroom.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                Classroom.InsertedByUserId = userId;
            }

            Unit_Of_Work.classroom_Repository.Add(Classroom);
            Unit_Of_Work.SaveChanges();
            return Ok(NewClassroom);
        }

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Classroom", "Administrator" }
        )]
        public IActionResult Edit(ClassroomPutDTO EditedClassroom)
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

            if (EditedClassroom == null)
            {
                return BadRequest("Classroom cannot be null");
            }
            if (EditedClassroom.Name == null)
            {
                return BadRequest("the name cannot be null");
            }
            if (EditedClassroom.GradeID != 0)
            {
                Grade Grade = Unit_Of_Work.grade_Repository.First_Or_Default(g => g.ID == EditedClassroom.GradeID && g.IsDeleted != true);
                if (Grade == null)
                {
                    return BadRequest("No Grade with this ID");
                }
            }

            if (EditedClassroom.FloorID != 0)
            {
                Floor Floor = Unit_Of_Work.floor_Repository.First_Or_Default(g => g.ID == EditedClassroom.FloorID && g.IsDeleted != true);
                if (Floor == null)
                {
                    return BadRequest("No Floor with this ID");
                }
            }

            if (EditedClassroom.AcademicYearID != 0)
            {
                AcademicYear AcademicYear = Unit_Of_Work.academicYear_Repository.First_Or_Default(g => g.ID == EditedClassroom.AcademicYearID && g.IsDeleted != true);
                if (AcademicYear == null)
                {
                    return BadRequest("No Academic Year with this ID");
                }
            }

            Classroom ClassroomExists = Unit_Of_Work.classroom_Repository.First_Or_Default(g => g.ID ==EditedClassroom.ID && g.IsDeleted != true);
            if (ClassroomExists == null || ClassroomExists.IsDeleted == true)
            {
                return NotFound("No Classroom with this ID");
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Classroom");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId&&RD.IsDeleted!=true);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (ClassroomExists.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Classroom page doesn't exist");
                }
            }

            mapper.Map(EditedClassroom, ClassroomExists);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            ClassroomExists.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                ClassroomExists.UpdatedByOctaId = userId;
                if (ClassroomExists.UpdatedByUserId != null)
                {
                    ClassroomExists.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                ClassroomExists.UpdatedByUserId = userId;
                if (ClassroomExists.UpdatedByOctaId != null)
                {
                    ClassroomExists.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.classroom_Repository.Update(ClassroomExists);
            Unit_Of_Work.SaveChanges();
            return Ok(EditedClassroom);
        }

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Classroom", "Administrator" }
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
                return BadRequest("Enter Classroom ID");
            }

            Classroom classroom = Unit_Of_Work.classroom_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);


            if (classroom == null)
            {
                return NotFound();
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Classroom");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Delete_For_Others == false)
                    {
                        if (classroom.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Classroom page doesn't exist");
                }
            }

            classroom.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            classroom.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                classroom.DeletedByOctaId = userId;
                if (classroom.DeletedByUserId != null)
                {
                    classroom.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                classroom.DeletedByUserId = userId;
                if (classroom.DeletedByOctaId != null)
                {
                    classroom.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.classroom_Repository.Update(classroom);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }

        [HttpPut("CopyClassroom")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Classroom", "Administrator" }
        )]
        public IActionResult CopyClassroom(CopyClassroomDTO copyClassroomDTO)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (copyClassroomDTO == null)
            {
                return BadRequest("Copy Classroom cannot be null");
            }

            if (copyClassroomDTO.FromAcademicYearID != 0)
            {
                AcademicYear academicYearFrom = Unit_Of_Work.academicYear_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == copyClassroomDTO.FromAcademicYearID);
                if (academicYearFrom == null)
                {
                    return BadRequest("No From Academic Year with this ID");
                }
            }

            if (copyClassroomDTO.ToAcademicYearID != 0)
            {
                AcademicYear academicYearTo = Unit_Of_Work.academicYear_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == copyClassroomDTO.ToAcademicYearID);
                if (academicYearTo == null)
                {
                    return BadRequest("No To Academic Year with this ID");
                }
            }

            List<Classroom> FromClassRooms = Unit_Of_Work.classroom_Repository.FindBy(t => t.IsDeleted != true && t.AcademicYearID == copyClassroomDTO.FromAcademicYearID);

            if (FromClassRooms == null || FromClassRooms.Count == 0)
            {
                return NotFound();
            }
            for (int i = 0; i < FromClassRooms.Count; i++)
            {
                ClassroomAddDTO ClassroomAddDTO = mapper.Map<ClassroomAddDTO>(FromClassRooms[i]);
                ClassroomAddDTO.AcademicYearID = copyClassroomDTO.ToAcademicYearID;

                Classroom Classroom = mapper.Map<Classroom>(ClassroomAddDTO);

                TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
                Classroom.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                if (userTypeClaim == "octa")
                {
                    Classroom.InsertedByOctaId = userId;
                }
                else if (userTypeClaim == "employee")
                {
                    Classroom.InsertedByUserId = userId;
                }

                Unit_Of_Work.classroom_Repository.Add(Classroom);
            }

            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
