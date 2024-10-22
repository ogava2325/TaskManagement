using MediatR;

namespace TaskManagement.Application.Features.Task.Queries.GetAllTasksByUserId;

public class GetAllTasksByUserIdQuery : IRequest<List<TaskDto>>
{
    public Guid UserId { get; set; }
}
