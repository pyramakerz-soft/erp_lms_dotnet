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
                .HasIndex(p => p.ar_name)
                .IsUnique();
            modelBuilder.Entity<Page>()
                .HasIndex(p => p.en_name)
                .IsUnique();
            modelBuilder.Entity<Page>()
                .Property(p => p.ID)
                .ValueGeneratedNever();


            ////////////////////////////////////
            ///
            modelBuilder.Entity<Page>()
                 .HasOne(p => p.Parent)
                 .WithMany(p => p.ChildPages)
                 .HasForeignKey(p => p.Page_ID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                 .HasOne(p => p.Role)
                 .WithMany(p => p.Employess)
                 .HasForeignKey(p => p.Role_ID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Domain_Page_Detailes>()
                 .HasOne(p => p.Domain)
                 .WithMany(p => p.Domain_Page_Detailes)
                 .HasForeignKey(p => p.Domain_ID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Domain_Page_Detailes>()
                 .HasOne(p => p.Page)
                 .WithMany(p => p.Domain_Page_Detailes)
                 .HasForeignKey(p => p.Page_ID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                 .HasOne(p => p.Domain)
                 .WithMany(p => p.Employess)
                 .HasForeignKey(p => p.Domain_ID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Role>()
                 .HasOne(p => p.Domain)
                 .WithMany(p => p.Roles)
                 .HasForeignKey(p => p.Domain_ID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Role_Detailes>()
                 .HasOne(p => p.Role)
                 .WithMany(p => p.Role_Detailes)
                 .HasForeignKey(p => p.Role_ID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Role_Detailes>()
                 .HasOne(p => p.Page)
                 .WithMany(p => p.Role_Detailes)
                 .HasForeignKey(p => p.Page_ID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<School>()
                 .HasOne(p => p.Domain)
                 .WithMany(p => p.Schools)
                 .HasForeignKey(p => p.Domain_id)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                 .HasOne(p => p.Parent)
                 .WithMany(p => p.Students)
                 .HasForeignKey(p => p.Parent_Id)
                 .OnDelete(DeleteBehavior.Restrict);

        }
    }
}