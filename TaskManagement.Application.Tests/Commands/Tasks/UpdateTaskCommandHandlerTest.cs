using AutoMapper;
using MediatR;
using Moq;
using TaskManagement.Application.Features.Task.Commands.Update;
using TaskManagement.Domain.Enums;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Tests.Commands.Tasks;

public class UpdateTaskCommandHandlerTest
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ITaskRepository> _taskRepositoryMock;
    private readonly UpdateTaskCommandHandler _handler;

    public UpdateTaskCommandHandlerTest()
    {
        _mapperMock = new Mock<IMapper>();
        _taskRepositoryMock = new Mock<ITaskRepository>();
        _handler = new UpdateTaskCommandHandler(_mapperMock.Object, _taskRepositoryMock.Object);
    }

    [Fact]
    public async Task Should_Update_Task()
    {
        // Arrange
        var command = new UpdateTaskCommand
        {
            Id = Guid.NewGuid(),
            Title = "Task",
            Description = "Description",
            DueDate = DateTime.Now.AddDays(1),
            Status = Status.Pending,
            Priority = Priority.Low,
            UserId = Guid.NewGuid()
        };

        var task = new Domain.Entities.Task
        {
            Id = command.Id,
            Title = command.Title,
            Description = command.Description,
            DueDate = command.DueDate,
            Status = command.Status,
            Priority = command.Priority,
            UserId = command.UserId
        };

        _mapperMock.Setup(mapper => mapper.Map<Domain.Entities.Task>(command)).Returns(task);
        _taskRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.Task>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(Unit.Value, result);
        
        _mapperMock.Verify(mapper => mapper.Map<Domain.Entities.Task>(command), Times.Once);
        _taskRepositoryMock.Verify(repo => repo.UpdateAsync(It.Is<Domain.Entities.Task>(t =>
            t.Id == command.Id &&
            t.Title == command.Title &&
            t.Description == command.Description &&
            t.DueDate == command.DueDate &&
            t.Status == command.Status &&
            t.Priority == command.Priority &&
            t.UserId == command.UserId)), Times.Once);
    }
}