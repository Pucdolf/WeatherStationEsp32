using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Weather32Api.Data;
using Weather32Api.DTO;
using Weather32Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
    option.ConfigureWarnings(w =>
        w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddAutoMapper(o =>
{
    o.CreateMap<WeatherDataCreateDTO, WeatherData>();
    o.CreateMap<WeatherDataDTO, WeatherData>().ReverseMap();
    o.CreateMap<UserCreateDTO, User>();
    o.CreateMap<UserUpdateDTO, User>();
    o.CreateMap<UserDTO, User>().ReverseMap();
    o.CreateMap<UserDTO, UserUpdateDTO>();
});

var app = builder.Build();
await SeedDataAsync(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapControllers();
app.Run();

static async Task SeedDataAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    await context.Database.MigrateAsync();
}