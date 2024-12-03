using LMS_CMS_DAL.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Services
{
    public class DbContextFactoryService
    {
        public string CreateOneDbContext(HttpContext httpContext)
        {
            var connectionString = httpContext.Items["ConnectionString"] as string;

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("Connection string not found in HttpContext.");
            }

            return connectionString;
        }
    }
}
