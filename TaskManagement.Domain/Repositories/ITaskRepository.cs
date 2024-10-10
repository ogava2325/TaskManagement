using Task = TaskManagement.Domain.Entities.Task;

namespace TaskManagement.Domain.Repositories;

public interface ITaskRepository : IGenericRepository<Task>
{
    Task<IEnumerable<Task>> GetTasksByUserIdAsync(Guid userId);
}