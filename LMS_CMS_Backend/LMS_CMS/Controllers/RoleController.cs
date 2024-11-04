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
    public class RoleController : ControllerBase
    {
        UOW unitOfWork;
        IMapper mapper;
        public RoleController(UOW unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Role> roles = unitOfWork.role_Repository.Select_All();
            if (roles == null)
            {
                return NotFound();
            }
            List<Role_GetDTO> roleDTOs = mapper.Map<List<Role_GetDTO>>(roles);

            return Ok(roleDTOs);
        }

        [HttpGet("{Id}")]
        public IActionResult Get_By_Id(int Id)
        {
            if (Id == 0)
            {
                return BadRequest("Role ID cannot be null.");
            }

            Role role = unitOfWork.role_Repository.Select_By_Id(Id);
            if (role == null)
            {
                return NotFound();
            }
            else
            {
                Role_GetDTO roleDTO = mapper.Map<Role_GetDTO>(role);
                return Ok(roleDTO);
            }
        }

        [HttpPost]
        public ActionResult Add(Role_AddDTO roleDTO)
        {
            if (roleDTO == null)
            {
                return BadRequest("Role cannot be null.");
            }

            Role role = mapper.Map<Role>(roleDTO);
            unitOfWork.role_Repository.Add(role);
            unitOfWork.SaveChanges();

            return CreatedAtAction(nameof(Get_By_Id), new { id = role.ID }, roleDTO);
        }

        [HttpPut]
        public ActionResult Edit(Role_GetDTO roleDTO)
        {
            if (roleDTO == null)
            {
                return BadRequest("Role cannot be null.");
            }

            Role existingRole = unitOfWork.role_Repository.Select_By_Id(roleDTO.ID);
            if (existingRole == null)
            {
                return NotFound();
            }
            else
            {
                mapper.Map(roleDTO, existingRole);
                unitOfWork.role_Repository.Update(existingRole);
                unitOfWork.SaveChanges();

                return Ok(roleDTO); 
            }
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            if (Id == 0)
            {
                return BadRequest("Role ID cannot be null.");
            }

            Role role = unitOfWork.role_Repository.Select_By_Id(Id);
            if (role == null)
            {
                return NotFound();
            }
            else
            {
                unitOfWork.role_Repository.Delete(Id);
                unitOfWork.SaveChanges();
                return Ok("Role has Successfully been deleted");
            }
        }
    }
}
