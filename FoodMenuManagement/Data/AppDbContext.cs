
using FoodMenuManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodMenuManagement.Data;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(
                "Server=.\\SQLEXPRESS;Database=FoodMenuDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }

    public DbSet<FoodItem> FoodItems => Set<FoodItem>();

    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();


}
