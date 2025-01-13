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
     allowedTypes: new[] { "octa", "employee" },
     pages: new[] { "Registration Confirmation", "Registration" }
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
                QuestionWithAnswer = groupedByQuestionType
            };

            return Ok(response);
        }

        ///////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
       allowedTypes: new[] { "octa", "employee" },
       pages: new[] { "Registration Confirmation", "Registration" }
     )]

        public async Task<IActionResult> Add(IEnumerable<RegisterationFormTestAnswerAddDTO> newAnswersList)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

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

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");

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

                addedAnswers.Add(answer);
                Unit_Of_Work.registerationFormTestAnswer_Repository.Add(answer);
            }
            Unit_Of_Work.SaveChanges();
            return Ok(newAnswersList);
        }

    }
}
