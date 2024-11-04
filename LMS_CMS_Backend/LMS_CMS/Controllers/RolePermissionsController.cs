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
    public class RolePermissionsController : ControllerBase
    {
        UOW unitOfWork;
        public RolePermissionsController(UOW unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    List<Role_Permissions> Role_Detailed_Permissions = unitOfWork.role_Detailed_Permissions_Repository.Select_All();
        //    if (Role_Detailed_Permissions == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(Role_Detailed_Permissions);
        //}

        [HttpPost]
        public ActionResult Add(Role_Permissions_AddDTO RolePermissionsDTO)
        {
            if (RolePermissionsDTO == null)
            {
                return NotFound("Role Permissions cannot be null.");
            }

            Role role = unitOfWork.role_Repository.Select_By_Id(RolePermissionsDTO.Role_ID);
            if (role == null)
            {
                return NotFound("No Role with this ID");
            }

            Master_Permissions master = unitOfWork.master_Permissions_Repository.Select_By_Id(RolePermissionsDTO.Master_ID);
            if (master == null)
            {
                return NotFound("No Master Permission with this ID");
            }

            Detailed_Permissions detailed = unitOfWork.detailed_Permissions_Repository.Select_By_Id(RolePermissionsDTO.Detailed_ID);
            if (detailed == null)
            {
                return NotFound("No Detailed Permission with this ID");
            }

            Master_Detailes_Permissions masterDetailedPermission = unitOfWork.master_Detailes_Permissions_Repository.First_Or_Default(
                md => md.Details_Id == RolePermissionsDTO.Detailed_ID && md.Master_Id == RolePermissionsDTO.Master_ID);
            if (masterDetailedPermission == null)
            {
                return NotFound("No Master Detailed Permission with this ID");
            }

            Role_Permissions rolePermission = new Role_Permissions();
            rolePermission.Master_Detailed_Permissions_ID = masterDetailedPermission.ID;
            rolePermission.Role_ID = RolePermissionsDTO.Role_ID;

            unitOfWork.role_Permissions_Repository.Add(rolePermission);
            unitOfWork.SaveChanges();

            //return CreatedAtAction(nameof(Get_By_Id), new { id = employee.ID }, employeeDTO);
            return Ok(RolePermissionsDTO);
        }

        [HttpPut]
        public ActionResult Edit(Role_Permissions_PutDTO RolePermissionsDTO)
        {
            if (RolePermissionsDTO == null)
            {
                return NotFound("Role Permissions cannot be null.");
            }

            Role_Permissions rolePermissionExisting = unitOfWork.role_Permissions_Repository.Select_By_Id(RolePermissionsDTO.Id);
            if (rolePermissionExisting == null)
            {
                return NotFound("No Role Permission with this ID");
            }

            Role role = unitOfWork.role_Repository.Select_By_Id(RolePermissionsDTO.Role_ID);
            if (role == null)
            {
                return NotFound("No Role with this ID");
            }

            Master_Permissions master = unitOfWork.master_Permissions_Repository.Select_By_Id(RolePermissionsDTO.Master_ID);
            if (master == null)
            {
                return NotFound("No Master Permission with this ID");
            }

            Detailed_Permissions detailed = unitOfWork.detailed_Permissions_Repository.Select_By_Id(RolePermissionsDTO.Detailed_ID);
            if (detailed == null)
            {
                return NotFound("No Detailed Permission with this ID");
            }

            Master_Detailes_Permissions masterDetailedPermission = unitOfWork.master_Detailes_Permissions_Repository.First_Or_Default(
                md => md.Details_Id == RolePermissionsDTO.Detailed_ID && md.Master_Id == RolePermissionsDTO.Master_ID);
            if (masterDetailedPermission == null)
            {
                return NotFound("No Master Detailed Permission with this ID");
            }

            rolePermissionExisting.Master_Detailed_Permissions_ID = masterDetailedPermission.ID;
            rolePermissionExisting.Role_ID = RolePermissionsDTO.Role_ID;

            unitOfWork.role_Permissions_Repository.Update(rolePermissionExisting);
            unitOfWork.SaveChanges();

            //return CreatedAtAction(nameof(Get_By_Id), new { id = employee.ID }, employeeDTO);
            return Ok(rolePermissionExisting);
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            if (Id == 0)
            {
                return BadRequest("Role Permission ID cannot be null.");
            }

            Role_Permissions rolePermissionExisting = unitOfWork.role_Permissions_Repository.Select_By_Id(Id);
            if (rolePermissionExisting == null)
            {
                return NotFound("No Role Permission with this ID");
            }
            else
            {
                unitOfWork.role_Permissions_Repository.Delete(Id);
                unitOfWork.SaveChanges();
                return Ok("Role Permission has Successfully been deleted");
            }
        }
    }
}
