using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains
{
    public class DomainsContextFactory : IDesignTimeDbContextFactory<LMS_CMS_Context>
    {
        public LMS_CMS_Context CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<LMS_CMS_Context>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("con"));

            return new LMS_CMS_Context(optionsBuilder.Options);
        }
    }
}
