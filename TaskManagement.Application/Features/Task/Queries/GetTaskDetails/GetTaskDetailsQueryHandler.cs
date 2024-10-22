using AutoMapper;
using MediatR;
using TaskManagement.Application.Features.Task.Queries.GetAllTasksByUserId;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Features.Task.Queries.GetTaskDetails;

public class GetTaskDetailsQueryHandler(
    IMapper mapper, 
    ITaskRepository taskRepository)
    : IRequestHandler<GetTaskDetailsQuery, TaskDetailsDto>
{
    private readonly IMapper _mapper = mapper;
    private readonly ITaskRepository _taskRepository = taskRepository;

    public async Task<TaskDetailsDto> Handle(GetTaskDetailsQuery request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id);

        return _mapper.Map<TaskDetailsDto>(task);
    }
}