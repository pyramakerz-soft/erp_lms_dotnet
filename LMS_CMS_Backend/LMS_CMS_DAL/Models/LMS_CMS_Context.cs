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
        public DbSet<Pyramakerz> Pyramakerz { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Domain> Domains { get; set; }
        public DbSet<Domain_Page_Detailes> Domain_Page_Details { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Role_Detailes> Role_Detailes { get; set; }


        public LMS_CMS_Context(DbContextOptions<LMS_CMS_Context> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ///////////////////////// Unique Values: /////////////////////////
            modelBuilder.Entity<Parent>()
                .HasIndex(p => p.Email)
                .IsUnique();
            modelBuilder.Entity<Parent>()
                .HasIndex(p => p.User_Name)
                .IsUnique();
            modelBuilder.Entity<Student>()
                .HasIndex(p => p.Email)
                .IsUnique();
            modelBuilder.Entity<Student>()
                .HasIndex(p => p.User_Name)
                .IsUnique();
            modelBuilder.Entity<Pyramakerz>()
               .HasIndex(p => p.Email)
               .IsUnique();
            modelBuilder.Entity<Pyramakerz>()
                .HasIndex(p => p.User_Name)
                .IsUnique();

            modelBuilder.Entity<Employee>()
                .HasIndex(p => p.User_Name)
                .IsUnique();
            modelBuilder.Entity<Domain>()
                .HasIndex(p => p.Name)
                .IsUnique();
            modelBuilder.Entity<Page>()
                .HasIndex(p => p.Name)
                .IsUnique();
        }
    }
}