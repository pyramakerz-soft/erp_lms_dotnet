using AutoMapper;
using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Migrations.Domains;
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
    public class QuestionBankController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;

        public QuestionBankController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
        }

        ///////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" }
            //,
            //pages: new[] { "Template" }
        )]
        public async Task<IActionResult> Get()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<LMS_CMS_DAL.Models.Domains.LMS.QuestionBank> Questions;

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            Questions =await Unit_Of_Work.questionBank_Repository.Select_All_With_IncludesById<LMS_CMS_DAL.Models.Domains.LMS.QuestionBank>(
                    f => f.IsDeleted != true,
                    query => query.Include(emp => emp.BloomLevel),
                    query => query.Include(emp => emp.DokLevel),
                    query => query.Include(emp => emp.QuestionType),
                    query => query.Include(emp => emp.QuestionBankOption),
                    query => query.Include(emp => emp.Lesson.Subject),
                    query => query.Include(emp => emp.Lesson)
                    );

            if (Questions == null || Questions.Count == 0)
            {
                return NotFound();
            }

            List<QuestionBankGetDTO> Dto = mapper.Map<List<QuestionBankGetDTO>>(Questions);

            return Ok(Dto);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("{id}")]
        //[Authorize_Endpoint_(
        //    allowedTypes: new[] { "octa", "employee" },
        //    pages: new[] { "Lesson Resource" }
        //)]
        public async Task<IActionResult> GetById(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            LMS_CMS_DAL.Models.Domains.LMS.QuestionBank Questions;

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            Questions = await Unit_Of_Work.questionBank_Repository.FindByIncludesAsync(
                    f => f.IsDeleted != true&&f.ID==id,
                    query => query.Include(emp => emp.BloomLevel),
                    query => query.Include(emp => emp.DokLevel),
                    query => query.Include(emp => emp.QuestionType),
                    query => query.Include(emp => emp.QuestionBankOption),
                    query => query.Include(emp => emp.Lesson.Subject),
                    query => query.Include(emp => emp.Lesson)
                    );

            if (Questions == null )
            {
                return NotFound();
            }

            QuestionBankGetDTO Dto = mapper.Map<QuestionBankGetDTO>(Questions);

            return Ok(Dto);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        //[Authorize_Endpoint_(
        //    allowedTypes: new[] { "octa", "employee" },
        //    pages: new[] { "Lesson Resource" }
        //)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Add([FromForm] LMS_CMS_BL.DTO.LMS.QuestionBankAddDTO NewData)

        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            //////////////////////////////////////////////////// Validation ////////////////////////////////////////////////
            
            if(NewData.DifficultyLevel>6 || NewData.DifficultyLevel < 1)
            {
                return BadRequest("DifficultyLevel Should be from 1 to 6");
            }

            if (NewData.LessonID != null)
            {
                Lesson lesson =Unit_Of_Work.lesson_Repository.First_Or_Default(l=>l.ID== NewData.LessonID);
                if (lesson == null) 
                { 
                  return BadRequest("this Lesson doesn`t exist");
                }
            }
            else
            {
                return BadRequest("this Lesson doesn`t exist");
            }

            if (NewData.BloomLevelID != null)
            {
                BloomLevel bloomLevel = Unit_Of_Work.bloomLevel_Repository.First_Or_Default(l => l.ID == NewData.BloomLevelID);
                if (bloomLevel == null)
                {
                    return BadRequest("this bloomLevel doesn`t exist");
                }
            }
            else
            {
                return BadRequest("this bloomLevel doesn`t exist");
            }

            if (NewData.DokLevelID != null)
            {
                DokLevel dokLevel = Unit_Of_Work.dokLevel_Repository.First_Or_Default(l => l.ID == NewData.DokLevelID);
                if (dokLevel == null)
                {
                    return BadRequest("this dokLevel doesn`t exist");
                }
            }
            else
            {
                return BadRequest("this dokLevel doesn`t exist");
            }

            if (NewData.QuestionTypeID != null)
            {
                LMS_CMS_DAL.Models.Domains.LMS.QuestionBankType questionBankType = Unit_Of_Work.questionBankType_Repository.First_Or_Default(l => l.ID == NewData.QuestionTypeID);
                if (questionBankType == null)
                {
                    return BadRequest("this questionBankType doesn`t exist");
                }
            }
            else
            {
                return BadRequest("this questionBankType doesn`t exist");
            }

            if (NewData.QuestionBankTagsDTO != null && NewData.QuestionBankTagsDTO.Count > 0)
            {
                foreach (var tagId in NewData.QuestionBankTagsDTO)
                {
                    if (tagId != 0)
                    {
                        var tag = Unit_Of_Work.tag_Repository.First_Or_Default(l => l.ID == tagId);
                        if (tag == null)
                        {
                            return BadRequest($"Tag with ID {tagId} does not exist.");
                        }
                    }
                }
            }
            ////////// Validation For True False Question 

            if (NewData.QuestionTypeID == 1)
            {
                if (NewData.CorrectAnswerName != "true" && NewData.CorrectAnswerName != "false")
                {
                    return BadRequest("Correct Answer should be true or false");
                }
            }

            ////////// Validation For Mcq Question 

            if (NewData.QuestionTypeID == 2)
            {
                if (string.IsNullOrWhiteSpace(NewData.CorrectAnswerName))
                {
                    return BadRequest("Correct Answer is required.");
                }

                if (NewData.QuestionBankOptionsDTO.Count == 0)
                {
                    return BadRequest("Question Bank Options Is Requiered");
                }
            }

            ////////// Validation For Fill in blank and Order - Sequencing Question 

            if (NewData.QuestionTypeID == 3 || NewData.QuestionTypeID == 5)
            {

                if (NewData.QuestionBankOptionsDTO.Count == 0)
                {
                    return BadRequest("Question Bank Options Is Requiered");
                }
            }

            ////////// Validation For Drag & Drop Question 

            if (NewData.QuestionTypeID == 4)
            {
                if (NewData.SubBankQuestionsDTO.Count == 0)
                {
                    return BadRequest("SubBankQuestions Is Requiered");
                }
            }

            //////////////////////////////////////////////////// Create ////////////////////////////////////////////////

            //////////  Create For True False and Essay Question

            LMS_CMS_DAL.Models.Domains.LMS.QuestionBank questionBank = mapper.Map<LMS_CMS_DAL.Models.Domains.LMS.QuestionBank>(NewData);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            questionBank.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                questionBank.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                questionBank.InsertedByUserId = userId;
            }

            Unit_Of_Work.questionBank_Repository.Add(questionBank);
            Unit_Of_Work.SaveChanges();

            //////////  Create For Tags

            if (NewData.QuestionBankTagsDTO != null && NewData.QuestionBankTagsDTO.Count > 0)
            {
                List<QuestionBankTags> questionBankTags = new List<QuestionBankTags>();
                foreach (int tagId in NewData.QuestionBankTagsDTO)
                {
                    if (tagId != 0)
                    {
                        QuestionBankTags tagObject = new QuestionBankTags
                        {
                            QuestionBankID = questionBank.ID,
                            TagID = tagId,
                            InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone),
                            InsertedByOctaId = userTypeClaim == "octa" ? userId : (int?)null,
                            InsertedByUserId = userTypeClaim == "employee" ? userId : (int?)null
                        };
                        questionBankTags.Add(tagObject);
                    }
                }
                if (questionBankTags.Any())
                {
                    Unit_Of_Work.questionBankTags_Repository.AddRange(questionBankTags);
                    Unit_Of_Work.SaveChanges();
                }
            }

            //////////  Create For Mcq ,Order - Sequencing ,Fill in blank Question

            if (NewData.QuestionBankOptionsDTO != null && NewData.QuestionBankOptionsDTO.Count != 0 && (NewData.QuestionTypeID == 2 || NewData.QuestionTypeID == 3 || NewData.QuestionTypeID == 5))
            {
                List<QuestionBankOption> options = new List<QuestionBankOption>();
                long correctOption = 0;
                foreach (var item in NewData.QuestionBankOptionsDTO)
                {
                    QuestionBankOption option = new QuestionBankOption
                    {
                        QuestionBankID = questionBank.ID,
                        Order = item.Order,
                        Option = item.Option,
                        InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone),
                        InsertedByOctaId = userTypeClaim == "octa" ? userId : (int?)null,
                        InsertedByUserId = userTypeClaim == "employee" ? userId : (int?)null
                    };
                    options.Add(option);
                }
                Unit_Of_Work.questionBankOption_Repository.AddRange(options);
                Unit_Of_Work.SaveChanges();
                var correctOptionEntity = Unit_Of_Work.questionBankOption_Repository
                    .First_Or_Default(o => o.QuestionBankID == questionBank.ID && o.Option == NewData.CorrectAnswerName);
                if (correctOptionEntity != null)
                {
                    questionBank.CorrectAnswerID = correctOptionEntity.ID;
                    Unit_Of_Work.questionBank_Repository.Update(questionBank);
                    Unit_Of_Work.SaveChanges();
                }

            }

            //////////  Create For Drag & Drop Question

            if (NewData.SubBankQuestionsDTO.Count != 0 && NewData.QuestionTypeID==4)
            {
                List<SubBankQuestion> subQuestions = new List<SubBankQuestion>();
                List<DragAndDropAnswer> dragAnswers = new List<DragAndDropAnswer>();

                foreach (SubBankQuestionAddDTO item in NewData.SubBankQuestionsDTO)
                {
                    SubBankQuestion subQuestion = new SubBankQuestion
                    {
                        QuestionBankID = questionBank.ID,
                        Description = item.Description,
                        InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone),
                        InsertedByOctaId = userTypeClaim == "octa" ? userId : (int?)null,
                        InsertedByUserId = userTypeClaim == "employee" ? userId : (int?)null
                    };
                    subQuestions.Add(subQuestion);
                    DragAndDropAnswer answer = new DragAndDropAnswer
                    {
                        Answer = item.Answer,
                        SubBankQuestion = subQuestion, // navigation property
                        InsertedAt = subQuestion.InsertedAt,
                        InsertedByOctaId = subQuestion.InsertedByOctaId,
                        InsertedByUserId = subQuestion.InsertedByUserId
                    };

                    dragAnswers.Add(answer);
                }
                Unit_Of_Work.subBankQuestion_Repository.AddRange(subQuestions);
                Unit_Of_Work.dragAndDropAnswer_Repository.AddRange(dragAnswers);
                Unit_Of_Work.SaveChanges();
            }

            ///////////// image Create


            var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/QuestonBank");
            var medalFolder = Path.Combine(baseFolder, questionBank.ID.ToString());
            if (!Directory.Exists(medalFolder))
            {
                Directory.CreateDirectory(medalFolder);
            }

            if (NewData.ImageForm != null && NewData.ImageForm.Length > 0)
            {
                var fileName = Path.GetFileName(NewData.ImageForm.FileName);
                var filePath = Path.Combine(medalFolder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await NewData.ImageForm.CopyToAsync(stream);
                }
                //medal.ImageLink = Path.Combine("Uploads", "Medal", medal.ID.ToString(), fileName);
                questionBank.Image = $"{Request.Scheme}://{Request.Host}/Uploads/QuestonBank/{questionBank.ID.ToString()}/{fileName}";

            }

            Unit_Of_Work.questionBank_Repository.Update(questionBank);
            Unit_Of_Work.SaveChanges();

            return Ok();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
        allowedTypes: new[] { "octa", "employee" },
        allowEdit: 1
    //   ,
    //pages: new[] { "" }
    )]
        public async Task<IActionResult> Edit([FromForm] QuestionBankEditDTO NewData)
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

            if (NewData == null)
            {
                return BadRequest("Medal cannot be null");
            }

            LMS_CMS_DAL.Models.Domains.LMS.QuestionBank questionBank = Unit_Of_Work.questionBank_Repository.First_Or_Default(q => q.ID == NewData.ID && q.IsDeleted!=true);
            if (questionBank == null)
                return NotFound("Question bank not found.");

            //if (userTypeClaim == "employee")
            //{
            //    IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Subject", roleId, userId, medal);
            //    if (accessCheck != null)
            //    {
            //        return accessCheck;
            //    }
            //}
            //////////////////////////////////////////////////// Validation ////////////////////////////////////////////////

            if (NewData.DifficultyLevel > 6 || NewData.DifficultyLevel < 1)
            {
                return BadRequest("DifficultyLevel Should be from 1 to 6");
            }

            if (NewData.LessonID != null)
            {
                Lesson lesson = Unit_Of_Work.lesson_Repository.First_Or_Default(l => l.ID == NewData.LessonID);
                if (lesson == null)
                {
                    return BadRequest("this Lesson doesn`t exist");
                }
            }
            else
            {
                return BadRequest("this Lesson doesn`t exist");
            }

            if (NewData.BloomLevelID != null)
            {
                BloomLevel bloomLevel = Unit_Of_Work.bloomLevel_Repository.First_Or_Default(l => l.ID == NewData.BloomLevelID);
                if (bloomLevel == null)
                {
                    return BadRequest("this bloomLevel doesn`t exist");
                }
            }
            else
            {
                return BadRequest("this bloomLevel doesn`t exist");
            }

            if (NewData.DokLevelID != null)
            {
                DokLevel dokLevel = Unit_Of_Work.dokLevel_Repository.First_Or_Default(l => l.ID == NewData.DokLevelID);
                if (dokLevel == null)
                {
                    return BadRequest("this dokLevel doesn`t exist");
                }
            }
            else
            {
                return BadRequest("this dokLevel doesn`t exist");
            }

            if (NewData.QuestionTypeID != null)
            {
                LMS_CMS_DAL.Models.Domains.LMS.QuestionBankType questionBankType = Unit_Of_Work.questionBankType_Repository.First_Or_Default(l => l.ID == NewData.QuestionTypeID);
                if (questionBankType == null)
                {
                    return BadRequest("this questionBankType doesn`t exist");
                }
            }
            else
            {
                return BadRequest("this questionBankType doesn`t exist");
            }

            if (NewData.QuestionBankTagsDTO != null && NewData.QuestionBankTagsDTO.Count > 0)
            {
                foreach (var tagId in NewData.QuestionBankTagsDTO)
                {
                    if (tagId != 0)
                    {
                        var tag = Unit_Of_Work.tag_Repository.First_Or_Default(l => l.ID == tagId);
                        if (tag == null)
                        {
                            return BadRequest($"Tag with ID {tagId} does not exist.");
                        }
                    }
                }
            }
            ////////// Validation For True False Question 

            if (NewData.QuestionTypeID == 1)
            {
                if (NewData.CorrectAnswerName != "true" && NewData.CorrectAnswerName != "false")
                {
                    return BadRequest("Correct Answer should be true or false");
                }
            }

            ////////// Validation For Mcq Question 

            if (NewData.QuestionTypeID == 2)
            {
                if (string.IsNullOrWhiteSpace(NewData.CorrectAnswerName))
                {
                    return BadRequest("Correct Answer is required.");
                }

                if (NewData.QuestionBankOptionsDTO.Count == 0)
                {
                    return BadRequest("Question Bank Options Is Requiered");
                }
            }

            ////////// Validation For Fill in blank and Order - Sequencing Question 

            if (NewData.QuestionTypeID == 3 || NewData.QuestionTypeID == 5)
            {

                if (NewData.QuestionBankOptionsDTO.Count == 0)
                {
                    return BadRequest("Question Bank Options Is Requiered");
                }
            }

            ////////// Validation For Drag & Drop Question 

            if (NewData.QuestionTypeID == 4)
            {
                if (NewData.SubBankQuestionsDTO.Count == 0)
                {
                    return BadRequest("SubBankQuestions Is Requiered");
                }
            }
            mapper.Map(NewData, questionBank);

            if (NewData.ImageForm != null)
            {
                var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/QuestonBank", NewData.ID.ToString());
                if (!Directory.Exists(baseFolder)) Directory.CreateDirectory(baseFolder);

                var fileName = Path.GetFileName(NewData.ImageForm.FileName);
                var filePath = Path.Combine(baseFolder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await NewData.ImageForm.CopyToAsync(stream);

                questionBank.Image = $"{Request.Scheme}://{Request.Host}/Uploads/QuestonBank/{NewData.ID.ToString()}/{fileName}";
            }

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            questionBank.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                questionBank.UpdatedByOctaId = userId;
                if (questionBank.UpdatedByUserId != null)
                {
                    questionBank.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                questionBank.UpdatedByUserId = userId;
                if (questionBank.UpdatedByOctaId != null)
                {
                    questionBank.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.questionBank_Repository.Update(questionBank);
            Unit_Of_Work.SaveChanges();

            ///////////////////////////// Delete Old Data 
            Unit_Of_Work.questionBankTags_Repository.RemoveRange(
                Unit_Of_Work.questionBankTags_Repository.FindBy(t => t.QuestionBankID == NewData.ID));

            Unit_Of_Work.questionBankOption_Repository.RemoveRange(
                Unit_Of_Work.questionBankOption_Repository.FindBy(o => o.QuestionBankID == NewData.ID));

            // Step 1: Get all sub-question IDs for the given QuestionBank
            var subQuestionIds = Unit_Of_Work.subBankQuestion_Repository
                .FindBy(sbq => sbq.QuestionBankID == NewData.ID)
                .Select(sbq => sbq.ID)
                .ToList();

            // Step 2: Delete all DragAndDropAnswers for those sub-question IDs
            var dragAnswers = Unit_Of_Work.dragAndDropAnswer_Repository
                .FindBy(ans => subQuestionIds.Contains(ans.SubBankQuestionID))
                .ToList();

            Unit_Of_Work.dragAndDropAnswer_Repository.RemoveRange(dragAnswers);

            // Step 3: Delete the sub-questions themselves
            var subQuestions = Unit_Of_Work.subBankQuestion_Repository
                .FindBy(sbq => sbq.QuestionBankID == NewData.ID)
                .ToList();

            Unit_Of_Work.subBankQuestion_Repository.RemoveRange(subQuestions);

            // Save changes (if needed at this step)
            Unit_Of_Work.SaveChanges();

            /////////////////////////////////////////////////////////////////////////  ReCreate 

            //////////  Create For Tags

            if (NewData.QuestionBankTagsDTO != null && NewData.QuestionBankTagsDTO.Count > 0)
            {
                List<QuestionBankTags> questionBankTags = new List<QuestionBankTags>();
                foreach (int tagId in NewData.QuestionBankTagsDTO)
                {
                    if (tagId != 0)
                    {
                        QuestionBankTags tagObject = new QuestionBankTags
                        {
                            QuestionBankID = questionBank.ID,
                            TagID = tagId,
                            InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone),
                            InsertedByOctaId = userTypeClaim == "octa" ? userId : (int?)null,
                            InsertedByUserId = userTypeClaim == "employee" ? userId : (int?)null
                        };
                        questionBankTags.Add(tagObject);
                    }
                }
                if (questionBankTags.Any())
                {
                    Unit_Of_Work.questionBankTags_Repository.AddRange(questionBankTags);
                    Unit_Of_Work.SaveChanges();
                }
            }

            //////////  Create For Mcq ,Order - Sequencing ,Fill in blank Question

            if (NewData.QuestionBankOptionsDTO != null && NewData.QuestionBankOptionsDTO.Count != 0 && (NewData.QuestionTypeID == 2 || NewData.QuestionTypeID == 3 || NewData.QuestionTypeID == 5))
            {
                List<QuestionBankOption> options = new List<QuestionBankOption>();
                long correctOption = 0;
                foreach (var item in NewData.QuestionBankOptionsDTO)
                {
                    QuestionBankOption option = new QuestionBankOption
                    {
                        QuestionBankID = questionBank.ID,
                        Order = item.Order,
                        Option = item.Option,
                        InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone),
                        InsertedByOctaId = userTypeClaim == "octa" ? userId : (int?)null,
                        InsertedByUserId = userTypeClaim == "employee" ? userId : (int?)null
                    };
                    options.Add(option);
                }
                Unit_Of_Work.questionBankOption_Repository.AddRange(options);
                Unit_Of_Work.SaveChanges();
                var correctOptionEntity = Unit_Of_Work.questionBankOption_Repository
                    .First_Or_Default(o => o.QuestionBankID == questionBank.ID && o.Option == NewData.CorrectAnswerName);
                if (correctOptionEntity != null)
                {
                    questionBank.CorrectAnswerID = correctOptionEntity.ID;
                    Unit_Of_Work.questionBank_Repository.Update(questionBank);
                    Unit_Of_Work.SaveChanges();
                }

            }

            //////////  Create For Drag & Drop Question

            if (NewData.SubBankQuestionsDTO.Count != 0 && NewData.QuestionTypeID == 4)
            {
                List<SubBankQuestion> newsubQuestions = new List<SubBankQuestion>();
                List<DragAndDropAnswer> newdragAnswers = new List<DragAndDropAnswer>();

                foreach (SubBankQuestionAddDTO item in NewData.SubBankQuestionsDTO)
                {
                    SubBankQuestion subQuestion = new SubBankQuestion
                    {
                        QuestionBankID = questionBank.ID,
                        Description = item.Description,
                        InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone),
                        InsertedByOctaId = userTypeClaim == "octa" ? userId : (int?)null,
                        InsertedByUserId = userTypeClaim == "employee" ? userId : (int?)null
                    };
                    newsubQuestions.Add(subQuestion);
                    DragAndDropAnswer answer = new DragAndDropAnswer
                    {
                        Answer = item.Answer,
                        SubBankQuestion = subQuestion, // navigation property
                        InsertedAt = subQuestion.InsertedAt,
                        InsertedByOctaId = subQuestion.InsertedByOctaId,
                        InsertedByUserId = subQuestion.InsertedByUserId
                    };

                    newdragAnswers.Add(answer);
                }
                Unit_Of_Work.subBankQuestion_Repository.AddRange(newsubQuestions);
                Unit_Of_Work.dragAndDropAnswer_Repository.AddRange(newdragAnswers);
                Unit_Of_Work.SaveChanges();
            }

            return Ok(NewData);
        }

    }
}
