using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;
using Task = TaskManagement.Domain.Entities.Task;

namespace TaskManagement.Persistence;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Task> Tasks { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    // Override the SaveChangesAsync method to automatically update audit fields
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in base.ChangeTracker.Entries<AuditableEntity>()
                     .Where(q => q.State is EntityState.Added or EntityState.Modified))
        {
            // Set the UpdatedAtUtc timestamp to the current UTC time for all modified or added entities
            entry.Entity.UpdatedAtUtc = DateTime.UtcNow;

            switch (entry.State)
            {
                // If the entity is newly added, also set the CreatedAtUtc timestamp
                case EntityState.Added:
                    entry.Entity.CreatedAtUtc = DateTime.UtcNow;
                    break;
                
                // For modified entities, ensure that CreatedAtUtc remains unchanged
                case EntityState.Modified:
                    entry.Property(e => e.UpdatedAtUtc).IsModified = false;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}