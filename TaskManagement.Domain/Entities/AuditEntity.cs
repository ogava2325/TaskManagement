namespace TaskManagement.Domain.Entities;

public abstract class AuditEntity
{
    public DateTime CreatedAtUtc { get; private set; }
    public DateTime? UpdatedAtUtc { get; private set; }
}