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
    public class DomainModulesController : ControllerBase
    {
        UOW unitOfWork;
        IMapper mapper;
        private readonly LMS_CMS_Context _context;
        public DomainModulesController(LMS_CMS_Context context, UOW unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this._context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Domain_Modules> domainModules = unitOfWork.domain_Modules_Repository.Select_All_With_Includes(dm => dm.Module, d => d.Domain);
            if (domainModules == null)
            {
                return NotFound();
            }
            List<Domain_Modules_GetDTO> domainModuleDTOs = mapper.Map<List<Domain_Modules_GetDTO>>(domainModules);

            return Ok(domainModuleDTOs);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get_By_Id(int Id)
        {
            if (Id == 0)
            {
                return BadRequest("Domain Module ID cannot be null.");
            }

            var domainModule = await unitOfWork.domain_Modules_Repository.FindByIncludesAsync(
                dm => dm.ID == Id,
                query => query.Include(d => d.Domain),
                query => query.Include(d => d.Module)
            );

            if (domainModule == null)
            {
                return NotFound();
            }
            else
            {
                Domain_Modules_GetDTO domainModuleDTO = mapper.Map<Domain_Modules_GetDTO>(domainModule);
                return Ok(domainModuleDTO);
            }
        }

        [HttpGet("Domain_Modules/{domainID}")]
        public async Task<IActionResult> Domain_Modules_Permission(int domainID)
        {
            // Step 1: Fetch all the data for the specified domain
            var Domain_ModulePermissions = await _context.Domain_Modules_Permission_View
                .Where(ep => ep.DomainID == domainID)
                .ToListAsync();

            // Step 2: Check if we have any data
            if (!Domain_ModulePermissions.Any())
            {
                return NotFound();
            }

            // Step 3: Perform the grouping in memory after fetching the data
            var groupedByDomain = Domain_ModulePermissions
                .GroupBy(ep => new { ep.DomainID, ep.DomainName })
                .Select(domainGroup => new
                {
                    domainGroup.Key.DomainID,
                    domainGroup.Key.DomainName,
                    Modules = domainGroup
                        .GroupBy(ep => new { ep.ModuleID, ep.ModuleName })
                        .Select(moduleGroup => new
                        {
                            moduleGroup.Key.ModuleID,
                            moduleGroup.Key.ModuleName,
                            Permissions = moduleGroup
                                .GroupBy(ep => new { ep.MasterPermissionID, ep.MasterPermissionName })
                                .Select(masterGroup => new
                                {
                                    masterGroup.Key.MasterPermissionID,
                                    masterGroup.Key.MasterPermissionName,
                                    DetailedPermissions = masterGroup
                                        .GroupBy(ep => new { ep.DetailedPermissionID, ep.DetailedPermissionName })
                                        .Select(detailGroup => new
                                        {
                                            detailGroup.Key.DetailedPermissionID,
                                            detailGroup.Key.DetailedPermissionName
                                        })
                                })
                        })
                })
                .ToList();

            return Ok(groupedByDomain);
        }
    }
}
