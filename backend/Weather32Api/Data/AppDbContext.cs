using Microsoft.EntityFrameworkCore;
using Weather32Api.Models;

namespace Weather32Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<WeatherStation> WeatherStations { get; set; }
    public DbSet<WeatherStationFeatures> WeatherStationFeatures { get; set; }
    public DbSet<WeatherData> WeatherRecords { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Username = "Pucdolf",
                Email = "pucdolf@example.com",
                Password = "password",
                Role = "Admin",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new User
            {
                Id = 2,
                Username = "PGKuba",
                Email = "pgkuba@example.com",
                Password = "1234",
                Role = "User",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            }
        );

        modelBuilder.Entity<WeatherStation>().HasData(
            new WeatherStation
            {
                Id = 1,
                Name = "PucekStation",
                Location = "Bielsko",
                UserId = 1,
            },
            new WeatherStation
            {
                Id = 2,
                Name = "PGKubaStation",
                Location = "Rybki",
                UserId = 2
            }
        );
// 3. Seed WeatherStationFeatures
modelBuilder.Entity<WeatherStationFeatures>().HasData(
    new WeatherStationFeatures { Id = 1, WeatherStationId = 1, Name = "DHT11", Description = "Temperature and humidity sensor" },
    new WeatherStationFeatures { Id = 2, WeatherStationId = 1, Name = "DS18B20", Description = "High-precision temperature sensor" },
    new WeatherStationFeatures { Id = 3, WeatherStationId = 2, Name = "DHT11", Description = "Temperature and humidity sensor" }
);

        modelBuilder.Entity<WeatherData>().HasData(
            new WeatherData
            {
                Id = 1,
                DhtTemperatureC = 24.5f,
                DhtTemperatureF = 76.1f,
                DhtTemperatureK = 297.6f,
                Humidity = 45f,
                DsTemperatureC = 23.8f,
                DSTemperatureF = 74.8f,
                DSTemperatureK = 296.9f,
                TimeStamp = new DateTime(2024, 3, 18, 8, 30, 0, DateTimeKind.Utc),
                WeatherStationId = 1
            },
            new WeatherData
            {
                Id = 2,
                DhtTemperatureC = 25.1f,
                DhtTemperatureF = 77.2f,
                DhtTemperatureK = 298.2f,
                Humidity = 42f,
                DsTemperatureC = 24.2f,
                DSTemperatureF = 75.6f,
                DSTemperatureK = 297.3f,
                TimeStamp = new DateTime(2024, 3, 18, 12, 45, 12, DateTimeKind.Utc),
                WeatherStationId = 1
            },
            new WeatherData
            {
                Id = 3,
                DhtTemperatureC = 24.8f,
                DhtTemperatureF = 76.6f,
                DhtTemperatureK = 297.9f,
                Humidity = 44f,
                DsTemperatureC = 24.0f,
                DSTemperatureF = 75.2f,
                DSTemperatureK = 297.1f,
                TimeStamp = new DateTime(2024, 3, 19, 9, 15, 20, DateTimeKind.Utc),
                WeatherStationId = 1
            },
            new WeatherData
            {
                Id = 4,
                DhtTemperatureC = 24.2f,
                DhtTemperatureF = 75.6f,
                DhtTemperatureK = 297.3f,
                Humidity = 46f,
                DsTemperatureC = 23.5f,
                DSTemperatureF = 74.3f,
                DSTemperatureK = 296.6f,
                TimeStamp = new DateTime(2024, 3, 19, 18, 20, 30, DateTimeKind.Utc),
                WeatherStationId = 2
            },
            new WeatherData
            {
                Id = 5,
                DhtTemperatureC = 23.9f,
                DhtTemperatureF = 75.0f,
                DhtTemperatureK = 297.0f,
                Humidity = 47f,
                DsTemperatureC = 23.2f,
                DSTemperatureF = 73.8f,
                DSTemperatureK = 296.3f,
                TimeStamp = new DateTime(2024, 3, 20, 7, 10, 45, DateTimeKind.Utc),
                WeatherStationId = 2
            }
        );
    }
}
