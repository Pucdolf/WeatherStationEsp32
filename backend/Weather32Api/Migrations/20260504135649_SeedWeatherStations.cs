using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Weather32Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedWeatherStations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "WeatherStations",
                columns: new[] { "Id", "Location", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, "Bielsko", "PucekStation", 1 },
                    { 2, "Rybki", "PGKubaStation", 2 }
                });

            migrationBuilder.InsertData(
                table: "WeatherStationFeatures",
                columns: new[] { "Id", "Description", "Name", "WeatherStationId" },
                values: new object[,]
                {
                    { 1, "Temperature and humidity sensor", "DHT11", 1 },
                    { 2, "High-precision temperature sensor", "DS18B20", 1 },
                    { 3, "Temperature and humidity sensor", "DHT11", 2 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 4, 13, 56, 48, 957, DateTimeKind.Utc).AddTicks(668), new DateTime(2026, 5, 4, 13, 56, 48, 957, DateTimeKind.Utc).AddTicks(989) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 4, 13, 56, 48, 957, DateTimeKind.Utc).AddTicks(1292), new DateTime(2026, 5, 4, 13, 56, 48, 957, DateTimeKind.Utc).AddTicks(1292) });

            migrationBuilder.UpdateData(
                table: "WeatherRecords",
                keyColumn: "Id",
                keyValue: 1,
                column: "WeatherStationId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "WeatherRecords",
                keyColumn: "Id",
                keyValue: 2,
                column: "WeatherStationId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "WeatherRecords",
                keyColumn: "Id",
                keyValue: 3,
                column: "WeatherStationId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "WeatherRecords",
                keyColumn: "Id",
                keyValue: 4,
                column: "WeatherStationId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "WeatherRecords",
                keyColumn: "Id",
                keyValue: 5,
                column: "WeatherStationId",
                value: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WeatherStationFeatures",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WeatherStationFeatures",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WeatherStationFeatures",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "WeatherStations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WeatherStations",
                keyColumn: "Id",
                keyValue: 2);

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

            migrationBuilder.UpdateData(
                table: "WeatherRecords",
                keyColumn: "Id",
                keyValue: 1,
                column: "WeatherStationId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "WeatherRecords",
                keyColumn: "Id",
                keyValue: 2,
                column: "WeatherStationId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "WeatherRecords",
                keyColumn: "Id",
                keyValue: 3,
                column: "WeatherStationId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "WeatherRecords",
                keyColumn: "Id",
                keyValue: 4,
                column: "WeatherStationId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "WeatherRecords",
                keyColumn: "Id",
                keyValue: 5,
                column: "WeatherStationId",
                value: 0);
        }
    }
}
