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
using Microsoft.IdentityModel.Tokens;
using static System.Net.Mime.MediaTypeNames;

namespace LMS_CMS_PL.Controllers.Domains.Registeration
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;

        public QuestionController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
        }

        [HttpGet]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee"}
         ,
         pages: new[] { "Admission Test" }
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

            foreach (var question in questionDTO) 
            { 
            
            string serverUrl = $"{Request.Scheme}://{Request.Host}/";

            if (!string.IsNullOrEmpty(question.Image))
            {
                    question.Image = $"{serverUrl}{question.Image.Replace("\\", "/")}";
            }
            if (!string.IsNullOrEmpty(question.Video))
            {
                question.Video = $"{serverUrl}{question.Video.Replace("\\", "/")}";
            }
            }

            return Ok(questionDTO);
        }
        //////////////////////////////////////////////////////////////////////////////

        [HttpGet("ByTest/{id}")]
        [Authorize_Endpoint_(
       allowedTypes: new[] { "octa", "employee","parent" },
       pages: new[] { "Admission Test" }
       )]
        public async Task<IActionResult> GetAsyncbyId(int id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<Question> questions = await Unit_Of_Work.question_Repository.Select_All_With_IncludesById<Question>(
                    b => b.IsDeleted != true &&b.TestID==id,
                    query => query.Include(emp => emp.QuestionType),
                    query => query.Include(emp => emp.mCQQuestionOption),
                    query => query.Include(emp => emp.test),
                    query => query.Include(emp => emp.MCQQuestionOptions)
                    );

            if (questions == null)
            {
                return NotFound();
            }

            List<questionGetDTO> questionDTO = mapper.Map<List<questionGetDTO>>(questions);
            foreach (var question in questionDTO)
            {

                string serverUrl = $"{Request.Scheme}://{Request.Host}/";

                if (!string.IsNullOrEmpty(question.Image))
                {
                    question.Image = $"{serverUrl}{question.Image.Replace("\\", "/")}";
                }
                if (!string.IsNullOrEmpty(question.Video))
                {
                    question.Video = $"{serverUrl}{question.Video.Replace("\\", "/")}";
                }
            }

            return Ok(questionDTO);
        }
        //////////////////////////////////////////////////////////////////////////////

        [HttpGet("ByTestGroupBy/{id}")]
        [Authorize_Endpoint_(
          allowedTypes: new[] { "octa", "employee" ,"parent"},
          pages: new[] { "Admission Test" }
      )]
        public async Task<IActionResult> GetAsyncbyTestId(int id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<Question> questions = await Unit_Of_Work.question_Repository.Select_All_With_IncludesById<Question>(
                b => b.IsDeleted != true && b.TestID == id,
                query => query.Include(emp => emp.QuestionType)
                              .Include(emp => emp.mCQQuestionOption)
                              .Include(emp => emp.test)
                              .Include(emp => emp.MCQQuestionOptions)
            );

            Test test =Unit_Of_Work.test_Repository.First_Or_Default(t=>t.ID==id);
            if (questions == null || !questions.Any())
            {
                return NotFound();
            }

            // Map questions to DTO
            List<questionGetDTO> questionDTO = mapper.Map<List<questionGetDTO>>(questions);
            foreach (var question in questionDTO)
            {

                string serverUrl = $"{Request.Scheme}://{Request.Host}/";

                if (!string.IsNullOrEmpty(question.Image))
                {
                    question.Image = $"{serverUrl}{question.Image.Replace("\\", "/")}";
                }
                if (!string.IsNullOrEmpty(question.Video))
                {
                    question.Video = $"{serverUrl}{question.Video.Replace("\\", "/")}";
                }
            }
            // Group by QuestionType
            var groupedByQuestionType = questionDTO
                .GroupBy(q => new { q.QuestionTypeID, q.QuestionTypeName })
                .Select(group => new
                {
                    QuestionTypeID = group.Key.QuestionTypeID,
                    QuestionTypeName = group.Key.QuestionTypeName,
                    Questions = group.ToList() // All questions in this group
                })
                .ToList();

            var response = new
            {
                TestName = test.Title,
                groupedByQuestionType = groupedByQuestionType
            };
            return Ok(response);
        }

        //////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
    allowedTypes: new[] { "octa", "employee" },
    pages: new[] { "Admission Test" }
)]
        public async Task<IActionResult> Add([FromForm] QuestionAddDTO newQuestion)
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

            if (newQuestion.QuestionTypeID != 3)
            {
                if (newQuestion.options.Count == 0)
                {
                    return BadRequest("options in msq question is required");
                }
                if (newQuestion.CorrectAnswerName == "")
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

            if (newQuestion.QuestionTypeID != 3)
            {
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
                    if(newQuestion.CorrectAnswerName == item)
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
            }

            var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/Questions");

            if (newQuestion.ImageFile != null || newQuestion.VideoFile != null)
            {
                var questionFolder = Path.Combine(baseFolder, question.ID.ToString());

                if (!Directory.Exists(questionFolder))
                {
                    Directory.CreateDirectory(questionFolder);
                }

                if (newQuestion.ImageFile != null)
                {
                    var imagePath = Path.Combine(questionFolder, newQuestion.ImageFile.FileName);

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        newQuestion.ImageFile.CopyTo(stream);
                    }
                    question.Image = Path.Combine("Uploads", "Questions", question.ID.ToString(), newQuestion.ImageFile.FileName); 

                }

                if (newQuestion.VideoFile != null)
                {
                    var videoPath = Path.Combine(questionFolder, newQuestion.VideoFile.FileName);

                    using (var stream = new FileStream(videoPath, FileMode.Create))
                    {
                        await newQuestion.VideoFile.CopyToAsync(stream);
                    }
                    question.Video = Path.Combine("Uploads", "Questions", question.ID.ToString(), newQuestion.VideoFile.FileName);
                }
            }

            Unit_Of_Work.question_Repository.Update(question);
            await Unit_Of_Work.SaveChangesAsync();

            return Ok(newQuestion);
        }

        //////////////////////////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
        allowedTypes: new[] { "octa", "employee" },
         allowEdit: 1,
         pages: new[] { "Admission Test" }
       )]
        public async Task<IActionResult> Edit([FromForm] QuestionEditDTO newQuestion)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
            var userRoleClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            long.TryParse(userRoleClaim, out long roleId);

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (newQuestion == null)
            {
                return BadRequest("Building cannot be null");
            }

            if (newQuestion == null)
            {
                return BadRequest("test cannot be null");
            }
            Question question = Unit_Of_Work.question_Repository.First_Or_Default(q => q.ID == newQuestion.ID && q.IsDeleted != true);
            if (question == null)
            {
                return NotFound("there is no question with this id");
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

            if (newQuestion.QuestionTypeID != 3)
            {
                if (newQuestion.options.Count == 0)
                {
                    return BadRequest("options in msq question is required");
                }
                if (newQuestion.correctAnswerName == "")
                {
                    return BadRequest("CorrectAnswer in msq question is required");
                }
            }
             
            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Admission Test", roleId, userId, question);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            mapper.Map(newQuestion, question);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            question.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                question.UpdatedByOctaId = userId;
                if (question.UpdatedByUserId != null)
                {
                    question.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                question.UpdatedByUserId = userId;
                if (question.UpdatedByOctaId != null)
                {
                    question.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.question_Repository.Update(question);
            await Unit_Of_Work.SaveChangesAsync();

            if (newQuestion.QuestionTypeID != 3) { 

                 long corectId = 0;
                List<MCQQuestionOption> Oldoptions = await Unit_Of_Work.mCQQuestionOption_Repository.Select_All_With_IncludesById<MCQQuestionOption>(
                        b => b.IsDeleted != true && b.Question_ID == newQuestion.ID);

                foreach (var i in Oldoptions)
                {
                    await Unit_Of_Work.mCQQuestionOption_Repository.DeleteAsync(i.ID);
                    await Unit_Of_Work.SaveChangesAsync();
                }
                foreach (var item in newQuestion.options)
                {
                    if (item != "")
                    {
                        MCQQuestionOption option = new MCQQuestionOption();
                        option.Question_ID = newQuestion.ID;
                        option.Name = item;
                        option.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                        if (userTypeClaim == "octa")
                        {
                            option.InsertedByOctaId = userId;
                        }
                        else if (userTypeClaim == "employee")
                        {
                            option.InsertedByUserId = userId;
                        }
                        await Unit_Of_Work.mCQQuestionOption_Repository.AddAsync(option);
                        await Unit_Of_Work.SaveChangesAsync();
                        if (item == newQuestion.correctAnswerName)
                        {
                            corectId = option.ID;
                        }
                    }
                }

                if (question.QuestionTypeID == 3)
                {
                    question.CorrectAnswerID = null;
                    question.mCQQuestionOption = null;
                }
                else
                {
                    question.CorrectAnswerID = corectId;

                }
            }
            var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/Questions");

            if (newQuestion.ImageFile != null || newQuestion.VideoFile != null)
            {
                var questionFolder = Path.Combine(baseFolder, question.ID.ToString());
                if (Directory.Exists(questionFolder))
                {
                    var files = Directory.GetFiles(questionFolder);

                    foreach (var file in files)
                    {
                        System.IO.File.Delete(file);
                    }
                }

                if (!Directory.Exists(questionFolder))
                {
                    Directory.CreateDirectory(questionFolder);
                }

                if (newQuestion.ImageFile != null)
                {
                    var imagePath = Path.Combine(questionFolder, newQuestion.ImageFile.FileName);

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        newQuestion.ImageFile.CopyTo(stream);
                    }
                    question.Image = Path.Combine("Uploads", "Questions", question.ID.ToString(), newQuestion.ImageFile.FileName);
                }

                // Save VideoFile if provided
                if (newQuestion.VideoFile != null)
                {
                    var videoPath = Path.Combine(questionFolder, newQuestion.VideoFile.FileName);

                    using (var stream = new FileStream(videoPath, FileMode.Create))
                    {
                        newQuestion.VideoFile.CopyTo(stream);
                    }
                    question.Video = Path.Combine("Uploads", "Questions", question.ID.ToString(), newQuestion.VideoFile.FileName);
                }
            }

            Unit_Of_Work.question_Repository.Update(question);
            await Unit_Of_Work.SaveChangesAsync();
            return Ok();
        }
        //////////////////////////////////////////////////////////////////////////////

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
        allowedTypes: new[] { "octa", "employee" },
        allowDelete: 1,
       pages: new[] { "Admission Test" }
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

            Question question = Unit_Of_Work.question_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);


            if (question == null)
            {
                return NotFound();
            } 

            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "Admission Test", roleId, userId, question);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            question.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            question.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                question.DeletedByOctaId = userId;
                if (question.DeletedByUserId != null)
                {
                    question.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                question.DeletedByUserId = userId;
                if (question.DeletedByOctaId != null)
                {
                    question.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.question_Repository.Update(question);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
