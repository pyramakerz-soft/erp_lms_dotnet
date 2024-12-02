using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private UOW Unit_Of_Work;
        IMapper mapper;

        public StudentController(UOW Unit_Of_Work, IMapper mapper)
        {
            this.Unit_Of_Work = Unit_Of_Work;
            this.mapper = mapper;
        }


        [HttpGet("{Id}")]
        public IActionResult GetByID(long Id)
        {
            Student student = Unit_Of_Work.student_Repository.Select_By_Id(Id);

            if (student == null || student.IsDeleted == true)
            {
                return NotFound("No employee found");
            }

            StudentGetDTO StudentDTO = mapper.Map<StudentGetDTO>(student);

            return Ok(StudentDTO);
        }
    }
}
