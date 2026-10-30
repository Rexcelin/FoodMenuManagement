
using FoodMenuManagement.Data;
using FoodMenuManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodMenuManagement.Repositories;

public class AuditLogRepository
{
    private readonly AppDbContext _context;

    public AuditLogRepository(AppDbContext context)
    {
        _context = context;
    }

    // Add a new audit log
    public async Task<int> AddAsync(AuditLog auditLog)
    {
        await _context.AuditLogs.AddAsync(auditLog);
        await _context.SaveChangesAsync();

        return auditLog.AuditLogId;
    }

    // Get all audit logs
    public async Task<List<AuditLog>> GetAllAsync()
    {
        return await _context.AuditLogs
            .AsNoTracking()
            .OrderByDescending(log => log.PerformedAt)
            .ToListAsync();
    }

    // Get audit logs for a particular food item
    public async Task<List<AuditLog>> GetByEntityIdAsync(int entityId)
    {
        return await _context.AuditLogs
            .AsNoTracking()
            .Where(log =>
                log.EntityName == "FoodItem" &&
                log.EntityId == entityId)
            .OrderByDescending(log => log.PerformedAt)
            .ToListAsync();
    }
}
