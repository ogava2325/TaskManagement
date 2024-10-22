namespace TaskManagement.Application.Features.Task.Queries.GetAllTasksByUserId;

public class TaskDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateTime? DueDate { get; set; }
    public Guid UserId { get; set; }
}