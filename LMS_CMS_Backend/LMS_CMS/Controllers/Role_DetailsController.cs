﻿using AutoMapper;
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
    public class Role_DetailsController : ControllerBase
    {
        private UOW Unit_Of_Work;
        IMapper mapper;


        public Role_DetailsController(UOW Unit_Of_Work, IMapper mapper)
        {
            this.Unit_Of_Work = Unit_Of_Work;
            this.mapper = mapper;
        }

        [HttpGet("Get_With_RoleID_Group_By/{roleId}")]
        public async Task<IActionResult> Get_With_RoleID_Group_By(long roleId)
        {
            var roleDetailsList = await Unit_Of_Work.role_Detailes_Repository.Database().Role_Detailes
                .Where(rd => rd.Role_ID == roleId)
                .Include(rd => rd.Page)  // Include the related Page entity
                .ThenInclude(p => p.ChildPages) // Include the child pages
                .ToListAsync();

            if (roleDetailsList == null || !roleDetailsList.Any())
            {
                return NotFound("No pages found for the specified role.");
            }

            // Convert roleDetailsList to a dictionary for fast lookup by Page_ID
            var roleDetails = roleDetailsList
                .GroupBy(rd => rd.Page_ID)
                .ToDictionary(g => g.Key, g => g.First());

            // Group role details by parent page
            var parentPages = roleDetailsList
                .Where(rd => rd.Page.Page_ID == null)  // Only root-level pages
                .GroupBy(rd => rd.Page.ID) // Group by parent page ID to avoid duplication
                .Select(group => new Role_Details_GetDTO
                {
                    ID = group.Key,
                    en_name = group.First().Page.en_name,
                    ar_name = group.First().Page.ar_name,
                    Allow_Edit = group.First().Allow_Edit,
                    Allow_Delete = group.First().Allow_Delete,
                    Allow_Edit_For_Others = group.First().Allow_Edit_For_Others,
                    Allow_Delete_For_Others = group.First().Allow_Delete_For_Others,
                    Children = GetChildPagesRecursive(group.First().Page, roleDetails) // Use the optimized recursive method
                })
                .ToList();

            return Ok(parentPages);
        }

        private List<Role_Details_GetDTO> GetChildPagesRecursive(Page parentPage, Dictionary<long, Role_Detailes> roleDetails)
        {
            // Recursively fetch children and their nested children
            return parentPage.ChildPages
                .Where(child => roleDetails.ContainsKey(child.ID)) // Ensure child exists in Role_Details
                .Select(child => new Role_Details_GetDTO
                {
                    ID = child.ID,
                    en_name = child.en_name,
                    ar_name = child.ar_name,
                    Allow_Edit = roleDetails[child.ID].Allow_Edit,
                    Allow_Delete = roleDetails[child.ID].Allow_Delete,
                    Children = GetChildPagesRecursive(child, roleDetails) // Recursively get children
                })
                .ToList();
        }
    }
}