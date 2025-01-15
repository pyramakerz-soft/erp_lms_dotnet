using AutoMapper;
using LMS_CMS_BL.DTO.Registration;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_DAL.Models.Domains.RegisterationModule;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers.Domains.Registeration
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class TestController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public TestController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" },
         pages: new[] { "Admission Test", "Registration" }
         )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<Test> tests = await Unit_Of_Work.test_Repository.Select_All_With_IncludesById<Test>(
                    b => b.IsDeleted != true,
                    query => query.Include(emp => emp.academicYear),
                    query => query.Include(emp => emp.subject),
                    query => query.Include(emp => emp.Grade),
                    query => query.Include(emp => emp.academicYear.School)
                    );

            if (tests == null || tests.Count == 0)
            {
                return NotFound();
            }

            List<TestGetDTO> testDTO = mapper.Map<List<TestGetDTO>>(tests);

             return Ok(testDTO);
        }
        //////////////////////////////////////////////////////////////////////////////
        [HttpGet("byGradeId/{id}")]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" },
         pages: new[] { "Admission Test", "Registration" }
         )]
        public async Task<IActionResult> GetbyGradeIdAsync(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<Test> tests = await Unit_Of_Work.test_Repository.Select_All_With_IncludesById<Test>(
                    b => b.IsDeleted != true && b.GradeID==id,
                    query => query.Include(emp => emp.academicYear),
                    query => query.Include(emp => emp.subject),
                    query => query.Include(emp => emp.Grade),
                    query => query.Include(emp => emp.academicYear.School)
                    );

            if (tests == null || tests.Count == 0)
            {
                return NotFound();
            }

            List<TestGetDTO> testDTO = mapper.Map<List<TestGetDTO>>(tests);

            return Ok(testDTO);
        }

        //////////////////////////////////////////////////////////////////////////////
        [HttpGet("byRegistrationFormParentID/{id}")]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" ,"parent"},
         pages: new[] { "Admission Test", "Registration" }
         )]
        public async Task<IActionResult> GetbyRegistrationFormParentIDAsync(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            RegisterationFormParent registerationFormParent = Unit_Of_Work.registerationFormParent_Repository.First_Or_Default(r => r.ID == id);
            long GradeId = Convert.ToInt64(registerationFormParent.GradeID);

            List<Test> tests = await Unit_Of_Work.test_Repository.Select_All_With_IncludesById<Test>(
                    b => b.IsDeleted != true && b.GradeID == GradeId,
                    query => query.Include(emp => emp.academicYear),
                    query => query.Include(emp => emp.subject),
                    query => query.Include(emp => emp.Grade),
                    query => query.Include(emp => emp.academicYear.School)
                    );

            if (tests == null || tests.Count == 0)
            {
                return NotFound();
            }

            List<TestGetDTO> testDTO = mapper.Map<List<TestGetDTO>>(tests);
            foreach (var item in testDTO)
            {
                RegisterationFormTest registerationFormTest =await Unit_Of_Work.registerationFormTest_Repository.FindByIncludesAsync(r => r.RegisterationFormParentID == id && r.TestID == item.ID,
                    query => query.Include(emp => emp.TestState));

                if (registerationFormTest != null)
                {
                    item.RegistrationTestMark = registerationFormTest.Mark;
                    item.RegistrationTestState=registerationFormTest.TestState.Name;
                    item.RegistrationTestVisibleToParent = registerationFormTest.VisibleToParent;
                    item.RegistrationTestID = registerationFormTest.ID;
                    item.RegistrationTestStateId = registerationFormTest.StateID;

                }
                else
                {
                    item.RegistrationTestMark = null;
                    item.RegistrationTestState = null;
                    item.RegistrationTestVisibleToParent = null;
                    item.RegistrationTestID = null;
                    item.RegistrationTestStateId = null;

                }

            }
            var response = new
            {
                StudentName = registerationFormParent.StudentName,
                Tests = testDTO
            };
            return Ok(response);
        }

        //////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" },
         pages: new[] { "Admission Test", "Registration" }
       )]
        public IActionResult Add(TestAddDTO newTest)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (newTest == null)
            {
                return BadRequest("test cannot be null");
            }
            Subject subject = Unit_Of_Work.subject_Repository.First_Or_Default(s=>s.ID==newTest.SubjectID&&s.IsDeleted!=true);
            if(subject == null)
            {
                return BadRequest("this subject not exist");
            }
            Grade grade = Unit_Of_Work.grade_Repository.First_Or_Default(s => s.ID == newTest.GradeID && s.IsDeleted != true);
            if (grade == null)
            {
                return BadRequest("this grade not exist");
            }
            AcademicYear academic = Unit_Of_Work.academicYear_Repository.First_Or_Default(s => s.ID == newTest.AcademicYearID && s.IsDeleted != true);
            if (academic == null)
            {
                return BadRequest("this AcademicYear not exist");
            }
            if (subject.GradeID != newTest.GradeID)
            {
                return BadRequest("this subject not exist in this grade");
            }
            Test test = mapper.Map<Test>(newTest);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            test.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                test.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                test.InsertedByUserId = userId;
            }

            Unit_Of_Work.test_Repository.Add(test);
            Unit_Of_Work.SaveChanges();
            return Ok(newTest);
        }
        //////////////////////////////////////////////////////////////////////////////
        ///
        [HttpGet("{id}")]
        [Authorize_Endpoint_(
        allowedTypes: new[] { "octa", "employee" },
        pages: new[] { "Admission Test", "Registration" }
        )]
        public async Task<IActionResult> GetAsyncbyId(int id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            Test tests = await Unit_Of_Work.test_Repository.FindByIncludesAsync(
                    b => b.IsDeleted != true&&b.ID==id,
                    query => query.Include(emp => emp.academicYear),
                    query => query.Include(emp => emp.subject),
                    query => query.Include(emp => emp.Grade),
                    query => query.Include(emp => emp.academicYear.School)
                    );

            if (tests == null )
            {
                return NotFound();
            }

            TestGetDTO testDTO = mapper.Map<TestGetDTO>(tests);

            return Ok(testDTO);
        }
        //////////////////////////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
        allowedTypes: new[] { "octa", "employee" },
        allowEdit: 1,
       pages: new[] { "Admission Test", "Registration" }
    )]
        public IActionResult Edit(TestEditDTO newTest)
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

            if (newTest == null)
            {
                return BadRequest("Building cannot be null");
            }

            Test test = Unit_Of_Work.test_Repository.First_Or_Default(b => b.ID == newTest.ID && b.IsDeleted != true);
            if (test == null)
            {
                return NotFound("No test with this ID");
            }
            Subject subject = Unit_Of_Work.subject_Repository.First_Or_Default(s => s.ID == newTest.SubjectID && s.IsDeleted != true);
            if (subject == null)
            {
                return BadRequest("this subject not exist");
            }
            Grade grade = Unit_Of_Work.grade_Repository.First_Or_Default(s => s.ID == newTest.GradeID && s.IsDeleted != true);
            if (grade == null)
            {
                return BadRequest("this grade not exist");
            }
            AcademicYear academic = Unit_Of_Work.academicYear_Repository.First_Or_Default(s => s.ID == newTest.AcademicYearID && s.IsDeleted != true);
            if (academic == null)
            {
                return BadRequest("this AcademicYear not exist");
            }
            if (subject.GradeID != newTest.GradeID)
            {
                return BadRequest("this subject not exist in this grade");
            }
            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Admission Test");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (test.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Admission Test page doesn't exist");
                }
            }

            mapper.Map(newTest, test);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
             test.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                test.UpdatedByOctaId = userId;
                if (test.UpdatedByUserId != null)
                {
                    test.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                test.UpdatedByUserId = userId;
                if (test.UpdatedByOctaId != null)
                {
                    test.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.test_Repository.Update(test);
            Unit_Of_Work.SaveChanges();
            return Ok(newTest);
        }

        //////////////////////////////////////////////////////////////////////////////////

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
          allowedTypes: new[] { "octa", "employee" },
          allowDelete: 1,
         pages: new[] { "Admission Test", "Registration" }
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
                return BadRequest("Enter Category ID");
            }

            Test test = Unit_Of_Work.test_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);


            if (test == null)
            {
                return NotFound();
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Admission Test");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Delete_For_Others == false)
                    {
                        if (test.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Admission Test page doesn't exist");
                }
            }

            test.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            test.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                test.DeletedByOctaId = userId;
                if (test.DeletedByUserId != null)
                {
                    test.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                test.DeletedByUserId = userId;
                if (test.DeletedByOctaId != null)
                {
                    test.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.test_Repository.Update(test);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
