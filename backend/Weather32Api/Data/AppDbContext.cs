using Microsoft.EntityFrameworkCore;
using WeatherStationApi.Models;

namespace WeatherStationApi.Data;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }
  public DbSet<WeatherData> WeatherRecords{get; set;}
}