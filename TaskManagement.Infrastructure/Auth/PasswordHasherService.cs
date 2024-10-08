using TaskManagement.Application.Interfaces.Auth;

namespace TaskManagement.Infrastructure.Auth;

public class PasswordHasherService : IPasswordHasherService
{
    public string Generate(string password)
    { 
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }

    public bool Verify(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
    }

}