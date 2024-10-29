using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models
{
    public partial class LMS_CMS_Context : DbContext
    {
        public LMS_CMS_Context()
        {
        }

        public LMS_CMS_Context(DbContextOptions<LMS_CMS_Context> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasIndex(r => r.Name)
                .IsUnique();

            modelBuilder.Entity<Master_Permissions>()
                .HasIndex(MP => MP.Name)
                .IsUnique();

            modelBuilder.Entity<Detailed_Permissions>()
                .HasIndex(DP => DP.Name)
                .IsUnique();

            modelBuilder.Entity<Detailed_Permissions>()
                .HasOne(d => d.Master_Permissions)
                .WithMany(m => m.Detailed_Permissions)
                .HasForeignKey(d => d.Master_Permission_ID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
