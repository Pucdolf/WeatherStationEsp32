using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weather32Api.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdatedAtToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 12, 33, 48, 474, DateTimeKind.Utc).AddTicks(3999));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 12, 33, 48, 474, DateTimeKind.Utc).AddTicks(4294));
        }
    }
}
