
using FoodMenuManagement.Data;
using FoodMenuManagement.Models;
using FoodMenuManagement.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace FoodMenuManagement.Services;

public class FoodItemService
{
    private readonly FoodItemRepository _repository;
    private readonly AuditLogService _auditLogService;
    private readonly AppDbContext _context;

    public FoodItemService(
        FoodItemRepository repository,
        AuditLogService auditLogService,
        AppDbContext context)
    {
        _repository = repository;
        _auditLogService = auditLogService;
        _context = context;
    }

    // Add food item with audit logging
    public async Task<int> AddFoodItemAsync(FoodItem foodItem)
    {
        ValidateFoodItem(foodItem);

        string name = foodItem.Name.Trim();

        await using var transaction =
            await _context.Database.BeginTransactionAsync();

        try
        {
            bool exists = await _repository.ExistsByNameAsync(name);

            if (exists)
            {
                throw new ArgumentException(
                    "A food item with this name already exists.");
            }

            foodItem.Name = name;

            int id = await _repository.AddAsync(foodItem);

            await _auditLogService.AddAuditLogAsync(new AuditLog
            {
                EntityName = "FoodItem",
                EntityId = id,
                Action = "CREATE",
                Details = $"Food item '{foodItem.Name}' was added.",
                NewValues = JsonSerializer.Serialize(foodItem),
                PerformedAt = DateTime.UtcNow
            });

            await transaction.CommitAsync();

            return id;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
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

    // Update food item with audit logging
    public async Task<bool> UpdateFoodItemAsync(FoodItem foodItem)
    {
        ValidateFoodItem(foodItem);

        if (foodItem.Id <= 0)
            throw new ArgumentException("Invalid food item ID.");

        string name = foodItem.Name.Trim();

        await using var transaction =
            await _context.Database.BeginTransactionAsync();

        try
        {
            var existingItem =
                await _repository.GetByIdAsync(foodItem.Id);

            if (existingItem == null)
            {
                await transaction.RollbackAsync();
                return false;
            }

            bool exists = await _repository.ExistsByNameAsync(
                name,
                foodItem.Id);

            if (exists)
            {
                throw new ArgumentException(
                    "Another food item with this name already exists.");
            }

            string oldValues = JsonSerializer.Serialize(existingItem);

            foodItem.Name = name;

            bool updated = await _repository.UpdateAsync(foodItem);

            if (!updated)
            {
                await transaction.RollbackAsync();
                return false;
            }

            await _auditLogService.AddAuditLogAsync(new AuditLog
            {
                EntityName = "FoodItem",
                EntityId = foodItem.Id,
                Action = "UPDATE",
                Details = $"Food item '{foodItem.Name}' was updated.",
                OldValues = oldValues,
                NewValues = JsonSerializer.Serialize(foodItem),
                PerformedAt = DateTime.UtcNow
            });

            await transaction.CommitAsync();

            return true;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    // Delete food item with audit logging
    public async Task<bool> DeleteFoodItemAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid food item ID.");

        await using var transaction =
            await _context.Database.BeginTransactionAsync();

        try
        {
            var existingItem = await _repository.GetByIdAsync(id);

            if (existingItem == null)
            {
                await transaction.RollbackAsync();
                return false;
            }

            string oldValues = JsonSerializer.Serialize(existingItem);

            bool deleted = await _repository.DeleteAsync(id);

            if (!deleted)
            {
                await transaction.RollbackAsync();
                return false;
            }

            await _auditLogService.AddAuditLogAsync(new AuditLog
            {
                EntityName = "FoodItem",
                EntityId = id,
                Action = "DELETE",
                Details = $"Food item '{existingItem.Name}' was deleted.",
                OldValues = oldValues,
                PerformedAt = DateTime.UtcNow
            });

            await transaction.CommitAsync();

            return true;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    // Get filtered food items
    public async Task<List<FoodItem>> GetFilteredFoodItemsAsync(
        string? category,
        bool availableOnly)
    {
        if (!string.IsNullOrWhiteSpace(category))
        {
            category = category.Trim();
        }

        return await _repository.GetFilteredAsync(
            category,
            availableOnly);
    }

    // Validation
    private static void ValidateFoodItem(FoodItem foodItem)
    {
        if (foodItem == null)
            throw new ArgumentNullException(nameof(foodItem));

        if (string.IsNullOrWhiteSpace(foodItem.Name))
            throw new ArgumentException("Food name cannot be empty.");

        if (string.IsNullOrWhiteSpace(foodItem.Category))
            throw new ArgumentException("Food category cannot be empty.");

        if (foodItem.Price <= 0)
            throw new ArgumentException(
                "Food price must be greater than zero.");
    }
}
