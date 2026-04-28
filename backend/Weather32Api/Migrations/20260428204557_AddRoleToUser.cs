using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weather32Api.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Role", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 28, 20, 45, 57, 235, DateTimeKind.Utc).AddTicks(695), "Admin", new DateTime(2026, 4, 28, 20, 45, 57, 235, DateTimeKind.Utc).AddTicks(1015) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Role", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 28, 20, 45, 57, 235, DateTimeKind.Utc).AddTicks(1303), "User", new DateTime(2026, 4, 28, 20, 45, 57, 235, DateTimeKind.Utc).AddTicks(1303) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 27, 13, 51, 59, 878, DateTimeKind.Utc).AddTicks(2852), new DateTime(2026, 4, 27, 13, 51, 59, 878, DateTimeKind.Utc).AddTicks(3153) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 27, 13, 51, 59, 878, DateTimeKind.Utc).AddTicks(3434), new DateTime(2026, 4, 27, 13, 51, 59, 878, DateTimeKind.Utc).AddTicks(3434) });
        }
    }
}
