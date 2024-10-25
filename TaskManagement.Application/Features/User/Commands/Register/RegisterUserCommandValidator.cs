using FluentValidation;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Features.User.Commands.Register;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    private readonly IUserRepository _userRepository;
    public RegisterUserCommandValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

        RuleFor(q => q)
            .MustAsync(IsUsernameUnique).WithMessage("Username is already taken.")
            .MustAsync(IsEmailUnique).WithMessage("Email is already taken.");
    }

    private async Task<bool> IsUsernameUnique(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        return await _userRepository.IsUsernameUnique(command.Username);
    }
    private async Task<bool> IsEmailUnique(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        return await _userRepository.IsEmailUnique(command.Email);
    }
}