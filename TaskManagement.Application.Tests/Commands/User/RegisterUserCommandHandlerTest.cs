using AutoMapper;
using Moq;
using TaskManagement.Application.Features.User.Commands.Register;
using TaskManagement.Application.Interfaces.Auth;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Tests.Commands.User;

public class RegisterUserCommandHandlerTest
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IPasswordHasherService> _passwordHashMock;
    private readonly RegisterUserCommandHandler _handler;

    public RegisterUserCommandHandlerTest()
    {
        _mapperMock = new Mock<IMapper>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _passwordHashMock = new Mock<IPasswordHasherService>();
        _handler = new RegisterUserCommandHandler(_mapperMock.Object,_userRepositoryMock.Object, _passwordHashMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateUserAndReturnId_WhenCommandIsValid()
    {
        // Arrange
        var command = new RegisterUserCommand 
        { 
            Username = "user", 
            Email = "user@example.com", 
            Password = "password" 
        };

        var hashedPassword = "hashedPassword";
        var userId = Guid.NewGuid();

        var user = new Domain.Entities.User
        {
            Id = userId,
            Username = command.Username,
            Email = command.Email,
            PasswordHash = hashedPassword
        };

        _passwordHashMock.Setup(hasher => hasher.Generate(command.Password)).Returns(hashedPassword);
        _mapperMock.Setup(mapper => mapper.Map<Domain.Entities.User>(command)).Returns(user);
        _userRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.User>())).Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(userId, result);

        _passwordHashMock.Verify(hasher => hasher.Generate(command.Password), Times.Once);
        _mapperMock.Verify(mapper => mapper.Map<Domain.Entities.User>(command), Times.Once);
        _userRepositoryMock.Verify(repo => repo.CreateAsync(It.Is<Domain.Entities.User>(u =>
            u.Username == command.Username &&
            u.Email == command.Email &&
            u.PasswordHash == hashedPassword)), Times.Once);
    }
}