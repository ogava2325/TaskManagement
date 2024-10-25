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
            .EmailAddress().WithMessage("Invalid email format.")
            .MustAsync(IsEmailUnique).WithMessage("Email is already taken.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MustAsync(IsUsernameUnique).WithMessage("Username is already taken.");
    }

    private async Task<bool> IsUsernameUnique(string username, CancellationToken cancellationToken)
    {
        return await _userRepository.IsUsernameUnique(username);
    }
    private async Task<bool> IsEmailUnique(string email, CancellationToken cancellationToken)
    {
        return await _userRepository.IsEmailUnique(email);
    }
}