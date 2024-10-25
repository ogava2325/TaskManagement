using AutoMapper;
using TaskManagement.Application.Features.Task.Commands.Create;
using TaskManagement.Application.Features.Task.Commands.Update;
using TaskManagement.Application.Features.Task.Queries.GetAllTasksByUserId;
using TaskManagement.Application.Features.Task.Queries.GetTaskDetails;
using Task = TaskManagement.Domain.Entities.Task;

namespace TaskManagement.Application.MappingProfiles;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<CreateTaskCommand, Task>();
        CreateMap<UpdateTaskCommand, Task>();
        
        CreateMap<Task, TaskDto>().ReverseMap();
        CreateMap<Task, TaskDetailsDto>().ReverseMap();
    }
}