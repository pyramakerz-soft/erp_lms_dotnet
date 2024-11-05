using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolRoleController : ControllerBase
    {
        UOW unitOfWork;
        private readonly IMapper _mapper;
        public SchoolRoleController(UOW unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //----------Get Schools ----------//

        [HttpGet]
        public IActionResult Get()
        {
            List<School_Roles> schools = unitOfWork.school_Roles_Repository.Select_All();

            if (schools == null || !schools.Any())
            {
                return NotFound();
            }

            return Ok(schools);
        }

        //----------Get School_roles by School_id----------//
        [HttpGet("{schoolId}")]
        public async Task<IActionResult> GetBySchoolId(int schoolId)
        {
            var schools = await unitOfWork.school_Roles_Repository.Select_All_With_Includesbyid(
                emp => emp.School_Id == schoolId,
                e => e.Role
            );

            if (schools == null || schools.Count == 0)
            {
                return NotFound();
            }

            var schoolRoleDTO = _mapper.Map<List<schoolRoleDTO>>(schools);
            return Ok(schoolRoleDTO);
        }

        //----------add Role By School id ----------//
        [HttpPost]
        public IActionResult addRoleInSchool(AddRoleInSchoolDTO newSchoolRole)
        {
            if (newSchoolRole == null)
            {
                return BadRequest("Role data cannot be null.");
            }

            // Check if the school exists
            var schoolExists = unitOfWork.school_Repository.Select_By_Id(newSchoolRole.School_id) != null;
            if (!schoolExists)
            {
                return BadRequest("The specified School ID does not exist.");
            }

            // Create the role
            Role role = new Role
            {
                Name = newSchoolRole.Role_Name
            };

            unitOfWork.role_Repository.Add(role);
            unitOfWork.SaveChanges();

            // Create the school role association
            School_Roles school_Roles = new School_Roles
            {
                School_Id = newSchoolRole.School_id,
                Role_Id = role.ID
            };

            unitOfWork.school_Roles_Repository.Add(school_Roles);
            unitOfWork.SaveChanges();

            return Ok();
        }




    }
}
