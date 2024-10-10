using MediatR;
using TaskManagement.Application.Interfaces.Auth;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Features.User.Commands.Register;

public class RegisterCommandHandler(
    IUserRepository userRepository,
    IPasswordHasherService passwordHasher) 
    : IRequestHandler<RegisterCommand, Guid>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasherService _passwordHasher = passwordHasher;

    public async Task<Guid> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var hashedPassword = _passwordHasher.Generate(request.Password);

        var user = new Domain.Entities.User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = hashedPassword
        };

        await _userRepository.CreateAsync(user);

        return user.Id;
    }
}