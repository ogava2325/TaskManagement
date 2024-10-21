using TaskManagement.Domain.Enums;

namespace TaskManagement.Domain.Entities;

public class Task : AuditableEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? DueDate { get; set; }
    public Status Status { get; set; }
    public Priority Priority { get; set; }
    public Guid UserId { get; set; }
}