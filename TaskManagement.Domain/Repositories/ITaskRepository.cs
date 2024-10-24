using Task = TaskManagement.Domain.Entities.Task;

namespace TaskManagement.Domain.Repositories;

public interface ITaskRepository : IGenericRepository<Task>
{
    IQueryable<Task> GetTasksByUserId(Guid userId);
}