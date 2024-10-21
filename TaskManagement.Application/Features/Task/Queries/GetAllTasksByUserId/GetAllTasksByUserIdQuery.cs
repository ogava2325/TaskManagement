using MediatR;

namespace TaskManagement.Application.Features.Task.Queries.GetAllTasksByUserId;

public class GetAllTasksByUserIdQuery : IRequest<List<Domain.Entities.Task>>
{
    public Guid UserId { get; set; }
}
