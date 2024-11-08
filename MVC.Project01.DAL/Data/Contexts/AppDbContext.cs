﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC.Project01.DAL.Data.Configurations;
using MVC.Project01.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Project01.DAL.Data.Contexts
{
    public class AppDbContext:IdentityDbContext<ApplicationUser > // By This I Inhert 7 DbSets
    {
        
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new DepartmentConfigurations());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			base.OnModelCreating(modelBuilder);

			//modelBuilder.Entity<IdentityRole>().ToTable("Roles");
		}

		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
		//    optionsBuilder.UseSqlServer("Server=.;Database= CompanyMVC; Trusted_Connection=True; TrustServerCertificate=True");
		//}

		public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        //public DbSet<IdentityUser> Users { get; set; }
        //public DbSet<IdentityUser> Roles { get; set; }

    }
}
