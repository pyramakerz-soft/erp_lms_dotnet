using AutoMapper;
using LMS_CMS_BL.DTO.Bus;
using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers.Domains
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public StudentController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }


        [HttpGet("{Id}")]
        public IActionResult GetByID(long Id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            Student student = Unit_Of_Work.student_Repository.Select_By_Id(Id);

            if (student == null || student.IsDeleted == true)
            {
                return NotFound("No Student found");
            }

            StudentGetDTO StudentDTO = mapper.Map<StudentGetDTO>(student);

            return Ok(StudentDTO);
        }

        [HttpGet("Get_By_ClassID/{Id}")]
        public async Task<IActionResult> GetByClassID(long Id)
        {
            if (Id == 0)
            {
                return BadRequest("ID can't e null");
            }

            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            Class cls = Unit_Of_Work.class_Repository.Select_By_Id(Id);
            if (cls == null)
            {
                return NotFound("No Class with this Id");
            }

            List<StudentAcademicYear> studentAcademicYears = await Unit_Of_Work.studentAcademicYear_Repository.Select_All_With_IncludesById<StudentAcademicYear>(
                query => query.IsDeleted != true && query.ClassID == Id,
                query => query.Include(stu => stu.Student)
            );

            if (studentAcademicYears == null || studentAcademicYears.Count == 0)
            {
                return NotFound("No students found.");
            }

            List<Student> students = studentAcademicYears.Select(sa => sa.Student).ToList();
            List<StudentGetDTO> studentDTOs = mapper.Map<List<StudentGetDTO>>(students);

            return Ok(studentDTOs);
        }
    }
}
