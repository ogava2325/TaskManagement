using MediatR;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Features.Task.Commands.Create;

public class CreateTaskCommand : IRequest<Guid>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? DueDate { get; set; }
    public Status Status { get; set; }
    public Priority Priority { get; set; }
    
    public Guid UserId { get; set; }
}