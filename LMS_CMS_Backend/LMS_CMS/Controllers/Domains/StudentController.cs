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
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using LMS_CMS_DAL.Models.Domains.BusModule;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        private readonly CheckPageAccessService _checkPageAccessService;
        private readonly SchoolHeaderService _schoolHeaderService;

        public StudentController(DbContextFactoryService dbContextFactory, IMapper mapper, UOW Unit_Of_Work, CheckPageAccessService checkPageAccessService, SchoolHeaderService schoolHeaderService)
        {

            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _Unit_Of_Work_Octa = Unit_Of_Work;
            _checkPageAccessService = checkPageAccessService;
            _schoolHeaderService = schoolHeaderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<Student> students = await Unit_Of_Work.student_Repository.Select_All_With_IncludesById<Student>(
                query => query.IsDeleted != true,
                query => query.Include(stu => stu.AccountNumber),
                query => query.Include(stu => stu.Gender));

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
                        item.NationalityEnName = nationality.Name;
                        item.NationalityArName = nationality.ArName;
                }
            }
            return Ok(StudentDTO);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> SearchStudents(string keyword, int pageNumber = 1, int pageSize = 10)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            // Build query with optional filter
            var query = Unit_Of_Work.student_Repository
                .SelectQuery<Student>(s => s.IsDeleted!= true && s.User_Name.Contains(keyword))
                .Include(s => s.AccountNumber)
                .Include(s => s.Gender)
                .OrderBy(s => s.en_name); // You can change to ar_name or Id if needed

            // Get total count for pagination info (optional)
            int totalRecords = await query.CountAsync();

            // Apply pagination
            var pagedStudents = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Check for results
            if (pagedStudents == null || pagedStudents.Count == 0)
            {
                return NotFound("No students found");
            }

            // Map to DTO
            var studentDTOs = mapper.Map<List<StudentGetDTO>>(pagedStudents);

            // Add nationality info
            foreach (var item in studentDTOs)
            {
                var nationality = _Unit_Of_Work_Octa.nationality_Repository.Select_By_Id_Octa(item.Nationality);
                if (nationality != null)
                {
                    item.NationalityEnName = nationality.Name;
                    item.NationalityArName = nationality.ArName;
                }
            }

            // Return with optional pagination info
            return Ok(new
            {
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Students = studentDTOs
            });
        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIDAsync(long Id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            Student student = await Unit_Of_Work.student_Repository.FindByIncludesAsync(
                query => query.IsDeleted != true && query.ID == Id,
                query => query.Include(stu => stu.Gender),
                query => query.Include(stu => stu.AccountNumber));

            if (student == null || student.IsDeleted == true)
            {
                return NotFound("No Student found");
            }

            StudentGetDTO StudentDTO = mapper.Map<StudentGetDTO>(student);
            Nationality nationality = _Unit_Of_Work_Octa.nationality_Repository.Select_By_Id_Octa(StudentDTO.Nationality);
            if (nationality != null)
            {
                StudentDTO.NationalityEnName = nationality.Name;
                StudentDTO.NationalityArName = nationality.ArName;
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

            Classroom cls = Unit_Of_Work.classroom_Repository.First_Or_Default(d => d.ID == Id && d.IsDeleted != true);
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

        /////
         
        [HttpGet("Get_By_SchoolID/{Id}")]
        public async Task<IActionResult> GetBySchoolID(long Id)
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

            School cls = Unit_Of_Work.school_Repository.First_Or_Default(d => d.ID == Id && d.IsDeleted != true);
            if (cls == null)
            {
                return NotFound("No School with this Id");
            }

            List<StudentAcademicYear> studentAcademicYears = await Unit_Of_Work.studentAcademicYear_Repository.Select_All_With_IncludesById<StudentAcademicYear>(
                query => query.IsDeleted != true && query.SchoolID == Id,
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

        /////

        [HttpGet("GetByAcademicYearID/{Id}")]
        public async Task<IActionResult> GetByAcademicYearID(long Id)
        {
            if (Id == 0)
            {
                return BadRequest("ID can't be null");
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

            AcademicYear year = Unit_Of_Work.academicYear_Repository.First_Or_Default(d => d.ID == Id && d.IsDeleted != true);
            if (year == null)
            {
                return NotFound("No Academic Year with this Id");
            }
            
            List<Classroom> classes = Unit_Of_Work.classroom_Repository.FindBy(d => d.IsDeleted != true && d.AcademicYearID == Id);
            if (classes == null)
            {
                return NotFound("No Classes with this Id");
            }

            List<Student> students = new List<Student>();

            for (int i = 0; i < classes.Count; i++)
            {
                List<StudentAcademicYear> studentAcademicYears = await Unit_Of_Work.studentAcademicYear_Repository.Select_All_With_IncludesById<StudentAcademicYear>(
                    query => query.IsDeleted != true && query.ClassID == classes[i].ID,
                    query => query.Include(stu => stu.Student)
                ); 

                for (int j = 0; j < studentAcademicYears.Count; j++)
                {
                    if (!students.Contains(studentAcademicYears[j].Student))
                    {
                        students.Add(studentAcademicYears[j].Student);
                    }
                }
            } 

            if (students == null || students.Count == 0)
            {
                return NotFound("No students found.");
            }
             
            List<StudentGetDTO> studentDTOs = mapper.Map<List<StudentGetDTO>>(students);

            return Ok(studentDTOs);
        }

        /////

        [HttpGet("GetBySchoolGradeClassID")]
        public async Task<IActionResult> GetBySchoolGradeClassID([FromQuery] long schoolId, [FromQuery] long gradeId, [FromQuery] long classId)
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

            if (schoolId == null || schoolId == 0)
            {
                return BadRequest("School Id can't be null");
            }
            
            if (gradeId == null || gradeId == 0)
            {
                return BadRequest("Grade Id can't be null");
            }

            if (classId == null || classId == 0)
            {
                return BadRequest("Class Id can't be null");
            } 
            
            Grade grade = Unit_Of_Work.grade_Repository.First_Or_Default(
                d => d.IsDeleted != true && d.ID == gradeId
                );
            if (grade == null)
            {
                return NotFound("No Grade with this Id");
            } 

            Classroom cls = Unit_Of_Work.classroom_Repository.First_Or_Default(
                d => d.IsDeleted != true && d.ID == classId
                );
            if (cls == null)
            {
                return NotFound("No Class with this Id");
            }

             
            List<StudentAcademicYear> studentAcademicYears = await Unit_Of_Work.studentAcademicYear_Repository
                .Select_All_With_IncludesById<StudentAcademicYear>(
                    s => s.IsDeleted != true && s.ClassID == classId && s.GradeID == gradeId && s.SchoolID == schoolId,
                    query => query.Include(stu => stu.Student)
                      .ThenInclude(stu => stu.Gender)
                );


            if (studentAcademicYears == null || studentAcademicYears.Count == 0)
            {
                return NotFound("No students found.");
            }

            List<Student> students = studentAcademicYears.Select(sa => sa.Student).ToList();
            List<StudentGetDTO> studentDTOs = mapper.Map<List<StudentGetDTO>>(students);

            for(int i = 0; i < studentDTOs.Count; i++)
            {
                Nationality nationality = _Unit_Of_Work_Octa.nationality_Repository.Select_By_Id_Octa(studentDTOs[i].Nationality);
                if (nationality != null)
                {
                    studentDTOs[i].NationalityEnName = nationality.Name;
                    studentDTOs[i].NationalityArName = nationality.ArName;
                }
            }
              
            ClassroomGetDTO classsDTO = mapper.Map<ClassroomGetDTO>(cls);

            string timeZoneId = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? "Egypt Standard Time"
                : "Africa/Cairo";

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

            School_GetDTO schoolDTO = _schoolHeaderService.GetSchoolHeader(Unit_Of_Work, schoolId, Request);

            return Ok(new
            {
                Students = studentDTOs,
                StudentsCount = studentDTOs.Count,
                School = schoolDTO,
                Class = classsDTO,
                Date = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone)
            });
        }

        /////
       
        [HttpGet("GetStudentByYearID")]
        public async Task<IActionResult> GetStudentByYearID([FromQuery] long yearId, [FromQuery] long stuId, [FromQuery] long schoolId)
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

            if (yearId == null || yearId == 0)
            {
                return BadRequest("Year Id can't be null");
            }
            
            if (stuId == null || stuId == 0)
            {
                return BadRequest("Student Id can't be null");
            }

            if (schoolId == null || schoolId == 0)
            {
                return BadRequest("School Id can't be null");
            }

            Student student = await Unit_Of_Work.student_Repository.FindByIncludesAsync(
                 query => query.IsDeleted != true && query.ID == stuId,
                 query => query.Include(stu => stu.Gender),
                 query => query.Include(stu => stu.Parent));

            if (student == null)
            {
                return NotFound("No Student with this Id");
            }   
             
            AcademicYear academicYear = Unit_Of_Work.academicYear_Repository.First_Or_Default(
                d => d.IsDeleted != true && d.ID == yearId
                );
            if (academicYear == null)
            {
                return NotFound("No Academic Year with this Id");
            } 
             
            List<Classroom> classrooms = Unit_Of_Work.classroom_Repository.FindBy(d => d.IsDeleted != true &&  d.AcademicYearID == yearId);

            if (classrooms == null || classrooms.Count == 0)
            {
                return NotFound("No Classes found.");
            }

            long clsID = 0;
            for (int i = 0; i < classrooms.Count; i++)
            {
                StudentAcademicYear stuAY = Unit_Of_Work.studentAcademicYear_Repository.First_Or_Default(d => d.IsDeleted != true && d.ClassID == classrooms[i].ID && d.StudentID == stuId);
                if( stuAY != null)
                {
                    clsID = stuAY.ClassID;
                }
            }

            Classroom cls = Unit_Of_Work.classroom_Repository.First_Or_Default(d => d.IsDeleted != true && d.ID == clsID);
             
            ClassroomGetDTO classDTO = mapper.Map<ClassroomGetDTO>(cls);

            StudentGetDTO studentDTO = mapper.Map<StudentGetDTO>(student);
            Nationality nationality = _Unit_Of_Work_Octa.nationality_Repository.Select_By_Id_Octa(studentDTO.Nationality);

            if (nationality != null)
            {
                studentDTO.NationalityEnName = nationality.Name;
                studentDTO.NationalityArName = nationality.ArName;
            }  

            string timeZoneId = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? "Egypt Standard Time"
                : "Africa/Cairo";

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

            School_GetDTO schoolDTO = _schoolHeaderService.GetSchoolHeader(Unit_Of_Work, schoolId, Request);

            return Ok(new
            {
                Student = studentDTO, 
                School = schoolDTO,
                Class = classDTO,
                Date = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone)
            });
        }
        
        /////
      
        [HttpGet("GetStudentProofRegistrationAndSuccessForm")]
        public async Task<IActionResult> GetStudentProofRegistrationAndSuccessForm([FromQuery] long yearId, [FromQuery] long stuId, [FromQuery] long schoolId)
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

            if (yearId == null || yearId == 0)
            {
                return BadRequest("Year Id can't be null");
            }
            
            if (stuId == null || stuId == 0)
            {
                return BadRequest("Student Id can't be null");
            }

            if (schoolId == null || schoolId == 0)
            {
                return BadRequest("School Id can't be null");
            }

            Student student = Unit_Of_Work.student_Repository.First_Or_Default(
                 query => query.IsDeleted != true && query.ID == stuId);

            if (student == null)
            {
                return NotFound("No Student with this Id");
            }   
             
            AcademicYear academicYear = Unit_Of_Work.academicYear_Repository.First_Or_Default(
                d => d.IsDeleted != true && d.ID == yearId
                );
            if (academicYear == null)
            {
                return NotFound("No Academic Year with this Id");
            }
             
            List<Classroom> classroomsTo = await Unit_Of_Work.classroom_Repository.Select_All_With_IncludesById<Classroom>(
                query => query.IsDeleted != true && query.AcademicYearID == yearId,
                query => query.Include(d => d.AcademicYear),
                query => query.Include(d => d.Grade));

            if (classroomsTo == null || classroomsTo.Count == 0)
            {
                return NotFound("No Classes found.");
            }

            long clsToID = 0;
            for (int i = 0; i < classroomsTo.Count; i++)
            {
                StudentAcademicYear stuAY = Unit_Of_Work.studentAcademicYear_Repository.First_Or_Default(d => d.IsDeleted != true && d.ClassID == classroomsTo[i].ID && d.StudentID == stuId);
                if (stuAY != null)
                {
                    clsToID = stuAY.ClassID;
                }
            }

            Classroom clsTo = Unit_Of_Work.classroom_Repository.First_Or_Default(d => d.IsDeleted != true && d.ID == clsToID);

            ClassroomGetDTO classToDTO = mapper.Map<ClassroomGetDTO>(clsTo);

            List<Classroom> classroomsFrom = await Unit_Of_Work.classroom_Repository.Select_All_With_IncludesById<Classroom>(
                query => query.IsDeleted != true,
                query => query.Include(d => d.AcademicYear),
                query => query.Include(d => d.Grade));

            if (classroomsFrom == null || classroomsFrom.Count == 0)
            {
                return NotFound("No Classes found.");
            }

            classroomsFrom = classroomsFrom.OrderBy(c => c.AcademicYear.DateFrom).ToList();

            List<Classroom> classroomsFilteredFrom = new List<Classroom>();
            for (int i = 0; i < classroomsFrom.Count; i++)
            {
                StudentAcademicYear stuAY = Unit_Of_Work.studentAcademicYear_Repository.First_Or_Default(d => d.IsDeleted != true && d.ClassID == classroomsFrom[i].ID && d.StudentID == stuId);
                if (stuAY != null)
                {
                    classroomsFilteredFrom.Add(classroomsFrom[i]);
                }
            }
             
            List<ClassroomGetDTO> classFromDTOs = mapper.Map<List<ClassroomGetDTO>>(classroomsFilteredFrom);

            StudentGetDTO studentDTO = mapper.Map<StudentGetDTO>(student);
            Nationality nationality = _Unit_Of_Work_Octa.nationality_Repository.Select_By_Id_Octa(studentDTO.Nationality);

            if (nationality != null)
            {
                studentDTO.NationalityEnName = nationality.Name;
                studentDTO.NationalityArName = nationality.ArName;
            }  

            string timeZoneId = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? "Egypt Standard Time"
                : "Africa/Cairo";

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

            School_GetDTO schoolDTO = _schoolHeaderService.GetSchoolHeader(Unit_Of_Work, schoolId, Request);

            return Ok(new
            {
                Student = studentDTO, 
                School = schoolDTO,
                ClassFrom = classFromDTOs[0],
                ClassTo = classToDTO,
                Date = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone)
            });
        }
         
        /////
      
        [HttpGet("GetStudentProofRegistration")]
        public async Task<IActionResult> GetStudentProofRegistration([FromQuery] long yearId, [FromQuery] long stuId, [FromQuery] long schoolId)
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

            if (yearId == null || yearId == 0)
            {
                return BadRequest("Year Id can't be null");
            }
            
            if (stuId == null || stuId == 0)
            {
                return BadRequest("Student Id can't be null");
            }

            if (schoolId == null || schoolId == 0)
            {
                return BadRequest("School Id can't be null");
            }

            Student student = Unit_Of_Work.student_Repository.First_Or_Default(
                 query => query.IsDeleted != true && query.ID == stuId);

            if (student == null)
            {
                return NotFound("No Student with this Id");
            }   
             
            AcademicYear academicYear = Unit_Of_Work.academicYear_Repository.First_Or_Default(
                d => d.IsDeleted != true && d.ID == yearId
                );
            if (academicYear == null)
            {
                return NotFound("No Academic Year with this Id");
            } 
             
            List<Classroom> classrooms = await Unit_Of_Work.classroom_Repository.Select_All_With_IncludesById<Classroom>(
                query => query.IsDeleted != true && query.AcademicYearID == yearId,
                query => query.Include(d => d.AcademicYear),
                query => query.Include(d => d.Grade));

            if (classrooms == null || classrooms.Count == 0)
            {
                return NotFound("No Classes found.");
            }

            long clsID = 0;
            for (int i = 0; i < classrooms.Count; i++)
            {
                StudentAcademicYear stuAY = Unit_Of_Work.studentAcademicYear_Repository.First_Or_Default(d => d.IsDeleted != true && d.ClassID == classrooms[i].ID && d.StudentID == stuId);
                if (stuAY != null)
                {
                    clsID = stuAY.ClassID;
                }
            }

            Classroom cls = Unit_Of_Work.classroom_Repository.First_Or_Default(d => d.IsDeleted != true && d.ID == clsID);

            ClassroomGetDTO classDTO = mapper.Map<ClassroomGetDTO>(cls);
             
            StudentGetDTO studentDTO = mapper.Map<StudentGetDTO>(student);
            Nationality nationality = _Unit_Of_Work_Octa.nationality_Repository.Select_By_Id_Octa(studentDTO.Nationality);

            if (nationality != null)
            {
                studentDTO.NationalityEnName = nationality.Name;
                studentDTO.NationalityArName = nationality.ArName;
            }   

            string timeZoneId = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? "Egypt Standard Time"
                : "Africa/Cairo";

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

            School_GetDTO schoolDTO = _schoolHeaderService.GetSchoolHeader(Unit_Of_Work, schoolId, Request);

            return Ok(new
            {
                Student = studentDTO, 
                School = schoolDTO, 
                Class = classDTO,
                Date = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone)
            });
        }

        /////

        [HttpGet("GetByClassIDReport")]
        public async Task<IActionResult> GetByClassID([FromQuery] long schoolId, [FromQuery] long classId)
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

            if (schoolId == null || schoolId == 0)
            {
                return BadRequest("School Id can't be null");
            } 

            if (classId == null || classId == 0)
            {
                return BadRequest("Class Id can't be null");
            }
             
            Classroom cls = Unit_Of_Work.classroom_Repository.First_Or_Default(
                d => d.IsDeleted != true && d.ID == classId
                );
            if (cls == null)
            {
                return NotFound("No Class with this Id");
            }


            List<StudentAcademicYear> studentAcademicYears = await Unit_Of_Work.studentAcademicYear_Repository
                .Select_All_With_IncludesById<StudentAcademicYear>(
                    s => s.IsDeleted != true && s.ClassID == classId && s.SchoolID == schoolId,
                    query => query.Include(stu => stu.Student)
                      .ThenInclude(stu => stu.Gender)
                );


            if (studentAcademicYears == null || studentAcademicYears.Count == 0)
            {
                return NotFound("No students found.");
            }

            List<Student> students = studentAcademicYears.Select(sa => sa.Student).ToList();
            List<StudentGetDTO> studentDTOs = mapper.Map<List<StudentGetDTO>>(students);

            for (int i = 0; i < studentDTOs.Count; i++)
            {
                Nationality nationality = _Unit_Of_Work_Octa.nationality_Repository.Select_By_Id_Octa(studentDTOs[i].Nationality);
                if (nationality != null)
                {
                    studentDTOs[i].NationalityEnName = nationality.Name;
                    studentDTOs[i].NationalityArName = nationality.ArName;
                }

                List<BusStudent> busStudents = await Unit_Of_Work.busStudent_Repository.Select_All_With_IncludesById<BusStudent>(
                   query => query.IsDeleted != true && query.Semester.AcademicYearID == cls.AcademicYearID && query.Semester.IsCurrent == true,
                   query => query.Include(d => d.BusCategory),
                   query => query.Include(d => d.Semester));

                if (busStudents.Count == 0)
                {
                    studentDTOs[i].IsRegisteredToBus = null;
                }
                else
                {
                    busStudents = busStudents.OrderBy(c => c.Semester.DateFrom).ToList();
                    studentDTOs[i].IsRegisteredToBus = $"Yes / {busStudents[busStudents.Count-1].BusCategory.Name}";
                }
            }
             

            string timeZoneId = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? "Egypt Standard Time"
                : "Africa/Cairo";

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

            School_GetDTO schoolDTO = _schoolHeaderService.GetSchoolHeader(Unit_Of_Work, schoolId, Request);

            return Ok(new
            {
                Students = studentDTOs, 
                School = schoolDTO, 
                Date = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone)
            });
        }

        /////

        [HttpGet("AcademicSequentialReport")]
        public async Task<IActionResult> AcademicSequentialReport([FromQuery] long stuId, [FromQuery] long schoolId)
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

            if (stuId == null || stuId == 0)
            {
                return BadRequest("Student Id can't be null");
            }

            if (schoolId == null || schoolId == 0)
            {
                return BadRequest("School Id can't be null");
            }

            Student student = await Unit_Of_Work.student_Repository.FindByIncludesAsync(
                 query => query.IsDeleted != true && query.ID == stuId,
                 query => query.Include(stu => stu.Gender),
                 query => query.Include(stu => stu.Parent));

            if (student == null)
            {
                return NotFound("No Student with this Id");
            }

            List<StudentAcademicYear> StudentAcademicYear = await Unit_Of_Work.studentAcademicYear_Repository.Select_All_With_IncludesById<StudentAcademicYear>(
                 query => query.IsDeleted != true && query.StudentID == stuId && query.SchoolID == schoolId,
                 query => query.Include(stu => stu.Grade),
                 query => query.Include(stu => stu.Classroom).ThenInclude(d => d.AcademicYear));

            if(StudentAcademicYear == null)
            {
                return NotFound("No Student Academic Year With This Student ID");
            }

            List<GradeWithAcademicYearGetDTO> gradeWithAcYears = new List<GradeWithAcademicYearGetDTO>();
            
            string currentGradeName = "";
            
            for(int i = 0; i < StudentAcademicYear.Count; i++)
            {
                GradeWithAcademicYearGetDTO data = new GradeWithAcademicYearGetDTO();
                data.GradeID = StudentAcademicYear[i].GradeID;
                data.GradeName = StudentAcademicYear[i].Grade.Name;
                data.AcademicYearID = StudentAcademicYear[i].Classroom.AcademicYearID;
                data.AcademicYearName = StudentAcademicYear[i].Classroom.AcademicYear.Name;
                gradeWithAcYears.Add(data);

                if (StudentAcademicYear[i].Classroom.AcademicYear.IsActive == true)
                {
                    currentGradeName = StudentAcademicYear[i].Grade.Name;
                }
            }

            StudentGetDTO studentDTO = mapper.Map<StudentGetDTO>(student);
            studentDTO.CurrentGradeName = currentGradeName;             

            Nationality nationality = _Unit_Of_Work_Octa.nationality_Repository.Select_By_Id_Octa(studentDTO.Nationality);

            if (nationality != null)
            {
                studentDTO.NationalityEnName = nationality.Name;
                studentDTO.NationalityArName = nationality.ArName;
            }

            string timeZoneId = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? "Egypt Standard Time"
                : "Africa/Cairo";

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

            School_GetDTO schoolDTO = _schoolHeaderService.GetSchoolHeader(Unit_Of_Work, schoolId, Request);

            return Ok(new
            {
                Student = studentDTO,
                School = schoolDTO,
                Grades = gradeWithAcYears,
                Date = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone)
            });
        }

        //////

        [HttpGet("TransferedFromKindergartenReport")]
        public async Task<IActionResult> TransferedFromKindergartenReport([FromQuery] long stuId, [FromQuery] long schoolId)
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

            if (stuId == null || stuId == 0)
            {
                return BadRequest("Student Id can't be null");
            }

            if (schoolId == null || schoolId == 0)
            {
                return BadRequest("School Id can't be null");
            }

            Student student = await Unit_Of_Work.student_Repository.FindByIncludesAsync(
                 query => query.IsDeleted != true && query.ID == stuId,
                 query => query.Include(stu => stu.Gender),
                 query => query.Include(stu => stu.Parent));

            if (student == null)
            {
                return NotFound("No Student with this Id");
            }

            List<StudentAcademicYear> StudentAcademicYear = await Unit_Of_Work.studentAcademicYear_Repository.Select_All_With_IncludesById<StudentAcademicYear>(
                 query => query.IsDeleted != true && query.StudentID == stuId && query.SchoolID == schoolId,
                 query => query.Include(stu => stu.Grade),
                 query => query.Include(stu => stu.Classroom).ThenInclude(d => d.AcademicYear));

            if (StudentAcademicYear == null)
            {
                return NotFound("No Student Academic Year With This Student ID");
            } 

            string currentGradeName = "";
            string CurrentAcademicYear = "";

            for (int i = 0; i < StudentAcademicYear.Count; i++)
            { 
                if (StudentAcademicYear[i].Classroom.AcademicYear.IsActive == true)
                {
                    currentGradeName = StudentAcademicYear[i].Grade.Name;
                    CurrentAcademicYear = StudentAcademicYear[i].Classroom.AcademicYear.Name;
                }
            }

            StudentGetDTO studentDTO = mapper.Map<StudentGetDTO>(student);
            studentDTO.CurrentGradeName = currentGradeName;
            studentDTO.CurrentAcademicYear = CurrentAcademicYear;

            Nationality nationality = _Unit_Of_Work_Octa.nationality_Repository.Select_By_Id_Octa(studentDTO.Nationality);

            if (nationality != null)
            {
                studentDTO.NationalityEnName = nationality.Name;
                studentDTO.NationalityArName = nationality.ArName;
            }

            string timeZoneId = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? "Egypt Standard Time"
                : "Africa/Cairo";

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

            School_GetDTO schoolDTO = _schoolHeaderService.GetSchoolHeader(Unit_Of_Work, schoolId, Request);

            return Ok(new
            {
                Student = studentDTO,
                School = schoolDTO, 
                Date = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone)
            });
        }

        //////

        [HttpPut("StudentAccounting")]
        [Authorize_Endpoint_(
          allowedTypes: new[] { "octa", "employee" },
          allowEdit: 1,
          pages: new[] { "Student Edit Accounting" }
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
                IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Student Edit Accounting", roleId, userId, student);
                if (accessCheck != null)
                {
                    return accessCheck;
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
                query => query.Include(stu => stu.Gender),
                query => query.Include(stu => stu.AccountNumber));

            if (student == null || student.IsDeleted == true)
            {
                return NotFound("No Student found");
            }

            StudentGetDTO StudentDTO = mapper.Map<StudentGetDTO>(student);
            Nationality nationality = _Unit_Of_Work_Octa.nationality_Repository.Select_By_Id_Octa(StudentDTO.Nationality);
            if (nationality != null)
            {
                StudentDTO.NationalityEnName = nationality.Name;
                StudentDTO.NationalityArName = nationality.ArName;
            }


            return Ok(StudentDTO);
        }

    }
}
