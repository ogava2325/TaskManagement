using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Interfaces.Persistence;
using Task = TaskManagement.Domain.Entities.Task;

namespace TaskManagement.Persistence.Repositories;

public class TaskRepository(ApplicationDbContext context) : GenericRepository<Task>(context), ITaskRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<Task>> GetTasksByUserIdAsync(Guid userId)
    {
        return await _context.Tasks
            .AsNoTracking()
            .Where(t => t.UserId == userId)
            .ToListAsync();
    }
}