
using FoodMenuManagement.Data;
using FoodMenuManagement.Repositories;
using FoodMenuManagement.Services;
using FoodMenuManagement.Menus;
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

builder.Services.AddScoped<FoodMenu>();

using var host = builder.Build();

using var scope = host.Services.CreateScope();

var menu = scope.ServiceProvider
    .GetRequiredService<FoodMenu>();

await menu.RunAsync();