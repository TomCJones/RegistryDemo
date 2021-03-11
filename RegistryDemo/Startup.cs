using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using RegistryDemo.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryDemo
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
            string dbPath = Configuration.GetConnectionString("DefaultConnection");
            Console.WriteLine();
            Console.WriteLine("dbPath = " + dbPath);
            services.AddDbContext<SqliteDBContext>(options =>
                options.UseSqlite(dbPath));
            services.AddControllersWithViews(options =>
                options.CacheProfiles.Add("Default30",
                    new Microsoft.AspNetCore.Mvc.CacheProfile()
                    {
                        Duration = 30
                    }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            string errSTR;
            try
            {
                var libraryPath = Path.GetFullPath(Path.Combine(env.ContentRootPath, ".", "wwwroot"));
                var provider = new PhysicalFileProvider(libraryPath);
                var contents = provider.GetDirectoryContents(string.Empty);
                var filePath = Path.Combine("wwwroot", "js", "site.js");
                var fileInfo = provider.GetFileInfo(filePath);
                string foo = "bar";
            }
            catch (Exception ex)
            {
                errSTR = ex.Message;
            }


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        //        app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
         //       endpoints.MapControllerRoute(
          //          name: "registryquery",
          //          pattern: "{controller=RegistryTest}/{action=Index}/{id?}");
            });
        }
    }
}
