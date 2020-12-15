using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IdentityAuthencation.Authorization;
using IdentityAuthencation.Entities;
using IdentityAuthencation.Extensions;
using IdentityAuthencation.Helpers;
using IdentityAuthencation.Repository;
using IdentityAuthencation.Repository.BaseRepository;
using IdentityAuthencation.Service.Handle;
using IdentityAuthencation.Service.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using static IdentityAuthencation.Authorization.Permission;
using Serilog;
using Microsoft.AspNetCore.Authorization;
using IdentityAuthencation.Authorization.AuthorizationHandler;
using IdentityAuthencation.SeedData;

namespace IdentityAuthencation
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
            services.ConfigSqlContext(Configuration);
            services.ConfigIdentityContext();
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            services.ConfigureSwagger();
            services.ConfigService();
            services.ConfigPermission();
            services.ConfigureLoggerService();
            services.ConfigJwtToken(Configuration);

            services.AddControllers();

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseRouting();
            app.UseAuthentication();
            //MyIdentityDataInitializer.SeedData(userManager, roleManager);
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
