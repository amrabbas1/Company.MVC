using Company.G03.BLL;
using Company.G03.BLL.Interfaces;
using Company.G03.BLL.Reprositories;
using Company.G03.DAL.Data.Contexts;
using Company.G03.DAL.Models;
using Company.G03.PL.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Company.G03.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //builder.Services.AddScoped<AppDbContext>();//Allow DI for AppDbContext

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });//Allow DI for AppDbContext

            builder.Services.AddScoped<IDepartmentReprository , DepartmentRepository>();//Allow DI for DepartmentRepository
            builder.Services.AddScoped<IEmployeeReprository, EmployeeRepository>();//Allow DI for EmployeeRepository
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();//Allow DI for EmployeeRepository
            builder.Services.AddAutoMapper(typeof(EmployeeProfile));//Allow DI for EmployeeProfile
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            //LifeTime For DI
            //builder.Services.AddScoped();//Life time per request
            //builder.Services.AddTransient();//Life time per operation 
            //builder.Services.AddSingleton();//Life time per request 

            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Account/SignIn";
            }
            );

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
