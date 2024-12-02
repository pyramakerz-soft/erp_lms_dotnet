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
            try
            {
                // Use the factory service to create the DbContext
                using (var dbContext = _dbContextFactory.CreateOneDbContext(HttpContext))
                {
                    // Fetch data from TableAs
                    var data = await dbContext.Domains.ToListAsync();

                    if (data == null)
                    {
                        return NotFound("No data found in TableAs.");
                    }

                    return Ok(data);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
