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

    public async Task<Task?> GetWithCommentsAsync(Guid taskId, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(new TaskByIdWithCommentsBaseSpecification(taskId))
            .FirstOrDefaultAsync(t => t.Id == taskId, cancellationToken);
    }
}
