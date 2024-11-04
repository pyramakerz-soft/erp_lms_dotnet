using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        UOW unitOfWork;
        private readonly IMapper _mapper;
        public SchoolController(UOW unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //----------Get Schools ----------//

        [HttpGet]
        public IActionResult Get()
        {
            List<School> schools = unitOfWork.school_Repository.Select_All();

            if (schools == null || !schools.Any())
            {
                return NotFound();
            }

            return Ok(schools);
        }

        //----------Get School by id----------//
        [HttpGet("{schoolId}")]
        public IActionResult GetById(int schoolId)
        {
            School school = unitOfWork.school_Repository.Select_By_Id(schoolId);

            if (school == null)
            {
                return NotFound();
            }

            return Ok(school);
        }

        //----------add School ----------//
        [HttpPost]
        public IActionResult addSchool(SchoolAddDTO newSchool)
        {
            if (newSchool == null) { return BadRequest(); }
            var domainExists = unitOfWork.domain_Repository.Select_By_Id(newSchool.DomainId);
            if (domainExists == null)
            {
                return BadRequest("The specified domain does not exist.");
            }
            School school = _mapper.Map<School>(newSchool);
            unitOfWork.school_Repository.Add(school);
            unitOfWork.SaveChanges();
            return Ok(newSchool);
        }

        ////----------Update School----------//

        [HttpPut]

        public IActionResult EditSchool(SchoolDTO newSchool)
        {
            if (newSchool == null) { BadRequest(); }
            School school = _mapper.Map<School>(newSchool);
            unitOfWork.school_Repository.Update(school);
            unitOfWork.SaveChanges();
            return Ok(newSchool);
        }

        ////----------Delete Domain----------//

        [HttpDelete]

        public IActionResult deletSchool(int id)
        {
            unitOfWork.school_Repository.Delete(id);
            unitOfWork.SaveChanges();
            return Ok();

        }


    }
}
