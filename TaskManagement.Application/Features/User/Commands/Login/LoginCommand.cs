using MediatR;

namespace TaskManagement.Application.Features.User.Commands.Login;

public class LoginCommand : IRequest<string>
{
    public string Email { get; set; }
    public string Password { get; set; }
}