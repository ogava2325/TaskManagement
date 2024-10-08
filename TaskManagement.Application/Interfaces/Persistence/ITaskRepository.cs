using Task = TaskManagement.Domain.Entities.Task;

namespace TaskManagement.Application.Interfaces.Persistence;

public interface ITaskRepository : IGenericRepository<Task>
{
    Task<IEnumerable<Task>> GetTasksByUserIdAsync(Guid userId);
}