using FluentValidation.TestHelper;
using TaskManagement.Application.Features.Task.Commands.Update;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Tests.Validators.Tasks;

public class UpdateTaskCommandValidatorTest
{
    private readonly UpdateTaskCommandValidator _taskCommandValidator = new();
    
    [Fact]
    public async System.Threading.Tasks.Task Should_Have_Error_When_Title_Is_Empty()
    {
        var command = new UpdateTaskCommand { Title = "" };
        
        var result = await _taskCommandValidator.TestValidateAsync(command);
        
        result.ShouldHaveValidationErrorFor(x => x.Title).WithErrorMessage("Title is required.");
    }
    
    [Fact]
    public async System.Threading.Tasks.Task Should_Have_Error_When_Description_Is_Empty()
    {
        var command = new UpdateTaskCommand { Description = "" };
        
        var result = await _taskCommandValidator.TestValidateAsync(command);
        
        result.ShouldHaveValidationErrorFor(x => x.Description).WithErrorMessage("Description is required.");
    }
    
    [Fact]
    public async System.Threading.Tasks.Task Should_Have_Error_When_DueDate_Is_In_Past()
    {
        var command = new UpdateTaskCommand { DueDate = DateTime.UtcNow.AddDays(-1) };
        
        var result = await _taskCommandValidator.TestValidateAsync(command);
        
        result.ShouldHaveValidationErrorFor(x => x.DueDate).WithErrorMessage("Due date must be in the future.");
    }
    
    [Fact]
    public async System.Threading.Tasks.Task Should_Have_Error_When_Status_Is_Invalid()
    {
        var command = new UpdateTaskCommand { Status = (Status)999 };

        var result = await _taskCommandValidator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Status)
            .WithErrorMessage("Status must be a valid enum value.");
    }

    [Fact]
    public async System.Threading.Tasks.Task Should_Have_Error_When_Priority_Is_Invalid()
    {
        var command = new UpdateTaskCommand { Priority = (Priority)999 }; 

        var result = await _taskCommandValidator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Priority);
    }
    
    [Fact]
    public async System.Threading.Tasks.Task Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new UpdateTaskCommand
        {
            Title = "Valid Title",
            Description = "Valid Description",
            DueDate = DateTime.UtcNow.AddDays(1),
            Status = Status.Pending,
            Priority = Priority.High
        };
        
        var result = await _taskCommandValidator.TestValidateAsync(command);
        
        result.ShouldNotHaveAnyValidationErrors();
    }
}