using MediatR;
using TaskManagement.Application.Common.Filtering;
using TaskManagement.Application.Common.Pagination;

namespace TaskManagement.Application.Features.Task.Queries.GetAllTasksByUserId;

public class GetAllTasksByUserIdQuery : IRequest<PaginatedList<TaskDto>>
{
    public Guid UserId { get; set; }
    public TaskFilter Filter { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
