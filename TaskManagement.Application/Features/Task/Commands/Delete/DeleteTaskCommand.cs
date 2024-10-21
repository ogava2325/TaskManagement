using MediatR;

namespace TaskManagement.Application.Features.Task.Commands.Delete;

public class DeleteTaskCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}