using MediatR;

namespace TaskManagement.Application.Features.User.Commands.RegisterUser;

public class RegisterCommand : IRequest<Guid>
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}