using AutoMapper;
using MediatR;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Features.Task.Commands.Update;

public class UpdateTaskCommandHandler(
    IMapper mapper,
    ITaskRepository taskRepository)
    : IRequestHandler<UpdateTaskCommand, Unit>
{
    private readonly IMapper _mapper = mapper;
    private readonly ITaskRepository _taskRepository = taskRepository;

    public async Task<Unit> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    { 
        var task = _mapper.Map<Domain.Entities.Task>(request);
        
        await _taskRepository.UpdateAsync(task);
        
        return Unit.Value;
    } 
}