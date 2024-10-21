using MediatR;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Features.Task.Commands.Update;

public class UpdateTaskCommandHandler(
    ITaskRepository taskRepository)
    : IRequestHandler<UpdateTaskCommand, Unit>
{
    private readonly ITaskRepository _taskRepository = taskRepository;

    public async Task<Unit> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    { 
        // Retrieve the task from the repository using the ID
        var task = await _taskRepository.GetByIdAsync(request.Id);

        // Update the properties of the task
        task.Title = request.Title;
        task.Description = request.Description;
        task.DueDate = request.DueDate;
        task.Priority = request.Priority;
        task.Status = request.Status;
        task.UserId = request.UserId;  // Ensure this property is relevant for the update

        
        await _taskRepository.UpdateAsync(task);
        return Unit.Value;
    } 
}