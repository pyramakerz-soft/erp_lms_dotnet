﻿using Microsoft.EntityFrameworkCore;
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
        public DbSet<Octa> Octa { get; set; }
        public DbSet<Page> Page { get; set; }
        public DbSet<SchoolType> SchoolType { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Nationality> Nationality { get; set; }


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

            modelBuilder.Entity<Octa>()
                .HasIndex(p => p.User_Name)
                .IsUnique();

            modelBuilder.Entity<Page>()
                .HasIndex(p => p.ar_name)
                .IsUnique();

            modelBuilder.Entity<Page>()
                .HasIndex(p => p.en_name)
                .IsUnique();

            modelBuilder.Entity<SchoolType>()
                .HasIndex(p => p.Name)
                .IsUnique();

            modelBuilder.Entity<Country>()
                .HasIndex(c => c.Name)
                .IsUnique();  

            modelBuilder.Entity<Nationality>()
                .HasIndex(c => c.Name)
                .IsUnique(); 

            ///////////////////////// No Identity: /////////////////////////

            modelBuilder.Entity<Page>()
                .Property(p => p.ID)
                .ValueGeneratedNever();
            
            modelBuilder.Entity<Country>()
                .Property(p => p.ID)
                .ValueGeneratedNever();
            
            modelBuilder.Entity<Nationality>()
                .Property(p => p.ID)
                .ValueGeneratedNever();

            ////////////////////////////////////////////
            
            modelBuilder.Entity<Page>()
                .HasOne(p => p.Parent)
                .WithMany(p => p.ChildPages)
                .HasForeignKey(p => p.Page_ID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Nationality>()
                .HasOne(p => p.Country)
                .WithMany(p => p.Nationalities)
                .HasForeignKey(p => p.CountryID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
