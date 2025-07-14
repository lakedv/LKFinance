using BudgetService.API.Data;
using BudgetService.API.Repositories;
using BudgetService.API.Repositories.Interfaces;
using BudgetService.API.Services.Interfaces;
using BudgetService.API.Validations;
using BudgetService.API.Middleware;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using BudgetService.API.Mappings;

var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration.GetConnectionString("connString");
builder.Services.AddDbContext<BudgetDbContext>(options =>
options.UseSqlServer(connString));

// Add services to the container.
builder.Services.AddValidatorsFromAssemblyContaining<BudgetCreateRequestValidator>();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IBudgetRepository, BudgetRepository>();
builder.Services.AddScoped<IBudgetService, BudgetService.API.Services.BudgetService>();

builder.Services.AddControllers();
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

app.UseMiddleware<ExceptionMiddleware>();
app.UseUserContext();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
