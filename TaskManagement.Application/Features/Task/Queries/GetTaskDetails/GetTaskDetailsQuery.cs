using MediatR;

namespace TaskManagement.Application.Features.Task.Queries.GetTaskDetails;

public class GetTaskDetailsQuery(Guid id) : IRequest<Domain.Entities.Task>
{
    public Guid Id { get; set; } = id;
}