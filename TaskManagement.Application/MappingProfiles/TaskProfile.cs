using AutoMapper;
using TaskManagement.Application.Features.Task.Queries.GetAllTasksByUserId;
using TaskManagement.Application.Features.Task.Queries.GetTaskDetails;
using Task = TaskManagement.Domain.Entities.Task;

namespace TaskManagement.Application.MappingProfiles;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<Task, TaskDto>().ReverseMap();
        CreateMap<Task, TaskDetailsDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority.ToString()))
            .ReverseMap();
    }
}