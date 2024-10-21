using MediatR;
using TaskManagement.Application.Features.Task.Queries.GetAllTasksByUserId;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Features.Task.Queries.GetAllLeaveTypesByUserId;

public class GetAllTasksByUserIdQueryHandler(ITaskRepository taskRepository)
    : IRequestHandler<GetAllTasksByUserIdQuery, List<Domain.Entities.Task>>
{
    private readonly ITaskRepository _taskRepository = taskRepository;

    public async Task<List<Domain.Entities.Task>> Handle(GetAllTasksByUserIdQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository.GetTasksByUserIdAsync(request.UserId);

        return tasks.ToList();
    }
}