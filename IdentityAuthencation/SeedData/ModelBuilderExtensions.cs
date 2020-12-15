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
                            //Set permission for SuperAdmin
                            new ApplicationRoleClaim { Id = 1, RoleId = Guid.Parse("5A66428D-A7E0-4C43-BAE2-13BDC9623240"), ClaimType = "permission", ClaimValue = "Permissions.Users.View" },
                            new ApplicationRoleClaim { Id = 2, RoleId = Guid.Parse("5A66428D-A7E0-4C43-BAE2-13BDC9623240"), ClaimType = "permission", ClaimValue = "Permissions.Users.Create" },
                            new ApplicationRoleClaim { Id = 3, RoleId = Guid.Parse("5A66428D-A7E0-4C43-BAE2-13BDC9623240"), ClaimType = "permission", ClaimValue = "Permissions.Users.Edit" },
                            new ApplicationRoleClaim { Id = 4, RoleId = Guid.Parse("5A66428D-A7E0-4C43-BAE2-13BDC9623240"), ClaimType = "permission", ClaimValue = "Permissions.Users.Delete" },

                            new ApplicationRoleClaim { Id = 5, RoleId = Guid.Parse("5A66428D-A7E0-4C43-BAE2-13BDC9623240"), ClaimType = "permission", ClaimValue = "Permissions.Categories.View" },
                            new ApplicationRoleClaim { Id = 6, RoleId = Guid.Parse("5A66428D-A7E0-4C43-BAE2-13BDC9623240"), ClaimType = "permission", ClaimValue = "Permissions.Categories.Create" },
                            new ApplicationRoleClaim { Id = 7, RoleId = Guid.Parse("5A66428D-A7E0-4C43-BAE2-13BDC9623240"), ClaimType = "permission", ClaimValue = "Permissions.Categories.Edit" },
                            new ApplicationRoleClaim { Id = 8, RoleId = Guid.Parse("5A66428D-A7E0-4C43-BAE2-13BDC9623240"), ClaimType = "permission", ClaimValue = "Permissions.Categories.Delete" },

                            new ApplicationRoleClaim { Id = 9, RoleId = Guid.Parse("5A66428D-A7E0-4C43-BAE2-13BDC9623240"), ClaimType = "permission", ClaimValue = "Permissions.Products.View" },
                            new ApplicationRoleClaim { Id = 10, RoleId = Guid.Parse("5A66428D-A7E0-4C43-BAE2-13BDC9623240"), ClaimType = "permission", ClaimValue = "Permissions.Products.Create" },
                            new ApplicationRoleClaim { Id = 11, RoleId = Guid.Parse("5A66428D-A7E0-4C43-BAE2-13BDC9623240"), ClaimType = "permission", ClaimValue = "Permissions.Products.Edit" },
                            new ApplicationRoleClaim { Id = 12, RoleId = Guid.Parse("5A66428D-A7E0-4C43-BAE2-13BDC9623240"), ClaimType = "permission", ClaimValue = "Permissions.Products.Delete" },

                            new ApplicationRoleClaim { Id = 13, RoleId = Guid.Parse("5A66428D-A7E0-4C43-BAE2-13BDC9623240"), ClaimType = "permission", ClaimValue = "Permissions.Dashboards.View" },

                            //Set permission for USER
                            new ApplicationRoleClaim { Id = 14, RoleId = Guid.Parse("EC9092A5-A8B6-40E2-69E2-08D88F67500F"), ClaimType = "permission", ClaimValue = "Permissions.Products.View" },
                            new ApplicationRoleClaim { Id = 15, RoleId = Guid.Parse("EC9092A5-A8B6-40E2-69E2-08D88F67500F"), ClaimType = "permission", ClaimValue = "Permissions.Categories.View" },
                            new ApplicationRoleClaim { Id = 16, RoleId = Guid.Parse("EC9092A5-A8B6-40E2-69E2-08D88F67500F"), ClaimType = "permission", ClaimValue = "Permissions.Dashboards.View" },

                            //Set permission for ADMIN
                            new ApplicationRoleClaim { Id = 17, RoleId = Guid.Parse("90F990B2-FB59-4C35-3E90-08D88F64E152"), ClaimType = "permission", ClaimValue = "Permissions.Users.View" },
                            new ApplicationRoleClaim { Id = 18, RoleId = Guid.Parse("90F990B2-FB59-4C35-3E90-08D88F64E152"), ClaimType = "permission", ClaimValue = "Permissions.Users.Create" },
                            new ApplicationRoleClaim { Id = 19, RoleId = Guid.Parse("90F990B2-FB59-4C35-3E90-08D88F64E152"), ClaimType = "permission", ClaimValue = "Permissions.Users.Edit" },

                            new ApplicationRoleClaim { Id = 20, RoleId = Guid.Parse("90F990B2-FB59-4C35-3E90-08D88F64E152"), ClaimType = "permission", ClaimValue = "Permissions.Categories.View" },
                            new ApplicationRoleClaim { Id = 21, RoleId = Guid.Parse("90F990B2-FB59-4C35-3E90-08D88F64E152"), ClaimType = "permission", ClaimValue = "Permissions.Categories.Create" },
                            new ApplicationRoleClaim { Id = 22, RoleId = Guid.Parse("90F990B2-FB59-4C35-3E90-08D88F64E152"), ClaimType = "permission", ClaimValue = "Permissions.Categories.Edit" },
                            new ApplicationRoleClaim { Id = 23, RoleId = Guid.Parse("90F990B2-FB59-4C35-3E90-08D88F64E152"), ClaimType = "permission", ClaimValue = "Permissions.Categories.Delete" },

                            new ApplicationRoleClaim { Id = 24, RoleId = Guid.Parse("90F990B2-FB59-4C35-3E90-08D88F64E152"), ClaimType = "permission", ClaimValue = "Permissions.Products.View" },
                            new ApplicationRoleClaim { Id = 25, RoleId = Guid.Parse("90F990B2-FB59-4C35-3E90-08D88F64E152"), ClaimType = "permission", ClaimValue = "Permissions.Products.Create" },
                            new ApplicationRoleClaim { Id = 26, RoleId = Guid.Parse("90F990B2-FB59-4C35-3E90-08D88F64E152"), ClaimType = "permission", ClaimValue = "Permissions.Products.Edit" },
                            new ApplicationRoleClaim { Id = 27, RoleId = Guid.Parse("90F990B2-FB59-4C35-3E90-08D88F64E152"), ClaimType = "permission", ClaimValue = "Permissions.Products.Delete" },

                            new ApplicationRoleClaim { Id = 28, RoleId = Guid.Parse("90F990B2-FB59-4C35-3E90-08D88F64E152"), ClaimType = "permission", ClaimValue = "Permissions.Dashboards.View" },

                            //Set permission for MOD
                            new ApplicationRoleClaim { Id = 29, RoleId = Guid.Parse("F58EAE8E-0CBA-4ED8-69E1-08D88F67500F"), ClaimType = "permission", ClaimValue = "Permissions.Dashboards.View" },

                            new ApplicationRoleClaim { Id = 30, RoleId = Guid.Parse("F58EAE8E-0CBA-4ED8-69E1-08D88F67500F"), ClaimType = "permission", ClaimValue = "Permissions.Categories.View" },
                            new ApplicationRoleClaim { Id = 31, RoleId = Guid.Parse("F58EAE8E-0CBA-4ED8-69E1-08D88F67500F"), ClaimType = "permission", ClaimValue = "Permissions.Categories.Create" },
                            new ApplicationRoleClaim { Id = 32, RoleId = Guid.Parse("F58EAE8E-0CBA-4ED8-69E1-08D88F67500F"), ClaimType = "permission", ClaimValue = "Permissions.Categories.Edit" },


                            new ApplicationRoleClaim { Id = 33, RoleId = Guid.Parse("F58EAE8E-0CBA-4ED8-69E1-08D88F67500F"), ClaimType = "permission", ClaimValue = "Permissions.Products.View" },
                            new ApplicationRoleClaim { Id = 34, RoleId = Guid.Parse("F58EAE8E-0CBA-4ED8-69E1-08D88F67500F"), ClaimType = "permission", ClaimValue = "Permissions.Products.Create" },
                            new ApplicationRoleClaim { Id = 35, RoleId = Guid.Parse("F58EAE8E-0CBA-4ED8-69E1-08D88F67500F"), ClaimType = "permission", ClaimValue = "Permissions.Products.Edit" },

                            //Set permission for POSTManager
                            new ApplicationRoleClaim { Id = 36, RoleId = Guid.Parse("63730B7B-6127-4F9B-0398-08D8904C62B4"), ClaimType = "permission", ClaimValue = "Permissions.Products.View" },
                            new ApplicationRoleClaim { Id = 37, RoleId = Guid.Parse("63730B7B-6127-4F9B-0398-08D8904C62B4"), ClaimType = "permission", ClaimValue = "Permissions.Products.Create" },

                            new ApplicationRoleClaim { Id = 38, RoleId = Guid.Parse("63730B7B-6127-4F9B-0398-08D8904C62B4"), ClaimType = "permission", ClaimValue = "Permissions.Categories.View" },
                            new ApplicationRoleClaim { Id = 39, RoleId = Guid.Parse("63730B7B-6127-4F9B-0398-08D8904C62B4"), ClaimType = "permission", ClaimValue = "Permissions.Categories.Create" }
                    );
        }
    }
}
