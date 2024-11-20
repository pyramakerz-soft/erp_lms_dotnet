using LMS_CMS_DAL.Models.BusModule;
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

        public DbSet<EmployeeType> EmployeeType { get; set; }
        public DbSet<AcademicYear> AcademicYear { get; set; }
        public DbSet<Semester> Semester { get; set; }
        public DbSet<BusType> BusType { get; set; }
        public DbSet<BusRestrict> BusRestrict { get; set; }
        public DbSet<BusStatus> BusStatus { get; set; }
        public DbSet<BusCategory> BusCategory { get; set; }
        public DbSet<BusCompany> BusCompany { get; set; }
        public DbSet<Bus> Bus { get; set; }
        public DbSet<BusStudent> BusStudent { get; set; }





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

            modelBuilder.Entity<AcademicYear>()
                 .HasOne(p => p.School)
                 .WithMany(p => p.AcademicYears)
                 .HasForeignKey(p => p.SchoolID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Semester>()
                 .HasOne(p => p.AcademicYear)
                 .WithMany(p => p.Semesters)
                 .HasForeignKey(p => p.AcademicYearID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BusType>()
                 .HasOne(p => p.Domain)
                 .WithMany(p => p.BusTypes)
                 .HasForeignKey(p => p.DomainId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BusRestrict>()
                 .HasOne(p => p.Domain)
                 .WithMany(p => p.BusRestricts)
                 .HasForeignKey(p => p.DomainId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BusStatus>()
                 .HasOne(p => p.Domain)
                 .WithMany(p => p.BusStatus)
                 .HasForeignKey(p => p.DomainId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BusCategory>()
                 .HasOne(p => p.Domain)
                 .WithMany(p => p.BusCategories)
                 .HasForeignKey(p => p.DomainId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BusCompany>()
                 .HasOne(p => p.Domain)
                 .WithMany(p => p.BusCompanies)
                 .HasForeignKey(p => p.DomainId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bus>()
                 .HasOne(p => p.Domain)
                 .WithMany(p => p.Buses)
                 .HasForeignKey(p => p.DomainID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bus>()
                 .HasOne(p => p.BusType)
                 .WithMany(p => p.Buses)
                 .HasForeignKey(p => p.BusTypeID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bus>()
                 .HasOne(p => p.BusRestrict)
                 .WithMany(p => p.Buses)
                 .HasForeignKey(p => p.BusRestrictID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bus>()
                 .HasOne(p => p.BusStatus)
                 .WithMany(p => p.Buses)
                 .HasForeignKey(p => p.BusStatusID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bus>()
                 .HasOne(p => p.Driver)
                 .WithMany(p => p.DrivenBuses)
                 .HasForeignKey(p => p.DriverID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bus>()
                 .HasOne(p => p.DriverAssistant)
                 .WithMany(p => p.DriverAssistant)
                 .HasForeignKey(p => p.DriverAssistantID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bus>()
                 .HasOne(p => p.BusCompany)
                 .WithMany(p => p.Buses)
                 .HasForeignKey(p => p.BusCompanyID)
                 .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<BusStudent>()
                 .HasOne(p => p.Bus)
                 .WithMany(p => p.BusStudents)
                 .HasForeignKey(p => p.BusID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BusStudent>()
                 .HasOne(p => p.Student)
                 .WithMany(p => p.BusStudents)
                 .HasForeignKey(p => p.StudentID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BusStudent>()
                 .HasOne(p => p.BusCategory)
                 .WithMany(p => p.BusStudents)
                 .HasForeignKey(p => p.BusCategoryID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BusStudent>()
                 .HasOne(p => p.Semester)
                 .WithMany(p => p.BusStudents)
                 .HasForeignKey(p => p.SemseterID)
                 .OnDelete(DeleteBehavior.Restrict);
        }
    }
}