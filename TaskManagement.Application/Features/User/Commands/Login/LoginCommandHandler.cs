using MediatR;
using TaskManagement.Application.Interfaces.Auth;
using TaskManagement.Application.Interfaces.Persistence;

namespace TaskManagement.Application.Features.User.Commands.Login;

public class LoginCommandHandler(
    IUserRepository userRepository,
    IJwtTokeProvider jwtTokeProvider,
    IPasswordHasherService passwordHasher) 
    : IRequestHandler<LoginCommand, string>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IJwtTokeProvider _jwtTokeProvider = jwtTokeProvider;
    private readonly IPasswordHasherService _passwordHasher = passwordHasher;

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
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