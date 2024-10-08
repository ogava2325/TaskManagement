using TaskManagement.Domain.Enums;
using TaskStatus = System.Threading.Tasks.TaskStatus;

namespace TaskManagement.Domain.Entities;

public class Task : AuditableEntity
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime? DueDate { get; private set; }
    public TaskStatus Status { get; private set; }
    public TaskPriority Priority { get; private set; }
    public Guid UserId { get; private set; }
}