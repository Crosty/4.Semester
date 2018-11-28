using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Personkartotek.Interfaces;
using Personkartotek.Models;
using Personkartotek.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace Personkartotek
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //First Db
            //services.AddDbContext<PersonkartotekDBHandIn32Context>(options =>
            //options.UseSqlServer(Configuration.GetConnectionString("Personkartotek")));

            //Migration
            services.AddDbContext<PersonkartotekDBHandIn32Context>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DABServer")));

            //Swagger
            services.AddDbContext<PersonkartotekDBHandIn32Context>(opt =>
                opt.UseInMemoryDatabase("DABServer"));

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Personkartotek", Version = "v1" });
            });

            //Repositories
            services.AddTransient<IPersonRepo, PersonRepo>();

            //UnitOfWork
            services.AddTransient<IUnitOfWork, UnitOfWork.UnitOfWork>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //Swagger
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Personkartotek");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc();
        }
    }
}
