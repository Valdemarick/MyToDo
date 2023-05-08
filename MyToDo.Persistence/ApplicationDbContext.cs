using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MyToDo.Domain.Abstractions;
using MyToDo.Domain.Entities;
using Task = MyToDo.Domain.Entities.Task;
using TaskStatus = MyToDo.Domain.Enums.TaskStatus;

namespace MyToDo.Persistence;

public sealed class ApplicationDbContext : DbContext
{
    private readonly IDateTimeOffsetProvider _dateTimeOffsetProvider;
    
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options, 
        IDateTimeOffsetProvider dateTimeOffsetProvider) 
        : base(options)
    {
        _dateTimeOffsetProvider = dateTimeOffsetProvider;
    }

    public DbSet<Task> Tasks => Set<Task>();

    public DbSet<Member> Members => Set<Member>();

    public DbSet<Role> Roles => Set<Role>();

    public DbSet<Tag> Tags => Set<Tag>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        SetTaskDateTimeOffset();
        SetCommentDateTimeOffset();
        SetMemberDateTimeOffset();
        SetTagCommentDateTimeOffset();

        return await base.SaveChangesAsync(cancellationToken);
    }

    private void SetTaskDateTimeOffset()
    {
        foreach (var entry in ChangeTracker.Entries<Task>())
        {
            var entity = entry.Entity;
            
            switch (entry.State)
            {
                case EntityState.Added:
                    entity.SetCreatedOn(_dateTimeOffsetProvider.UtcNow);
                    break;
                case EntityState.Modified:
                    entity.SetLastUpdatedOn(_dateTimeOffsetProvider.UtcNow);
                    break;
            }

            if (entity.Status is TaskStatus.Completed)
            {
                entity.SetCompletedOn(_dateTimeOffsetProvider.UtcNow);
            }
        }
    }

    private void SetCommentDateTimeOffset()
    {
        foreach (var entry in ChangeTracker.Entries<Comment>())
        {
            var entity = entry.Entity;

            switch (entry.State)
            {
                case EntityState.Added:
                    entity.SetCreatedOn(_dateTimeOffsetProvider.UtcNow);
                    break;
                case EntityState.Modified:
                    entity.SetLastUpdatedOn(_dateTimeOffsetProvider.UtcNow);
                    break;
            }
        }
    }

    private void SetTagCommentDateTimeOffset()
    {
        foreach (var entry in ChangeTracker.Entries<Tag>())
        {
            var entity = entry.Entity;

            switch (entry.State)
            {
                case EntityState.Added:
                    entity.SetCreatedOn(_dateTimeOffsetProvider.UtcNow);
                    break;
                case EntityState.Modified:
                    entity.SetLastUpdatedOn(_dateTimeOffsetProvider.UtcNow);
                    break;
            }
        }
    }

    private void SetMemberDateTimeOffset()
    {
        foreach (var entry in ChangeTracker.Entries<Member>())
        {
            var entity = entry.Entity;

            if (entry.State is EntityState.Added)
            {
                entity.SetRegisteredOn(_dateTimeOffsetProvider.UtcNow);
            }
        }
    }
}
