﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HitmanService.Data;
using HitmanService.Models;
using HitmanService.Services;
using HitmanService.Services.Sql;

namespace HitmanService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(Configuration.GetConnectionString("DefaultSqliteConnection"));
                //if (true || _env.IsDevelopment())
                //{
                //    // Enable the line to use MS SQL Server.
                //    //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
                //    // Enable the line to use SQLite.
                //    //options.UseSqlite(Configuration.GetConnectionString("DefaultSqliteConnection"));
                //}
                //else
                //{
                //    // Enable the line to use MySql.
                //    //options.UseMySql(Configuration.GetConnectionString("DefaultMySqlConnection"));
                //}
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();

            services.AddScoped<IStorageService>(provider => new SqlDbStorageService(provider.GetService<ApplicationDbContext>()));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            DbInitializer.Initialize(dbContext);
        }
    }
}
