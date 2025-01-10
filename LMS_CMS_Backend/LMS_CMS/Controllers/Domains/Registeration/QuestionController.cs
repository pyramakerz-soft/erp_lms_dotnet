using AutoMapper;
using LMS_CMS_BL.DTO.Registration;
using LMS_CMS_BL.UOW;
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
    }
}
