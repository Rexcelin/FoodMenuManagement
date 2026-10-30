
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
    // Add
    var item = new FoodItem
    {
        Name = "Paneer Roll",
        Category = "Snacks",
        Description = "Fresh paneer roll",
        Price = 90,
        IsAvailable = true
    };

    int id = await service.AddFoodItemAsync(item);
    Console.WriteLine($"Added item. ID: {id}");

    // View all
    var items = await service.GetAllFoodItemsAsync();

    Console.WriteLine("\nFood Items:");

    foreach (var food in items)
    {
        Console.WriteLine(
            $"{food.Id} | {food.Name} | {food.Price}");
    }

    // View by ID
    var selected = await service.GetFoodItemByIdAsync(id);

    Console.WriteLine(
        $"\nSelected: {selected?.Name ?? "Not found"}");

    // Update
    if (selected != null)
    {
        selected.Price = 110;

        bool updated =
            await service.UpdateFoodItemAsync(selected);

        Console.WriteLine($"Updated: {updated}");
    }

    // Delete
    bool deleted = await service.DeleteFoodItemAsync(id);

    Console.WriteLine($"Deleted: {deleted}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
