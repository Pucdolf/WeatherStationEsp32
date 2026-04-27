using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Weather32Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Password", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 4, 27, 12, 33, 48, 474, DateTimeKind.Utc).AddTicks(3999), "pucdolf@example.com", "password", "Pucdolf" },
                    { 2, new DateTime(2026, 4, 27, 12, 33, 48, 474, DateTimeKind.Utc).AddTicks(4294), "pgkuba@example.com", "1234", "PGKuba" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
