using AutoMapper;
using MediatR;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Features.Task.Commands.Create;

public class CreateTaskCommandHandler(
    IMapper mapper,
    ITaskRepository taskRepository)
    : IRequestHandler<CreateTaskCommand, Guid>
{
    private readonly IMapper _mapper = mapper;
    private readonly ITaskRepository _taskRepository = taskRepository;

    public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = _mapper.Map<Domain.Entities.Task>(request);
        
        await _taskRepository.CreateAsync(task);

        return task.Id;
    }
}