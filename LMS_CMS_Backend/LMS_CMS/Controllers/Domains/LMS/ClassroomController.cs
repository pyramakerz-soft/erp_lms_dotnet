using AutoMapper;
using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        //[HttpPost]
        //[Authorize_Endpoint_(
        //    allowedTypes: new[] { "octa", "employee" },
        //    pages: new[] { "Classrooms", "Administrator" }
        //)]
        //public IActionResult Add(ClassroomAddDTO NewClassroom)
        //{
        //    UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

        //    var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
        //    long.TryParse(userIdClaim, out long userId);
        //    var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

        //    if (userIdClaim == null || userTypeClaim == null)
        //    {
        //        return Unauthorized("User ID or Type claim not found.");
        //    }

        //    if (NewClassroom == null)
        //    {
        //        return BadRequest("Classroom cannot be null");
        //    }

        //    if (NewClassroom.GradeID != 0)
        //    {
        //        Grade Grade = Unit_Of_Work.grade_Repository.Select_By_Id(NewClassroom.GradeID);
        //        if (building == null)
        //        {
        //            return BadRequest("No Building with this ID");
        //        }
        //    }

        //    Floor Floor = mapper.Map<Floor>(NewFloor);

        //    TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
        //    Floor.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
        //    if (userTypeClaim == "octa")
        //    {
        //        Floor.InsertedByOctaId = userId;
        //    }
        //    else if (userTypeClaim == "employee")
        //    {
        //        Floor.InsertedByUserId = userId;
        //    }

        //    Unit_Of_Work.floor_Repository.Add(Floor);
        //    Unit_Of_Work.SaveChanges();
        //    return Ok(NewFloor);
        //}
    }
}
