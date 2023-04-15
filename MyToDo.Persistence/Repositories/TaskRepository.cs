﻿using Microsoft.EntityFrameworkCore;
using MyToDo.Domain.Abstractions;
using MyToDo.Persistence.Specifications.TaskSpecifications;
using Task = MyToDo.Domain.Entities.Task;

namespace MyToDo.Persistence.Repositories;

public sealed class TaskRepository : BaseRepository<Task>, ITaskRepository
{
    public TaskRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Task?> GetByIdAsync(Guid taskId, bool isTracking = false, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(new TaskByIdSpecification(taskId, isTracking))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Task?> GetWithCommentsAsync(Guid taskId, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(new TaskByIdWithCommentsBaseSpecification(taskId))
            .FirstOrDefaultAsync(cancellationToken);
    }
}
