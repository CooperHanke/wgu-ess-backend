using Microsoft.EntityFrameworkCore.Migrations;

namespace WGU_ESS.API.Migrations
{
    public partial class AddResetPasswordFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "NeedPasswordReset",
                table: "Users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NeedPasswordReset",
                table: "Users");
        }
    }
}
