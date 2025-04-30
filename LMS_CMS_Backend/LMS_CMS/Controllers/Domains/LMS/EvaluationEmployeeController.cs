using AutoMapper;
using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
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
    public class EvaluationEmployeeController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper; 
        private readonly CheckPageAccessService _checkPageAccessService; 

        public EvaluationEmployeeController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {

            _dbContextFactory = dbContextFactory;
            this.mapper = mapper; 
            _checkPageAccessService = checkPageAccessService; 
        }

        [HttpPost]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" }
        )]
        public IActionResult Add(EvaluationEmployeeAddDTO newEval)
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

            if (newEval == null)
            {
                return BadRequest("Employee Evaluation cannot be null");
            }

            if(newEval.EvaluatorID == newEval.EvaluatedID)
            {
                return BadRequest("You Can't Evaluate Yourself");
            }

            Employee evaluator = Unit_Of_Work.employee_Repository.First_Or_Default(d => d.IsDeleted != true && d.ID == newEval.EvaluatorID);
            if(evaluator == null)
            {
                return NotFound("No Evaluator Employee with this ID");
            }
             
            Employee evaluated = Unit_Of_Work.employee_Repository.First_Or_Default(d => d.IsDeleted != true && d.ID == newEval.EvaluatedID);
            if(evaluated == null)
            {
                return NotFound("No Evaluated Employee with this ID");
            }
             
            Classroom classroom = Unit_Of_Work.classroom_Repository.First_Or_Default(d => d.IsDeleted != true && d.ID == newEval.ClassroomID);
            if(classroom == null)
            {
                return NotFound("No Class with this ID");
            }
             
            EvaluationBookCorrection evaluationBookCorrection = Unit_Of_Work.evaluationBookCorrection_Repository.First_Or_Default(d => d.IsDeleted != true && d.ID == newEval.EvaluationBookCorrectionID);
            if(evaluationBookCorrection == null)
            {
                return NotFound("No Evaluation Book Correction with this ID");
            }
             
            EvaluationTemplate evaluationTemplate = Unit_Of_Work.evaluationTemplate_Repository.First_Or_Default(d => d.IsDeleted != true && d.ID == newEval.EvaluationTemplateID);
            if(evaluationTemplate == null)
            {
                return NotFound("No Evaluation Template with this ID");
            }

            EvaluationEmployee evaluationEmployee = mapper.Map<EvaluationEmployee>(newEval);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            evaluationEmployee.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            
            if (userTypeClaim == "octa")
            {
                evaluationEmployee.InsertedByOctaId = userId;
            }
            else if(userTypeClaim == "employee")
            {
                evaluationEmployee.InsertedByUserId = userId;
            }
            
            Unit_Of_Work.evaluationEmployee_Repository.Add(evaluationEmployee);
            Unit_Of_Work.SaveChanges();

            for (int i = 0; i < newEval.EvaluationEmployeeStudentBookCorrectionsList.Count; i++)
            {
                Student student = Unit_Of_Work.student_Repository.First_Or_Default(d => d.IsDeleted != true && d.ID == newEval.EvaluationEmployeeStudentBookCorrectionsList[i].StudentID);
                if (student == null)
                {
                    return NotFound("No Student with this ID");
                }

                EvaluationEmployeeStudentBookCorrection evaluationEmployeeStudentBookCorrection = mapper.Map<EvaluationEmployeeStudentBookCorrection>(newEval.EvaluationEmployeeStudentBookCorrectionsList[i]);
                evaluationEmployeeStudentBookCorrection.EvaluationBookCorrectionID = newEval.EvaluationBookCorrectionID;
                evaluationEmployeeStudentBookCorrection.EvaluationEmployeeID = evaluationEmployee.ID;

                evaluationEmployeeStudentBookCorrection.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);

                if (userTypeClaim == "octa")
                {
                    evaluationEmployeeStudentBookCorrection.InsertedByOctaId = userId;
                }
                else if (userTypeClaim == "employee")
                {
                    evaluationEmployeeStudentBookCorrection.InsertedByUserId = userId;
                }

                Unit_Of_Work.evaluationEmployeeStudentBookCorrection_Repository.Add(evaluationEmployeeStudentBookCorrection);
            }
            Unit_Of_Work.SaveChanges();
            
            for (int i = 0; i < newEval.EvaluationEmployeeQuestionsList.Count; i++)
            {
                EvaluationTemplateGroupQuestion evaluationTemplateGroupQuestion = Unit_Of_Work.evaluationTemplateGroupQuestion_Repository.First_Or_Default(d => d.IsDeleted != true && d.ID == newEval.EvaluationEmployeeQuestionsList[i].EvaluationTemplateGroupQuestionID);
                if (evaluationTemplateGroupQuestion == null)
                {
                    return NotFound("No Evaluation Template Group Question with this ID");
                }

                EvaluationEmployeeQuestion evaluationEmployeeQuestion = mapper.Map<EvaluationEmployeeQuestion>(newEval.EvaluationEmployeeQuestionsList[i]); 
                evaluationEmployeeQuestion.EvaluationEmployeeID = evaluationEmployee.ID;

                evaluationEmployeeQuestion.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);

                if (userTypeClaim == "octa")
                {
                    evaluationEmployeeQuestion.InsertedByOctaId = userId;
                }
                else if (userTypeClaim == "employee")
                {
                    evaluationEmployeeQuestion.InsertedByUserId = userId;
                }

                Unit_Of_Work.evaluationEmployeeQuestion_Repository.Add(evaluationEmployeeQuestion);
            }
            Unit_Of_Work.SaveChanges();


            return Ok(); 
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        [HttpPut("AddFeedback")]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" }
        )]
        public IActionResult AddFeedback(EvaluationEmployeeFeedbackAddDTO feedback)
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

            if (feedback == null)
            {
                return BadRequest("Employee Evaluation cannot be null");
            } 

            EvaluationEmployee evaluationEmployee = Unit_Of_Work.evaluationEmployee_Repository.First_Or_Default(d => d.IsDeleted != true && d.ID == feedback.EvaluationEmployeeID);
            if (evaluationEmployee == null)
            {
                return NotFound("No Evaluation Employee with this ID");
            }

            evaluationEmployee.Feedback = feedback.Feedback;

            Unit_Of_Work.evaluationEmployee_Repository.Update(evaluationEmployee);
            Unit_Of_Work.SaveChanges();

            return Ok();
        }
        
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        [HttpGet("GetEvaluatorEvaluations/{evaluatorID}")]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" }
        )]
        public async Task<IActionResult> GetEvaluatorEvaluationsAsync(long evaluatorID)
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

            if (evaluatorID == null || evaluatorID == 0)
            {
                return BadRequest("Evaluator Id cannot be null");
            } 

            Employee evaluator = Unit_Of_Work.employee_Repository.First_Or_Default(d => d.IsDeleted != true && d.ID == evaluatorID);
            if (evaluator == null)
            {
                return NotFound("No Employee with this ID");
            }
             
            List<EvaluationEmployee> evaluationEmployees = await Unit_Of_Work.evaluationEmployee_Repository.Select_All_With_IncludesById<EvaluationEmployee>(
                    t => t.IsDeleted != true && t.EvaluatorID == evaluatorID,
                    query => query.Include(emp => emp.Evaluated),
                    query => query.Include(emp => emp.Evaluator),
                    query => query.Include(emp => emp.EvaluationTemplate),
                    query => query.Include(emp => emp.Classroom)
                    );

            List<EvaluationEmployeeGetDTO> evaluationEmployeesDTOs = mapper.Map<List<EvaluationEmployeeGetDTO>>(evaluationEmployees);

            return Ok(evaluationEmployeesDTOs);
        }
        
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        [HttpGet("GetEvaluatedEvaluations/{evaluatedID}")]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" }
        )]
        public async Task<IActionResult> GetEvaluatedEvaluationsAsync(long evaluatedID)
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

            if (evaluatedID == null || evaluatedID == 0)
            {
                return BadRequest("Evaluated Id cannot be null");
            } 

            Employee evaluated = Unit_Of_Work.employee_Repository.First_Or_Default(d => d.IsDeleted != true && d.ID == evaluatedID);
            if (evaluated == null)
            {
                return NotFound("No Employee with this ID");
            }
             
            List<EvaluationEmployee> evaluationEmployees = await Unit_Of_Work.evaluationEmployee_Repository.Select_All_With_IncludesById<EvaluationEmployee>(
                    t => t.IsDeleted != true && t.EvaluatedID == evaluatedID,
                    query => query.Include(emp => emp.Evaluated),
                    query => query.Include(emp => emp.Evaluator),
                    query => query.Include(emp => emp.EvaluationTemplate),
                    query => query.Include(emp => emp.Classroom)
                    );

            List<EvaluationEmployeeGetDTO> evaluationEmployeesDTOs = mapper.Map<List<EvaluationEmployeeGetDTO>>(evaluationEmployees);

            return Ok(evaluationEmployeesDTOs);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        [HttpGet("GetEvaluation/{evaluationID}")]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" }
        )]
        public async Task<IActionResult> GetEvaluationById(long evaluationID)
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

            if (evaluationID == null || evaluationID == 0)
            {
                return BadRequest("Evaluation Id cannot be null");
            }

            EvaluationEmployee evaluationEmployee = await Unit_Of_Work.evaluationEmployee_Repository.FindByIncludesAsync(
                d => d.IsDeleted != true && d.ID == evaluationID,
                    query => query.Include(emp => emp.Evaluated),
                    query => query.Include(emp => emp.Evaluator),
                    query => query.Include(emp => emp.EvaluationTemplate),
                    query => query.Include(emp => emp.Classroom)
                );

            if (evaluationEmployee == null)
            {
                return NotFound("No Evaluation with this ID");
            }

            EvaluationEmployeeWithQuestionsGetDTO evaluationEmployeesDTO = mapper.Map<EvaluationEmployeeWithQuestionsGetDTO>(evaluationEmployee);

            List<EvaluationEmployeeStudentBookCorrection> evaluationEmployeeStudentBookCorrections = await Unit_Of_Work.evaluationEmployeeStudentBookCorrection_Repository
                .Select_All_With_IncludesById<EvaluationEmployeeStudentBookCorrection>(
                    t => t.IsDeleted != true && t.EvaluationEmployeeID == evaluationID,
                    query => query.Include(emp => emp.Student)
                    );

            List<EvaluationEmployeeStudentBookCorrectionsGetDTO> evaluationEmployeeStudentBookCorrectionsGetDTOs = mapper.Map<List<EvaluationEmployeeStudentBookCorrectionsGetDTO>>(evaluationEmployeeStudentBookCorrections);

            evaluationEmployeesDTO.EvaluationEmployeeStudentBookCorrections = evaluationEmployeeStudentBookCorrectionsGetDTOs;

            List<EvaluationEmployeeQuestion> evaluationEmployeeQuestions = await Unit_Of_Work.evaluationEmployeeQuestion_Repository
                .Select_All_With_IncludesById<EvaluationEmployeeQuestion>(
                    t => t.IsDeleted != true && t.EvaluationEmployeeID == evaluationID,
                    query => query.Include(q => q.EvaluationTemplateGroupQuestion)
                                  .ThenInclude(gq => gq.EvaluationTemplateGroup)
                );

            var groupedQuestions = evaluationEmployeeQuestions
                .GroupBy(q => q.EvaluationTemplateGroupQuestion.EvaluationTemplateGroup)
                .Select(group => new EvaluationEmployeeQuestionGroupGetDTO
                {
                    ID = group.Key.ID,
                    EnglishTitle = group.Key.EnglishTitle,
                    ArabicTitle = group.Key.ArabicTitle,
                    EvaluationEmployeeQuestions = mapper.Map<List<EvaluationEmployeeQuestionGetDTO>>(group.ToList())
                })
                .ToList();

            evaluationEmployeesDTO.EvaluationEmployeeQuestionGroups = groupedQuestions;

            return Ok(evaluationEmployeesDTO);
        }
    }
}
