using Microsoft.EntityFrameworkCore;
using Weather32Api.Models;

namespace Weather32Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<WeatherData> WeatherRecords { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
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
                TimeStamp = new DateTime(2024, 3, 18, 8, 30, 0, DateTimeKind.Utc)
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
                TimeStamp = new DateTime(2024, 3, 18, 12, 45, 12, DateTimeKind.Utc)
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
                TimeStamp = new DateTime(2024, 3, 19, 9, 15, 20, DateTimeKind.Utc)
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
                TimeStamp = new DateTime(2024, 3, 19, 18, 20, 30, DateTimeKind.Utc)
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
                TimeStamp = new DateTime(2024, 3, 20, 7, 10, 45, DateTimeKind.Utc)
            }
        );

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Username = "Pucdolf",
                Email = "pucdolf@example.com",
                Password = "password",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow

            },

            new User
            {
                Id = 2,
                Username = "PGKuba",
                Email = "pgkuba@example.com",
                Password = "1234",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }

    );
    }
}