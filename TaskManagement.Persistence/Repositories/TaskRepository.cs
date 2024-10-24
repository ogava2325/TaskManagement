using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Repositories;
using Task = TaskManagement.Domain.Entities.Task;

namespace TaskManagement.Persistence.Repositories;

public class TaskRepository(ApplicationDbContext context) : GenericRepository<Task>(context), ITaskRepository
{
    private readonly ApplicationDbContext _context = context;

    public IQueryable<Task> GetTasksByUserId(Guid userId)
    {
        return _context.Tasks
            .AsNoTracking()
            .Where(t => t.UserId == userId);
    }
}