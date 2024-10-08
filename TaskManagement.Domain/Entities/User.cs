namespace TaskManagement.Domain.Entities;

public class User : AuditableEntity
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }  
}