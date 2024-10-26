using Moq;
using TaskManagement.Application.Features.User.Commands.Login;
using TaskManagement.Application.Interfaces.Auth;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Tests.Commands.User;

public class LoginUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IJwtTokeProvider> _jwtTokenProviderMock;
    private readonly Mock<IPasswordHasherService> _passwordHasherMock;
    private readonly LoginUserCommandHandler _handler;

    public LoginUserCommandHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _jwtTokenProviderMock = new Mock<IJwtTokeProvider>();
        _passwordHasherMock = new Mock<IPasswordHasherService>();
        _handler = new LoginUserCommandHandler(_userRepositoryMock.Object, _jwtTokenProviderMock.Object,
            _passwordHasherMock.Object);
    }

    [Fact]
    public async Task Should_Throw_UnauthorizedAccessException_WhenUserNotFound()
    {
        // Arrange
        var command = new LoginUserCommand { Email = "nonexestsing@example.com", Password = "password" };

        _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(command.Email)).ReturnsAsync((Domain.Entities.User)null!);

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_Throw_UnauthorizedAccessException_WhenPasswordIsIncorrect()
    {
        // Arrange
        var command = new LoginUserCommand { Email = "user@gmail.com", Password = "password" };
        var user = new Domain.Entities.User { Email = command.Email, PasswordHash = "hashedPassword" };

        _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(command.Email)).ReturnsAsync(user);
        _passwordHasherMock.Setup(hasher => hasher.Verify(command.Password, user.PasswordHash)).Returns(false);

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_Return_JwtToken_WhenCredentialsAreValid()
    {
        // Arrange
        var command = new LoginUserCommand { Email = "user@example.com", Password = "password" };
        var user = new Domain.Entities.User { Email = command.Email, PasswordHash = "hashedPassword" };
        var expectedToken = "jwtToken";

        _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(command.Email)).ReturnsAsync(user);
        _passwordHasherMock.Setup(hasher => hasher.Verify(command.Password, user.PasswordHash)).Returns(true);
        _jwtTokenProviderMock.Setup(provider => provider.GenerateToken(user)).Returns(expectedToken);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(expectedToken, result);
    }
}