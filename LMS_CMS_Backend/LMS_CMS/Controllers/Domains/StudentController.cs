using AutoMapper;
using LMS_CMS_BL.DTO.Accounting;
using LMS_CMS_BL.DTO.Bus;
using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.AccountingModule;
using LMS_CMS_DAL.Models.Domains.Administration;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_DAL.Models.Octa;
using LMS_CMS_PL.Attribute;
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
        private readonly UOW _Unit_Of_Work_Octa;

        public StudentController(DbContextFactoryService dbContextFactory, IMapper mapper, UOW Unit_Of_Work)
        {

            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _Unit_Of_Work_Octa = Unit_Of_Work;

        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<Student> students = await Unit_Of_Work.student_Repository.Select_All_With_IncludesById<Student>(
                query => query.IsDeleted != true,
                query => query.Include(stu => stu.AccountNumber));

            if (students == null || students.Count == 0)
            {
                return NotFound("No Student found");
            }

            List<StudentGetDTO> StudentDTO = mapper.Map<List<StudentGetDTO>>(students);
            foreach (var item in StudentDTO)
            {
                Nationality nationality = _Unit_Of_Work_Octa.nationality_Repository.Select_By_Id_Octa(item.Nationality);
                if (nationality != null)
                {
                        item.NationalityName = nationality.Name;
                }
            }
            return Ok(StudentDTO);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIDAsync(long Id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            Student student = await Unit_Of_Work.student_Repository.FindByIncludesAsync(
                query => query.IsDeleted != true && query.ID == Id,
                query => query.Include(stu => stu.AccountNumber));

            if (student == null || student.IsDeleted == true)
            {
                return NotFound("No Student found");
            }

            StudentGetDTO StudentDTO = mapper.Map<StudentGetDTO>(student);
            Nationality nationality = _Unit_Of_Work_Octa.nationality_Repository.Select_By_Id_Octa(StudentDTO.Nationality);
            if (nationality != null)
            {
                StudentDTO.NationalityName = nationality.Name;
            }


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

            Classroom cls = Unit_Of_Work.classroom_Repository.Select_By_Id(Id);
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

        //////

        [HttpPut("StudentAccounting")]
        [Authorize_Endpoint_(
          allowedTypes: new[] { "octa", "employee" },
          allowEdit: 1,
          pages: new[] { "Accounting", "Student Edit Accounting" }
      )]
        public async Task<IActionResult> EditStudentAccountingAsync(AccountingStudentPutDTO newStudent)
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
                return Unauthorized("User ID, Type claim not found.");
            }

            Student student = Unit_Of_Work.student_Repository.First_Or_Default(s => s.ID == newStudent.ID && s.IsDeleted != true);
            if (student == null || student.IsDeleted == true)
            {
                return NotFound("No student with this ID");
            }

            AccountingTreeChart account = Unit_Of_Work.accountingTreeChart_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == newStudent.AccountNumberID);

            if (account == null)
            {
                return NotFound("No Account chart with this Id");
            }
            else
            {
                if (account.SubTypeID == 1)
                {
                    return BadRequest("You can't use main account, only sub account");
                }

                if (account.LinkFileID != 13)
                {
                    return BadRequest("Wrong Link File, it should be Asset file link");
                }
            }

            
            if (newStudent.Nationality != 0 && newStudent.Nationality != null)
            {
                Nationality nationality = _Unit_Of_Work_Octa.nationality_Repository.Select_By_Id_Octa(newStudent.Nationality);
                if (nationality == null)
                {
                    return BadRequest("There is no nationality with this id");
                }

            }

            if (userTypeClaim == "employee")
            {
                LMS_CMS_DAL.Models.Domains.Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Student Edit Accounting");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (student.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Employee page doesn't exist");
                }
            }

            mapper.Map(newStudent, student);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            student.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                student.UpdatedByOctaId = userId;
                if (student.UpdatedByUserId != null)
                {
                    student.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                student.UpdatedByUserId = userId;
                if (student.UpdatedByOctaId != null)
                {
                    student.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.student_Repository.Update(student);
            Unit_Of_Work.SaveChanges();
            return Ok(newStudent);
        }

        ////

        [HttpGet("SearchByNationality/{NationalID}")]
        public async Task<IActionResult> GetByNationalityAsync(string NationalID)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            Student student = await Unit_Of_Work.student_Repository.FindByIncludesAsync(
                query => query.IsDeleted != true && query.NationalID == NationalID,
                query => query.Include(stu => stu.AccountNumber));

            if (student == null || student.IsDeleted == true)
            {
                return NotFound("No Student found");
            }

            StudentGetDTO StudentDTO = mapper.Map<StudentGetDTO>(student);
            Nationality nationality = _Unit_Of_Work_Octa.nationality_Repository.Select_By_Id_Octa(StudentDTO.Nationality);
            if (nationality != null)
            {
                StudentDTO.NationalityName = nationality.Name;
            }


            return Ok(StudentDTO);
        }

    }
}
