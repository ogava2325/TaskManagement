using MediatR;
using TaskManagement.Application.Features.Task.Queries.GetAllTasksByUserId;

namespace TaskManagement.Application.Features.Task.Queries.GetTaskDetails;

public class GetTaskDetailsQuery(Guid id) : IRequest<TaskDetailsDto>
{
    public Guid Id { get; set; } = id;
}