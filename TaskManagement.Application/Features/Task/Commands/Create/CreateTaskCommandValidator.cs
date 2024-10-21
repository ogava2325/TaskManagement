using FluentValidation;

namespace TaskManagement.Application.Features.Task.Commands.Create;

public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.");
        
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.");
        
        RuleFor(x => x.DueDate)
            .GreaterThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Due date must be in the future.");
        
        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Status must be a valid enum value.");
        
        RuleFor(x => x.Priority)
            .IsInEnum()
            .WithMessage("Priority must be a valid enum value.");
    }
}