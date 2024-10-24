using System.Data.Entity;
using AutoMapper;
using MediatR;
using TaskManagement.Application.Common.Filtering;
using TaskManagement.Application.Common.Pagination;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Features.Task.Queries.GetAllTasksByUserId;

public class GetAllTasksByUserIdQueryHandler(
    ITaskRepository taskRepository, 
    IMapper mapper)
    : IRequestHandler<GetAllTasksByUserIdQuery, PaginatedList<TaskDto>>
{
    private readonly ITaskRepository _taskRepository = taskRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<PaginatedList<TaskDto>> Handle(GetAllTasksByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var tasks = _taskRepository.GetTasksByUserId(request.UserId);

        if (request.Filter != null)
        {
            // Apply filtering
            if (request.Filter.Status.HasValue)
            {
                tasks = tasks.Where(t => t.Status == request.Filter.Status.Value);
            }

            if (request.Filter.DueDate.HasValue)
            {
                tasks = tasks.Where(t => t.DueDate == request.Filter.DueDate.Value);
            }

            if (request.Filter.Priority.HasValue)
            {
                tasks = tasks.Where(t => t.Priority == request.Filter.Priority.Value);
            }

            // Apply sorting
            switch (request.Filter.SortBy)
            {
                case SortBy.DueDate:
                    tasks = request.Filter.SortOrder == SortOrder.Ascending
                        ? tasks.OrderBy(t => t.DueDate)
                        : tasks.OrderByDescending(t => t.DueDate);
                    break;

                case SortBy.Priority:
                    tasks = request.Filter.SortOrder == SortOrder.Ascending
                        ? tasks.OrderBy(t => t.Priority)
                        : tasks.OrderByDescending(t => t.Priority);
                    break;
            }
        }

        var totalCount =  tasks.Count();
        
        // Apply pagination
        var paginatedTasks = tasks
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToList();
        
        var taskDtos = _mapper.Map<List<TaskDto>>(paginatedTasks);
        
        return new PaginatedList<TaskDto>(taskDtos, totalCount, request.PageNumber, request.PageSize);
    }
}