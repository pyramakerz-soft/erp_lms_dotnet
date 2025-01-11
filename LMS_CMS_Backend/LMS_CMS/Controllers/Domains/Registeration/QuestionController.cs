using AutoMapper;
using LMS_CMS_BL.DTO.Registration;
using LMS_CMS_BL.UOW;
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
    public class QuestionController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public QuestionController(DbContextFactoryService dbContextFactory, IMapper mapper)
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

            List<Question> questions = await Unit_Of_Work.question_Repository.Select_All_With_IncludesById<Question>(
                    b => b.IsDeleted != true,
                    query => query.Include(emp => emp.QuestionType),
                    query => query.Include(emp => emp.mCQQuestionOption),
                    query => query.Include(emp => emp.test),
                    query => query.Include(emp => emp.MCQQuestionOptions)
                    );

            if (questions == null || questions.Count == 0)
            {
                return NotFound();
            }

            List<questionGetDTO> questionDTO = mapper.Map<List<questionGetDTO>>(questions);

            return Ok(questionDTO);
        }
        //////////////////////////////////////////////////////////////////////////////

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
       allowedTypes: new[] { "octa", "employee" },
       pages: new[] { "Admission Test", "Registration" }
       )]
        public async Task<IActionResult> GetAsyncbyId(int id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            Question questions = await Unit_Of_Work.question_Repository.FindByIncludesAsync(
                    b => b.IsDeleted != true &&b.ID==id,
                    query => query.Include(emp => emp.QuestionType),
                    query => query.Include(emp => emp.mCQQuestionOption),
                    query => query.Include(emp => emp.test),
                    query => query.Include(emp => emp.MCQQuestionOptions)
                    );

            if (questions == null)
            {
                return NotFound();
            }

            questionGetDTO questionDTO = mapper.Map<questionGetDTO>(questions);

            return Ok(questionDTO);
        }

        //////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" },
         pages: new[] { "Admission Test", "Registration" }
       )]
        public async Task<IActionResult> Add(QuestionAddDTO newQuestion)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (newQuestion == null)
            {
                return BadRequest("test cannot be null");
            }
            QuestionType questionType = Unit_Of_Work.questionType_Repository.First_Or_Default(s => s.ID == newQuestion.QuestionTypeID);
            if (questionType == null)
            {
                return BadRequest("this Question Type not exist");
            }
           
            Test test = Unit_Of_Work.test_Repository.First_Or_Default(s => s.ID == newQuestion.TestID && s.IsDeleted != true);
            if (test == null)
            {
                return BadRequest("this Test not exist");
            }

            if (newQuestion.QuestionTypeID == 2)
            {
                if (newQuestion.options.Count == 0)
                {
                    return BadRequest("options in msq question is required");
                }
                if (newQuestion.CorrectAnswer == "")
                {
                    return BadRequest("CorrectAnswer in msq question is required");
                }
            }
            Question question = mapper.Map<Question>(newQuestion);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            question.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                question.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                question.InsertedByUserId = userId;
            }

            Unit_Of_Work.question_Repository.Add(question);
            Unit_Of_Work.SaveChanges();
            long correctA = 0;
            foreach (var item in newQuestion.options)
            {
                MCQQuestionOption mCQQuestionOption=new MCQQuestionOption();
                mCQQuestionOption.Name = item;
                mCQQuestionOption.Question_ID=question.ID;
                mCQQuestionOption.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                if (userTypeClaim == "octa")
                {
                    mCQQuestionOption.InsertedByOctaId = userId;
                }
                else if (userTypeClaim == "employee")
                {
                    mCQQuestionOption.InsertedByUserId = userId;
                }
               await Unit_Of_Work.mCQQuestionOption_Repository.AddAsync(mCQQuestionOption);
               await Unit_Of_Work.SaveChangesAsync();
                if(newQuestion.CorrectAnswer==item)
                {
                   correctA=mCQQuestionOption.ID;
                }

            }
            if(correctA==0) 
            {
                return BadRequest("correct answer cannot be null");
            }

            question.CorrectAnswerID=correctA;
            Unit_Of_Work.question_Repository.Update(question);
            await Unit_Of_Work.SaveChangesAsync();
            return Ok(newQuestion);
        }
    }
}
