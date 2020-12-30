using IdentityAuthencation.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAuthencation.SeedData
{
    public static class MyIdentityDataInitializer
    {
        public static void SeedData(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByNameAsync("SuperAdmin").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "SuperAdmin";
                user.NormalizedUserName = "SUPERADMIN";
                user.Email = "luongbaotin2019@gmail.com";
                user.FirstName = "Luong";
                user.LastName = "Bao Tin";
                user.PhoneNumber = "0385618501";
                user.Id = Guid.NewGuid();
                user.Dob = new DateTime(1998, 10, 31);

                var result = userManager.CreateAsync
                (user, "Admin@123");

                if (result.Result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                                        "SuperAdministrator").Wait();
                }
            }
        }

        public static void SeedRoles(RoleManager<ApplicationRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("superadministrator").Result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Id = Guid.NewGuid();
                role.Name = "superadministrator";
                role.NormalizedName = "SUPERADMINISTRATOR";
                role.Description = "Perform all the operations.";

                var roleResult = roleManager.CreateAsync(role);
            }
        }
    }
}
