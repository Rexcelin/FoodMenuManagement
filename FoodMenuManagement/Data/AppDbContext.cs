
using FoodMenuManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodMenuManagement.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<FoodItem> FoodItems => Set<FoodItem>();
}
