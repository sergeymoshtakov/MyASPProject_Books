using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyProject.Data;
using MyProject.Hubs;
using MyProject.Models;
using MyProject.Services;
using System;
using System.Security.Claims;

namespace MyProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSignalR();

            // Adding configuration
            var configuration = builder.Configuration;
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Configure DbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Configure Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Your identity options here
            })
            .AddEntityFrameworkStores<AppDbContext>();

            // Configure Session
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.Name = "MyProject_Session";
            });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Account/Register");
                });

            builder.Services.AddTransient<IAuthorizationHandler, AgeHandler>();

            builder.Services.AddAuthorization(opts => {
                opts.AddPolicy("AgeLimit",
                    policy => policy.Requirements.Add(new AgeRequirement(18)));

                opts.AddPolicy("OnlyForOdessaOrKherson", policy => {
                    policy.RequireClaim(ClaimTypes.Locality, "Odessa", "Kherson");
                });
            });

            // Remove redundant call to AddControllersWithViews()
            // builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapHub<UserHub>("/hubs/userCount");

            app.Run();
        }
    }
}
