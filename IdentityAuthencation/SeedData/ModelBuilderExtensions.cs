using IdentityAuthencation.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAuthencation.SeedData
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationRoleClaim>().HasData(
                            //Set permission for USER
                            new ApplicationRoleClaim { Id = 1, RoleId = Guid.Parse("EC9092A5-A8B6-40E2-69E2-08D88F67500F"), ClaimType = "permission", ClaimValue = "Permission.Products.View" },
                            new ApplicationRoleClaim { Id = 2, RoleId = Guid.Parse("EC9092A5-A8B6-40E2-69E2-08D88F67500F"), ClaimType = "permission", ClaimValue = "Permission.Categories.View" },
                            new ApplicationRoleClaim { Id = 3, RoleId = Guid.Parse("EC9092A5-A8B6-40E2-69E2-08D88F67500F"), ClaimType = "permission", ClaimValue = "Permission.Dashboards.View" },
                            //Set permission for ADMIN
                            new ApplicationRoleClaim { Id = 4, RoleId = Guid.Parse("90F990B2-FB59-4C35-3E90-08D88F64E152"), ClaimType = "permission", ClaimValue = "Permission.Categories.View" },
                            new ApplicationRoleClaim { Id = 5, RoleId = Guid.Parse("90F990B2-FB59-4C35-3E90-08D88F64E152"), ClaimType = "permission", ClaimValue = "Permission.Categories.Create" },
                            new ApplicationRoleClaim { Id = 6, RoleId = Guid.Parse("90F990B2-FB59-4C35-3E90-08D88F64E152"), ClaimType = "permission", ClaimValue = "Permission.Categories.Edit" },
                            new ApplicationRoleClaim { Id = 7, RoleId = Guid.Parse("90F990B2-FB59-4C35-3E90-08D88F64E152"), ClaimType = "permission", ClaimValue = "Permission.Categories.Delete" },

                            new ApplicationRoleClaim { Id = 8, RoleId = Guid.Parse("90F990B2-FB59-4C35-3E90-08D88F64E152"), ClaimType = "permission", ClaimValue = "Permission.Products.View" },
                            new ApplicationRoleClaim { Id = 9, RoleId = Guid.Parse("90F990B2-FB59-4C35-3E90-08D88F64E152"), ClaimType = "permission", ClaimValue = "Permission.Products.Create" },
                            new ApplicationRoleClaim { Id = 10, RoleId = Guid.Parse("90F990B2-FB59-4C35-3E90-08D88F64E152"), ClaimType = "permission", ClaimValue = "Permission.Products.Edit" },
                            new ApplicationRoleClaim { Id = 11, RoleId = Guid.Parse("90F990B2-FB59-4C35-3E90-08D88F64E152"), ClaimType = "permission", ClaimValue = "Permission.Products.Delete" },

                            new ApplicationRoleClaim { Id = 12, RoleId = Guid.Parse("90F990B2-FB59-4C35-3E90-08D88F64E152"), ClaimType = "permission", ClaimValue = "Permission.Dashboards.View" },

                            //Set permission for MOD
                            new ApplicationRoleClaim { Id = 13, RoleId = Guid.Parse("F58EAE8E-0CBA-4ED8-69E1-08D88F67500F"), ClaimType = "permission", ClaimValue = "Permission.Dashboards.View" },

                            new ApplicationRoleClaim { Id = 14, RoleId = Guid.Parse("F58EAE8E-0CBA-4ED8-69E1-08D88F67500F"), ClaimType = "permission", ClaimValue = "Permission.Categories.View" },
                            new ApplicationRoleClaim { Id = 15, RoleId = Guid.Parse("F58EAE8E-0CBA-4ED8-69E1-08D88F67500F"), ClaimType = "permission", ClaimValue = "Permission.Categories.Create" },
                            new ApplicationRoleClaim { Id = 16, RoleId = Guid.Parse("F58EAE8E-0CBA-4ED8-69E1-08D88F67500F"), ClaimType = "permission", ClaimValue = "Permission.Categories.Edit" },
                            new ApplicationRoleClaim { Id = 17, RoleId = Guid.Parse("F58EAE8E-0CBA-4ED8-69E1-08D88F67500F"), ClaimType = "permission", ClaimValue = "Permission.Categories.Delete" },

                            new ApplicationRoleClaim { Id = 18, RoleId = Guid.Parse("F58EAE8E-0CBA-4ED8-69E1-08D88F67500F"), ClaimType = "permission", ClaimValue = "Permission.Products.View" },
                            new ApplicationRoleClaim { Id = 19, RoleId = Guid.Parse("F58EAE8E-0CBA-4ED8-69E1-08D88F67500F"), ClaimType = "permission", ClaimValue = "Permission.Products.Create" },
                            new ApplicationRoleClaim { Id = 20, RoleId = Guid.Parse("F58EAE8E-0CBA-4ED8-69E1-08D88F67500F"), ClaimType = "permission", ClaimValue = "Permission.Products.Edit" },
                            new ApplicationRoleClaim { Id = 21, RoleId = Guid.Parse("F58EAE8E-0CBA-4ED8-69E1-08D88F67500F"), ClaimType = "permission", ClaimValue = "Permission.Products.Delete" }
                    );
        }
    }
}
