using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Octa;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using System.Threading;

namespace LMS_CMS_PL.Services
{
    public class DynamicDatabaseService
    {
        private UOW Unit_Of_Work;
        private readonly IConfiguration _configuration;

        public DynamicDatabaseService(UOW Unit_Of_Work, IConfiguration configuration)
        {
            this.Unit_Of_Work = Unit_Of_Work;
            _configuration = configuration;
        }

        //public async Task AddDomainAndSetupDatabase(string domainName)
        //{
        //    var dataSource = _configuration["DynamicConStr:DataSource"];
        //    var integratedSecurity = _configuration["DynamicConStr:IntegratedSecurity"];
        //    var trustServerCertificate = _configuration["DynamicConStr:TrustServerCertificate"];

        //    string connectionString = $"{dataSource}{domainName}{integratedSecurity}{trustServerCertificate}";

        //    var domain = new Domain { Name = domainName, ConnectionString = connectionString };
        //    Unit_Of_Work.domain_Octa_Repository.Add_Octa(domain);
        //    Unit_Of_Work.SaveOctaChanges();

        //    var optionsBuilder = new DbContextOptionsBuilder<LMS_CMS_Context>();
        //    optionsBuilder.UseSqlServer(connectionString);

        //    using (var dbContext = new LMS_CMS_Context(optionsBuilder.Options))
        //    {
        //        // Create the database if it doesn't exist
        //        bool databaseExists = await dbContext.Database.CanConnectAsync();
        //        if (!databaseExists)
        //        {
        //            await dbContext.Database.EnsureCreatedAsync();
        //        }
        //    }
        //}


        public async Task AddDomainAndSetupDatabase(string domainName)
        {
            var dataSource = _configuration["DynamicConStr:DataSource"];
            var integratedSecurity = _configuration["DynamicConStr:IntegratedSecurity"];
            var trustServerCertificate = _configuration["DynamicConStr:TrustServerCertificate"];

            string connectionString = $"{dataSource}{domainName}{integratedSecurity}{trustServerCertificate}";

            var domain = new Domain { Name = domainName, ConnectionString = connectionString };
            Unit_Of_Work.domain_Octa_Repository.Add_Octa(domain);
            Unit_Of_Work.SaveOctaChanges();

            var optionsBuilder = new DbContextOptionsBuilder<LMS_CMS_Context>();
            optionsBuilder.UseSqlServer(connectionString);

            using (var dbContext = new LMS_CMS_Context(optionsBuilder.Options))
            {
                // Apply migrations directly
                await dbContext.Database.MigrateAsync();
            }
        }



        public async Task ApplyMigrations(string domainName)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            var dataSource = _configuration["DynamicConStr:DataSource"];
            var integratedSecurity = _configuration["DynamicConStr:IntegratedSecurity"];
            var trustServerCertificate = _configuration["DynamicConStr:TrustServerCertificate"];

            string connectionString = $"{dataSource}{domainName}{integratedSecurity}{trustServerCertificate}";

            var optionsBuilder = new DbContextOptionsBuilder<LMS_CMS_Context>();
            optionsBuilder.UseSqlServer(connectionString);

            using (var dbContext = new LMS_CMS_Context(optionsBuilder.Options))
            {
                bool databaseExists = await dbContext.Database.CanConnectAsync();

                if (databaseExists)
                {
                    var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
                    foreach (var migration in pendingMigrations)
                    {
                        Console.WriteLine(migration);
                    }
                        //if (pendingMigrations.Any())
                        //{
                        //    await dbContext.Database.MigrateAsync(cancellationToken);
                        //    //var migrator = dbContext.GetInfrastructure().GetService<IMigrator>();

                        //    //foreach (var migration in pendingMigrations)
                        //    //{
                        //    //    // Apply each migration in a transaction
                        //    //    using (var tran = await dbContext.Database.BeginTransactionAsync(cancellationToken))
                        //    //    {
                        //    //        try
                        //    //        {
                        //    //            // Apply the migration
                        //    //            await migrator.MigrateAsync(migration, cancellationToken);

                        //    //            // Commit the transaction if migration is successful
                        //    //            await tran.CommitAsync(cancellationToken);
                        //    //        }
                        //    //        catch (Exception exc)
                        //    //        {
                        //    //            // Rollback if migration fails
                        //    //            await tran.RollbackAsync(cancellationToken);
                        //    //            throw new Exception($"Error while applying db migration '{migration}'.", exc);
                        //    //        }
                        //    //    }
                        //    //}
                        //}

                        if (pendingMigrations.Any())
                    {
                        foreach (var migration in pendingMigrations)
                        {
                            try
                            {
                                // Attempt to apply each migration individually
                                await dbContext.Database.MigrateAsync(cancellationToken);
                            }
                            catch (SqlException ex)
                            {
                                if (IsObjectAlreadyExistsError(ex))
                                {
                                    // Log and continue with next migration
                                    continue;
                                }
                                else
                                {
                                    // If it's a different issue, rethrow the exception
                                    throw;
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool IsObjectAlreadyExistsError(SqlException ex)
        {
            // Check for common SQL Server error codes related to object already existing
            return ex.Number == 2714 || // Object already exists (e.g., table or index)
                   ex.Number == 2627 || // Primary key violation
                   ex.Number == 2601;   // Unique constraint violation
        }
    }
}
