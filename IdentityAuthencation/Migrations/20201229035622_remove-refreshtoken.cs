using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityAuthencation.Migrations
{
    public partial class removerefreshtoken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessToken",
                table: "RefreshToken");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccessToken",
                table: "RefreshToken",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
