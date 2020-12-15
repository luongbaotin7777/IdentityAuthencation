using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityAuthencation.Migrations
{
    public partial class SeedPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "permission", "Permissions.Users.View", new Guid("5a66428d-a7e0-4c43-bae2-13bdc9623240") },
                    { 22, "permission", "Permissions.Categories.Edit", new Guid("90f990b2-fb59-4c35-3e90-08d88f64e152") },
                    { 23, "permission", "Permissions.Categories.Delete", new Guid("90f990b2-fb59-4c35-3e90-08d88f64e152") },
                    { 24, "permission", "Permissions.Products.View", new Guid("90f990b2-fb59-4c35-3e90-08d88f64e152") },
                    { 25, "permission", "Permissions.Products.Create", new Guid("90f990b2-fb59-4c35-3e90-08d88f64e152") },
                    { 26, "permission", "Permissions.Products.Edit", new Guid("90f990b2-fb59-4c35-3e90-08d88f64e152") },
                    { 27, "permission", "Permissions.Products.Delete", new Guid("90f990b2-fb59-4c35-3e90-08d88f64e152") },
                    { 28, "permission", "Permissions.Dashboards.View", new Guid("90f990b2-fb59-4c35-3e90-08d88f64e152") },
                    { 21, "permission", "Permissions.Categories.Create", new Guid("90f990b2-fb59-4c35-3e90-08d88f64e152") },
                    { 29, "permission", "Permissions.Dashboards.View", new Guid("f58eae8e-0cba-4ed8-69e1-08d88f67500f") },
                    { 31, "permission", "Permissions.Categories.Create", new Guid("f58eae8e-0cba-4ed8-69e1-08d88f67500f") },
                    { 32, "permission", "Permissions.Categories.Edit", new Guid("f58eae8e-0cba-4ed8-69e1-08d88f67500f") },
                    { 33, "permission", "Permissions.Products.View", new Guid("f58eae8e-0cba-4ed8-69e1-08d88f67500f") },
                    { 34, "permission", "Permissions.Products.Create", new Guid("f58eae8e-0cba-4ed8-69e1-08d88f67500f") },
                    { 35, "permission", "Permissions.Products.Edit", new Guid("f58eae8e-0cba-4ed8-69e1-08d88f67500f") },
                    { 36, "permission", "Permissions.Products.View", new Guid("63730b7b-6127-4f9b-0398-08d8904c62b4") },
                    { 37, "permission", "Permissions.Products.Create", new Guid("63730b7b-6127-4f9b-0398-08d8904c62b4") },
                    { 30, "permission", "Permissions.Categories.View", new Guid("f58eae8e-0cba-4ed8-69e1-08d88f67500f") },
                    { 38, "permission", "Permissions.Categories.View", new Guid("63730b7b-6127-4f9b-0398-08d8904c62b4") },
                    { 20, "permission", "Permissions.Categories.View", new Guid("90f990b2-fb59-4c35-3e90-08d88f64e152") },
                    { 18, "permission", "Permissions.Users.Create", new Guid("90f990b2-fb59-4c35-3e90-08d88f64e152") },
                    { 2, "permission", "Permissions.Users.Create", new Guid("5a66428d-a7e0-4c43-bae2-13bdc9623240") },
                    { 3, "permission", "Permissions.Users.Edit", new Guid("5a66428d-a7e0-4c43-bae2-13bdc9623240") },
                    { 4, "permission", "Permissions.Users.Delete", new Guid("5a66428d-a7e0-4c43-bae2-13bdc9623240") },
                    { 5, "permission", "Permissions.Categories.View", new Guid("5a66428d-a7e0-4c43-bae2-13bdc9623240") },
                    { 6, "permission", "Permissions.Categories.Create", new Guid("5a66428d-a7e0-4c43-bae2-13bdc9623240") },
                    { 7, "permission", "Permissions.Categories.Edit", new Guid("5a66428d-a7e0-4c43-bae2-13bdc9623240") },
                    { 8, "permission", "Permissions.Categories.Delete", new Guid("5a66428d-a7e0-4c43-bae2-13bdc9623240") },
                    { 19, "permission", "Permissions.Users.Edit", new Guid("90f990b2-fb59-4c35-3e90-08d88f64e152") },
                    { 9, "permission", "Permissions.Products.View", new Guid("5a66428d-a7e0-4c43-bae2-13bdc9623240") },
                    { 11, "permission", "Permissions.Products.Edit", new Guid("5a66428d-a7e0-4c43-bae2-13bdc9623240") },
                    { 12, "permission", "Permissions.Products.Delete", new Guid("5a66428d-a7e0-4c43-bae2-13bdc9623240") },
                    { 13, "permission", "Permissions.Dashboards.View", new Guid("5a66428d-a7e0-4c43-bae2-13bdc9623240") },
                    { 14, "permission", "Permissions.Products.View", new Guid("ec9092a5-a8b6-40e2-69e2-08d88f67500f") },
                    { 15, "permission", "Permissions.Categories.View", new Guid("ec9092a5-a8b6-40e2-69e2-08d88f67500f") },
                    { 16, "permission", "Permissions.Dashboards.View", new Guid("ec9092a5-a8b6-40e2-69e2-08d88f67500f") },
                    { 17, "permission", "Permissions.Users.View", new Guid("90f990b2-fb59-4c35-3e90-08d88f64e152") },
                    { 10, "permission", "Permissions.Products.Create", new Guid("5a66428d-a7e0-4c43-bae2-13bdc9623240") },
                    { 39, "permission", "Permissions.Categories.Create", new Guid("63730b7b-6127-4f9b-0398-08d8904c62b4") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 39);
        }
    }
}
