﻿using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Octa;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LMS_CMS_PL.Controllers.Octa
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PageController : ControllerBase
    {
        private readonly UOW _Unit_Of_Work;
        private readonly DynamicDatabaseService _dynamicDatabaseService;
        IMapper mapper;

        public PageController(DynamicDatabaseService dynamicDatabaseService, UOW Unit_Of_Work, IMapper mapper)
        {
            _Unit_Of_Work = Unit_Of_Work;
            _dynamicDatabaseService = dynamicDatabaseService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var userClaims = HttpContext.User.Claims;
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userTypeClaim == null)
            {
                return Unauthorized("User Type claim not found.");
            }

            if (userTypeClaim != "octa")
            {
                return Unauthorized("Access Denied");
            }

            List<LMS_CMS_DAL.Models.Octa.Page> pages = _Unit_Of_Work.page_Octa_Repository.Select_All_Octa();
            List<Page_GetDTO> pagesDTO = mapper.Map<List<Page_GetDTO>>(pages);

            var hierarchicalPages = BuildHierarchy(pagesDTO);

            return Ok(hierarchicalPages);
        }

        private List<Page_GetDTO> BuildHierarchy(List<Page_GetDTO> pages)
        {
            var pageLookup = pages.ToDictionary(p => p.ID);

            var rootPages = new List<Page_GetDTO>();

            foreach (var page in pages)
            {
                if (page.Page_ID == null)
                {
                    rootPages.Add(page);
                }
                else if (pageLookup.TryGetValue(page.Page_ID.Value, out var parentPage))
                {
                    parentPage.Children.Add(page);
                }
            }
            return rootPages;
        }

    }
}