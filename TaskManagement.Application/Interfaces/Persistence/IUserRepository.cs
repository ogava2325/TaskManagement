using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Interfaces.Persistence;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> GetByEmailAsync(string email);
}