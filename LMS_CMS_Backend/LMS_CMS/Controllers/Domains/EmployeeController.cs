using LMS_CMS_BL.UOW;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers.Domains
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly UOW _Unit_Of_Work;
        private readonly DbContextFactoryService _dbContextFactory;

        public EmployeeController(UOW Unit_Of_Work, DbContextFactoryService dbContextFactory)
        {
            _Unit_Of_Work = Unit_Of_Work;
            _dbContextFactory = dbContextFactory;
        }

        [HttpGet]
        public async Task<ActionResult> GetTableAData()
        {
            string connectionString = _dbContextFactory.CreateOneDbContext(HttpContext);
            var uow = new UOW(connectionString);

            var data = uow.employee_Repository.Select_All();

            if (data == null)
            {
                return NotFound("No data found in TableAs.");
            }

            return Ok(data);
        }
    }
}
