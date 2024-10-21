using MediatR;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Features.Task.Commands.Delete;

public class DeleteTaskCommandHandler(ITaskRepository taskRepository) : IRequestHandler<DeleteTaskCommand, Unit>
{
    private readonly ITaskRepository _taskRepository = taskRepository;

    public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id);
        
        ArgumentNullException.ThrowIfNull(task);

        await _taskRepository.DeleteAsync(task);

        return Unit.Value;
    }
}