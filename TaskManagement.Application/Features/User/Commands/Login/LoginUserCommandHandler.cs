using MediatR;
using TaskManagement.Application.Interfaces.Auth;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Features.User.Commands.Login;

public class LoginUserCommandHandler(
    IUserRepository userRepository,
    IJwtTokeProvider jwtTokeProvider,
    IPasswordHasherService passwordHasher) 
    : IRequestHandler<LoginUserCommand, string>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IJwtTokeProvider _jwtTokeProvider = jwtTokeProvider;
    private readonly IPasswordHasherService _passwordHasher = passwordHasher;

    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user is null)
        {
            throw new UnauthorizedAccessException("User with the provided email does not exist.");
        }
        
        var passwordVerificationResult = _passwordHasher.Verify(request.Password, user.PasswordHash);

        if (!passwordVerificationResult)
        {
            throw new UnauthorizedAccessException("Invalid credentials provided.");
        }
        
        // Generate and return the JWT token for the authenticated user
        return _jwtTokeProvider.GenerateToken(user);
    }
}