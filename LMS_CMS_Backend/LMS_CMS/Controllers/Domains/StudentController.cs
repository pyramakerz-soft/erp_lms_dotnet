using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Controllers.Domains
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public StudentController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }


        [HttpGet("{Id}")]
        public IActionResult GetByID(long Id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            Student student = Unit_Of_Work.student_Repository.Select_By_Id(Id);

            if (student == null || student.IsDeleted == true)
            {
                return NotFound("No Student found");
            }

            StudentGetDTO StudentDTO = mapper.Map<StudentGetDTO>(student);

            return Ok(StudentDTO);
        }
    }
}
