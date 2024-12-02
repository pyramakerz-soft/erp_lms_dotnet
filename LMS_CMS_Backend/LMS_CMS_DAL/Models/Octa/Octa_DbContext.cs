using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Octa
{
    public class Octa_DbContext : DbContext
    {
        public DbSet<Domain> Domains { get; set; }

        public Octa_DbContext(DbContextOptions<Octa_DbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ///////////////////////// Unique Values: /////////////////////////
            modelBuilder.Entity<Domain>()
               .HasIndex(p => p.Name)
               .IsUnique();
        }
    }
}
