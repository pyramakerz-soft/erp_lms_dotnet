using AutoMapper;
using LMS_CMS_BL.DTO.Bus;
using LMS_CMS_BL.DTO.Violation;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.Violations;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers.Domains.Violations
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeTypeViolationController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public EmployeeTypeViolationController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        ///////////////////////////////////////////

        //[HttpGet]
        //[Authorize_Endpoint_(
        //    allowedTypes: new[] { "octa", "employee" },
        //    pages: new[] { "Violation Types", "Administrator" }
        //)]
        //public async Task<IActionResult> GetAsync()
        //{
        //    UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

        //    List<EmployeeTypeViolation> EmpTypeViolations = await Unit_Of_Work.employeeTypeViolation_Repository.Select_All_With_IncludesById<EmployeeTypeViolation>(
        //            bus => bus.IsDeleted != true,
        //            query => query.Include(emp => emp.EmployeeType),
        //            query => query.Include(assisstant => assisstant.Violation)
        //            );

        //    if (EmpTypeViolations == null || EmpTypeViolations.Count == 0)
        //    {
        //        return NotFound();
        //    }

        //    List<EmployeeTypeViolationGetDTO> EmpTypeViolationsDTO = mapper.Map<List<EmployeeTypeViolationGetDTO>>(EmpTypeViolations);

        //    return Ok(EmpTypeViolationsDTO);
        //}
    }
}
