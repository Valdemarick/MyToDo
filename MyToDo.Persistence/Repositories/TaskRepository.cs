using Microsoft.EntityFrameworkCore;
using MyToDo.Domain.Abstractions;
using MyToDo.Persistence.Specifications;
using MyToDo.Persistence.Specifications.TaskSpecifications;
using Task = MyToDo.Domain.Entities.Task;

namespace MyToDo.Persistence.Repositories;

internal sealed class TaskRepository : ITaskRepository
{
    private readonly ApplicationDbContext _dbContext;

    public TaskRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Task?> GetWithCommentsAsync(Guid taskId, CancellationToken cancellationToken = default)
    {
        return await SpecificationEvaluator.GetQuery(
                _dbContext.Set<Task>(),
                new TaskByIdWithCommentsSpecificationForRead(taskId))
            .FirstOrDefaultAsync(t => t.Id == taskId, cancellationToken);
    }

    public void Add(Task task)
    {
        _dbContext.Set<Task>().Add(task);
    }
}
