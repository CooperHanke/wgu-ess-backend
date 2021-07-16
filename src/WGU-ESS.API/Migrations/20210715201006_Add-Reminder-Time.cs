using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WGU_ESS.API.Migrations
{
    public partial class AddReminderTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ReminderTime",
                table: "Appointments",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreationTime", "FirstName", "IsHidden", "IsLocked", "LastName", "ModificationTime", "NeedPasswordReset", "Password", "Type", "UserName", "UsesDarkMode" },
                values: new object[] { new Guid("7267a7db-7bd3-45c4-869c-5d72804634ca"), new DateTime(2021, 7, 15, 20, 10, 6, 9, DateTimeKind.Utc).AddTicks(8519), "Super", false, false, "User", new DateTime(2021, 7, 15, 20, 10, 6, 9, DateTimeKind.Utc).AddTicks(8742), false, "10000.UlUzXM4ddGiwY/Us1TTOIw==.Vir0S/Ac6LaOCOmPK6u9slc8JoRToUN+zHYU7DeTVKs=", "Manager", "administrator", false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7267a7db-7bd3-45c4-869c-5d72804634ca"));

            migrationBuilder.DropColumn(
                name: "ReminderTime",
                table: "Appointments");
        }
    }
}
