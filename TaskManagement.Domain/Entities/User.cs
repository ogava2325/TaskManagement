namespace TaskManagement.Domain.Entities;

public class User : AuditEntity
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }  
}