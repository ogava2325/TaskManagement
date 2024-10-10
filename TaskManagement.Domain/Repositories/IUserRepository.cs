using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> GetByEmailAsync(string email);
}