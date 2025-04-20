using AutoMapper;
using LMS_CMS_BL.DTO.Registration;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.RegisterationModule;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace LMS_CMS_PL.Controllers.Domains.Registeration
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class RegistrationFormTestAnswerController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public RegistrationFormTestAnswerController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        ///////////////////////////////////////////////////////////////

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" ,"parent"},
         pages: new[] { "Registration Confirmation"}
     )]
        public async Task<IActionResult> GetAsync(long id, long testId)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var tests = await Unit_Of_Work.registerationFormTestAnswer_Repository.Select_All_With_IncludesById<RegisterationFormTestAnswer>(
                b => b.IsDeleted != true && b.RegisterationFormParentID == id && b.Question.TestID == testId,
                query => query
                    .Include(answer => answer.RegisterationFormParent)
                    .Include(answer => answer.MCQQuestionOption)
                    .Include(answer => answer.Question)
                        .ThenInclude(question => question.QuestionType)
                    .Include(answer => answer.Question)
                        .ThenInclude(question => question.MCQQuestionOptions)
            );

            if (tests == null || tests.Count == 0)
            {
                return NotFound();
            }
            Test test =Unit_Of_Work.test_Repository.First_Or_Default(t=>t.ID== testId&& t.IsDeleted!=true);
            if(test == null)
            {
                return NotFound("there is no test with this id ");
            }
            RegisterationFormTest registerationFormTest = Unit_Of_Work.registerationFormTest_Repository.First_Or_Default(r=>r.TestID==testId && r.RegisterationFormParentID==id && r.IsDeleted!=true);
            
            var testDTOs = mapper.Map<List<RegisterationFormTestAnswerGetDTO>>(tests);

            var groupedByQuestionType = testDTOs
                .GroupBy(dto => new { dto.QuestionTypeID, dto.QuestionTypeName })
                .Select(group => new GroupedQuestionTypeDTO
                {
                    QuestionTypeID = group.Key.QuestionTypeID,
                    QuestionTypeName = group.Key.QuestionTypeName,
                    Questions = group.ToList()
                })
                .ToList();

            var response = new
            {
                TestName = test.Title,
                Totalmark = test.TotalMark,
                mark = registerationFormTest.Mark,
                isVisibleToParent = registerationFormTest.VisibleToParent,
                State=registerationFormTest.StateID,
                QuestionWithAnswer = groupedByQuestionType
            };

            return Ok(response);
        }

        ///////////////////////////////////////////////////////////////

        [HttpPost("{RegisterationFormParentId}/{TestId}")]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee","parent" },
           pages: new[] { "Registration Confirmation" }
         )]

        public async Task<IActionResult> Add(IEnumerable<RegisterationFormTestAnswerAddDTO> newAnswersList ,long RegisterationFormParentId , long TestId)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (newAnswersList == null || !newAnswersList.Any())
            {
                return BadRequest("Test answers cannot be null or empty.");
            }


            var addedAnswers = new List<RegisterationFormTestAnswer>();

            foreach (var newAnswer in newAnswersList)
            {
                if (newAnswer == null)
                {
                    return BadRequest("One of the answers is null.");
                }

                var question = Unit_Of_Work.question_Repository.First_Or_Default(s => s.ID == newAnswer.QuestionID);
                if (question == null)
                {
                    return BadRequest($"Question with ID {newAnswer.QuestionID} does not exist.");
                }

                var registerationFormParent = Unit_Of_Work.registerationFormParent_Repository.First_Or_Default(r => r.ID == newAnswer.RegisterationFormParentID);
                if (registerationFormParent == null)
                {
                    return NotFound($"RegisterationFormParent with ID {newAnswer.RegisterationFormParentID} does not exist.");
                }

                var answer = mapper.Map<RegisterationFormTestAnswer>(newAnswer);
                answer.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);

                if (userTypeClaim == "octa")
                {
                    answer.InsertedByOctaId = userId;
                }
                else if (userTypeClaim == "employee")
                {
                    answer.InsertedByUserId = userId;
                }

                if(answer.AnswerID == 0 )
                {
                    answer.AnswerID = null;
                }

                addedAnswers.Add(answer);
                Unit_Of_Work.registerationFormTestAnswer_Repository.Add(answer); 
            }

            Unit_Of_Work.SaveChanges();

            RegisterationFormTest registerationFormTest = new RegisterationFormTest();
            registerationFormTest.TestID = TestId;
            registerationFormTest.RegisterationFormParentID = RegisterationFormParentId;
            registerationFormTest.StateID = 1;
            registerationFormTest.VisibleToParent = false;
            registerationFormTest.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                registerationFormTest.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                registerationFormTest.InsertedByUserId = userId;
            }

            Unit_Of_Work.registerationFormTest_Repository.Add(registerationFormTest);
            Unit_Of_Work.SaveChanges();


            return Ok(newAnswersList);
        }

    }
}
