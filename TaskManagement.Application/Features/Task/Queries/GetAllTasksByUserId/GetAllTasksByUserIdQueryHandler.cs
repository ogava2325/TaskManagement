using AutoMapper;
using MediatR;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Features.Task.Queries.GetAllTasksByUserId;

public class GetAllTasksByUserIdQueryHandler(
    ITaskRepository taskRepository, 
    IMapper mapper)
    : IRequestHandler<GetAllTasksByUserIdQuery, List<TaskDto>>
{
    private readonly ITaskRepository _taskRepository = taskRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<List<TaskDto>> Handle(GetAllTasksByUserIdQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository.GetTasksByUserIdAsync(request.UserId);
        return _mapper.Map<List<TaskDto>>(tasks);
    }
}