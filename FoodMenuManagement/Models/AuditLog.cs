
namespace FoodMenuManagement.Models;

public class AuditLog
{
    public int AuditLogId { get; set; }

    public string EntityName { get; set; } = string.Empty;

    public int EntityId { get; set; }

    public string Action { get; set; } = string.Empty;

    public string Details { get; set; } = string.Empty;

    public string? OldValues { get; set; }

    public string? NewValues { get; set; }

    public DateTime PerformedAt { get; set; } = DateTime.UtcNow;
}
