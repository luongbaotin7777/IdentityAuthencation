﻿using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAuthencation.Authorization
{
    public class Permission
    {
        //Const CRUD for permissions
        public static class Users
        {
            public const string View = "Permissions.Users.View";
            public const string Create = "Permissions.Users.Create";
            public const string Edit = "Permissions.Users.Edit";
            public const string Delete = "Permissions.Users.Delete";
        }
        public static class Dashboards
        {
            public const string View = "Permissions.Dashboards.View";

        }
        public static class Products
        {
            public const string View = "Permissions.Products.View";
            public const string Create = "Permissions.Products.Create";
            public const string Edit = "Permissions.Products.Edit";
            public const string Delete = "Permissions.Products.Delete";
        }
        public static class Categories
        {
            public const string View = "Permissions.Categories.View";
            public const string Create = "Permissions.Categories.Create";
            public const string Edit = "Permissions.Categories.Edit";
            public const string Delete = "Permissions.Categories.Delete";
        }
        public static class Tests
        {
            public const string View = "Permissions.Tests.View";
        }

        //Permissions will be assigned to roles with custom claim type
        public class CustomClaimTypes
        {
            public const string Permission = "permission";
        }
        //create a class that will hold the permission to be evaluated. 
        internal class PermissionRequirement : IAuthorizationRequirement
        {
            public string Permission { get; private set; }

            public PermissionRequirement(string permission)
            {
                Permission = permission;
            }
        }
    }
}
