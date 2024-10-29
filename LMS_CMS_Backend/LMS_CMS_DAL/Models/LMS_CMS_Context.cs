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


        public LMS_CMS_Context(DbContextOptions<LMS_CMS_Context> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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


            modelBuilder.Entity<Employee>()
                .HasIndex(p => p.User_Name)
                .IsUnique();

            modelBuilder.Entity<Employee>()
                .HasIndex(p => p.Email)
                .IsUnique();

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Parent)
                .WithMany(p => p.Students)
                .HasForeignKey(s => s.Parent_Id)
                .OnDelete(DeleteBehavior.Cascade); 


            base.OnModelCreating(modelBuilder);
        }
    }
}
