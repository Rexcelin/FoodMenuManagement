
using FoodMenuManagement.Models;
using FoodMenuManagement.Repositories;

namespace FoodMenuManagement.Services;

public class AuditLogService
{
    private readonly AuditLogRepository _repository;

    public AuditLogService(AuditLogRepository repository)
    {
        _repository = repository;
    }

    // Create an audit log
    public async Task<int> AddAuditLogAsync(AuditLog auditLog)
    {
        if (auditLog == null)
        {
            throw new ArgumentNullException(nameof(auditLog));
        }

        if (string.IsNullOrWhiteSpace(auditLog.EntityName))
        {
            throw new ArgumentException("Entity name is required.");
        }

        if (auditLog.EntityId <= 0)
        {
            throw new ArgumentException("Entity ID must be greater than zero.");
        }

        if (string.IsNullOrWhiteSpace(auditLog.Action))
        {
            throw new ArgumentException("Action is required.");
        }

        if (string.IsNullOrWhiteSpace(auditLog.Details))
        {
            throw new ArgumentException("Audit details are required.");
        }

        if (auditLog.PerformedAt == default)
        {
            auditLog.PerformedAt = DateTime.UtcNow;
        }

        return await _repository.AddAsync(auditLog);
    }

    // Get all audit logs
    public async Task<List<AuditLog>> GetAllAuditLogsAsync()
    {
        return await _repository.GetAllAsync();
    }

    // Get audit history for a food item
    public async Task<List<AuditLog>> GetFoodItemAuditLogsAsync(int foodItemId)
    {
        if (foodItemId <= 0)
        {
            throw new ArgumentException(
                "Food item ID must be greater than zero.");
        }

        return await _repository.GetByEntityIdAsync(foodItemId);
    }
}
