
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

    public async Task<int> AddFoodItemAsync(FoodItem foodItem)
    {
        ValidateFoodItem(foodItem);

        return await _repository.AddAsync(foodItem);
    }

    public async Task<bool> UpdateFoodItemAsync(FoodItem foodItem)
    {
        ValidateFoodItem(foodItem);

        return await _repository.UpdateAsync(foodItem);
    }

    private static void ValidateFoodItem(FoodItem foodItem)
    {
        if (foodItem == null)
            throw new ArgumentNullException(nameof(foodItem));

        if (string.IsNullOrWhiteSpace(foodItem.Name))
            throw new ArgumentException("Food name cannot be empty.");

        if (string.IsNullOrWhiteSpace(foodItem.Category))
            throw new ArgumentException("Food category cannot be empty.");

        if (foodItem.Price <= 0)
            throw new ArgumentException("Food price must be greater than zero.");
    }
}
