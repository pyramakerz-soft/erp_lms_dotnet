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
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Master_Permissions> Master_Permissions { get; set; }
        public DbSet<Detailed_Permissions> Detailed_Permissions { get; set; }
        public DbSet<Role_Detailed_Permissions> Role_Detailed_Permissions { get; set; }


        public LMS_CMS_Context(DbContextOptions<LMS_CMS_Context> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ///////////////////////// Unique Values: /////////////////////////
            modelBuilder.Entity<Role>()
                .HasIndex(r => r.Name)
                .IsUnique();

            modelBuilder.Entity<Master_Permissions>()
                .HasIndex(MP => MP.Name)
                .IsUnique();

            modelBuilder.Entity<Detailed_Permissions>()
                .HasIndex(DP => DP.Name)
                .IsUnique();

            modelBuilder.Entity<Parent>()
                .HasIndex(p => p.User_Name)
                .IsUnique();

            modelBuilder.Entity<Student>()
                .HasIndex(p => p.User_Name)
                .IsUnique();

            modelBuilder.Entity<Employee>()
                .HasIndex(p => p.User_Name)
                .IsUnique();


            ///////////////////////// On Delete Cascade: /////////////////////////
            modelBuilder.Entity<Detailed_Permissions>()
                .HasOne(d => d.Master_Permissions)
                .WithMany(m => m.Detailed_Permissions)
                .HasForeignKey(d => d.Master_Permission_ID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Parent)
                .WithMany(p => p.Students)
                .HasForeignKey(s => s.Parent_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Role_Detailed_Permissions>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.Role_Detailed_Permissions)
                .HasForeignKey(rp => rp.Role_ID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Role_Detailed_Permissions>()
                .HasOne(rp => rp.Detailed_Permissions)
                .WithMany(dp => dp.Role_Detailed_Permissions)
                .HasForeignKey(rp => rp.Detailed_Permissions_ID)
                .OnDelete(DeleteBehavior.Cascade);


            ///////////////////////// Composite primary key: /////////////////////////
            modelBuilder.Entity<Role_Detailed_Permissions>()
                .HasKey(rp => new { rp.Role_ID, rp.Detailed_Permissions_ID });
            

            base.OnModelCreating(modelBuilder);
        }
    }
}
