using FluentValidation.TestHelper;
using TaskManagement.Application.Features.User.Commands.Login;

namespace TaskManagement.Application.Tests.Validators.User;

public class LoginUserCommandValidatorTest
{
    private readonly LoginUserCommandValidator _userCommandValidator = new();
    
    [Fact]
    public async Task Should_Have_Error_When_Email_Is_Empty()
    {
        var command = new LoginUserCommand { Email = "" };
        
        var result = await _userCommandValidator.TestValidateAsync(command);
        
        result.ShouldHaveValidationErrorFor(x => x.Email).WithErrorMessage("Email is required.");
    }
    
    [Fact]
    public async Task Should_Have_Error_When_Email_Is_Invalid()
    {
        var command = new LoginUserCommand { Email = "invalid-email" };
        
        var result = await _userCommandValidator.TestValidateAsync(command);
        
        result.ShouldHaveValidationErrorFor(x => x.Email).WithErrorMessage("Invalid email format.");
    }
    
    [Fact]
    public async Task Should_Have_Error_When_Password_Is_Empty()
    {
        var command = new LoginUserCommand { Password = "" };
        
        var result = await _userCommandValidator.TestValidateAsync(command);
        
        result.ShouldHaveValidationErrorFor(x => x.Password).WithErrorMessage("Password is required.");
    }
    
    [Fact]
    public async Task Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new LoginUserCommand
        {
            Email = "validemail@example.com",
            Password = "Valid1Password!"
        };

        var result = await _userCommandValidator.TestValidateAsync(command);

        result.ShouldNotHaveAnyValidationErrors();
    }
}