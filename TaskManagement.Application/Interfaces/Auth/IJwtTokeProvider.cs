using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Interfaces.Auth;

public interface IJwtTokeProvider
{
    string GenerateToken(User user);
}