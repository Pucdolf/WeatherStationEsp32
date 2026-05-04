using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Scalar.AspNetCore;
using Weather32Api.Data;
using Weather32Api.Models;
using Weather32Api.Models.DTO;
using Weather32Api.Services;

var builder = WebApplication.CreateBuilder(args);
var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtSettings")["SecretKey"]);

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
    option.ConfigureWarnings(w =>
        w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Components ??= new();
        document.Components.SecuritySchemes = new Dictionary<string, IOpenApiSecurityScheme>
        {
            ["Bearer"] = new OpenApiSecurityScheme()
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Description = "Enter JWT Bearer token"
            }
        };

        document.Security =
        [
            new OpenApiSecurityRequirement
            {
                { new OpenApiSecuritySchemeReference("Bearer"), new List<string>() }
            }
        ];
        return Task.CompletedTask;
    });
});

builder.Services.AddAutoMapper(o =>
{
    o.CreateMap<WeatherDataCreateDTO, WeatherData>();
    o.CreateMap<WeatherDataDTO, WeatherData>().ReverseMap();

    o.CreateMap<UserCreateDTO, User>();
    o.CreateMap<UserUpdateDTO, User>();
    o.CreateMap<UserDTO, UserUpdateDTO>();
    o.CreateMap<UserDTO, User>().ReverseMap();

    o.CreateMap<WeatherStationCreateDTO, WeatherStation>();
    o.CreateMap<WeatherStationUpdateDTO, WeatherStation>();
    o.CreateMap<WeatherStationDTO, WeatherStation>().ReverseMap();

    o.CreateMap<WeatherStationFeaturesCreateDTO, WeatherStationFeatures>();
    o.CreateMap<WeatherStationFeaturesUpdateDTO, WeatherStationFeatures>();
    o.CreateMap<WeatherStationFeatures, WeatherStationFeaturesDTO>()
        .ForMember(dest => dest.WeatherStationName,
            opt => opt.MapFrom(src => src.WeatherStation != null ? src.WeatherStation.Name : null));
    o.CreateMap<WeatherStationFeaturesDTO, WeatherStationFeatures>();

});

builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();
await SeedDataAsync(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

static async Task SeedDataAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    await context.Database.MigrateAsync();
}

