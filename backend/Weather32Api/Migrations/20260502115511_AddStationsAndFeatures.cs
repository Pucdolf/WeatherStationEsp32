using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Weather32Api.Migrations
{
    /// <inheritdoc />
    public partial class AddStationsAndFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WeatherStationId",
                table: "WeatherRecords",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WeatherStations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherStations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherStations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeatherStationFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    WeatherStationId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherStationFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherStationFeatures_WeatherStations_WeatherStationId",
                        column: x => x.WeatherStationId,
                        principalTable: "WeatherStations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 2, 11, 55, 9, 921, DateTimeKind.Utc).AddTicks(5552), new DateTime(2026, 5, 2, 11, 55, 9, 921, DateTimeKind.Utc).AddTicks(6119) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 2, 11, 55, 9, 921, DateTimeKind.Utc).AddTicks(6662), new DateTime(2026, 5, 2, 11, 55, 9, 921, DateTimeKind.Utc).AddTicks(6663) });

            migrationBuilder.CreateIndex(
                name: "IX_WeatherRecords_WeatherStationId",
                table: "WeatherRecords",
                column: "WeatherStationId");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherStationFeatures_WeatherStationId",
                table: "WeatherStationFeatures",
                column: "WeatherStationId");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherStations_UserId",
                table: "WeatherStations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherRecords_WeatherStations_WeatherStationId",
                table: "WeatherRecords",
                column: "WeatherStationId",
                principalTable: "WeatherStations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeatherRecords_WeatherStations_WeatherStationId",
                table: "WeatherRecords");

            migrationBuilder.DropTable(
                name: "WeatherStationFeatures");

            migrationBuilder.DropTable(
                name: "WeatherStations");

            migrationBuilder.DropIndex(
                name: "IX_WeatherRecords_WeatherStationId",
                table: "WeatherRecords");

            migrationBuilder.DropColumn(
                name: "WeatherStationId",
                table: "WeatherRecords");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 28, 20, 45, 57, 235, DateTimeKind.Utc).AddTicks(695), new DateTime(2026, 4, 28, 20, 45, 57, 235, DateTimeKind.Utc).AddTicks(1015) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 28, 20, 45, 57, 235, DateTimeKind.Utc).AddTicks(1303), new DateTime(2026, 4, 28, 20, 45, 57, 235, DateTimeKind.Utc).AddTicks(1303) });
        }
    }
}
