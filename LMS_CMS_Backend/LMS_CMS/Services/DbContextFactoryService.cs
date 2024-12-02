using LMS_CMS_DAL.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Services
{
    public class DbContextFactoryService
    {
        public LMS_CMS_Context CreateOneDbContext(HttpContext httpContext)
        {
            var connectionString = httpContext.Items["ConnectionString"] as string;

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("Connection string not found in HttpContext.");
            }

            // Create a new DbContext with the dynamic connection string
            var optionsBuilder = new DbContextOptionsBuilder<LMS_CMS_Context>();
            optionsBuilder.UseSqlServer(connectionString);

            return new LMS_CMS_Context(optionsBuilder.Options);
        }
    }
}
