using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Services
{
    public class DbContextFactoryService
    {
        public UOW CreateOneDbContext(HttpContext httpContext)
        {
            string connectionString = httpContext.Items["ConnectionString"] as string;

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("Connection string not found in HttpContext.");
            }

            UOW Unit_Of_Work = new UOW(connectionString);
            return Unit_Of_Work;
        }
    }
}
