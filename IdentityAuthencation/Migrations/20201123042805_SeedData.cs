using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityAuthencation.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "permission", "Permission.Products.View", new Guid("ec9092a5-a8b6-40e2-69e2-08d88f67500f") },
                    { 19, "permission", "Permission.Products.Create", new Guid("f58eae8e-0cba-4ed8-69e1-08d88f67500f") },
                    { 18, "permission", "Permission.Products.View", new Guid("f58eae8e-0cba-4ed8-69e1-08d88f67500f") },
                    { 17, "permission", "Permission.Categories.Delete", new Guid("f58eae8e-0cba-4ed8-69e1-08d88f67500f") },
                    { 16, "permission", "Permission.Categories.Edit", new Guid("f58eae8e-0cba-4ed8-69e1-08d88f67500f") },
                    { 15, "permission", "Permission.Categories.Create", new Guid("f58eae8e-0cba-4ed8-69e1-08d88f67500f") },
                    { 14, "permission", "Permission.Categories.View", new Guid("f58eae8e-0cba-4ed8-69e1-08d88f67500f") },
                    { 13, "permission", "Permission.Dashboards.View", new Guid("f58eae8e-0cba-4ed8-69e1-08d88f67500f") },
                    { 12, "permission", "Permission.Dashboards.View", new Guid("90f990b2-fb59-4c35-3e90-08d88f64e152") },
                    { 20, "permission", "Permission.Products.Edit", new Guid("f58eae8e-0cba-4ed8-69e1-08d88f67500f") },
                    { 11, "permission", "Permission.Products.Delete", new Guid("90f990b2-fb59-4c35-3e90-08d88f64e152") },
                    { 9, "permission", "Permission.Products.Create", new Guid("90f990b2-fb59-4c35-3e90-08d88f64e152") },
                    { 8, "permission", "Permission.Products.View", new Guid("90f990b2-fb59-4c35-3e90-08d88f64e152") },
                    { 7, "permission", "Permission.Categories.Delete", new Guid("90f990b2-fb59-4c35-3e90-08d88f64e152") },
                    { 6, "permission", "Permission.Categories.Edit", new Guid("90f990b2-fb59-4c35-3e90-08d88f64e152") },
                    { 5, "permission", "Permission.Categories.Create", new Guid("90f990b2-fb59-4c35-3e90-08d88f64e152") },
                    { 4, "permission", "Permission.Categories.View", new Guid("90f990b2-fb59-4c35-3e90-08d88f64e152") },
                    { 3, "permission", "Permission.Dashboards.View", new Guid("ec9092a5-a8b6-40e2-69e2-08d88f67500f") },
                    { 2, "permission", "Permission.Categories.View", new Guid("ec9092a5-a8b6-40e2-69e2-08d88f67500f") },
                    { 10, "permission", "Permission.Products.Edit", new Guid("90f990b2-fb59-4c35-3e90-08d88f64e152") },
                    { 21, "permission", "Permission.Products.Delete", new Guid("f58eae8e-0cba-4ed8-69e1-08d88f67500f") }
                   
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
           
        }
    }
}
