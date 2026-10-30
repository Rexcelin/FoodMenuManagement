
using FoodMenuManagement.Data;
using FoodMenuManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodMenuManagement.Repositories;

public class FoodItemRepository
{
    private readonly AppDbContext _context;

    public FoodItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<FoodItem>> GetAllAsync()
    {
        return await _context.FoodItems
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<FoodItem?> GetByIdAsync(int id)
    {
        return await _context.FoodItems
            .AsNoTracking()
            .FirstOrDefaultAsync(item => item.Id == id);
    }

    public async Task<int> AddAsync(FoodItem foodItem)
    {
        await _context.FoodItems.AddAsync(foodItem);
        await _context.SaveChangesAsync();

        return foodItem.Id;
    }

    public async Task<bool> UpdateAsync(FoodItem foodItem)
    {
        var existingItem = await _context.FoodItems
            .FindAsync(foodItem.Id);

        if (existingItem == null)
            return false;

        existingItem.Name = foodItem.Name;
        existingItem.Category = foodItem.Category;
        existingItem.Description = foodItem.Description;
        existingItem.Price = foodItem.Price;
        existingItem.IsAvailable = foodItem.IsAvailable;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existingItem = await _context.FoodItems
            .FindAsync(id);

        if (existingItem == null)
            return false;

        _context.FoodItems.Remove(existingItem);
        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> ExistsByNameAsync(
        string name,
        int? excludeId = null)
    {
        return await _context.FoodItems.AnyAsync(item =>
            item.Name.ToLower() == name.ToLower() &&
            (excludeId == null || item.Id != excludeId));
    }

    public async Task<List<FoodItem>> GetFilteredAsync(
    string? category,
    bool availableOnly)
    {
        var query = _context.FoodItems.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(category))
        {
            query = query.Where(item =>
                item.Category.ToLower() == category.Trim().ToLower());
        }

        if (availableOnly)
        {
            query = query.Where(item => item.IsAvailable);
        }

        return await query.ToListAsync();
    }

}
