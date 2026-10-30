
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

// Test 1: Valid food item
var validItem = new FoodItem
{
    Name = "Paneer Pizza",
    Category = "Pizza",
    Description = "Fresh paneer pizza",
    Price = 180,
    IsAvailable = true
};

try
{
    int id = await service.AddFoodItemAsync(validItem);
    Console.WriteLine($"Valid item added successfully. ID: {id}");

    // Clean up the test record
    await repository.DeleteAsync(id);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Validation error: {ex.Message}");
}

// Test 2: Empty food name
var invalidNameItem = new FoodItem
{
    Name = "",
    Category = "Snacks",
    Price = 50,
    IsAvailable = true
};

try
{
    await service.AddFoodItemAsync(invalidNameItem);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Test 2: {ex.Message}");
}

// Test 3: Empty category
var invalidCategoryItem = new FoodItem
{
    Name = "Burger",
    Category = " ",
    Price = 100,
    IsAvailable = true
};

try
{
    await service.AddFoodItemAsync(invalidCategoryItem);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Test 3: {ex.Message}");
}

// Test 4: Invalid price
var invalidPriceItem = new FoodItem
{
    Name = "Sandwich",
    Category = "Snacks",
    Price = 0,
    IsAvailable = true
};

try
{
    await service.AddFoodItemAsync(invalidPriceItem);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Test 4: {ex.Message}");
}
