using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Common.Filtering;

public class TaskFilter
{
    public Status? Status { get; set; }
    public DateTime? DueDate { get; set; }
    public Priority? Priority { get; set; }
    public SortOrder SortOrder { get; set; } = SortOrder.None; 
    public SortBy SortBy { get; set; } = SortBy.None; 
}