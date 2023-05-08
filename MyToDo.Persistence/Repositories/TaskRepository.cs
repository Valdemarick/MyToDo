using Microsoft.EntityFrameworkCore;
using MyToDo.Domain.Abstractions;
using MyToDo.Persistence.Specifications.TaskSpecifications;
using Task = MyToDo.Domain.Entities.Task;

namespace MyToDo.Persistence.Repositories;

internal sealed class TaskRepository : BaseRepository<Task>, ITaskRepository
{
    public TaskRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Task?> GetByIdAsync(Guid taskId, CancellationToken cancellationToken = default, bool isTracking = false)
    {
        return await ApplySpecification(new TaskByIdSpecification(taskId, isTracking))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Task?> GetWithCommentsAsync(Guid taskId, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(new TaskByIdWithCommentsBaseSpecification(taskId))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Task?> GetWithTagsAsync(Guid taskId, bool isTracking = false, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(new TaskWithTagsSpecification(taskId, isTracking))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Task>> GetPageAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(new TaskPageSpecification())
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
}
