﻿

using IdentityAuthencation.Authorization;
using IdentityAuthencation.Entities;
using IdentityAuthencation.Logger;
using IdentityAuthencation.Repository;
using IdentityAuthencation.Repository.BaseRepository;
using IdentityAuthencation.Service.Handle;
using IdentityAuthencation.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using static IdentityAuthencation.Authorization.Permission;

namespace IdentityAuthencation.Extensions
{
    public static class ServiceExtensions
    {

        public static void ConfigSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RepositoryDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

        }
        public static void ConfigJwtToken(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(auths =>
            {
                auths.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auths.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                //auths.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
                //auths.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            // Adding JWT Bearer
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,//xác minh rằng khóa được sử dụng để ký mã thông báo đến là một phần của danh sách các khóa đáng tin cậy
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwts:Key"]))
                };
            });
        }
        public static void ConfigIdentityContext(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                    .AddEntityFrameworkStores<RepositoryDbContext>()
                    .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                //options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });
        }
        public static void ConfigService(this IServiceCollection services)
        {
            //DI RoleService
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            //DI Usermanager,Rolemanager,SigninManager
            services.AddScoped<UserManager<ApplicationUser>, UserManager<ApplicationUser>>();
            services.AddScoped<RoleManager<ApplicationRole>, RoleManager<ApplicationRole>>();
            services.AddScoped<SignInManager<ApplicationUser>, SignInManager<ApplicationUser>>();
            // DI UitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //DI TokenService
            services.AddScoped<ITokenService, TokenService>();
        }
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
        public static void ConfigPermission(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                //Policy for users
                options.AddPolicy(Permission.Users.Create, policy =>
                {
                    policy.AddRequirements(new PermissionRequirement(Permission.Users.Create));
                });
                options.AddPolicy(Permission.Users.View, policy =>
                {
                    policy.AddRequirements(new PermissionRequirement(Permission.Users.View));
                });
                options.AddPolicy(Permission.Users.Edit, policy =>
                {
                    policy.AddRequirements(new PermissionRequirement(Permission.Users.Edit));
                });
                options.AddPolicy(Permission.Users.Delete, policy =>
                {
                    policy.AddRequirements(new PermissionRequirement(Permission.Users.Delete));
                });
                //Policy for dashboard

                options.AddPolicy(Permission.Dashboards.View, policy =>
                {
                    policy.AddRequirements(new PermissionRequirement(Permission.Dashboards.View));
                });
                //Policy for Products
                options.AddPolicy(Permission.Products.Create, policy =>
                {
                    policy.AddRequirements(new PermissionRequirement(Permission.Products.Create));
                });
                options.AddPolicy(Permission.Products.View, policy =>
                {
                    policy.AddRequirements(new PermissionRequirement(Permission.Products.View));
                });
                options.AddPolicy(Permission.Products.Edit, policy =>
                {
                    policy.AddRequirements(new PermissionRequirement(Permission.Products.Edit));
                });
                options.AddPolicy(Permission.Products.Delete, policy =>
                {
                    policy.AddRequirements(new PermissionRequirement(Permission.Products.Delete));
                });
                //Policy for Categories
                options.AddPolicy(Permission.Categories.Create, policy =>
                {
                    policy.AddRequirements(new PermissionRequirement(Permission.Categories.Create));
                });
                options.AddPolicy(Permission.Categories.View, policy =>
                {
                    policy.AddRequirements(new PermissionRequirement(Permission.Categories.View));
                });
                options.AddPolicy(Permission.Categories.Edit, policy =>
                {
                    policy.AddRequirements(new PermissionRequirement(Permission.Categories.Edit));
                });
                options.AddPolicy(Permission.Categories.Delete, policy =>
                {
                    policy.AddRequirements(new PermissionRequirement(Permission.Categories.Delete));
                });
                options.AddPolicy(Permission.Tests.View, policy =>
                {
                    policy.AddRequirements(new PermissionRequirement(Permission.Tests.View));
                });
            });
        }
    }
}