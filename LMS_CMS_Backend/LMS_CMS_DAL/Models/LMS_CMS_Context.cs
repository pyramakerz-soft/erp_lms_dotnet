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
        public DbSet<Role_Permissions> Role_Detailed_Permissions { get; set; }
        public DbSet<Employee_Role> Employee_Roles { get; set; }



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
                .HasIndex(p => p.Email)
                .IsUnique();

            modelBuilder.Entity<Employee>()
                .HasIndex(p => p.User_Name)
                .IsUnique();

            modelBuilder.Entity<Modules>()
                .HasIndex(p => p.Name)
                .IsUnique();


            ///////////////////////// On Delete Cascade: /////////////////////////
            modelBuilder.Entity<Master_Detailes_Permissions>()
                .HasOne(d => d.Detailed_Permission)
                .WithMany(m => m.Master_Detailes_Permissions)
                .HasForeignKey(d => d.Details_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Modules_Master_permissions>()
                .HasOne(d => d.Module)
                .WithMany(m => m.Modules_Master_permissions)
                .HasForeignKey(d => d.Module_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Modules_Master_permissions>()
                .HasOne(d => d.Master_Permission)
                .WithMany(m => m.Modules_Master_permissions)
                .HasForeignKey(d => d.Master_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Master_Detailes_Permissions>()
                .HasOne(d => d.Master_Permission)
                .WithMany(m => m.Master_Detailes_Permissions)
                .HasForeignKey(d => d.Master_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Parent)
                .WithMany(p => p.Students)
                .HasForeignKey(s => s.Parent_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Role_Permissions>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.Role_Permissions)
                .HasForeignKey(rp => rp.Role_ID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Role_Permissions>()
                .HasOne(rp => rp.Master_Detailes_Permissions)
                .WithMany(dp => dp.Role_Permissions)
                .HasForeignKey(rp => rp.Master_Detailed_Permissions_ID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employee_Role>()
                .HasOne(rp => rp.Role)
                .WithMany(dp => dp.Employee_Roles)
                .HasForeignKey(rp => rp.Role_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employee_Role>()
                .HasOne(rp => rp.Employee)
                .WithMany(dp => dp.Employee_Roles)
                .HasForeignKey(rp => rp.Employee_Id)
                .OnDelete(DeleteBehavior.Cascade);



            base.OnModelCreating(modelBuilder);
        }
    }
}
