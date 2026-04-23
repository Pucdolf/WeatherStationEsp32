using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Weather32Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedWeatherData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "WeatherRecords",
                columns: new[] { "Id", "DSTemperatureF", "DSTemperatureK", "DhtTemperatureC", "DhtTemperatureF", "DhtTemperatureK", "DsTemperatureC", "Humidity", "TimeStamp" },
                values: new object[,]
                {
                    { 1, 74.8f, 296.9f, 24.5f, 76.1f, 297.6f, 23.8f, 45f, new DateTime(2024, 3, 18, 8, 30, 0, 0, DateTimeKind.Utc) },
                    { 2, 75.6f, 297.3f, 25.1f, 77.2f, 298.2f, 24.2f, 42f, new DateTime(2024, 3, 18, 12, 45, 12, 0, DateTimeKind.Utc) },
                    { 3, 75.2f, 297.1f, 24.8f, 76.6f, 297.9f, 24f, 44f, new DateTime(2024, 3, 19, 9, 15, 20, 0, DateTimeKind.Utc) },
                    { 4, 74.3f, 296.6f, 24.2f, 75.6f, 297.3f, 23.5f, 46f, new DateTime(2024, 3, 19, 18, 20, 30, 0, DateTimeKind.Utc) },
                    { 5, 73.8f, 296.3f, 23.9f, 75f, 297f, 23.2f, 47f, new DateTime(2024, 3, 20, 7, 10, 45, 0, DateTimeKind.Utc) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WeatherRecords",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WeatherRecords",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WeatherRecords",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "WeatherRecords",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "WeatherRecords",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
