using AuthService.API.Data;
using AuthService.API.Middleware;
using AuthService.API.Models;
using AuthService.API.Repositories;
using AuthService.API.Repositories.Interfaces;
using AuthService.API.Services;
using AuthService.API.Services.Interfaces;
using AuthService.API.Validations;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("connString");
Console.WriteLine($"ConnectionString: {connString}");

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("connString")));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();
// Add services to the container.

builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection("Jwt"));

builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();

builder.Services.AddControllers();

//Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

//Services
builder.Services.AddScoped<UserService>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "LKFinance API",
        Version = "v1"
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();
app.MapGet("/db-status", async (AuthDbContext db) =>
{
    try
    {
        await db.Database.OpenConnectionAsync();
        await db.Database.CloseConnectionAsync();
        return Results.Ok("Conexion con db OK.");
    }
    catch (Exception ex)
    {
        return Results.Problem("Error de conexion:" + ex.Message);
    }
});
app.Run();
