
using FoodMenuManagement.Models;
using FoodMenuManagement.Repositories;

namespace FoodMenuManagement.Services;

public class FoodItemService
{
    private readonly FoodItemRepository _repository;

    public FoodItemService(FoodItemRepository repository)
    {
        _repository = repository;
    }

    // Add food item
    public async Task<int> AddFoodItemAsync(FoodItem foodItem)
    {
        ValidateFoodItem(foodItem);

        string name = foodItem.Name.Trim();

        bool exists = await _repository.ExistsByNameAsync(name);

        if (exists)
        {
            throw new ArgumentException(
                "A food item with this name already exists.");
        }

        foodItem.Name = name;

        return await _repository.AddAsync(foodItem);
    }

    // Get all food items
    public async Task<List<FoodItem>> GetAllFoodItemsAsync()
    {
        return await _repository.GetAllAsync();
    }

    // Get food item by ID
    public async Task<FoodItem?> GetFoodItemByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid food item ID.");

        return await _repository.GetByIdAsync(id);
    }

    // Update food item
    public async Task<bool> UpdateFoodItemAsync(FoodItem foodItem)
    {
        ValidateFoodItem(foodItem);

        if (foodItem.Id <= 0)
            throw new ArgumentException("Invalid food item ID.");

        string name = foodItem.Name.Trim();

        bool exists = await _repository.ExistsByNameAsync(
            name,
            foodItem.Id);

        if (exists)
        {
            throw new ArgumentException(
                "Another food item with this name already exists.");
        }

        foodItem.Name = name;

        return await _repository.UpdateAsync(foodItem);
    }

    // Delete food item
    public async Task<bool> DeleteFoodItemAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid food item ID.");

        return await _repository.DeleteAsync(id);
    }

    // Validation
    private static void ValidateFoodItem(FoodItem foodItem)
    {
        if (foodItem == null)
            throw new ArgumentNullException(nameof(foodItem));

        if (string.IsNullOrWhiteSpace(foodItem.Name))
            throw new ArgumentException(
                "Food name cannot be empty.");

        if (string.IsNullOrWhiteSpace(foodItem.Category))
            throw new ArgumentException(
                "Food category cannot be empty.");

        if (foodItem.Price <= 0)
            throw new ArgumentException(
                "Food price must be greater than zero.");
    }

    public async Task<List<FoodItem>> GetFilteredFoodItemsAsync(
    string? category,
    bool availableOnly)
    {
        if (!string.IsNullOrWhiteSpace(category))
        {
            category = category.Trim();
        }

        return await _repository.GetFilteredAsync(category, availableOnly);
    }
}
