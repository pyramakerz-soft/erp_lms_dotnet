using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Octa
{
    public class OctaDbContextFactory : IDesignTimeDbContextFactory<Octa_DbContext>
    {
        public Octa_DbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<Octa_DbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("con"));

            return new Octa_DbContext(optionsBuilder.Options);
        }
    }
}
