using MediatR;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Features.Task.Queries.GetTaskDetails;

public class GetTaskDetailsQueryHandler(ITaskRepository taskRepository)
    : IRequestHandler<GetTaskDetailsQuery, Domain.Entities.Task>
{
    private readonly ITaskRepository _taskRepository = taskRepository;

    public async Task<Domain.Entities.Task> Handle(GetTaskDetailsQuery request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id);

        return task;
    }
}