using MediatR;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Features.Task.Commands.Create;

public class CreateTaskCommandHandler(
    ITaskRepository taskRepository)
    : IRequestHandler<CreateTaskCommand, Guid>
{
    private readonly ITaskRepository _taskRepository = taskRepository;

    public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = new Domain.Entities.Task
        {
            Title = request.Title,
            Description = request.Description,
            DueDate = request.DueDate,
            Priority = request.Priority,
            Status = request.Status,
            UserId = request.UserId
        };

        await _taskRepository.CreateAsync(task);

        return task.Id;
    }
}