using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Octa;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Services
{
    public class DynamicDatabaseService
    {
        private UOW Unit_Of_Work;

        public DynamicDatabaseService(UOW Unit_Of_Work)
        {
            this.Unit_Of_Work = Unit_Of_Work;
        }

        public async Task AddDomainAndSetupDatabase(string domainName)
        {
            string connectionString = $"Data Source=.;Initial Catalog={domainName};Integrated Security=True; TrustServerCertificate=True";
            //string connectionString = $"Data Source=MENNA-PC\\SQLEXPRESS02;Initial Catalog={domainName};Integrated Security=True; TrustServerCertificate=True";

            // Add the domain to the Domain table
            var domain = new LMS_CMS_DAL.Models.Octa.Domain { Name = domainName, ConnectionString = connectionString };
            Unit_Of_Work.domain_Octa_Repository.Add_Octa(domain);
            Unit_Of_Work.SaveOctaChanges();

            // Create the new database and apply migrations
            var optionsBuilder = new DbContextOptionsBuilder<LMS_CMS_Context>();
            optionsBuilder.UseSqlServer(connectionString);

            using (var dbContext = new LMS_CMS_Context(optionsBuilder.Options))
            {
                // Create the database if it doesn't exist
                bool databaseExists = await dbContext.Database.CanConnectAsync();
                if (!databaseExists)
                {
                    await dbContext.Database.EnsureCreatedAsync();  // Only create the database if it doesn't exist
                }
                else
                {
                    await dbContext.Database.MigrateAsync();  // This will apply pending migrations only
                }
            }
        }
    }
}
