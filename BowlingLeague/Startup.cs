using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BowlingLeague.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BowlingLeague
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
            services.AddControllersWithViews();

            services.AddDbContext<BowlingLeagueContext>(options =>
           {
               options.UseSqlite(Configuration["ConnectionStrings:BowlingLeagueDbConnection"]);
           });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                //This endpoint is used when you are passing team id, team name, and page num info
                endpoints.MapControllerRoute("TeamNamePageNum",
                    "TeamName/{teamID}/{teamName}/{pageNum}",
                    new { Controller = "Home", action = "Index" });

                //This endpoint is used when you are passing team id and team name
                endpoints.MapControllerRoute("TeamID",
                    "Team/{teamID}/{teamName}",
                    new { Controller = "Home", action = "Index" });

                //This endpoint is used when you are passing page number info
                endpoints.MapControllerRoute("PageNum",
                    "{pageNum}",
                    new { Controller = "Home", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
