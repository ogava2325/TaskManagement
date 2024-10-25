using FluentValidation.TestHelper;
using Moq;
using TaskManagement.Application.Features.User.Commands.Register;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Tests.Validators.User;

public class RegisterUserCommandValidatorTest
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly RegisterUserCommandValidator _userCommandValidator;
    
    public RegisterUserCommandValidatorTest()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userCommandValidator = new RegisterUserCommandValidator(_userRepositoryMock.Object);
    }
    
    [Fact]
    public async System.Threading.Tasks.Task Should_Have_Error_When_Email_Is_Empty()
    {
        var command = new RegisterUserCommand { Email = "" };
        
        var result = await _userCommandValidator.TestValidateAsync(command);
        
        result.ShouldHaveValidationErrorFor(x => x.Email).WithErrorMessage("Email is required.");
    }
    
    [Fact]
    public async System.Threading.Tasks.Task Should_Have_Error_When_Email_Is_Invalid()
    {
        var command = new RegisterUserCommand { Email = "invalid-email" };
        
        var result = await _userCommandValidator.TestValidateAsync(command);
        
        result.ShouldHaveValidationErrorFor(x => x.Email).WithErrorMessage("Invalid email format.");
    }
    
    [Fact]
    public async System.Threading.Tasks.Task Should_Have_Error_When_Email_Is_Not_Unique()
    {
        // Arrange
        _userRepositoryMock.Setup(x => x.IsEmailUnique(It.IsAny<string>()))
            .ReturnsAsync(false);
        
        var command = new RegisterUserCommand { Email = "existing@example.com", Password = "Valid1Password!" };
        
        // Act
        var result = await _userCommandValidator.TestValidateAsync(command);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email).WithErrorMessage("Email is already taken.");
    }
    
    [Fact]
    public async System.Threading.Tasks.Task Should_Have_Error_When_Password_Is_Empty()
    {
        var command = new RegisterUserCommand { Password = "" };
        
        var result = await _userCommandValidator.TestValidateAsync(command);
        
        result.ShouldHaveValidationErrorFor(x => x.Password).WithErrorMessage("Password is required.");
    }
    
    [Fact]
    public async System.Threading.Tasks.Task Should_Have_Error_When_Password_Is_Too_Short()
    {
        var command = new RegisterUserCommand { Password = "short" };
        
        var result = await _userCommandValidator.TestValidateAsync(command);
        
        result.ShouldHaveValidationErrorFor(x => x.Password).WithErrorMessage("Password must be at least 8 characters long.");
    }
    
    [Fact]
    public async System.Threading.Tasks.Task Should_Have_Error_When_Password_Does_Not_Contain_Digit()
    {
        var command = new RegisterUserCommand { Password = "NoDigits!" };

        var result = await _userCommandValidator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Password).WithErrorMessage("Password must contain at least one digit.");
    }
    
    [Fact]
    public async System.Threading.Tasks.Task Should_Have_Error_When_Password_Does_Not_Contain_Uppercase()
    {
        var command = new RegisterUserCommand { Password = "lowercase1!" };
        
        var result = await _userCommandValidator.TestValidateAsync(command);
        
        result.ShouldHaveValidationErrorFor(x => x.Password).WithErrorMessage("Password must contain at least one uppercase letter.");
    }
    
    [Fact]
    public async System.Threading.Tasks.Task Should_Have_Error_When_Password_Does_Not_Contain_Lowercase()
    {
        var command = new RegisterUserCommand { Password = "UPPERCASE1!" };
        
        var result = await _userCommandValidator.TestValidateAsync(command);
        
        result.ShouldHaveValidationErrorFor(x => x.Password).WithErrorMessage("Password must contain at least one lowercase letter.");
    }
    
    
    [Fact]
    public async System.Threading.Tasks.Task Should_Have_Error_When_Password_Does_Not_Contain_Special_Character()
    {
        var command = new RegisterUserCommand { Password = "NoSpecial1" };

        var result = await _userCommandValidator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Password).WithErrorMessage("Password must contain at least one special character.");
    }
    
    [Fact]
    public async System.Threading.Tasks.Task Should_Have_Error_When_Username_Is_Empty()
    {
        var command = new RegisterUserCommand { Username = "" };
        
        var result = await _userCommandValidator.TestValidateAsync(command);
        
        result.ShouldHaveValidationErrorFor(x => x.Username).WithErrorMessage("Username is required.");
    }
    
    [Fact]
    public async System.Threading.Tasks.Task Should_Have_Error_When_Username_Is_Not_Unique()
    {
        // Arrange
        _userRepositoryMock.Setup(x => x.IsUsernameUnique(It.IsAny<string>()))
            .ReturnsAsync(false);
        
        var command = new RegisterUserCommand { Username = "existingUser", Email = "dima@example.com", Password = "existingPassword"};
        
        // Act
        var result = await _userCommandValidator.TestValidateAsync(command);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Username).WithErrorMessage("Username is already taken.");
    }
    
    
    [Fact]
    public async System.Threading.Tasks.Task Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new RegisterUserCommand
        {
            Username = "newUser",
            Email = "newuser@example.com",
            Password = "Valid1Password!"
        };
        _userRepositoryMock.Setup(repo => repo.IsUsernameUnique(command.Username)).ReturnsAsync(true);
        _userRepositoryMock.Setup(repo => repo.IsEmailUnique(command.Email)).ReturnsAsync(true);

        var result = await _userCommandValidator.TestValidateAsync(command);

        result.ShouldNotHaveAnyValidationErrors();
    }
}