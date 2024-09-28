using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MVC.Project01.BLL.Interfaces;
using MVC.Project01.BLL.Repositories;
using MVC.Project01.DAL.Data.Contexts;
using MVC.Project01.DAL.Models;
using MVC.Project01.Pl.Mapping.Departments;
using MVC.Project01.Pl.Mapping.Employees;

namespace MVC.Project01.Pl
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //builder.Services.AddScoped<AppDbContext>(); // Allow DI For AppDbContext
            builder.Services.AddDbContext<AppDbContext>(Options=>
            {
                //Options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }); // Allow DI For AppDbContext With EF.SQL
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>(); // Allow DI For DepartmentRepository
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>(); // Allow DI For EmployeeRepository
            
            builder.Services.AddAutoMapper(typeof(EmployeeProfile));
            builder.Services.AddAutoMapper(typeof(DepartmentProfile));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                            .AddEntityFrameworkStores<AppDbContext>()
                            .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Account/SignIn";
                //config.AccessDeniedPath= "/Account/AccessDenied";
                //config.Cookie.Expiration = TimeSpan.FromDays(1);
            });

            // Allow The Indepancancy InJections
            //builder.Services.AddScoped();
            //builder.Services.AddTransient();
            //builder.Services.AddSingleton();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
