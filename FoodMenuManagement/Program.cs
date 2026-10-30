
using FoodMenuManagement.Data;
using FoodMenuManagement.Repositories;
using FoodMenuManagement.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

// Read connection string from appsettings.json
var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "Database connection string is missing.");
}

// Register database context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Register repository
builder.Services.AddScoped<FoodItemRepository>();

// Register service
builder.Services.AddScoped<FoodItemService>();

// Build application
using var host = builder.Build();

// Create dependency injection scope
using var scope = host.Services.CreateScope();

// Get service automatically from DI
var service =
    scope.ServiceProvider.GetRequiredService<FoodItemService>();

Console.WriteLine("==================================");
Console.WriteLine("   FOOD MENU MANAGEMENT SYSTEM");
Console.WriteLine("==================================");
Console.WriteLine("Dependency Injection configured!");

// Test database connection
bool canConnect = await scope.ServiceProvider
    .GetRequiredService<AppDbContext>()
    .Database.CanConnectAsync();

Console.WriteLine(
    canConnect
        ? "Database connection successful."
        : "Database connection failed.");
