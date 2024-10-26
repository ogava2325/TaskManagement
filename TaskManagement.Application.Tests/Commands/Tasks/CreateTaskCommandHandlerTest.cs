using AutoMapper;
using Moq;
using TaskManagement.Application.Features.Task.Commands.Create;
using TaskManagement.Domain.Enums;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Tests.Commands.Tasks;

public class CreateTaskCommandHandlerTest
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ITaskRepository> _taskRepositoryMock;
    private readonly CreateTaskCommandHandler _handler;

    public CreateTaskCommandHandlerTest()
    {
        _mapperMock = new Mock<IMapper>();
        _taskRepositoryMock = new Mock<ITaskRepository>();
        _handler = new CreateTaskCommandHandler(_mapperMock.Object, _taskRepositoryMock.Object);
    }

    [Fact]
    public async Task Should_Create_Task_And_Return_TaskId()
    {
        var command = new CreateTaskCommand
        {
            Title = "Task",
            Description = "Description",
            DueDate = DateTime.Now.AddDays(1),
            Status = Status.Pending,
            Priority = Priority.Low,
            UserId = Guid.NewGuid()
        };

        var taskId = Guid.NewGuid();

        var task = new Domain.Entities.Task
        {
            Id = taskId,
            Title = command.Title,
            Description = command.Description,
            DueDate = command.DueDate,
            Status = command.Status,
            Priority = command.Priority,
            UserId = command.UserId
        };

        _mapperMock.Setup(mapper => mapper.Map<Domain.Entities.Task>(command)).Returns(task);
        _taskRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Task>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mapperMock.Verify(mapper => mapper.Map<Domain.Entities.Task>(command), Times.Once);
        _taskRepositoryMock.Verify(repo => repo.CreateAsync(It.Is<Domain.Entities.Task>(t =>
            t.Title == command.Title &&
            t.Description == command.Description &&
            t.DueDate == command.DueDate &&
            t.Status == command.Status &&
            t.Priority == command.Priority &&
            t.UserId == command.UserId)), Times.Once);
        
        Assert.Equal(taskId, result);
    }
}