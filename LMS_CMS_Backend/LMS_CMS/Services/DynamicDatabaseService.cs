using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.BusModule;
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

        public async Task AddDomainAndSetupDatabase(string domainName, long userId)
        {
            string connectionString = BuildConnectionString(domainName);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            //var domain = new Domain { Name = domainName, ConnectionString = connectionString, InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone), InsertedByUserId =  userId };
            var domain = new Domain { Name = domainName, InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone), InsertedByUserId =  userId };
            Unit_Of_Work.domain_Octa_Repository.Add_Octa(domain);
            Unit_Of_Work.SaveOctaChanges();

            var optionsBuilder = new DbContextOptionsBuilder<LMS_CMS_Context>();
            optionsBuilder.UseSqlServer(connectionString);

            using (var dbContext = new LMS_CMS_Context(optionsBuilder.Options))
            {
                bool databaseExists = await dbContext.Database.CanConnectAsync();
                if (!databaseExists)
                {
                    await dbContext.Database.MigrateAsync();
                    await CreateAndPopulateMigrationHistoryTable(dbContext);
                }
            }
        }

        public async Task ApplyMigrations(string domainName)
        {
            string connectionString = BuildConnectionString(domainName);

            var optionsBuilder = new DbContextOptionsBuilder<LMS_CMS_Context>();
            optionsBuilder.UseSqlServer(connectionString);

            using (var dbContext = new LMS_CMS_Context(optionsBuilder.Options))
            {
                var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
                if (pendingMigrations.Any())
                {
                    await dbContext.Database.MigrateAsync();
                    // For All, Added Or Deleted Migrations
                    await CreateAndPopulateMigrationHistoryTable(dbContext);
                }
                else
                {
                    // For Deleted Migrations
                    await CreateAndPopulateMigrationHistoryTable(dbContext);
                }
            }
        }

        private string BuildConnectionString(string domainName)
        {
            var dataSource = _configuration["DynamicConStr:DataSource"];
            var integratedSecurity = _configuration["DynamicConStr:IntegratedSecurity"];
            var trustServerCertificate = _configuration["DynamicConStr:TrustServerCertificate"];

            return $"{dataSource}{domainName}{integratedSecurity}{trustServerCertificate}";
        }

        private async Task CreateAndPopulateMigrationHistoryTable(DbContext dbContext)
        {
            var connection = dbContext.Database.GetDbConnection();
            await connection.OpenAsync();

            // Create the __EFMigrationsHistory table if it doesn't exist
            var createTableCommand = connection.CreateCommand();

            createTableCommand.CommandText = @"
                IF NOT EXISTS (
                    SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '__EFMigrationsHistory'
                )
                BEGIN
                    CREATE TABLE __EFMigrationsHistory (
                        MigrationId NVARCHAR(150) NOT NULL PRIMARY KEY,
                        ProductVersion NVARCHAR(32) NOT NULL
                    );
                END";

            await createTableCommand.ExecuteNonQueryAsync();

            // Get all migrations from the assembly
            var migrations = dbContext.GetService<IMigrationsAssembly>().Migrations;

            ///////////////////////////////
            // Get all migrations currently in the history table
            var getHistoryCommand = connection.CreateCommand();
            getHistoryCommand.CommandText = "SELECT MigrationId FROM __EFMigrationsHistory";
            var historyReader = await getHistoryCommand.ExecuteReaderAsync();
            var historyMigrations = new List<string>();

            while (await historyReader.ReadAsync())
            {
                historyMigrations.Add(historyReader.GetString(0));
            }

            historyReader.Close();

            // Identify and remove obsolete migrations
            var obsoleteMigrations = historyMigrations.Except(migrations.Keys);
            foreach (var obsoleteMigration in obsoleteMigrations)
            {
                var deleteCommand = connection.CreateCommand();
                deleteCommand.CommandText = "DELETE FROM __EFMigrationsHistory WHERE MigrationId = @MigrationId";
                var migrationIdParam = deleteCommand.CreateParameter();
                migrationIdParam.ParameterName = "MigrationId";
                migrationIdParam.Value = obsoleteMigration;
                deleteCommand.Parameters.Add(migrationIdParam);

                await deleteCommand.ExecuteNonQueryAsync();
            }
            ///////////////////////////////

            // Insert migration data into the __EFMigrationsHistory table
            foreach (var migration in migrations)
            {
                var insertCommand = connection.CreateCommand();
                insertCommand.CommandText = $@"
                    IF NOT EXISTS (
                        SELECT * FROM __EFMigrationsHistory WHERE MigrationId = @MigrationId
                    )
                    BEGIN
                        INSERT INTO __EFMigrationsHistory (MigrationId, ProductVersion)
                        VALUES (@MigrationId, @ProductVersion);
                    END";

                var migrationIdParam = insertCommand.CreateParameter();
                migrationIdParam.ParameterName = "MigrationId";
                migrationIdParam.Value = migration.Key;
                insertCommand.Parameters.Add(migrationIdParam);

                var productVersionParam = insertCommand.CreateParameter();
                productVersionParam.ParameterName = "ProductVersion";
                productVersionParam.Value = dbContext.Database.ProviderName;
                insertCommand.Parameters.Add(productVersionParam);

                await insertCommand.ExecuteNonQueryAsync();
            }
        }

    }
}
