
using FoodMenuManagement.Data;
using FoodMenuManagement.Models;
using FoodMenuManagement.Repositories;
using FoodMenuManagement.Services;
using Microsoft.EntityFrameworkCore;

var options = new DbContextOptionsBuilder<AppDbContext>()
    .UseSqlServer(
        "Server=.\\SQLEXPRESS;Database=FoodMenuDB;Trusted_Connection=True;TrustServerCertificate=True;")
    .Options;

await using var context = new AppDbContext(options);

var repository = new FoodItemRepository(context);
var service = new FoodItemService(repository);

try
{
    var firstItem = new FoodItem
    {
        Name = "Chicken Burger",
        Category = "Fast Food",
        Price = 150,
        IsAvailable = true
    };

    int firstId = await service.AddFoodItemAsync(firstItem);
    Console.WriteLine($"First item added. ID: {firstId}");

    var duplicateItem = new FoodItem
    {
        Name = "chicken burger",
        Category = "Fast Food",
        Price = 180,
        IsAvailable = true
    };

    try
    {
        await service.AddFoodItemAsync(duplicateItem);
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Duplicate rejected: {ex.Message}");
    }

    // Update the first item without changing its name.
    firstItem.Price = 160;

    bool updated = await service.UpdateFoodItemAsync(firstItem);
    Console.WriteLine($"Same item updated: {updated}");

    // Clean up the test record.
    await repository.DeleteAsync(firstId);
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
