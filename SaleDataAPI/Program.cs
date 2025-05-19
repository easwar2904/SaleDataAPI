using Microsoft.EntityFrameworkCore;
using SaleDataAPI.BusinesslogicLayer;
using SaleDataAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
var connectionString = "Server=localhost;Database=SalesDb;User=root;Password=**;";

builder.Services.AddDbContext<SalesDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddScoped<ICsvRefreshService, CsvRefreshService>();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
