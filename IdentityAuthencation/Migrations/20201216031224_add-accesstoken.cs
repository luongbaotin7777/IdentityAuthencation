using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityAuthencation.Migrations
{
    public partial class addaccesstoken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccessToken",
                table: "RefreshToken",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessToken",
                table: "RefreshToken");
        }
    }
}
