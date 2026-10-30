
using FoodMenuManagement.Data;
using FoodMenuManagement.Models;
using FoodMenuManagement.Repositories;
using Microsoft.EntityFrameworkCore;

var options = new DbContextOptionsBuilder<AppDbContext>()
    .UseSqlServer(
        "Server=.\\SQLEXPRESS;Database=FoodMenuDB;Trusted_Connection=True;TrustServerCertificate=True;")
    .Options;

await using var context = new AppDbContext(options);

var repository = new FoodItemRepository(context);

// 1. ADD
var foodItem = new FoodItem
{
    Name = "Veg Burger",
    Category = "Fast Food",
    Description = "Fresh vegetable burger",
    Price = 120,
    IsAvailable = true
};

int id = await repository.AddAsync(foodItem);
Console.WriteLine($"Added successfully. ID: {id}");

// 2. VIEW ALL
var items = await repository.GetAllAsync();

Console.WriteLine("\nAll Food Items:");

foreach (var item in items)
{
    Console.WriteLine(
        $"{item.Id} | {item.Name} | {item.Category} | {item.Price}");
}

// 3. VIEW BY ID
var selectedItem = await repository.GetByIdAsync(id);

Console.WriteLine(
    $"\nSelected Item: {selectedItem?.Name ?? "Not found"}");

// 4. UPDATE
if (selectedItem != null)
{
    selectedItem.Price = 150;
    selectedItem.Description = "Updated vegetable burger";

    bool updated = await repository.UpdateAsync(selectedItem);

    Console.WriteLine($"Updated: {updated}");
}

// 5. DELETE
bool deleted = await repository.DeleteAsync(id);

Console.WriteLine($"Deleted: {deleted}");
