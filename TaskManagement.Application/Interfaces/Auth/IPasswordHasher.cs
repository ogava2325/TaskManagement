namespace TaskManagement.Application.Interfaces.Auth;

public interface IPasswordHasherService
{
    string Generate(string password);
    bool Verify(string password, string hashedPassword);
}