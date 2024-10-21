using MediatR;

namespace TaskManagement.Application.Features.User.Commands.Register;

public class RegisterUserCommand : IRequest<Guid>
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}