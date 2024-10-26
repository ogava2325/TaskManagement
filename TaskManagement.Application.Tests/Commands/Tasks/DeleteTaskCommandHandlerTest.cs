using MediatR;
using Moq;
using TaskManagement.Application.Features.Task.Commands.Delete;
using TaskManagement.Application.Features.Task.Commands.Update;
using TaskManagement.Domain.Enums;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Tests.Commands.Tasks;

public class DeleteTaskCommandHandlerTest
{
    private readonly Mock<ITaskRepository> _taskRepositoryMock;
    private readonly DeleteTaskCommandHandler _handler;

    public DeleteTaskCommandHandlerTest()
    {
        _taskRepositoryMock = new Mock<ITaskRepository>();
        _handler = new DeleteTaskCommandHandler(_taskRepositoryMock.Object);
    }
    
    [Fact]
    public async Task Should_Throw_ArgumentNullException_When_Task_Not_Found()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var command = new DeleteTaskCommand { Id = taskId };
        
        _taskRepositoryMock.Setup(repo => repo.GetByIdAsync(taskId))
            .ReturnsAsync((Domain.Entities.Task)null!);


        //Act & Assert

        await Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(command, CancellationToken.None));
        
        _taskRepositoryMock.Verify(repo => repo.GetByIdAsync(taskId), Times.Once);
        _taskRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Domain.Entities.Task>()), Times.Never);
        // Act
        var exception = await Record.ExceptionAsync(() => _handler.Handle(command, CancellationToken.None));
    }
    
    [Fact]
    public async Task Should_Delete_Task_When_Task_Exists()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var command = new DeleteTaskCommand { Id = taskId };

        var task = new Domain.Entities.Task { Id = taskId };
        
        _taskRepositoryMock.Setup(repo => repo.GetByIdAsync(taskId))
            .ReturnsAsync(task);
        _taskRepositoryMock.Setup(repo => repo.DeleteAsync(task))
            .Returns(Task.CompletedTask);
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.Equal(Unit.Value, result);
        _taskRepositoryMock.Verify(repo => repo.GetByIdAsync(taskId), Times.Once);
        _taskRepositoryMock.Verify(repo => repo.DeleteAsync(task), Times.Once);
    }
    
}